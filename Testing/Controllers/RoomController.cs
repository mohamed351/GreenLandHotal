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

        public RoomController(IRoomReposity reposity)
        {
            this.reposity = reposity;
        }
        
        public ActionResult Index()
        {
           
            return View(reposity.GetByCondition(a => a.IsDeleted == false).ToList());
        }
        public ActionResult Details(int? ID)
        {
            if(ID == null)
                throw new HttpException(400, "Bad Request");

            var room = reposity.GetByID(ID.Value);
            if (room == null)
                throw new HttpException(404, "The Room is not Found");

            return View(room);
        }
    }
}