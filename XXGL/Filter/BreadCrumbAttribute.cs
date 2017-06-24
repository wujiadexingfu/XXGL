using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.Models.Authenticated;

namespace XXGL.Filter
{
    public class BreadCrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var account = HttpContext.Current.Session["Account"] as Account;
            filterContext.Controller.ViewBag.firstPageDescription = null;
            filterContext.Controller.ViewBag.secondPageDescription = null;
            filterContext.Controller.ViewBag.thirdPageDescription = null;
            filterContext.Controller.ViewBag.firstPageIcon = null;
            filterContext.Controller.ViewBag.secondPageIcon = null;
            filterContext.Controller.ViewBag.thirdPageIcon = null;

            string area = string.Empty;
            if (filterContext.RouteData.DataTokens["Area"] != null)
            {
                area = filterContext.RouteData.DataTokens["Area"].ToString();
            }

            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();

            if (controller.ToLower() != "home" && action.ToLower() != "login")
            {

                if (account != null)
                {

                    if (string.IsNullOrEmpty(area))
                    {
                        var ancestorPage = account.FunctionItemList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                        if (ancestorPage != null)
                        {
                            filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                            filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;

                        }
                        else
                        {
                            foreach (var ancestorPageItem in account.FunctionItemList)
                            {
                                var parentPage = ancestorPageItem.SubFunctionItemList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                                if (parentPage != null)
                                {
                                    filterContext.Controller.ViewBag.firstPageDescription = ancestorPageItem.Description;
                                    filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;

                                    filterContext.Controller.ViewBag.firstPageIcon = ancestorPageItem.Icon;
                                    filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;
                                    break;
                                }

                                foreach (var currentPageItem in parentPage.SubFunctionItemList)
                                {
                                    var currnetPage = currentPageItem.SubFunctionItemList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                                    if (currnetPage != null)
                                    {
                                        filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                                        filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;
                                        filterContext.Controller.ViewBag.thirdPageDescription = currnetPage.Description;

                                        filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;
                                        filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;
                                        filterContext.Controller.ViewBag.thirdPageIcon = currnetPage.Icon;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var ancestorPage = account.FunctionItemList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                        if (ancestorPage != null)
                        {
                            filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                            filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;
                        }
                        else
                        {
                            foreach (var ancestorPageItem in account.FunctionItemList)
                            {
                                var parentPage = ancestorPageItem.SubFunctionItemList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                                if (parentPage != null)
                                {
                                    filterContext.Controller.ViewBag.firstPageDescription = ancestorPageItem.Description;
                                    filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;

                                    filterContext.Controller.ViewBag.firstPageIcon = ancestorPageItem.Icon;
                                    filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;
                                    break;
                                }

                                foreach (var currentPageItem in parentPage.SubFunctionItemList)
                                {
                                    var currnetPage = currentPageItem.SubFunctionItemList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                                    if (currnetPage != null)
                                    {
                                        filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                                        filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;
                                        filterContext.Controller.ViewBag.thirdPageDescription = currnetPage.Description;

                                        filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;
                                        filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;
                                        filterContext.Controller.ViewBag.thirdPageIcon = currnetPage.Icon;
                                        break;
                                    }
                                }
                            }
                        }
                    }


                }
            }
        }
    }
}