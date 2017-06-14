using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DbCommon
{
  public  class DbSessionFactory
    {
      public  static DbContext GetCurrenntDbSession()

     {

         //这里的GetData()方法的key不能和上下文的一样

         DbContext _dbSession = CallContext.GetData("DbSession") as DbContext;

         if (_dbSession == null)

         {

             _dbSession = new XXGLEntities();

             //将值设置到数据槽里面去

             CallContext.SetData("DbSession", _dbSession);

         }

         return _dbSession;

     }


    }
}
