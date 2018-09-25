using System.Web;
using System.Web.Mvc;
using XXGL.Filter;

namespace XXGL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
          //  filters.Add(new PermissionAttribute());
           filters.Add(new BreadCrumbAttribute());  //导航栏

           filters.Add(DependencyResolver.Current.GetService<MyExceptionAttribute>());
          // filters.Add(new MyExceptionAttribute());

        }
    }
}
