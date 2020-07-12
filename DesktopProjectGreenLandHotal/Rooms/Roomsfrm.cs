using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityDataLayer;

namespace DesktopProjectGreenLandHotal.Rooms
{
    public partial class Roomsfrm : Form
    {
        IRoomReposity roomReposity;
        public Roomsfrm()
        {
            InitializeComponent();
            roomReposity = new RoomRepository(new GreenLandProjectEntities());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Roomsfrm_Load(object sender, EventArgs e)
        {
         RoomsGridView.DataSource =  roomReposity.GetAll().Select(a => new
            {

                a.Number,
                a.Price,
                a.NumberOfBeds,
                a.NumberOfPeople
                
            }).ToList();
        }
    }
}
