using EntityDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public interface IRoomReposity :IRepository<Room,int>
    {
         bool CheckThereIsARoom(int Number);
    }
}
