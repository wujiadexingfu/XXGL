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
using XXGL.Base.Models.OrganizationModel;

namespace XXGL.Base.Service
{
  public  class OrganizationService
    {
        public static string GetOrganizationTreeNodesByParentId(string uniqueID)
        {
            var db = new XXGLEntities();
            if (string.IsNullOrEmpty(uniqueID)) //最开始加载的时候，uniqueid为空
            {
                uniqueID = "*";
            }
            var subOrganizationList = db.Sys_Organization.Where(x => x.ParentUniqueID == uniqueID).OrderBy(x => x.Seq).Select(x => new OrganizationTreeNode
            {
               UniqueID=x.UniqueID,
               Id=x.ID,
               Icon=x.Icon,
               IsOpen=false,
               IsParent=db.Sys_Organization.Any(x1=>x1.ParentUniqueID==x.UniqueID),
               Name=x.Name
            }).ToList();
            var result = JsonConvert.SerializeObject(subOrganizationList);
          
            return result;
        }

        public static OrganizationItem GetOrganiztaionItemByUniqueID(string uniqueID)
        {
            var db = new XXGLEntities();
            var result = db.Sys_Organization.Where(x => x.UniqueID == uniqueID).Select(x => new OrganizationItem()
            {
                UniqueID = x.UniqueID,
                ID = x.ID,
                Name = x.Name,
                Manager = db.Sys_User.Where(x1 => x1.UniqueID == x.ManagerUniqueID).Select(x1=>x1.Name).FirstOrDefault(),
                CreateUser = db.Sys_User.Where(x1 => x1.UniqueID == x.CreateUser).Select(x1 => x1.Name).FirstOrDefault(),
                CreateTime =x.CreateTime.Value
            }).FirstOrDefault();

            return result;


        }


