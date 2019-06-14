using DbEntity.MSSQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.WebFunction;
using Extends;


/********************************************************************************

** 类名称：  WebFunctionService

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-09-17 20:12:31

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.42000      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Service
{
    public  class WebFunctionService
    {
        public static List<WebFunction> GetWebFunctionList()
        {
            XXGLEntities db = new XXGLEntities();
            var webFunctionList = db.Sys_WebFunction.Where(x => x.IsDisplay == true).Select(x => new WebFunction { 
             ID=x.ID,
             Area=x.Area,
             Controller=x.Controller,
             Action=x.Action,
             Description=x.Description,
             Icon=x.Icon,
             ParentID=x.ParentID,
             Seq=x.Seq
            }).ToList();
            return webFunctionList;
        }

        
        /// <summary>
        /// 根据指定的UniqueId获取下级子节点
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public static string GetWebPermissionTreeNodesByParentId(string Id)
        {
            var db = new XXGLEntities();
            if (string.IsNullOrEmpty(Id)) //最开始加载的时候，uniqueid为空
            {
                Id = "";
            }
            var subWebFunctionList = db.Sys_WebFunction.Where(x => x.ParentID == Id).OrderBy(x => x.Seq).Select(x => new WebFunctionTreeNode
            {
                UniqueID = x.ID,
                Id = x.ID,
                IsOpen = false,
                IsParent = db.Sys_WebFunction.Any(x1 => x1.ParentID == x.ID),
                Name = x.Description
            }).ToList();
            var result = JsonConvert.SerializeObject(subWebFunctionList);

            return result;
        }

        public static List<WebFunctionGridItem> GetWebFunctionListByParentId(WebFunctionParameter webFunctionParameter,out int count)
        {


            List<WebFunctionGridItem> selectedWebFunctions = new List<WebFunctionGridItem>();


            using (var db = new XXGLEntities())
            {
                var allWebFunctionList = db.Sys_WebFunction.Select(x=>new WebFunctionGridItem() {
                     ID=x.ID,
                     ParentID=x.ParentID,
                     Area=x.Area,
                     Action=x.Action,
                     Icon=x.Icon,
                     Seq=x.Seq,
                     Controller=x.Controller,
                     Description=x.Description
                     
                }).ToList();

                GetWebFunctionListByRecursion(allWebFunctionList, webFunctionParameter.WebFunctionId, selectedWebFunctions);

            }
            count = selectedWebFunctions.Count;
            return selectedWebFunctions.AsQueryable().OrderByField<WebFunctionGridItem>(webFunctionParameter.OrderName, webFunctionParameter.Order).Skip(webFunctionParameter.Offset).Take(webFunctionParameter.Limit).ToList();

        }



        public static List<WebFunctionGridItem> GetWebFunctionListByParentId(string webFunctionId)
        {

            List<WebFunctionGridItem> selectedWebFunctions = new List<WebFunctionGridItem>();


            using (var db = new XXGLEntities())
            {
                var allWebFunctionList = db.Sys_WebFunction.Select(x => new WebFunctionGridItem()
                {
                    ID = x.ID,
                    ParentID = x.ParentID,
                    Area = x.Area,
                    Action = x.Action,
                    Icon = x.Icon,
                    Seq = x.Seq,
                    Controller = x.Controller,
                    Description = x.Description

                }).ToList();

                GetWebFunctionListByRecursion(allWebFunctionList, webFunctionId, selectedWebFunctions);

            }
            return selectedWebFunctions;

        }


        public static void GetWebFunctionListByRecursion(List<WebFunctionGridItem> allWebFunctionList, string parentId,List<WebFunctionGridItem> selectedWebFunctions)
        {
            var parentWebFunction = allWebFunctionList.Where(x => x.ID == parentId).FirstOrDefault();
            selectedWebFunctions.Add(parentWebFunction);

            var sonWebFunctionList = allWebFunctionList.Where(x => x.ParentID == parentId).ToList();
            if (sonWebFunctionList != null && sonWebFunctionList.Count > 0)
            {
                foreach (var item in sonWebFunctionList)
                {
                    GetWebFunctionListByRecursion(allWebFunctionList, item.ID, selectedWebFunctions);
                }
            }
            
        }


        public static RequestResult Create(CreateWebFunctionInputForm createWebPermissionInputFormViewModel)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var exits = db.Sys_WebFunction.Where(x => x.ID == createWebPermissionInputFormViewModel.CreateWebFunctionId).FirstOrDefault();
                if (exits != null)
                {
                    result.ReturnFailMessage("菜单编号重复");  //菜单编号重复
                }
                else
                {
                    Sys_WebFunction webFunction = new Sys_WebFunction();
                    webFunction.ID = createWebPermissionInputFormViewModel.CreateWebFunctionId;
                    webFunction.ParentID = createWebPermissionInputFormViewModel.CreateWebFunctionParentId;
                    webFunction.Area = createWebPermissionInputFormViewModel.CreateWebFunctionArea;
                    webFunction.Controller = createWebPermissionInputFormViewModel.CreateWebFunctionController;
                    webFunction.Action = createWebPermissionInputFormViewModel.CreateWebFunctionAction;
                    webFunction.Icon = createWebPermissionInputFormViewModel.CreateWebFunctionIcon;
                    webFunction.Seq = createWebPermissionInputFormViewModel.CreateWebFunctionSeq;
                    webFunction.Description = createWebPermissionInputFormViewModel.CreateWebFunctionDescription;
                    webFunction.IsDisplay = createWebPermissionInputFormViewModel.CreateWebFunctionIsDisplay;
                    webFunction.CreateTime = DateTime.Now;
                    webFunction.CreateUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;
                    db.Sys_WebFunction.Add(webFunction);
                    db.SaveChanges();
                    result.RetunSuccessMessage("新增成功");
                }
            }
            catch (Exception ex)
            {

                result.ReturnFailMessage("新增失败");
            }

            return result;
        }

        public static RequestResult Delete(string webFunctionId)
        {
            RequestResult result = new RequestResult();
            try
            {
                var db = new XXGLEntities();
                var webFunction = db.Sys_WebFunction.Where(x =>x.ID==webFunctionId).FirstOrDefault();
                var deleteWebFunctions= GetWebFunctionListByParentId(webFunction.ID); //获取本节点及下属组织节点
                var deleteWebFunctionIdList = deleteWebFunctions.Select(x => x.ID).ToList();
                db.Sys_WebFunction.RemoveRange(db.Sys_WebFunction.Where(x => x.ID == webFunctionId));
                db.SaveChanges();
                result.RetunSuccessMessage("删除成功");

            }
            catch (Exception ex)
            {
                result.ReturnFailMessage("删除失败");

            }
            return result;
        }

        public static EditWebFunctionInputForm GetEditWebFunctionInputForm(string webFunctionId)
        {
            var db = new XXGLEntities();
            var item = db.Sys_WebFunction.Where(x => x.ID == webFunctionId).FirstOrDefault();
            EditWebFunctionInputForm inputForm = new EditWebFunctionInputForm();
            inputForm.EditWebFunctionId =item.ID;
            inputForm.EditWebFunctionParentId = item.ParentID;
            inputForm.EditWebFunctionArea = item.Area;
            inputForm.EditWebFunctionController = item.Controller;
            inputForm.EditWebFunctionAction = item.Action;
            inputForm.EditWebFunctionIcon = item.Icon;
            inputForm.EditWebFunctionSeq = item.Seq;
            inputForm.EditWebFunctionIsDisplay = item.IsDisplay;
            inputForm.EditWebFunctionDescription = item.Description;
            return inputForm;
        }

        public static RequestResult Edit(EditWebFunctionInputForm editWebFunctionInputForm)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var item = db.Sys_WebFunction.Where(x => x.ID == editWebFunctionInputForm.EditWebFunctionId).FirstOrDefault();
                item.ParentID = editWebFunctionInputForm.EditWebFunctionParentId;
                item.Area = editWebFunctionInputForm.EditWebFunctionArea;
                item.Controller = editWebFunctionInputForm.EditWebFunctionController;
                item.Action = editWebFunctionInputForm.EditWebFunctionAction;
                item.Icon = editWebFunctionInputForm.EditWebFunctionIcon;
                item.Seq = editWebFunctionInputForm.EditWebFunctionSeq;
                item.IsDisplay = editWebFunctionInputForm.EditWebFunctionIsDisplay;
                item.Description = editWebFunctionInputForm.EditWebFunctionDescription;
                db.SaveChanges();
                result.RetunSuccessMessage("修改成功");   
            }
            catch (Exception ex)
            {

                result.ReturnFailMessage("修改失败");
            }

            return result;
        }



    }
}
