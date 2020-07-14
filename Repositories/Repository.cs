using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<T, ID> : IRepository<T, ID> where T : class
    {
        private readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().AsNoTracking().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public Task<IEnumerable<T>> GetByConditionAsync(Func<T, bool> func)
        {
            return Task.FromResult<IEnumerable<T>>(dbContext.Set<T>().AsNoTracking().Where(func).ToList());
        }

        public IEnumerable<T> GetByCondition(Func<T, bool> func)
        {
            return dbContext.Set<T>().Where(func).ToList();
        }

        public T GetByID(ID id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public Task<T> GetByIDAsync(ID id)
        {
            return dbContext.Set<T>().FindAsync(id);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetByConditionIQueryable(Func<T, bool> func)
        {
            
            return dbContext.Set<T>().AsNoTracking().Where(func).AsQueryable();
        }

        public T GetByIDNoTracking(Func<T, bool> IDCondtion)
        {
            return dbContext.Set<T>().AsNoTracking().FirstOrDefault(IDCondtion);
        }

        public IEnumerable<T> GetAllWithTracking()
        {
            return dbContext.Set<T>().AsNoTracking().ToList();
        }
        public IQueryable<T> GetIQueryable()
        {
            return dbContext.Set<T>().AsNoTracking();
        }

        public void EditAttach(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Detached;
         

        }
    }
}
