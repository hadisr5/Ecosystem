using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace Seventy.Web.StartupCustomizations
{
   
    public class FeatureLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(FeatureLocationExpander);
        }
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area name

            object area;
            if (context.ActionContext.RouteData.Values.TryGetValue("area", out area))
            {
                yield return "/Areas/{2}/{1}/{0}.cshtml";
                yield return "/Areas/{2}/Shared/{0}.cshtml";
                yield return "/Areas/Shared/{0}.cshtml";
            }
            else
            {
                yield return "/Features/{1}/{0}.cshtml";
                yield return "/Features/Shared/{0}.cshtml";
            }
        }


    }

}
