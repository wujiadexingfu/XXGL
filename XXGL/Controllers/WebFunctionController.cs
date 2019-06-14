using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using XXGL.Base.Models.WebFunction;
using XXGL.Base.Service;

namespace XXGL.Controllers
{
    public class WebFunctionController : Controller
    {
        // GET: WebFunction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitTree()
        {

            return PartialView("_Tree");
        }


        
        public ActionResult GetTreeNodes(string uniqueID)
        {
           var result= WebFunctionService.GetWebPermissionTreeNodesByParentId(uniqueID);
           return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GridView(string webFunctionUniqueId)
        {
            ViewBag.WebFunctionUniqueId = webFunctionUniqueId;
            return View("_GridView");
        }
        public ActionResult Query(WebFunctionParameter webFunctionParameter)
        {
            int count = 0;
            var list=  WebFunctionService.GetWebFunctionListByParentId(webFunctionParameter,out  count);
            return Json(new { total = count, rows = list }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Create(string webFunctionId)
        {

            CreateWebFunctionInputForm inputForm = new CreateWebFunctionInputForm() {   CreateWebFunctionParentId= webFunctionId, CreateWebFunctionIsDisplay = true};
            return View("_Create", inputForm);

        }

        [HttpPost]
        public ActionResult Create(CreateWebFunctionInputForm model)
        {
            var result = WebFunctionService.Create(model);
            return Content(JosnNetHelper.ObjectToJson(result));
        }


        public ActionResult Delete(string webFunctionId)
        {
            var result = WebFunctionService.Delete(webFunctionId);
            return Content(JosnNetHelper.ObjectToJson(result));
        }

        [HttpGet]
        public ActionResult Edit(string webFunctionId)
        {

            var model= WebFunctionService.GetEditWebFunctionInputForm(webFunctionId);

            return View("_Edit", model);


        }

        [HttpPost]
        public ActionResult Edit(EditWebFunctionInputForm model)
        {
            var result = WebFunctionService.Edit(model);
            return Content(JosnNetHelper.ObjectToJson(result));
        }





    }
}