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
            filterContext.Controller.ViewBag.firstPageDescription = null;   //一级菜单描述
            filterContext.Controller.ViewBag.secondPageDescription = null; //二级菜单描述
            filterContext.Controller.ViewBag.thirdPageDescription = null; //三级菜单描述
            filterContext.Controller.ViewBag.firstPageIcon = null; //一级菜单图案
            filterContext.Controller.ViewBag.secondPageIcon = null; //二级级菜单图案
            filterContext.Controller.ViewBag.thirdPageIcon = null; //三级菜单图案

            filterContext.Controller.ViewBag.currentWebFunctionID = null;  //当前所在页的WebFunctionID ，主要用于设置当菜单栏的状态

            string area = string.Empty;
            if (filterContext.RouteData.DataTokens["Area"] != null)
            {
                area = filterContext.RouteData.DataTokens["Area"].ToString();
            }

            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();

            if (controller.ToLower()!= "home" || action.ToLower()!= "login")
            {
                if (controller.ToLower() != "exception")
                {
                    if (account != null)
                    {

                        if (string.IsNullOrEmpty(area))
                        {
                            var ancestorPage = account.WebFunctionList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                            if (ancestorPage != null)
                            {
                                filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                                filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;
                                filterContext.Controller.ViewBag.currentWebFunctionID = ancestorPage.ID;
                            }
                            else
                            {
                                foreach (var ancestorPageItem in account.WebFunctionList)
                                {
                                    var parentPage = ancestorPageItem.SubWebFuntionList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                                    if (parentPage != null)
                                    {
                                        filterContext.Controller.ViewBag.firstPageDescription = ancestorPageItem.Description;
                                        filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;

                                        filterContext.Controller.ViewBag.firstPageIcon = ancestorPageItem.Icon;
                                        filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;

                                        filterContext.Controller.ViewBag.currentWebFunctionID = parentPage.ID;
                                        break;
                                    }
                                    else
                                    {
                                        foreach (var parentPageItem in ancestorPageItem.SubWebFuntionList)
                                        {
                                            var currnetPage = parentPageItem.SubWebFuntionList.Where(x => x.Controller == controller && x.Action == action).FirstOrDefault();
                                            if (currnetPage != null)
                                            {
                                                filterContext.Controller.ViewBag.firstPageDescription = ancestorPageItem.Description;
                                                filterContext.Controller.ViewBag.secondPageDescription = parentPageItem.Description;
                                                filterContext.Controller.ViewBag.thirdPageDescription = currnetPage.Description;

                                                filterContext.Controller.ViewBag.firstPageIcon = ancestorPageItem.Icon;
                                                filterContext.Controller.ViewBag.secondPageIcon = parentPageItem.Icon;
                                                filterContext.Controller.ViewBag.thirdPageIcon = currnetPage.Icon;

                                                filterContext.Controller.ViewBag.currentWebFunctionID = currnetPage.ID;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var ancestorPage = account.WebFunctionList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                            if (ancestorPage != null)
                            {
                                filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                                filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;

                                filterContext.Controller.ViewBag.currentWebFunctionID = ancestorPage.ID;
                            }
                            else
                            {
                                foreach (var ancestorPageItem in account.WebFunctionList)
                                {
                                    var parentPage = ancestorPageItem.SubWebFuntionList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                                    if (parentPage != null)
                                    {
                                        filterContext.Controller.ViewBag.firstPageDescription = ancestorPageItem.Description;
                                        filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;

                                        filterContext.Controller.ViewBag.firstPageIcon = ancestorPageItem.Icon;
                                        filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;

                                        filterContext.Controller.ViewBag.currentWebFunctionID = parentPage.ID;
                                        break;
                                    }
                                    else
                                    {
                                        foreach (var currentPageItem in parentPage.SubWebFuntionList)
                                        {
                                            var currnetPage = currentPageItem.SubWebFuntionList.Where(x => x.Area == area && x.Controller == controller && x.Action == action).FirstOrDefault();
                                            if (currnetPage != null)
                                            {
                                                filterContext.Controller.ViewBag.firstPageDescription = ancestorPage.Description;
                                                filterContext.Controller.ViewBag.secondPageDescription = parentPage.Description;
                                                filterContext.Controller.ViewBag.thirdPageDescription = currnetPage.Description;

                                                filterContext.Controller.ViewBag.firstPageIcon = ancestorPage.Icon;
                                                filterContext.Controller.ViewBag.secondPageIcon = parentPage.Icon;
                                                filterContext.Controller.ViewBag.thirdPageIcon = currnetPage.Icon;

                                                filterContext.Controller.ViewBag.currentWebFunctionID = currnetPage.ID;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    filterContext.Controller.ViewBag.firstPageDescription = "异常";
                    filterContext.Controller.ViewBag.firstPageIcon = "fa-cog";

                    filterContext.Controller.ViewBag.secondPageDescription = null ;
                    filterContext.Controller.ViewBag.thirdPageDescription = null;

                }

                
            }
        }
    }
}