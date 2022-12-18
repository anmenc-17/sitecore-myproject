using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;
using System;

namespace MyProject.Models
{
    public class MyServiceModel
    {
        public TextField DateHeader { get; set; }
        public TextField ValueHeader { get; set; }
        public DateTime ServiceDate { get; set; }
        public int ServiceValue { get; set; }
        [SitecoreContextProperty]
        public bool IsEditing { get; set; }
    }
}
