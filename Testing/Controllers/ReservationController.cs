using EntityDataLayer;
using GreenLandHotal.ViewModels;
using Microsoft.AspNet.Identity;
using Repositories;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using GreenLandHotal.Hubs;

namespace GreenLandHotal.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservation;
        private readonly IRoomReposity roomReposity;

        public ReservationController(IReservationRepository reservation, IRoomReposity roomReposity)
        {
            this._reservation = reservation;
            this.roomReposity = roomReposity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModels reservationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"The Information is Not Vaild");
            }
          if(!CheckRoom(reservationViewModel, User.Identity.GetUserId()))
            {
              
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Room is Already Reserved please see Reservations");
            }


                Reservation reservation = new Reservation()
                {
                    UserID = User.Identity.GetUserId(),
                    ReservationDate = reservationViewModel.CheckIn,
                    DepatureDate = reservationViewModel.CheckOut,
                    Checkout = false,
                    IsApproved = false,
                    IsDeleted = false,
                    LeavingDate = null,
                    ComingDate = null,
                    RoomID = reservationViewModel.RoomID,

                };
                _reservation.Add(reservation);
            _reservation.CloseTheRoomForReservation(reservationViewModel.RoomID);
            if (_reservation.SaveChanges() > 0)
            {

                //send Data to Desktop Application
                var Ihub = GlobalHost.ConnectionManager.GetHubContext<ReservationHub>();
                Ihub.Clients.All.SendMessage(reservation.RoomID, reservation.UserID,reservation.ReservationDate,reservation.DepatureDate);

                this.roomReposity.SaveChanges();
                return Json(new { result = true });
            }
            else
            {
                
                return Json(new { result = false });
            }
            
        }

        private void MakeRoomIsNotAvaliable(Reservation reservation)
        {
            Room room = this.roomReposity.GetByID(reservation.RoomID);
            room.IsAvailable = false;
            this.roomReposity.Edit(room);
        }

        private bool CheckRoom(ReservationViewModels  reservationViewModels, string userId)
        {
           var result = _reservation.GetByCondition(a => DateTime.Compare(reservationViewModels.CheckIn, a.ReservationDate) == 0 &&
              a.UserID == userId && a.RoomID == reservationViewModels.RoomID).FirstOrDefault();
            return result == null ? true : false;
        }
      

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpHelpers.RegisterReservationCookies(HttpContext, User.Identity.GetUserId(), this._reservation);
            }
            base.EndExecute(asyncResult);
        }


        protected override void Execute(RequestContext requestContext)
        {
         
            base.Execute(requestContext);
        }


    }
}