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
    public partial class CreateUserFrm : Form
    {
        RoleManager<IdentityRole> role;
        UserManager<ApplicationUser> user;
        public CreateUserFrm()
        {
            InitializeComponent();
            role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            user = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        private  void CreateUserFrm_Load(object sender, EventArgs e)
        {

            comborole.DataSource =  role.Roles.ToList();
            comborole.ValueMember = "Name";
            comborole.DisplayMember = "Name";
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            var newUser = new ApplicationUser()
            {
                Email = txtEmail.Text,
                Name = txtName.Text,
                PhoneNumber = txtPhone.Text,
                UserName = txtUserName.Text,

            };
            var result = await user.CreateAsync(newUser, txtpassword.Text);
            if (result.Succeeded)
            {

             var  roleResult =   user.AddToRoles(newUser.Id, comborole.SelectedValue.ToString());
                if (roleResult.Succeeded)
                {
                    MessageBox.Show("Successful adding User","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    foreach (var item in roleResult.Errors)
                    {
                        MessageBox.Show(item, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    MessageBox.Show(item, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
