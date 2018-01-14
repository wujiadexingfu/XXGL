using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc.Ajax;
using Resources;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Web;

/********************************************************************************

** 类名称：  Class1

** 描述：设置一些webSite总需要用到的公共类，如分页信息
 *这儿的分页需要调用三个dll，PagedList.Mvc，PagedList和System.Web.Mvc；

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-06 10:06:16

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/


namespace Utility
{
  public  static class SiteLibrary
    {
      public static PagedListRenderOptions GetDefaultPagerOptions(string UpdateDivID)
      {

          PagedListRenderOptions result = null;
          var maxPageNumber = 10;  
          if (!String.IsNullOrEmpty(Config.MaxPageNumber))  //从xml文件中读取页数信息，为空的话则使用默认的10页
          {
              if (!int.TryParse(Config.MaxPageNumber, out  maxPageNumber))//转换失败的话，则使用之前默认的数据
              {
                  maxPageNumber = 10;
              }
          }
          var options = new PagedListRenderOptions()
          {
              //Display=PagedListDisplayMode.Never, //是否需要显示分页
              DisplayLinkToFirstPage = PagedListDisplayMode.Always, //首页是否可见
              DisplayLinkToLastPage = PagedListDisplayMode.Always, //尾页是否可见
              DisplayLinkToPreviousPage = PagedListDisplayMode.Always, //上一页是否可见
              DisplayLinkToNextPage = PagedListDisplayMode.Always, //下一页是否可见
              DisplayLinkToIndividualPages = true,  //设置为false则不会有页码选择，只有上一页和下一页
              //ContainerDivClasses = null,
              //ClassToApplyToFirstListItemInPager = null,
             // ClassToApplyToLastListItemInPager = null,
              //DisplayPageCountAndCurrentLocation = true,
              //PageCountAndCurrentLocationFormat = "Page {0} of {1}",  //当DisplayPageCountAndCurrentLocation为true的时候，会在上一页和页码之间显示该信息
              //DisplayItemSliceAndTotal = true,
              //ItemSliceAndTotalFormat = "总共{2},当前显示{0}到{1}条数据", //当ItemSliceAndTotalFormat为true时，会显示具体查询信息的项次
              //LinkToIndividualPageFormat = "-{0}-", //每一页显示的格式
             // DisplayEllipsesWhenNotShowingAllPageNumbers = true,//如果为true，则当超出最大显示数量时，默认会用...展示，如果设置了EllipsesFormat属性，则用该属性的值展示
              //EllipsesFormat = "....",
              //DelimiterBetweenPageNumbers = "",
              //FunctionToDisplayEachPageNumber = (x => { return x + "/10"; }),  //这儿使用C#代码的方式设置显示页码的格式。
              MaximumPageNumbersToDisplay = maxPageNumber,  //设置最大显示页数
              LinkToPreviousPageFormat = Resources.Resource.PreviousPageFormat,//上一页
              LinkToNextPageFormat = Resources.Resource.NextPageFormat, //下一页
              LinkToFirstPageFormat = Resources.Resource.FirstPageFormat,//首页
              LinkToLastPageFormat = Resources.Resource.LastPageFormat  //尾页

          };
          result = PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(options, GetDefaultAjaxOptions(UpdateDivID));
          return options;
      }

      public static AjaxOptions GetDefaultAjaxOptions(string UpdateDivID)
      {
          var updateTargetId = "divGridView";
          if (!string.IsNullOrEmpty(UpdateDivID))  
          {
              updateTargetId = UpdateDivID;
          }
          return new AjaxOptions
          {
              UpdateTargetId = updateTargetId,
              InsertionMode = InsertionMode.Replace,
              OnBegin = "$.ShowLoading('" + updateTargetId + "')",
              OnComplete = "$.HideLoading('" + updateTargetId + "')",
          };
      }

      /// <summary>
      /// http://lastattacker.blogspot.tw/2011/11/mvc-2-hidden-helper-extension.html
      /// 该方法主要用于保存分页的数据
      /// </summary>
      /// <typeparam name="TModel"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="htmlHelper"></param>
      /// <param name="expression"></param>
      /// <returns></returns>
      public static MvcHtmlString HiddenEntirelyFor<TModel, TResult>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
      {
          ModelMetadata modelMetadata =
             ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
          List<string> htmlEntries =
             modelMetadata.Properties
                .Select(property => htmlHelper.Hidden(ExpressionHelper.GetExpressionText(expression) + "." + property.PropertyName, property.Model, null))
                .Select(mvcHtmlString => mvcHtmlString.ToHtmlString())
                .ToList();
          return MvcHtmlString.Create(String.Join(Environment.NewLine, htmlEntries.ToArray()));
      }

    }
}
