using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public interface IRepository<T, ID> where T:class
    {
        void Add(T entity);
        void Edit(T entity);
        void EditAttach(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithTracking();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByConditionAsync(Func<T,bool> func);
        IEnumerable<T> GetByCondition(Func<T, bool> func);
        IQueryable<T> GetByConditionIQueryable(Func<T, bool> func);
        Task<T> GetByIDAsync(ID id);
        T GetByID(ID id);
        T GetByIDNoTracking(Func<T, bool> IDCondtion);
        void Delete(T entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IQueryable<T> GetIQueryable();


    }
}
