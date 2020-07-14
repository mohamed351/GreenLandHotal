using EntityDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repositories
{
    public interface IReservationRepository :IRepository<Reservation,int>
    {

        IEnumerable<Reservation> GetReservationsCookie(string UserId);
        void CloseTheRoomForReservation(int roomID);

    }

    public class ReservationRepository : Repository<Reservation, int>, IReservationRepository
    {
        private readonly DbContext dbContext;

        public ReservationRepository(DbContext dbContext) 
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Reservation> GetReservationsCookie(string UserId)
        {
            return dbContext.Set<Reservation>().AsNoTracking().Where(a=>a.UserID == UserId).Include(async => async.Room)
                .Include(a => a.Room)
                .ToList();
              
        }
        public void CloseTheRoomForReservation(int roomID)
        {
         var room =  dbContext.Set<Room>().FirstOrDefault(a => a.ID == roomID);
            room.IsAvailable = false;
        }
    }
}
