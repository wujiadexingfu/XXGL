using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Interface;

namespace DbCommon
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool SaveChage()
        {
          var db=  DbSessionFactory.GetCurrenntDbSession();  //获取到数据库唯一的数据库连接对象
          if (db.SaveChanges() > 0)
          {
              return true;
          }//保存
          else
          {
              return false;
          }
        }
    }
}
