using EntityDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomRepository : Repository<Room, int>, IRoomReposity
    {
        public RoomRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
