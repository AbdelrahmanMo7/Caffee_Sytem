using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.ASH
{
    public partial class ProductsPage : Templete
    {

        private Coffee_SystemEntities Context = new Coffee_SystemEntities();
        //private Coffee_Context Context = new Coffee_Context();
        public ProductsPage()
        {
            InitializeComponent();
            LoadData();
            //LoadFilterCombo();
        }

        public void LoadData()
        {
            var query = (from e in Context.Products
                         select new
                         {
                             Id = e.P_ID,
                             Name = e.P_Name,
                             Price = e.P_Price,
                             Quantity = e.P_Quantity,
                             Code = e.P_Code,
                             Category = e.Category.Cat_Name,
                             Offer = e.Offer.Off_Name
                         }).ToList();


            // Set the DataSource to the list of products
            dataGridView1.DataSource = query;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            //AddProductPage add = new AddProductPage(this);
            //add.ShowDialog();
        }

    }
}
