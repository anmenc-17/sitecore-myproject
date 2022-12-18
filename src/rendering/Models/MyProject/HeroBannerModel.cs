using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models.MyProject
{
    public class HeroBannerModel
    {
        public ImageField Image { get; set; }
        public TextField Headline { get; set; }
        public HyperLinkField CTA { get; set; }
    }
}
