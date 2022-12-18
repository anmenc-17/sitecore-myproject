using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace MyProject.Models
{
    public class FieldUsageNumber : HeadingAndDescription
    {
        public NumberField Sample { get; set; } = default!;
    }
}
