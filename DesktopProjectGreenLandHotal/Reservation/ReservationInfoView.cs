using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopProjectGreenLandHotal.Reservation
{
    public class ReservationInfoView
    {
        public string UserID { get; set; }
        public int RoomID { get; set; }
        public DateTime ReservationDate { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime DepatureDate { get; set; }
        public int Number { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAvailable { get; set; }
        public int NumberOfBeds { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ComingDate { get; set; }


    }
}
