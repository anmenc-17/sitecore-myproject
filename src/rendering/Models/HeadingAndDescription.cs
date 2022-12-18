using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models
{
    public class HeadingAndDescription : HeadingOnly
    {
        public RichTextField Description { get; set; } = default!;
    }
}
