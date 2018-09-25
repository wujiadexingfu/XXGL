using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XXGL.Filter
{
    public class MyExceptionAttribute:ActionFilterAttribute, IActionFilter
    {



        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
    }
}