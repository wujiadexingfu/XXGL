using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.IService;
using XXGL.Base.Models.UserViewModel;
using Utility.Extension;
using XXGL.Base.Models;
using Utility;
using XXGL.Base.Models.Authenticated;

namespace XXGL.Controllers
{
    public class UserController : Controller
    {
        public IUsersService _userService;


        public UserController(IUsersService userService)
        {
         
            _userService = userService;
        }


        // GET: User
        public ActionResult Index()
        {
            var user = _userService.GetAccount("admin");
            Session["Account"] = user;
            UserViewModel viewModel = new UserViewModel();

            viewModel.Parameter.ID = "1";
            viewModel.Parameter.Name = "1";
            return View(viewModel);
        }

        public ActionResult Query(UserParameter parameter)
        {
           UserViewModel viewModel = new UserViewModel();
           viewModel.Parameter = parameter;
            var totalCount=0;
            var list= _userService.GetUserList(parameter,out totalCount);
            var result = new PagedList.StaticPagedList<UserGridItem>(list, parameter.PageNo,parameter.PageSize ,totalCount);
            viewModel.GridItem = result;
            return View("_GridView",viewModel);
        }

        
        [HttpGet]
        public ActionResult Edit(string uniqueID)
        {
            var user = _userService.GetUserByUniqueID(uniqueID);
            return View("_Edit", user);
        }

        [HttpPost]
        public ActionResult Edit(EditUserInputFormViewModel editUserInputFormViewModel)
        {
            RequestResult result = new RequestResult();
            if (ModelState.IsValid)
            {
                result=_userService.EditUser(editUserInputFormViewModel);
            }
            else
            {
                result.ReturnFailMessage(ModelState.GetErrorsStringWithNumber());
            }
            return Content(JosnNetHelper.ObjectToJson(result));
        }

        /// <summary>
        /// 设置该人员的密码为初始密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword(string uniqueID)
        {
            var result = _userService.ChangePassword(uniqueID, Define.InitialPassword);
             return Content(JosnNetHelper.ObjectToJson(result));
        }

    }
}