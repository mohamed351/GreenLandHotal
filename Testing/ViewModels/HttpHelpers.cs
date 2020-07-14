using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenLandHotal.ViewModels
{
    public static class HttpHelpers
    {
        public static void RegisterReservationCookies(HttpContextBase context, string UserId, IReservationRepository reservation)
        {

            var elements = reservation.GetReservationsCookie(UserId).Select(a =>
            new {
               id = a.RoomID,
               price = a.Room.Price,
               ReservationDate = a.ReservationDate,
               DepartureDate = a.DepatureDate,
               category = a.Room.Category.CategoryName,
               roomNumber= a.Room.Number,
               status = ViewSatus(a.IsApproved,a.IsDeleted,a.Checkout)

            });

            string obj = Newtonsoft.Json.JsonConvert.SerializeObject(elements);
            HttpCookie httpCookie = new HttpCookie("UserReservation", obj);
            context.Response.Cookies.Add(httpCookie);

        }
        public static string ViewSatus(bool IsApproved, bool IsDeleted, bool CheckOut)
        {
            string status = "pending";
            if (IsApproved)
            {
                status = "Approved";
                if (IsDeleted)
                {
                    status = "Cancelled";
                }
                else
                {
                    if (CheckOut)
                    {
                        status = "Checkout";
                    }
                }
            }

            return status;

        }
    }
}