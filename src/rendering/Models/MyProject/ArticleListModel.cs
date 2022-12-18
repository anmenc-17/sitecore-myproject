using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using System.Collections.Generic;

namespace MyProject.Models.MyProject
{
    public class ArticleListModel
    {
        [SitecoreComponentField]
        public IEnumerable<ArticleCardModel> ArticleCards { get; set; }
    }
}
