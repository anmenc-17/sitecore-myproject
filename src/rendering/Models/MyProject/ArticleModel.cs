using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models.MyProject
{
    public class ArticleModel
    {
        public TextField Category { get; set; }

        [SitecoreComponentField(Name = "Publishing Date")]
        public DateField Date { get; set; }
        public TextField Title { get; set; }
        public ImageField Image { get; set; }

        [SitecoreComponentField(Name = "Article Body")]
        public RichTextField ArticleBody { get; set; }
    }
}
