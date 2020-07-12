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
      private readonly  DbContext context;
        public RoomRepository(DbContext dbContext) 
            : base(dbContext)
        {
            this.context = dbContext;
        }

        public bool CheckThereIsARoom(int Number)
        {
            var result = this.context.Set<Room>().FirstOrDefault(a => a.Number == Number);
           return result == null ?true:false;
        }
    }
}
