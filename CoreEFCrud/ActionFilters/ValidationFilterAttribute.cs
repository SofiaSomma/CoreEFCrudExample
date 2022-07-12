using CoreEFCrud.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.ActionFilters
{
    ///<!-- <summary>
    /// with this action filter we can execute code before and after the action.
    /// Like the other types of filters, the action filter can be added to different scope levels: 
    /// Global, Action, Controller.
    /// If we want to use our filter globally, we need to register it inside the AddControllers()
    /// method in the ConfigureServices method
    /// services.AddControllers(config => 
    /// {
    ///   config.Filters.Add(new GlobalFilterExample());
    /// });
    /// Action or Controller level, we need to register it in the same ConfigureServices method but 
    /// as a service in the IoC container:
    /// services.AddScoped<ActionFilterExample>();
    /// services.AddScoped<ControllerFilterExample>();
    /// [ServiceFilter(typeof(ControllerFilterExample))]
    /// [Route("api/[controller]")]
    ///  [ApiController]
    ///  public class TestController : ControllerBase
    /// {
    ///    [HttpGet]
    ///    [ServiceFilter(typeof(ActionFilterExample))]
    ///    public IEnumerable<string> Get()
    ///    {
    ///        return new string[] { "example", "data" };
    ///    }
    /// }
    /// <param name="customer"></param>
    /// <returns></returns>
    /// </summary> -->
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.

        }
    }
}
