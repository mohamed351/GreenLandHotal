using EntityDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.ViewModels
{
    public class CheckAvailableViewModel
    {

        public byte[] Image { get; set; }

        public string CateogyName { get; set; }

        public IEnumerable<Room>  Rooms  { get; set; }

    }
}