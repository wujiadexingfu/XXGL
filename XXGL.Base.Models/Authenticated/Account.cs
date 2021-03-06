﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Permission;

namespace XXGL.Base.Models.Authenticated
{
   public class Account
    {
       /// <summary>
       /// Guid
       /// </summary>
       public string UniqueID { get; set; }

       /// <summary>
       /// ID
       /// </summary>
       public string ID { get; set; }

       /// <summary>
       /// 姓名
       /// </summary>
       public string Name { get; set; }

       /// <summary>
       /// 组织的GUID
       /// </summary>

       public string OrganizationUniqueID { get; set; }

       /// <summary>
       /// 照片
       /// </summary>
       public string Photo { get; set; }

       /// <summary>
       /// 职称
       /// </summary>
       public string Title { get; set; }

       /// <summary>
       /// 语言
       /// </summary>

       public string LanuageUniqueID { get; set; }

       /// <summary>
       /// 记录菜单和操作权限
       /// </summary>
       public List<PermissionModel> PermissionList { get; set; }

       /// <summary>
       /// 菜单
       /// </summary>
       public List<WebFunction> WebFunctionList { get; set; }

       public Account()
       {
           PermissionList = new List<PermissionModel>();
           WebFunctionList = new List<WebFunction>();
       }
    }
}
