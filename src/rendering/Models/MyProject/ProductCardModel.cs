using Sitecore.AspNet.RenderingEngine.Binding.Attributes;

namespace MyProject.Models.MyProject
{
    public class ProductCardModel
    {
        [SitecoreComponentField]
        public string Image { get; set; }

        [SitecoreComponentField]
        public string Title { get; set; }

        [SitecoreComponentField]
        public string Description { get; set; }

        [SitecoreComponentField]
        public string Link { get; set; }

        [SitecoreComponentField]
        public bool ShowInCarousel { get; set; }
    }
}
