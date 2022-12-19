using MyProject.Helpers;
using Sitecore;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var contextItem = Sitecore.Context.Database.Items.GetItem(Templates.Article.Id);
            Log.Info($"Context item {contextItem.Name}", this);
            Log.Info($"Context id {contextItem.ID}", this);


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
            var articleCards = root.Children
                .Where(articlePage => articlePage.Children.Any(articleContent => articleContent.DescendsFrom(Templates.Article.TemplateId)))
                .Select(articlePage =>
                {
                    var contentItem = articlePage.Children.First(articleContent => articleContent.DescendsFrom(Templates.Article.TemplateId));

                    Log.Info($"Title: {contentItem[Templates.Article.Fields.Title]}", this);
                    Log.Info($"Link: {_linkManager.GetItemUrl(articlePage)}", this);
                    Log.Info($"Image: {contentItem[Templates.Article.Fields.Image]}", this);
                    Log.Info($"Host: {Environment.GetEnvironmentVariable("Sitecore_Identity_Server_CallbackAuthority")}", this);

                    var showInCarousel = contentItem[Templates.Article.Fields.ShowInCarousel];
                    var realItemTag = contentItem.Fields.Where(x => x.ID == Templates.Article.Fields.Image).FirstOrDefault().Value;

                    string realTag = string.Empty;
                    string imageSrc = string.Empty;
                    if (realItemTag != null)
                    {
                        var realItemId = TagHelper.GetIdFromTag(realItemTag);
                        Log.Info($"Real item id: {realItemId}", this);
                        var host = Environment.GetEnvironmentVariable("Sitecore_Identity_Server_CallbackAuthority");
                        imageSrc = $"{host}/sitecore/shell/-/media/{realItemId}.ashx";
                        realTag = $"<img style=\"width: 100%;\" src=\"{imageSrc}\" />";
                    }

                    return new
                    {
                        Link = _linkManager.GetItemUrl(articlePage),
                        Title = contentItem[Templates.Article.Fields.Title],
                        Image = imageSrc,
                        Date = DateUtil.IsoDateToDateTime(contentItem[Templates.Article.Fields.Date]),
                        ShowInCarousel = showInCarousel == "1",
                    };
                });

            return articleCards;
        }
    }
}