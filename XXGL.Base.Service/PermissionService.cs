using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Permission;


/********************************************************************************

** 类名称：  PermissionService

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-09-17 19:19:01

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.42000      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Service
{
   public class PermissionService
    {
       /// <summary>
        /// 根据人员的ID获取到该人员的所有菜单权限
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
       public static List<PermissionModel> GetPermissionByUserId(string Id)
       {
           XXGLEntities db=new XXGLEntities();
           var permissionList=(from x in db.Sys_Permission
                               join x1 in db.Sys_RolePermission  on x.UniqueID equals x1.PermissionUniqueID
                               join x2 in db.Sys_UserRole on x1.RoleUniqueID equals x2.RolesUniqueID
                               join x3 in db.Sys_User on x2.UsersUniqueID equals x3.UniqueID
                               join x4 in db.Sys_WebFunction on x.WebFunctionID equals x4.ID
                               where x3.ID==Id && x.IsDelete.ToString()!="1"  select new  PermissionModel() {
                                UniqueID=x.UniqueID,
                                Area=x.Area,
                                Action=x.Action,
                                Controller=x.Controller,
                                Description=x.Description,
                                Seq=x.Seq,
                                Value=x.Value,
                                WebFunctionID = x.WebFunctionID,
                                WebFunctionParentID=x4.ParentID
                               }).ToList();
           return permissionList;
       }

       


    }
}
