using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public void Create(T entity)
        {
            using (var ctx = new UserContext())
            {
                ctx.Set<T>().Add(entity);
                ctx.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var db = new UserContext())
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (var db = new UserContext())
            {
                var entity = db.Set<T>().Single(u => u.Id == id);
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public T Get(long id, string include = null)
        {
            using (var db = new UserContext())
            {
                if (include != null)
                {
                    return db.Set<T>().Include(include).Single(u => u.Id == id);
                }
                else
                {
                    return db.Set<T>().Single(u => u.Id == id);
                }                
            }
        }

        public IEnumerable<T> GetAll(string fetch)
        {
            using (var ctx = new UserContext())
            {
                return ctx.Set<T>().Include(fetch).ToList();
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var ctx = new UserContext())
            {
                return ctx.Set<T>().ToList();
            }
        }
    }
}