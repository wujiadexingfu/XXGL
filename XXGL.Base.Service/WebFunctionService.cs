using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Authenticated;


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
    }
}
