using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.A.M.A
{
  
    public partial class Make_Bill : Form
    {
        User sys_user;
        Coffee_SystemEntities context ;
        Product selected_Item;
       

        public Make_Bill( User user )
        {
            this.sys_user = user;
            InitializeComponent();
            context = new Coffee_SystemEntities();
            All_Cat_comboBox1.DataSource = context.Categories.Select(c => c.Cat_Name).ToList();

            var p1 = context.Products.Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            Show_Products_dataGridView1.DataSource = p1;
        }


        private void Search_Btn_Click(object sender, EventArgs e)
        {
            string selected_Item = Item_search_Txt.Text.ToString();
            Show_Products_dataGridView1.DataSource = context.Products.Where(p=> p.P_Name==selected_Item).Select(p =>new { Name = p.P_Name, Category = p.Category.Cat_Name , Price= p.P_Price, Offer= p.Offer.Off_Name  }).ToList();
        }

        private void All_Cat_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_Cat = All_Cat_comboBox1.SelectedItem.ToString();
            Show_Products_dataGridView1.DataSource = null;
            Show_Products_dataGridView1.DataSource = context.Products.Where(p => p.Category.Cat_Name == selected_Cat).Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
        }

        private void Show_Products_dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selected_Item = context.Products.Select(p => p).ToList()[e.RowIndex];
            Veiw_Item_Txt.Text = selected_Item.P_Name;
            Veiw_ItemPrice_Txt.Text = selected_Item.P_Price.ToString();
            Veiw_ItemCategory_Txt.Text = selected_Item.Category.Cat_Name;
            Veiw_ItemOffer_Txt.Text = selected_Item.Offer.Off_Name;
            Veiw_Item_TotalPrice_Txt.Text = ((selected_Item.P_Price) * ((int)ItemAmoun_numericUpDown1.Value)).ToString(); 
        }

        private void ItemAmoun_numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Veiw_Item_TotalPrice_Txt.Text = ((selected_Item.P_Price) * ((int)ItemAmoun_numericUpDown1.Value)).ToString();
        }

        private void Show_Products_dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_Item = context.Products.Select(p => p).ToList()[e.RowIndex];
            Veiw_Item_Txt.Text = selected_Item.P_Name;
            Veiw_ItemPrice_Txt.Text = selected_Item.P_Price.ToString();
            Veiw_ItemCategory_Txt.Text = selected_Item.Category.Cat_Name;
            Veiw_ItemOffer_Txt.Text = selected_Item.Offer.Off_Name;
        }

        private void Show_Bills_dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
