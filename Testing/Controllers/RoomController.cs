using EntityDataLayer;
using GreenLandHotal.ViewModels;
using Microsoft.AspNet.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Testing.ViewModels;

namespace GreenLandHotal.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomReposity reposity;
        private readonly IReservationRepository _reservation;
        private readonly ICategoriesRepository categoriesRepository;

        public RoomController(IRoomReposity reposity, IReservationRepository reservation, ICategoriesRepository categoriesRepository)
        {
            this.reposity = reposity;
            this._reservation = reservation;
            this.categoriesRepository = categoriesRepository;
        }

        public ActionResult Index()
        {

            return View(reposity.GetByConditionIQueryable(a => a.IsDeleted == false).ToList());
        }
        [Authorize]
        public ActionResult Details(int? ID)
        {
            if (ID == null)
                throw new HttpException(400, "Bad Request");

            var room = reposity.GetByIDNoTracking(a => a.ID == ID.Value);
            if (room == null)
                throw new HttpException(404, "The Room is not Found");

            return View(room);
        }
        [HttpGet]
        public ActionResult GetAvaliableRooms(int? categoryId, int? guests)
        {
            CheckAvailableViewModel viewModel = GetRooms(categoryId, guests);
            return PartialView("_CategoryItem", viewModel);
        }

      

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpHelpers.RegisterReservationCookies(HttpContext, User.Identity.GetUserId(), this._reservation);
            }
            base.EndExecute(asyncResult);
            

        }
        private CheckAvailableViewModel GetRooms(int? categoryId, int? guests)
        {
            IEnumerable<Room> Info = new List<Room>();
            if (guests == 0 || guests == null)
            {
                Info = reposity.GetByConditionIQueryable(a => a.IsDeleted == false
                  && a.IsAvailable && a.CategoryID == categoryId).ToList();
            }
            else
            {
                Info = reposity.GetByConditionIQueryable(a => a.IsDeleted == false
                && a.IsAvailable && a.CategoryID == categoryId && a.NumberOfPeople == guests).ToList();
            }
            var category = categoriesRepository.GetByID(categoryId.Value);

            CheckAvailableViewModel viewModel = new CheckAvailableViewModel()
            {
                Image = category.Image,
                CateogyName = category.CategoryName,
                Rooms = Info

            };
            return viewModel;
        }

    }
}