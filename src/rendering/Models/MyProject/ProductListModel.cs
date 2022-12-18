using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using System.Collections.Generic;

namespace MyProject.Models.MyProject
{
    public class ProductListModel
    {
        [SitecoreComponentField]
        public IEnumerable<ProductCardModel> ProductCards { get; set; }
    }
}
