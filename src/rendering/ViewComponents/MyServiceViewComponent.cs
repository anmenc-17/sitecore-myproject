using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using Sitecore.AspNet.RenderingEngine.Binding;
using System.Threading.Tasks;
using System;

namespace MyProject.ViewComponents
{
    public class MyServiceViewComponent : ViewComponent
    {
        private readonly IViewModelBinder modelBinder;
        private readonly IFakeService fakeService;

        // Use the view component support for dependency injection to inject
        // FakeService and the
        // Sitecore.AspNet.RenderingEngine.Binding.IViewModelBinder service.
        public MyServiceViewComponent(IViewModelBinder modelBinder, IFakeService fakeService)
        {
            this.modelBinder = modelBinder;
            this.fakeService = fakeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Bind MyServiceModel to the Sitecore Layout Service response
            // when the view component is invoked.
            var model = await modelBinder.Bind<MyServiceModel>(this.ViewContext);

            if (model.IsEditing)
            {
                // Set mock values to be displayed in edit mode only.
                // This avoids invoking FakeService in the Experience Editor and
                // can be very helpful for content authors, especially if
                // FakeService requires authentication or the user values are
                // not available in edit mode.
                model.ServiceDate = DateTime.MinValue;
                model.ServiceValue = 42;
            }
            else
            {
                // Invoke FakeService to populate the date and value of the model.
                var serviceResult = await fakeService.GetFakeValueAsync();
                model.ServiceDate = fakeService.FakeDate;
                model.ServiceValue = serviceResult;
            }

            // Return the model to the Default.cshtml Razor view.
            return View(model);
        }
    }
}
