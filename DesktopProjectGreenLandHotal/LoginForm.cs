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

namespace DesktopProjectGreenLandHotal
{
    public partial class LoginForm : Form
    {
        UserManager<ApplicationUser> userManager;
        public LoginForm()
        {
            InitializeComponent();
            new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ApplicationUser user = userManager.FindByName(txtUserName.Text);
            if(user != null)
            {
               if(userManager.CheckPassword(user, txtPassword.Text))
                {
                    if(userManager.IsInRole(user.Id, "Admin"))
                    {
                        MessageBox.Show("Test");
                    }
                    else
                    {
                        MessageBox.Show("Your Not Admin please contact your administration","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
               else
                {
                    MessageBox.Show("The Password is Wrong","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
           else
            {
                MessageBox.Show("The UserName is Wrong ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
