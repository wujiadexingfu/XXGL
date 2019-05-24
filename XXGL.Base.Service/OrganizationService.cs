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


        public static RequestResult Create(CreateOrganizationInputFormViewModel createOrganizationInputFormViewModel)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var exitsUser = db.Sys_Organization.Where(x => x.ID == createOrganizationInputFormViewModel.ID).FirstOrDefault();
                if (exitsUser != null)
                {
                    result.ReturnFailMessage("部门编号重复"); 
                }
                else
                {
                    Sys_Organization organization = new Sys_Organization();
                    organization.UniqueID = Guid.NewGuid().ToString();
                    organization.ID = createOrganizationInputFormViewModel.ID;
                    organization.Name = createOrganizationInputFormViewModel.Name;
                    organization.Icon = createOrganizationInputFormViewModel.Icon;
                    organization.ParentUniqueID = createOrganizationInputFormViewModel.ParentUniqueID;
                    organization.ManagerUniqueID = createOrganizationInputFormViewModel.ManagerUniqueID;
                    organization.IsDelete = false;
                    organization.CreateTime = DateTime.Now;
                    organization.CreateUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;
                    organization.Seq = Convert.ToInt32(createOrganizationInputFormViewModel.Seq);
                 
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
        public static List<OrganizationItem> GetDownOrganizationsByParentUniqueID(string parentUniqueID,bool contains)
        {

            var db = new XXGLEntities();
            var result = db.Sys_Organization.Where(x => x.ParentUniqueID == parentUniqueID).Select(x => new OrganizationItem()
            {
                UniqueID = x.UniqueID,
                ID = x.ID,
                Name = x.Name,
                Manager = db.Sys_User.Where(x1 => x1.UniqueID == x.ManagerUniqueID).Select(x1 => x1.Name).FirstOrDefault(),
                CreateUser = db.Sys_User.Where(x1 => x1.UniqueID == x.CreateUser).Select(x1 => x1.Name).FirstOrDefault(),
                CreateTime = x.CreateTime.Value
            }).ToList();  //下属组织
             
           

            if (contains)  //如果包含，则加入本组织信息
            {
                var parentOrganizationItem = db.Sys_Organization.Where(x => x.UniqueID == parentUniqueID).Select(x=>new OrganizationItem()
                {
                    UniqueID =x.UniqueID,
                    ID =x.ID,
                    Manager = db.Sys_User.Where(x1 => x1.UniqueID == x.ManagerUniqueID).Select(x1 => x1.Name).FirstOrDefault(),
                    CreateUser = db.Sys_User.Where(x1 => x1.UniqueID == x.CreateUser).Select(x1 => x1.Name).FirstOrDefault(),
                    CreateTime = x.CreateTime.Value
                }).FirstOrDefault();
                result.Add(parentOrganizationItem);
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
