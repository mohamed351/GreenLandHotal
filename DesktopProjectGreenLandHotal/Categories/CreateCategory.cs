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
using EntityDataLayer;

namespace DesktopProjectGreenLandHotal.Categories
{
    public partial class CreateCategory : Form
    {
        ICategoriesRepository categoriesRepository;
        public CreateCategory()
        {
            InitializeComponent();
            categoriesRepository = new CategoryRepository(new GreenLandProjectEntities());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Image Files (*.png;*.jpg)|*.PNG;*.JPG";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(fileDialog.FileName);

                }
            }

        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            var categories = new Category()
            {
                CategoryName = txtName.Text,
                Image = WindowsFormsUtilities.WindowsImageConverter.ConvertImage(pictureBox1.Image)
            };

           var validation= WindowsFormValidationContext.Validated<Category>(categories);
            if (validation.Item1)
            {
                categoriesRepository.Add(categories);
               var result =await categoriesRepository.SaveChangesAsync();
                if(result > 0)
                {
                    MessageBox.Show("The Category has been Added","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                foreach (var item in validation.Item2)
                {
                    MessageBox.Show(item.ErrorMessage,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }



        }
    }
}

