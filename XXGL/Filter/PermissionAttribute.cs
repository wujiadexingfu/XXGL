using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.Models.Authenticated;

namespace XXGL.Filter
{
    public class PermissionAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        { 
            var account=  HttpContext.Current.Session["Account"] as Account;
             string area = string.Empty;

                if (filterContext.RouteData.DataTokens["area"] != null)
                {
                    area = filterContext.RouteData.DataTokens["area"].ToString();
                }

                string controller = filterContext.RouteData.Values["controller"].ToString();

            if (account != null)
            {

            }
            else
            {
                if (controller!="Home")
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "area", "" }, { "controller", "Home" }, { "action", "Login" } });
            }
                
        }
    }
}