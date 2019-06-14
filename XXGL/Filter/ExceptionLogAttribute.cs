using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XXGL.Filter
{
    public class ExceptionLogAttribute : HandleErrorAttribute, IExceptionFilter
    {
        public new  void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;

            string areaName = (string)filterContext.RouteData.Values["area"];
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];

            if (filterContext.ExceptionHandled == true)  //为了防止使用多个异常过滤器导致操作发生冲突
            {
                return;
            }
            HttpException httpException = new HttpException(null, exception);
      
            int statusCode = new HttpException(null, filterContext.Exception).GetHttpCode(); //获取当前错误原因Code值
            //filterContext.Exception.Message可获取错误信息

            /*
             * 1、根据对应的HTTP错误码跳转到错误页面
             * 2、这里对HTTP 404/400错误进行捕捉和处理
             * 3、其他错误默认为HTTP 500服务器错误
             */
            if (statusCode == 404)
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.Result = new RedirectResult("_LostView"); //设置404请求错误页面

                //  filterContext.HttpContext.Response.Write("错误的请求路径");
                //   filterContext.HttpContext.Response.WriteFile("");
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;

                //  filterContext.Result = new RedirectResult("~/Home/Error");  //跳转方式一
                //     filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login", area = string.Empty })); //跳转方式二
                // filterContext.HttpContext.Response.Write("服务器内部错误"); //直接输出
                // filterContext.HttpContext.Response.WriteFile("");//输出文件

                //MExceptions mException = new MExceptions() { controller = controllerName, action = actionName, message = exception.Message };
                //filterContext.Result = new ViewResult
                //{

                //    ViewName = "_ExceptionView",
                //    TempData = filterContext.Controller.TempData,
                //    ViewData = new ViewDataDictionary<MExceptions>(mException)  //定义ViewData
                //};

                filterContext.Result = new RedirectResult("_ExceptionView"); //设置404请求错误页面
            }
            /*---------------------------------------------------------
             * 这里可进行相关自定义业务处理，比如日志记录等
             ---------------------------------------------------------*/

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;
            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}