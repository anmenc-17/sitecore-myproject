using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models
{
    public class Tab
    {
        public TextField Title { get; set; } = default!;

        public RichTextField Content { get; set; } = default!;
    }
}
