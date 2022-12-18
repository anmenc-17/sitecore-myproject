using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models.MyProject
{
    public class TextWithImageModel
    {
        public TextField Headline { get; set; }
        public RichTextField Text { get; set; }
        public HyperLinkField Link { get; set; }
        public ImageField Image { get; set; }
    }
}
