using GreenLandHotal.ViewModels;
using Microsoft.AspNet.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReservationRepository _reservation;
        private readonly ICategoriesRepository categoriesRepository;

        public HomeController(IReservationRepository reservation , ICategoriesRepository categoriesRepository) 
        {
            this._reservation = reservation;
            this.categoriesRepository = categoriesRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Category = new SelectList(this.categoriesRepository.GetAll(), "ID", "CategoryName");
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpHelpers.RegisterReservationCookies(HttpContext, User.Identity.GetUserId(), _reservation);
            }
           
            base.EndExecute(asyncResult);


        }
    }
}