using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AllowSynchronousIOAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var syncIOFeature = context.HttpContext.Features.Get<IHttpBodyControlFeature>();

            if (syncIOFeature is not null) syncIOFeature.AllowSynchronousIO = true;
        }
    }
}
