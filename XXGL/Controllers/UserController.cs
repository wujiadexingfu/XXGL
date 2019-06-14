using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.Models.UserViewModel;
using Utility.Extension;
using XXGL.Base.Models;
using Utility;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Service;

namespace XXGL.Controllers
{
    public class UserController : Controller
    {
        

        // GET: User
        public ActionResult Index()
        {
            var user = UsersService.GetAccount("admin");
            Session["Account"] = user;
            UserViewModel viewModel = new UserViewModel();
            return View(viewModel);
        }

        public ActionResult Query(UserParameter parameter)
        {
           UserViewModel viewModel = new UserViewModel();
            int count = 0;
            var list = UsersService.GetUserList(parameter, out count);
            return Json(new { total = count, rows = list }, JsonRequestBehavior.AllowGet);

        }

        
        [HttpGet]
        public ActionResult Edit(string uniqueID)
        {
            var user = UsersService.GetEditUserInputFormByUniqueID(uniqueID);
            return View("_Edit", user);
        }

        [HttpPost]
        public ActionResult Edit(EditUserInputFormViewModel editUserInputFormViewModel)
        {
            RequestResult result = new RequestResult();
            if (ModelState.IsValid)
            {
                result = UsersService.EditUser(editUserInputFormViewModel);
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
            var result = UsersService.ChangePassword(uniqueID, Define.InitialPassword);
             return Content(JosnNetHelper.ObjectToJson(result));
        }

        /// <summary>
        /// 将失效人员变为正常
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public ActionResult RevertUser(string uniqueID)
        {
            var result = UsersService.RevertUser(uniqueID);
            return Content(JosnNetHelper.ObjectToJson(result));
        }

        public ActionResult Delete(string selecteds)
        {
            var selectedUniqueIDs = JosnNetHelper.JsonToObject<List<string>>(selecteds);
            var result = UsersService.Delete(selectedUniqueIDs);
            return Content(JosnNetHelper.ObjectToJson(result));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("_Create", new CreateUserInputFormViewModel() { CreateUerIsLogin = true});
        }

        [HttpPost]
        public ActionResult Create(CreateUserInputFormViewModel model)
        {
            var result = UsersService.Create(model);
           return Content(JosnNetHelper.ObjectToJson(result));   
        }

    }
}