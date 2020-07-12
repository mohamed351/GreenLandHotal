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
using EntityDataLayer;
namespace DesktopProjectGreenLandHotal.Categories
{
    public partial class DeleteCategory : Form
    {
        ICategoriesRepository categoriesRepository;
        public DeleteCategory()
        {
            InitializeComponent();
            categoriesRepository = new CategoryRepository(new GreenLandProjectEntities());
        }

        private void DeleteCategory_Load(object sender, EventArgs e)
        {
            comboUsers.DataSource = categoriesRepository.GetByCondition(a => a.IsDeleted == false).ToList();
            comboUsers.ValueMember = "ID";
            comboUsers.DisplayMember = "CategoryName";

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboUsers.SelectedValue != null)
            {
                Category category = (Category)comboUsers.SelectedItem;
                category.IsDeleted = true;
                categoriesRepository.Edit(category);
                if (categoriesRepository.SaveChanges() > 0)
                {
                    MessageBox.Show("The Category has been Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("The Category is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
