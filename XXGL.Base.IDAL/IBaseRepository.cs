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

      void AddEntities(IEnumerable<T> entities);


      void UpdateEntity(T entity);

      void UpdateEntities(IEnumerable<T> entities);

      void RemoveEntity(T entity);

      void RemoveEntities(IEnumerable<T> entities);

      IQueryable<T> GetAsQueryable();

      IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> where);

      T GetEntity(Expression<Func<T,bool>> where);

    }
}
