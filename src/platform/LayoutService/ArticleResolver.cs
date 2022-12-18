using MyProject.Helpers;
using Sitecore;
using Sitecore.Abstractions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace MyProject.LayoutService
{
    public class ArticleResolver : RenderingContentsResolver
    {

        private readonly BaseLinkManager _linkManager;

        public ArticleResolver(BaseLinkManager linkManager)
        {
            _linkManager = linkManager;
        }

        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Log.Info("Resolver started.", this);
            //var contextItem = GetContextItem(rendering, renderingConfig);
            var contextItem = Sitecore.Context.Database.Items.GetItem(Templates.Article.Id);
            Log.Info($"Context item {contextItem.Name}", this);
            Log.Info($"Context id {contextItem.ID}", this);

            //var root = contextItem?.Axes.GetAncestors().FirstOrDefault(item => item.ID == Templates.Article.Id);

            if (contextItem == null)
            {
                Log.Warn("Context item is null", this);
                return null;
            }

            if (contextItem.ID != Templates.Article.Id)
            {
                Log.Warn("Context item is not Articles", this);
                return null;
            }

            Log.Info($"Root name is {contextItem.Name}", this);

            var articleCards = GetArticles(contextItem);

            object res = new
            {
                ArticleCards = articleCards
            };

            return res;
        }

        private IEnumerable<object> GetArticles(Item root)
        {
            Log.Info($"Children: {string.Join(", ", root.Children.Select(x => x.Name))}", this);

            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            var articleCards = root.Children
                .Where(articlePage => articlePage.Children.Any(articleContent => articleContent.DescendsFrom(Templates.Article.TemplateId)))
                .Select(articlePage =>
                {
                    var contentItem = articlePage.Children.First(articleContent => articleContent.DescendsFrom(Templates.Article.TemplateId));
                    
                    var image = contentItem.Fields[Templates.Article.Fields.Image].Item;

                    MediaItem media = new MediaItem(image);
                    ImageField f = contentItem.Fields[Templates.Article.Fields.Image];
                    
                    string src = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));

                    var imgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", src, media.Alt);

                    ImageField imageField = contentItem.Fields["Image"];

                    if (imageField != null && imageField.MediaItem != null)
                    {
                        MediaItem image1 = new MediaItem(imageField.MediaItem);

                        string src1 = StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image1));

                        string imgTag1 = String.Format(@"<img src=""{0}"" alt=""{1}"" />", src1, image1.Alt);
                        Log.Info($"Image32: {imgTag}", this);
                    }
                    Log.Info($"Image child: {string.Join(", ", contentItem.Fields.Select(x=>x.ID))}", this);
                    Log.Info($"Image field2: {contentItem.Fields.Where(x => x.ID == Templates.Article.Fields.Image).First().Item.ID}", this);
                    Log.Info($"Image field2: {contentItem.Fields.Where(x => x.ID == Templates.Article.Fields.Image).First().Value}", this);


                    Log.Info($"Title: {contentItem[Templates.Article.Fields.Title]}", this);
                    Log.Info($"Link: {_linkManager.GetItemUrl(articlePage)}", this);
                    Log.Info($"Image: {contentItem[Templates.Article.Fields.Image]}", this);
                    Log.Info($"Image: {media.MediaPath}", this);
                    Log.Info($"Image: {media.Path}", this);
                    Log.Info($"Image: {media}", this);
                    Log.Info($"Image: {imgTag}", this);
                    Log.Info($"Image uri: {image.Uri}", this);
                    Log.Info($"Image uri: {image.Uri.Path}", this);
                    Log.Info($"Image uri: {image.Uri}", this);
                    Log.Info($"Source3215: {contentItem.Fields[Templates.Article.Fields.Image].Source}", this);
                    Log.Info($"image id: {image.ID}", this);
                    Log.Info($"image id: {media.ID}", this);
                    Log.Info($"image id: {contentItem.ID}", this);
                    Log.Info($"Date: {DateUtil.IsoDateToDateTime(contentItem[Templates.Article.Fields.Date])}", this);
                    Log.Info($"Host: {Environment.GetEnvironmentVariable("CM_HOST")}", this);
                    Log.Info($"Host: {Environment.GetEnvironmentVariable("Sitecore_Identity_Server_CallbackAuthority")}", this);

                    var realItemTag = contentItem.Fields.Where(x => x.ID == Templates.Article.Fields.Image).FirstOrDefault().Value;

                    string realTag = string.Empty;
                    if (realItemTag != null)
                    {
                        var realItemId = TagHelper.GetIdFromTag(realItemTag);
                        Log.Info($"real item id: {realItemId}", this);
                        var host = Environment.GetEnvironmentVariable("Sitecore_Identity_Server_CallbackAuthority");
                        realTag = $"<img style=\"width: 100%;\" src=\"{host}/sitecore/shell/-/media/{realItemId}.ashx\" />";
                    }

                    return new
                    {
                        Link = _linkManager.GetItemUrl(articlePage),
                        Title = contentItem[Templates.Article.Fields.Title],
                        Image = realTag,
                        //Image = contentItem[Templates.Article.Fields.Image],
                        Date = DateUtil.IsoDateToDateTime(contentItem[Templates.Article.Fields.Date])
                    };
                });

            Log.Info($"article cards: {string.Join(", ", articleCards.Select(x => x.Title))}", this);

            return articleCards;
        }
    }
}