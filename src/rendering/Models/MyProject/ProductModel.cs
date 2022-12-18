using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models.MyProject
{
    public class ProductModel
    {
        [SitecoreComponentField(Name = "Product name")]
        public TextField ProductName { get; set; }

        [SitecoreComponentField(Name = "Packaging info")]
        public TextField PackagingInfo { get; set; }
        public FileField PDF { get; set; }
        public TextField Description { get; set; }
        public ImageField Image { get; set; }
    }
}
