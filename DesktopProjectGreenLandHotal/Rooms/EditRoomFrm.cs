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
using ValidationInputs;
using EntityDataLayer;
using WindowsFormsUtilities;

namespace DesktopProjectGreenLandHotal.Rooms
{
    public partial class EditRoomFrm : Form
    {
        IRoomReposity roomReposity;
        ICategoriesRepository categoriesRepository;
        Room room;
        public EditRoomFrm()
        {
            InitializeComponent();
            var context = new GreenLandProjectEntities();
            roomReposity = new RoomRepository(context);
            categoriesRepository = new CategoryRepository(context);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SelectElement();
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SelectElement();
            }
        }

        private void SelectElement()
        {
            int _id = Convert.ToInt32(numericUpDown1.Value);
            room =  roomReposity.GetByCondition(a => a.IsDeleted == false && a.Number == _id).FirstOrDefault();
            if(room != null)
            {
                txtRoomNumber.Value = room.Number;
                txtNumberOfBeds.Value = room.NumberOfBeds;
                txtPrice.Value = room.Price;
                txtPeopleCapacity.Value = room.NumberOfPeople;
                comboCategory.SelectedValue = room.CategoryID;
                ImageConverter imageConverter = new ImageConverter();
                pictureBox1.Image = (Image)imageConverter.ConvertFrom(room.Image);
                WindowsFormsUtilities.Helpers.EnableAllControls(this.groupBox1, this.btnEdit);
                checkIsAvaiable.Checked = room.IsAvailable ;
               checkIsEmpty.Checked = room.IsEmpty.Value ;
            }
            else
            {
                MessageBox.Show("Room doesn't exist ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(room != null)
            {
                room.Number = Convert.ToInt32(txtRoomNumber.Value);
                room.Image = WindowsImageConverter.ConvertImage(pictureBox1.Image);
                room.NumberOfBeds = Convert.ToInt32(txtNumberOfBeds.Value);
                room.Price = Convert.ToInt32(txtPrice.Value);
                room.CategoryID = Convert.ToInt32(comboCategory.SelectedValue.ToString());
                room.NumberOfPeople = Convert.ToInt32(txtPeopleCapacity.Value);
                room.IsAvailable = checkIsAvaiable.Checked;
                room.IsEmpty = checkIsEmpty.Checked;
                roomReposity.Edit(room);
               if(roomReposity.SaveChanges() > 0)
                {
                    MessageBox.Show("Successful Editing","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("You Didn't Select Any Room","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void EditRoomFrm_Load(object sender, EventArgs e)
        {
            comboCategory.DataSource = categoriesRepository.GetByCondition(a => a.IsDeleted == false).ToList();
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "ID";
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Image(*.PNG;*.JPG)|*.PNG;*.JPG";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                    txtpath.Text = fileDialog.FileName;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
