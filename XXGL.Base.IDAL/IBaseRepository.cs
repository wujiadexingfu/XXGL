using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.IDAL
{
  public   interface IBaseRepository<T> where T :class,new()
    {
      void  AddEntity(T entity);


      void UpdateEntity(T entity);

      void UpdateEntities(IEnumerable<T> entities);

      
      void Remove(T entity);

      void Remove(string UniqueID);

      void Remove(IEnumerable<T> entities);

      IQueryable<T> GetList();

      IQueryable<T> GetList(Expression<Func<T, bool>> where);

      T GetEntity(Expression<Func<T,bool>> where);

    }
}
