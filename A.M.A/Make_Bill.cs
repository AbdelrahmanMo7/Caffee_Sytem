using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.A.M.A
{
  
    public partial class Make_Bill : Form
    {
        class bill_list 
        {
            public Product Item { get; set; }
            public  int Count { get; set; }
            public double Total { get; set; }
            public bill_list(Product Item , int count, double total)
            {
                this.Item = Item;
                this.Count = count;
                this.Total = total;
            }
            public override bool Equals(object obj)
            {
                if (obj is Product)
                {
                    Product p = obj as Product;
                    return this.Item.P_ID == p.P_ID && this.Item.P_Name == p.P_Name;
                }
                return false;
            }
           
        }
        User sys_user;
        Coffee_SystemEntities context ;
        Product selected_Item;
        double selected_Item_total, selected_Item_price;
        Client selected_Client;
        List<bill_list> bill_Item_list;
        


        public Make_Bill(  )
        {
           // this.sys_user = user;
            InitializeComponent();
            Bill_timer1.Start();

            bill_Item_list = new List<bill_list>();
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
        private void ItemAmoun_numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            selected_Item_price = selected_Item.P_Price;
            if (ApplyOffer_checkBox1.Enabled == true&&ApplyOffer_checkBox1.Checked == true)
            {
                selected_Item_price -= selected_Item_price * selected_Item.Offer.Off_Limit / 100;
            }
            selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
            Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
           
        }


        private void Show_Products_dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                selected_Item = context.Products.Select(p => p).ToList()[e.RowIndex];
                Veiw_Item_Txt.Text = selected_Item.P_Name;
                Veiw_ItemPrice_Txt.Text = selected_Item.P_Price.ToString();
                selected_Item_price = selected_Item.P_Price;
                if (ApplyOffer_checkBox1.Enabled == true && ApplyOffer_checkBox1.Checked == true)
                {
                    selected_Item_price -= selected_Item_price * selected_Item.Offer.Off_Limit / 100;
                }
                if (selected_Item.P_Cat_Id != null)
                {
                    Veiw_ItemCategory_Txt.Text = selected_Item.Category.Cat_Name;
                }
                if (selected_Item.P_Of_Id != null)
                {
                    Veiw_ItemOffer_Txt.Text = selected_Item.Offer.Off_Name;
                    ApplyOffer_checkBox1.Enabled = true;
                    ApplyOffer_checkBox1.Visible = true;
                    ApplyOffer_checkBox1.Checked = false;
                }
                selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
                Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
            }
           
        }

       
      
        private void Show_Products_dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Veiw_Item_Txt.Text = null;
            Veiw_ItemPrice_Txt.Text = "";
            Veiw_ItemCategory_Txt.Text = "";
            Veiw_ItemOffer_Txt.Text = "";
            Veiw_Item_TotalPrice_Txt.Text = "";
            ApplyOffer_checkBox1.Enabled = false;
            ApplyOffer_checkBox1.Visible = false;

        }



        private void Add_Item_ToBill_Btn_Click(object sender, EventArgs e)
        {
            bill_list Item = bill_Item_list.Find(i=> i.Equals( selected_Item)) ;
              //var Item = bill_Item_list.Where(i=> i.Item.P_ID==selected_Item.P_ID).FirstOrDefault() ;
            if (Item == null||bill_Item_list.Count==0)
            {
                bill_Item_list.Add(new bill_list(selected_Item, ((int)ItemAmoun_numericUpDown1.Value),selected_Item_total ));
                var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                Show_Bills_Items_dataGridView1.DataSource = show_list;
                Veiw_Item_Txt.Text = null;
                Veiw_ItemPrice_Txt.Text = "";
                Veiw_ItemCategory_Txt.Text = "";
                Veiw_ItemOffer_Txt.Text = "";
                Veiw_Item_TotalPrice_Txt.Text = "";
                ApplyOffer_checkBox1.Checked = false;
              //  Add_Item_ToBill_Btn.Enabled = false;

            }
            else
            {
                MessageBox.Show($" Item {selected_Item.P_Name} is already existed in the Bill ");
            }
           

            
        }

       

        private void Bill_timer1_Tick(object sender, EventArgs e)
        {
           // Bill_Time_label.Text = DateTime.Now.ToString("HH:mm:ss tt");
            Bill_Date_label13.Text = DateTime.Now.ToString();
        }

        private void Veiw_Item_Txt_TextChanged(object sender, EventArgs e)
        {
           
            if (Veiw_Item_Txt.Text != null &&Veiw_Item_Txt.Text != "")
            {
                Add_Item_ToBill_Btn.Enabled = true;
            }
            else
            {
                Add_Item_ToBill_Btn.Enabled = false;
            }
        }

        private void ClientPhone_Txt_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(ClientPhone_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ClientPhone_Txt.Text = ClientPhone_Txt.Text.Remove(ClientPhone_Txt.Text.Length - 1);
            }
        }

        private void ClientName_Txt_TextChanged(object sender, EventArgs e)
        {
            if (ClientName_Txt.Text!= null&& ClientName_Txt.Text !="  "&& ClientName_Txt.Text !=" ")
            {
                Add_Client_Btn.Enabled = true;
            }
            else
            {
                Add_Client_Btn.Enabled = false;
            }
        }

        private void ApplyOffer_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            selected_Item_price -= selected_Item_price * selected_Item.Offer.Off_Limit / 100;
            selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
            Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
        }

        private void Add_Client_Btn_Click(object sender, EventArgs e)
        {
            foreach (var i in ClientName_Txt.Text)
            {
                if (i != ' ')
                {
                    if (ClientPhone_Txt.Text.Length < 5)
                    {
                        MessageBox.Show("Please enter Valiad numbers more than 5 numbers .");
                    }
                    else
                    {
                        selected_Client = new Client { C_Name = ClientName_Txt.Text, C_Address = ClientAddress_Txt.Text, C_Phone_Number = (long)int.Parse(ClientPhone_Txt.Text.ToString())   };
                        var Item = context.Clients.Where(c => c.C_Phone_Number==selected_Client.C_Phone_Number).FirstOrDefault();

                        if (Item == null)
                        {
                            context.Clients.Add(selected_Client);
                            context.SaveChanges();
                            selected_Client.C_ID=  context.Clients.Where(c => c.C_Phone_Number == selected_Client.C_Phone_Number).FirstOrDefault().C_ID;
                            ClientName_label15.Text = selected_Client.C_Name;
                            ClientAddress_label13.Text = selected_Client.C_Address;
                            ClientPhone_label14.Text = selected_Client.C_Phone_Number.ToString();
                            ClientName_label15.Text = selected_Client.C_Name;
                            MessageBox.Show($"Client {selected_Client.C_Name} is Add Successfully .");
                           
                        }
                        else
                        {
                            selected_Client = null;
                            MessageBox.Show($"This numberis already wxisted for client {Item.C_Name}. Please, enter another Number");
                        }
                       
                    }
                    break;
                }
            }
        }
    }
}

