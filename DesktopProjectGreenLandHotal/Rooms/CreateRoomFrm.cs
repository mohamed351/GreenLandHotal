using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repositories;
using EntityDataLayer;
using WindowsFormsUtilities;
using ValidationInputs;

namespace DesktopProjectGreenLandHotal.Rooms
{
    public partial class CreateRoomFrm : Form
    {
        IRoomReposity roomReposity;
        ICategoriesRepository categories;
        public CreateRoomFrm()
        {
            InitializeComponent();
            var dbContext = new GreenLandProjectEntities();
            roomReposity = new RoomRepository(dbContext);
            categories = new CategoryRepository(dbContext);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
               var result = roomReposity.CheckThereIsARoom(Convert.ToInt32(txtRoomNumber.Value));
                switch (result)
                {
                    case true:
                        CreateRoom();
                        break;
                    case false:
                    MessageBox.Show("This Room Number is Already Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                    
                }
           
         
        }

        private void CreateRoom()
        {
            var room = new Room()
            {
                CategoryID = Convert.ToInt32(comboCategory.SelectedValue),
                Image = WindowsImageConverter.ConvertImage(pictureBox1.Image),
                IsDeleted = false,
                Price = txtPrice.Value,
                Number = Convert.ToInt32(txtRoomNumber.Value),
                IsEmpty = checkIsAvaiable.Checked,
                IsAvailable = checkIsEmpty.Checked,
                NumberOfBeds = Convert.ToInt32(txtNumberOfBeds.Value),
                NumberOfPeople = Convert.ToInt32(txtPeopleCapacity.Value),



            };
            var result = WindowsFormValidationContext.Validated<Room>(room);
            if (result.Item1)
            {
                roomReposity.Add(room);
                if(roomReposity.SaveChanges() > 0)
                {
                    MessageBox.Show("The Room has been saved","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                foreach (var item in result.Item2)
                {
                    MessageBox.Show(item.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "(Image *.png;*.jpg)|*.PNG;*.JPG";
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                    txtpath.Text = fileDialog.FileName;
                }
            }
        }

        private void CreateRoomFrm_Load(object sender, EventArgs e)
        {
            comboCategory.DataSource = categories.GetByCondition(a => a.IsDeleted == false).ToList();
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "ID";
        }
    }
}
