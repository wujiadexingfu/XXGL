using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using XXGL.Base.Models.OrganizationModel;
using XXGL.Base.Service;

namespace XXGL.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
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
           var result= OrganizationService.GetOrganizationTreeNodesByParentId(uniqueID);
           return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrganizationDetail(string uniqueID)
        {
           var result=  OrganizationService.GetOrganiztaionItemByUniqueID(uniqueID);
           return PartialView("_Detail", result);
        }


        public ActionResult GetOrganizationUserIndex(string organizationUniqueID)
        {
            OrganizationUserViewModel model = new OrganizationUserViewModel();
            model.Parameter.OrganizationUniqueID = organizationUniqueID;
            return View("_OrganizationUserIndex", model);
        }





        public ActionResult QueryOrganizationUserGridList(OrganizationUserParameter parameter)
        {
            OrganizationUserViewModel viewModel = new OrganizationUserViewModel();
            viewModel.Parameter = parameter;
            var totalCount = 0;
            var list = UsersService.GetUserListByOrganizationUniqueID(parameter, out totalCount);
            var result = new PagedList.StaticPagedList<OrganizationUserGridItem>(list, parameter.PageNo, parameter.PageSize, totalCount);
            viewModel.GridItem = result;
            return View("_OrganizationUserGridView", viewModel);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View("_Create", new CreateOrganizationInputFormViewModel() );
        }

        [HttpPost]
        public ActionResult Create(CreateOrganizationInputFormViewModel model)
        {
            var result = UsersService.Create(null);
            return Content(JosnNetHelper.ObjectToJson(result));
        }



        public ActionResult ModalTree()
        {

            return PartialView("_ModalTree");
        }


    }
}