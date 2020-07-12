using EntityDataLayer;
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
using ValidationInputs;
using WindowsFormsUtilities;

namespace DesktopProjectGreenLandHotal.Categories
{
    public partial class EditCategory : Form
    {
        ICategoriesRepository categoriesRepository;
        private int? id=null;
        public EditCategory()
        {
            InitializeComponent();
          categoriesRepository = new CategoryRepository(new GreenLandProjectEntities());

        }

        private  void EditCategory_Load(object sender, EventArgs e)
        {
            comboCategory.DataSource =  categoriesRepository.GetByCondition(a=>a.IsDeleted == false).ToList();
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "ID";
            comboCategory.Text = "";
        }

        private void comboCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectCategory();
        }

        private void SelectCategory()
        {
            if (comboCategory.SelectedValue != null)
            {
                Category category = (Category)comboCategory.SelectedItem;
                txtName.Text = category.CategoryName;
                ImageConverter converter = new ImageConverter();
                pictureBox1.Image = (Image)converter.ConvertFrom(category.Image);
                id = category.ID;
                Helpers.EnableAllControls(this.groupBox1, this.btnEdit);


            }
            else
            {
                MessageBox.Show("The Category is not found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void comboCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectCategory();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {

            if (comboCategory.SelectedValue != null)
            {
                var categories = (Category)comboCategory.SelectedItem;

                categories.CategoryName = txtName.Text;
                categories.Image = WindowsFormsUtilities.WindowsImageConverter.ConvertImage(pictureBox1.Image);
                

                var validation = WindowsFormValidationContext.Validated<Category>(categories);
                if (validation.Item1)
                {
                    categoriesRepository.Edit(categories);
                    var result = await categoriesRepository.SaveChangesAsync();
                    if (result > 0)
                    {
                        MessageBox.Show("The Category has been Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    foreach (var item in validation.Item2)
                    {
                        MessageBox.Show(item.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
           
        }
    }
}
