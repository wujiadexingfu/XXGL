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


    public void Remove(T entity)
    { 
        db.Set<T>().Attach(entity);
        db.Entry<T>(entity).State = EntityState.Deleted;

    }

    void Remove(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Remove(entity);
        }
       
    }

    public  IQueryable<T> GetAsQueryable()
    {
       return db.Set<T>().AsQueryable();
    }

    IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> where)
    {
        return db.Set<T>().Where(where);
    }

    T GetEntity(Expression<Func<T, bool>> where)
    {
        return db.Set<T>().FirstOrDefault(where);
    }

 }
}
