using DbEntity.MSSQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.WebFunction;


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


    }
}