        public static RequestResult Create(CreateOrganizationInputForm createOrganizationInputForm)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var exits = db.Sys_Organization.Where(x => x.ID == createOrganizationInputForm.CreateOrganizationID).FirstOrDefault();
                if (exits != null)
                {
                    result.ReturnFailMessage("部门编号重复"); 
                }
                else
                {
                    Sys_Organization organization = new Sys_Organization();
                    organization.UniqueID = Guid.NewGuid().ToString();
                    organization.ID = createOrganizationInputForm.CreateOrganizationID;
                    organization.Name = createOrganizationInputForm.CreateOrganizationName;
                    organization.Icon = createOrganizationInputForm.CreateOrganizationIcon;
                    organization.ParentUniqueID = createOrganizationInputForm.CreateOrganizationParentUniqueID;
                    organization.ManagerUniqueID = createOrganizationInputForm.CreateOrganizationManagerUniqueID;
                    organization.IsDelete = false;
                    organization.CreateTime = DateTime.Now;
                    organization.CreateUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;
                    organization.Seq = Convert.ToInt32(createOrganizationInputForm.CreateOrganizationSeq);
                 
                    db.Sys_Organization.Add(organization);
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



        /// <summary>
        /// 根据父节点获取下属组织
        /// </summary>
        /// <param name="parentUniqueID">父节点的UnqieID</param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static List<OrganizationItem> GetDownOrganizationsByParentUniqueID(string uniqueID)
        {
            var db = new XXGLEntities();
            var organizationList = db.Sys_Organization.Select(x => new OrganizationItem()
            {
                UniqueID = x.UniqueID,
                ID = x.ID,
                Name = x.Name,
                Manager = db.Sys_User.Where(x1 => x1.UniqueID == x.ManagerUniqueID).Select(x1 => x1.Name).FirstOrDefault(),
                CreateUser = db.Sys_User.Where(x1 => x1.UniqueID == x.CreateUser).Select(x1 => x1.Name).FirstOrDefault(),
                CreateTime = x.CreateTime.Value,
                ParentUniqueId = x.ParentUniqueID
            }).ToList();

            List<OrganizationItem> selectedOrganizationList = new List<OrganizationItem>();
            GetOrganizationByParentUniqueId(organizationList, uniqueID, selectedOrganizationList);

            return selectedOrganizationList;

        }


        public static void GetOrganizationByParentUniqueId(List<OrganizationItem> allOrganizationList, string uniqueID, List<OrganizationItem> selectedOrganizationList)
        {
            var parentWOrganization = allOrganizationList.Where(x => x.UniqueID == uniqueID).FirstOrDefault();
            selectedOrganizationList.Add(parentWOrganization);

            var sonWOrganizationList = allOrganizationList.Where(x => x.ParentUniqueId == uniqueID).ToList();
            if (sonWOrganizationList != null && sonWOrganizationList.Count > 0)
            {
                foreach (var item in sonWOrganizationList)
                {
                    GetOrganizationByParentUniqueId(allOrganizationList, item.UniqueID, selectedOrganizationList);
                }
            }
        }


        public static  EditOrganizationInputForm GetEditOrganizationInputFormByUniqueId(string uniqueId)
        {
            var db = new XXGLEntities();
            var model = db.Sys_Organization.Where(x => x.UniqueID == uniqueId).Select(x=>new EditOrganizationInputForm() {

                 EditOrganizationUniqueId=x.UniqueID,
                 EditOrganizationID=x.ID,
                 EditOrganizationIcon=x.Icon,
                 EditOrganizationManagerUniqueID=x.ManagerUniqueID,
                 EditOrganizationName=x.Name,
                 EditOrganizationParentUniqueID=x.ParentUniqueID,
                 EditOrganizationSeq=x.Seq,
                 EditOrganizationParentName=db.Sys_Organization.Where(x1=>x1.UniqueID==x.ParentUniqueID).FirstOrDefault().ID+"/"+ db.Sys_Organization.Where(x1 => x1.UniqueID == x.ParentUniqueID).FirstOrDefault().Name
            }).FirstOrDefault();

            return model;
        }


        public static RequestResult Edit(EditOrganizationInputForm editOrganizationInputForm)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var exits = db.Sys_Organization.Where(x => x.ID == editOrganizationInputForm.EditOrganizationID&&x.UniqueID!= editOrganizationInputForm.EditOrganizationUniqueId).FirstOrDefault();
                if (exits != null)
                {
                    result.ReturnFailMessage("部门编号重复");
                }
                else
                {
                    Sys_Organization organization = new Sys_Organization();
                    var editOrganization = db.Sys_Organization.Where(x => x.UniqueID == editOrganizationInputForm.EditOrganizationUniqueId).FirstOrDefault();

                    editOrganization.ID = editOrganizationInputForm.EditOrganizationID;
                    editOrganization.Icon = editOrganizationInputForm.EditOrganizationIcon;
                    editOrganization.ManagerUniqueID = editOrganizationInputForm.EditOrganizationManagerUniqueID;
                    editOrganization.Name = editOrganizationInputForm.EditOrganizationName;
                    editOrganization.ParentUniqueID = editOrganizationInputForm.EditOrganizationParentUniqueID;
                    editOrganization.Seq = editOrganizationInputForm.EditOrganizationSeq;



                    db.SaveChanges();
                    result.RetunSuccessMessage("修改成功");
                }
            }
            catch (Exception ex)
            {

                result.ReturnFailMessage("修改失败");
            }

            return result;
        }



        ///// <summary>
        ///// 获取组织结构树
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="id"></param>
        ///// <param name="treeNodes"></param>
        ///// <returns></returns>
        //static void GetOrganizeSelectTreeNodes(List<BaseOrganizeEntity> list, string id, ref List<SelectTreeNode> treeNodes)
        //{
        //    if (list == null)
        //        return;
        //    List<BaseOrganizeEntity> sublist;
        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
        //        sublist = list.Where(t => t.ParentId == id).ToList();
        //    }
        //    else
        //    {
        //        sublist = list.Where(t => string.IsNullOrWhiteSpace(t.ParentId)).ToList();
        //    }
        //    if (!sublist.Any())
        //        return;
        //    foreach (var item in sublist)
        //    {
        //        treeNodes.Add(new SelectTreeNode() { id = item.Id, name = item.FullName, parentId = item.ParentId });
        //        GetOrganizeSelectTreeNodes(list, item.Id, ref treeNodes);
        //    }
        //}


    }
}
