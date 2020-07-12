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
    public partial class DeleteUserFrm : Form
    {
        UserManager<ApplicationUser> userManager;
        public DeleteUserFrm()
        {
            InitializeComponent();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        private void DeleteUserFrm_Load(object sender, EventArgs e)
        {
            this.comboUsers.DataSource = userManager.Users.ToList();
            this.comboUsers.ValueMember = "Id";
            this.comboUsers.DisplayMember = "Name";
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboUsers.SelectedValue != null) {
                string _id = comboUsers.SelectedValue.ToString();
                ApplicationUser user = userManager.Users.FirstOrDefault(a => a.Id == _id);
              var result =  await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    MessageBox.Show("The User has been Deleted","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        MessageBox.Show(item,"Errors",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
             }
            else
            {
                MessageBox.Show("The user is not found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
