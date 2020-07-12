using IdentityLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopProjectGreenLandHotal.Users
{
    public partial class UserForm : Form
    {
        UserManager<ApplicationUser> userManager;
        public UserForm()
        {
            InitializeComponent();
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            UserGridView.DataSource = userManager.Users.Select(a =>
            new {
                a.UserName,
                a.Email,
                a.PhoneNumber,
                a.Name                
            }).ToList();
        }
    }
}
