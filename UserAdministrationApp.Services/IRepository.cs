using System.Collections.Generic;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Create(T entity);
        void Update(T entity);

        void Delete(long id);
        
        IEnumerable<T> GetAll(string fetch);
        IEnumerable<T> GetAll();

        T Get(long id, string include = null);
    }
}