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
    public partial class CategoriesFrm : Form
    {
        ICategoriesRepository categoriesRepository;
        public CategoriesFrm()
        {
            InitializeComponent();
            categoriesRepository = new CategoryRepository(new GreenLandProjectEntities());
        }

        private void CategoriesFrm_Load(object sender, EventArgs e)
        {
            CategoryDataGridview.DataSource = this.categoriesRepository.GetByCondition(a=>a.IsDeleted == false).Select(a =>
             new
             {
                 a.CategoryName,

             }).ToList();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
