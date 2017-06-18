using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Interface;

namespace XXGL.Base.Service
{
   public class BaseService<T> where T:class,new ()
    {
       public IBaseRepository<T> CurrentRepository { get; set; }
       public IUnitOfWork UnitOfWork{get;set;}

       public bool AddEntity(T entity)
       {
           CurrentRepository.AddEntity(entity);
           return UnitOfWork.SaveChage();
       }

       public bool UpdateEntity(T entity)
       {
           CurrentRepository.AddEntity(entity);
           return UnitOfWork.SaveChage();
       }

       public bool RemoveEntity(T entity)
       {
           CurrentRepository.RemoveEntity(entity);
           return UnitOfWork.SaveChage();
       }

    }
}
