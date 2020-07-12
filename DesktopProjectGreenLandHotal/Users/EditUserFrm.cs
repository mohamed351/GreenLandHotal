using IdentityLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsUtilities;

namespace DesktopProjectGreenLandHotal.Users
{
    public partial class EditUserFrm : Form
    {
        private string _id;
        private string Id { get => _id; set{
                _id= value;
                Helpers.EnableAllControls(this.groupBox1, this.btnEdit);

            } }
       
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        ApplicationDbContext dbContext;
        public EditUserFrm()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
          
        }

        private void EditUserFrm_Load(object sender, EventArgs e)
        {
            this.comboUsers.DataSource = userManager.Users.ToList();
            this.comboUsers.ValueMember = "Id";
            this.comboUsers.DisplayMember = "Name";

            this.comborole.DataSource = roleManager.Roles.ToList();
            this.comborole.ValueMember = "Name";
            this.comborole.DisplayMember = "Name";
            this.comborole.Text = "";

        }


        private void comboUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SelectUser();

            }
        }

        private void SelectUser()
        {
            if (comboUsers.SelectedValue != null)
            {
                var applicationUser = (ApplicationUser)this.comboUsers.SelectedItem;
                txtEmail.Text = applicationUser.Email;
                txtName.Text = applicationUser.Name;
                txtPhone.Text = applicationUser.PhoneNumber;
                txtUserName.Text = applicationUser.UserName;
                Id = applicationUser.Id;
                comborole.SelectedValue =  userManager.GetRoles(Id).FirstOrDefault()?? "";


            }
            else
            {
                MessageBox.Show("The User Doesn't Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Helpers.DisableAllControls(this.groupBox1, this.btnEdit);
            }
        }
     


        private void comboUsers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectUser();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {

            ApplicationUser edituser = userManager.Users.FirstOrDefault(a => a.Id == _id);
         
            edituser.Name = txtName.Text;
            edituser.Email = txtEmail.Text;
            edituser.UserName = txtUserName.Text;
            edituser.PhoneNumber = txtPhone.Text;
            edituser.UserName = txtUserName.Text;
            dbContext.Entry(edituser).State = System.Data.Entity.EntityState.Modified;
           if (dbContext.SaveChanges() > 0)
            {
               IList<string>  roles = userManager.GetRoles(edituser.Id);
               var result = await userManager.RemoveFromRolesAsync(edituser.Id, roles.ToArray());
                userManager.AddToRole(edituser.Id, comborole.SelectedValue.ToString());
                this.Close();
            }


        }
    }
}
