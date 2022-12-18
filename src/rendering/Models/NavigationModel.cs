using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using System.Collections.Generic;

namespace MyProject.Models
{
    public class NavigationModel
    {
        [SitecoreComponentField]
        public string Title { get; set; }

        [SitecoreComponentField]
        public string Url { get; set; }

        [SitecoreComponentField]
        public IEnumerable<NavigationModel> Children { get; set; }
    }
}
