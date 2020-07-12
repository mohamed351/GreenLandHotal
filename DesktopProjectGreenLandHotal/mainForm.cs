using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdentityLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using DesktopProjectGreenLandHotal.Users;
using DesktopProjectGreenLandHotal.Rooms;
using DesktopProjectGreenLandHotal.Categories;

namespace DesktopProjectGreenLandHotal
{
    public partial class mainForm : Form
    {
        Form activeForm = null;
        public mainForm()
        {
            InitializeComponent();
            HideAll();
        }
        private void HideAll()
        {
            this.UserPanel.Visible = false;
            this.roomPanel.Visible = false;
            this.CategoryPanel.Visible = false;
            this.panelReservaion.Visible = false;
        }
        private void ShowThePanel(Panel panel)
        {
            panel.Visible =! panel.Visible;
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            HideAll();
            ShowThePanel(this.UserPanel);
        }

        private void openChildForm(Form frm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = frm;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(frm);
            mainPanel.Tag = frm;
            frm.BringToFront();
            frm.Show();
        }

        private void btnShowUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
            HideAll();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            openChildForm(new CreateUserFrm());
            HideAll();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            openChildForm(new EditUserFrm());
            HideAll();

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            openChildForm(new DeleteUserFrm());
            HideAll();
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            HideAll();
            ShowThePanel(this.roomPanel);
        }

        private void Rooms_Click(object sender, EventArgs e)
        {
            openChildForm(new Roomsfrm());
            HideAll();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            HideAll();
            ShowThePanel(this.CategoryPanel);
          
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoriesFrm());
            HideAll();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildForm(new CreateCategory());
            HideAll();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new EditCategory());
            HideAll();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new DeleteCategory());
            HideAll();
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            HideAll();
            ShowThePanel(this.panelReservaion);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new CreateRoomFrm());
            HideAll();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new EditRoomFrm());
            HideAll();
        }
    }
}
