using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using System;

namespace MyProject.Models.MyProject
{
    public class ArticleCardModel
    {
        [SitecoreComponentField]
        public string Image { get; set; }

        [SitecoreComponentField]
        public DateTime Date { get; set; }

        [SitecoreComponentField]
        public string Title { get; set; }

        [SitecoreComponentField]
        public string Link { get; set; }

        [SitecoreComponentField]
        public bool ShowInCarousel { get; set; }
    }
}
