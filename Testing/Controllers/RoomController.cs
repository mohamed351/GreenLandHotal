using GreenLandHotal.ViewModels;
using Microsoft.AspNet.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GreenLandHotal.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomReposity reposity;
        private readonly IReservationRepository _reservation;

        public RoomController(IRoomReposity reposity, IReservationRepository reservation)
        {
            this.reposity = reposity;
            this._reservation = reservation;
        }
        
        public ActionResult Index()
        {
           
            return View(reposity.GetByConditionIQueryable(a => a.IsDeleted == false).ToList());
        }
        [Authorize]
        public ActionResult Details(int? ID)
        {
            if(ID == null)
                throw new HttpException(400, "Bad Request");

            var room = reposity.GetByIDNoTracking(a => a.ID == ID.Value);
            if (room == null)
                throw new HttpException(404, "The Room is not Found");

            return View(room);
        }
        [HttpGet]
        public ActionResult GetAvaliableRooms(int ?categoryId, int? guests)
        {
            return PartialView("Index", reposity.GetByConditionIQueryable(a => a.IsDeleted == false
            && a.IsAvailable && a.CategoryID == categoryId && a.NumberOfPeople == guests).ToList());
        }

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpHelpers.RegisterReservationCookies(HttpContext, User.Identity.GetUserId(), this._reservation);
            }
            base.EndExecute(asyncResult);
            

        }
     
    }
}