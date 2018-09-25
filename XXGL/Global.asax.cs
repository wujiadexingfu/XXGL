using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;
using XXGL.Filter;


namespace XXGL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //需要重新引用下Log4Net
            //在AssemblyInfo中写入[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)] 
            //注册一下log4net.config文件
            //注意在XXGL.Log.Log4Net下的Log4Net需要设置为始终复制

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Define.Log4NetFile));
        }


       
    }
}
