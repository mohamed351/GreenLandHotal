using EntityDataLayer;
using GreenLandHotal.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenLandHotal.Controllers
{
    public class ReservationController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModels reservationViewModel)
        {
            Reservation reservation = new Reservation()
            {
                UserID = User.Identity.GetUserId(),
                ReservationDate = reservationViewModel.CheckIn,
                DepatureDate = reservationViewModel.CheckOut,
                Checkout = false,
                IsApproved = false,
                IsDeleted= false,
                LeavingDate = null,
                ComingDate = null,
                RoomID = reservationViewModel.RoomID,
               
            };

            return Json(new { result = true });
        }
    }
}