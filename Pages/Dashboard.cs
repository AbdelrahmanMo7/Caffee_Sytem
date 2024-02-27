using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using Cafffe_Sytem.Pages.SacondryPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages
{
    public partial class Dashboard : Templete
    {
        public Dashboard()
        {
            InitializeComponent();

            //card display
            usersNumberlbl.Text = DBConnection.Context.Users.Count().ToString();
            clintnumberlbl.Text = DBConnection.Context.Clients.Count().ToString();
            empnumberlbl.Text = DBConnection.Context.Employees.Count().ToString();
            billsnumberlbl.Text = DBConnection.Context.Bills.Count().ToString();
            List<Product> products = DBConnection.Context.Products.Select(p=>p).ToList();
            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.P_Name, item.P_Quantity, item.P_Price, item.P_Code);
            }


            //circle load
            var bills = DBConnection.Context.Bills.Where(p => p.B_IsDeleted_ == false).Select(p => p.Bill_has_Products);
            List<ReportItem2> reports = new List<ReportItem2>();
            foreach (var item in bills)
            {
                reports.AddRange(item.Select(p => new ReportItem2 { productItem = p.Product ,count = p.Product_Count}).ToList());

            }
            var categoryList = reports.GroupBy(p => p.productItem.Category.Cat_Name).Select(p => new { category = p.Key, productCount = p.Sum(i => i.count) }).ToList();
            int totalCount = categoryList.Sum(c=>c.productCount);
            for (int i=0;i<categoryList.Count;i++)
            {
                double x = ((double)categoryList[i].productCount / totalCount)*100;
                chart1.Series["s1"].Points.AddXY(categoryList[i].category, x.ToString());
            }
        }

        
    }
}
