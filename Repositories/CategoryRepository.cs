using EntityDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoriesRepository
    {
        public CategoryRepository(DbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
