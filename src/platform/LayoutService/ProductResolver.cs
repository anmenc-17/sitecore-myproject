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
    public class ProductResolver : RenderingContentsResolver
    {

        private readonly BaseLinkManager _linkManager;

        public ProductResolver(BaseLinkManager linkManager)
        {
            _linkManager = linkManager;
        }
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Log.Info("Resolver started.", this);
            //var contextItem = GetContextItem(rendering, renderingConfig);
            var contextItem = Sitecore.Context.Database.Items.GetItem(Templates.Product.Id);

            Log.Info($"Context item {contextItem.Name}", this);
            Log.Info($"Context id {contextItem.ID}", this);

            if (contextItem == null)
            {
                Log.Warn("Context item is null", this);
                return null;
            }

            if (contextItem.ID != Templates.Product.Id)
            {
                Log.Warn("Context item is not Products", this);
                return null;
            }

            Log.Info($"Root name is {contextItem.Name}", this);

            var productCards = GetProducts(contextItem);

            object res = new
            {
                ProductCards = productCards
            };

            return res;
        }

        private IEnumerable<object> GetProducts(Item root)
        {
            Log.Info($"Children: {string.Join(", ", root.Children.Select(x => x.Name))}", this);

            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            var productCards = root.Children
                .Where(productPage => productPage.Children.Any(productContent => productContent.DescendsFrom(Templates.Product.TemplateId)))
                .Select(productPage =>
                {
                    var contentItem = productPage.Children.First(productContent => productContent.DescendsFrom(Templates.Product.TemplateId));

                    var image = contentItem.Fields[Templates.Product.Fields.Image].Item;
                    MediaItem media = new MediaItem(image);
                    ImageField f = contentItem.Fields[Templates.Product.Fields.Image];
                    string src = StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));

                    var imgTag = string.Format(@"<img src=""{0}"" alt=""{1}"" />", src, media.Alt);


                    Log.Info($"Title: {contentItem[Templates.Product.Fields.Title]}", this);
                    Log.Info($"Link: {_linkManager.GetItemUrl(productPage)}", this);
                    Log.Info($"Image: {contentItem[Templates.Product.Fields.Image]}", this);
                    Log.Info($"Image: {media.MediaPath}", this);
                    Log.Info($"Image: {media.Path}", this);
                    Log.Info($"Image: {media}", this);
                    Log.Info($"Image: {imgTag}", this);


                    var showInCarousel = contentItem[Templates.Product.Fields.ShowInCarousel];
                    
                    Log.Info($"ShowInCarousel: {showInCarousel}", this);
                    Boolean.TryParse(showInCarousel, out var showInCarouselBool);
                    Log.Info($"ShowInCarousel: {showInCarouselBool}", this);
                    Log.Info($"Fields: {string.Join(", ", contentItem.Fields.Select(x => x.Name))}", this);
                    Log.Info($"Fields: {string.Join(", ", contentItem.Fields.Select(x => x.ID))}", this);
                    Log.Info($"Fields: {string.Join(", ", contentItem.Fields.Select(x => x.Value))}", this);

                    var realItemTag = contentItem.Fields.Where(x => x.ID == Templates.Product.Fields.Image).FirstOrDefault()?.Value;
                    string realTag = string.Empty;
                    string imageSrc = string.Empty;
                    if (realItemTag != null)
                    {
                        var realItemId = TagHelper.GetIdFromTag(realItemTag);
                        Log.Info($"real item id: {realItemId}", this);
                        var host = Environment.GetEnvironmentVariable("Sitecore_Identity_Server_CallbackAuthority");

                        imageSrc = $"{host}/sitecore/shell/-/media/{realItemId}.ashx";
                        realTag = $"<img style=\"width: 100%;\" src=\"{imageSrc}\" />";


                        Log.Info($"imageSrc: {imageSrc}", this);
                        Log.Info($"realTag: {realTag}", this);
                    }
                    return new
                    {
                        Title = contentItem[Templates.Product.Fields.Title],
                        Description = contentItem[Templates.Product.Fields.Description],
                        Image = imageSrc,
                        Link = _linkManager.GetItemUrl(productPage),
                        ShowInCarousel = showInCarousel == "1",
                        //Image = contentItem[Templates.Product.Fields.Image],
                    };
                });

            Log.Info($"Product cards: {string.Join(", ", productCards.Select(x => x.Title))}", this);

            return productCards;
        }
    }
}