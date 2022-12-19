using MyProject.Helpers;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var productCards = root.Children
                .Where(productPage => productPage.Children.Any(productContent => productContent.DescendsFrom(Templates.Product.TemplateId)))
                .Select(productPage =>
                {
                    var contentItem = productPage.Children.First(productContent => productContent.DescendsFrom(Templates.Product.TemplateId));

                    Log.Info($"Title: {contentItem[Templates.Product.Fields.Title]}", this);
                    Log.Info($"Link: {_linkManager.GetItemUrl(productPage)}", this);
                    Log.Info($"Image: {contentItem[Templates.Product.Fields.Image]}", this);

                    var showInCarousel = contentItem[Templates.Product.Fields.ShowInCarousel];

                    Log.Info($"ShowInCarousel: {showInCarousel}", this);

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
                    };
                });

            return productCards;
        }
    }
}