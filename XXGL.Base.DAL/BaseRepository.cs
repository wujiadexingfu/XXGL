using DbCommon;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.DAL
{
 public  class BaseRepository<T> where T:class ,new()
 {
     DbContext db = DbSessionFactory.GetCurrenntDbSession();
     public void   AddEntity(T entity)
    {
        db.Entry<T>(entity).State = EntityState.Added;
    }

     public void AddEntities(IEnumerable<T> entities)
     {
         foreach (var entity in entities)
         {
             AddEntity(entity);
         }
     }


     public void UpdateEntity(T entity)
     {
         db.Set<T>().Attach(entity);
         db.Entry<T>(entity).State = EntityState.Modified;
     }

    public  void UpdateEntities(IEnumerable<T> entities)
     {
         foreach (var entity in entities)
         {
             UpdateEntity(entity);
         }
     }


    public void RemoveEntity(T entity)
    { 
        db.Set<T>().Attach(entity);
        db.Entry<T>(entity).State = EntityState.Deleted;

    }

   public void RemoveEntities(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            RemoveEntity(entity);
        }
       
    }

    public  IQueryable<T> GetAsQueryable()
    {
       return db.Set<T>().AsQueryable();
    }

    public  IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> where)
    {
        return db.Set<T>().Where(where);
    }

    public  T GetEntity(Expression<Func<T, bool>> where)
    {
        return db.Set<T>().FirstOrDefault(where);
    }

 }
}
