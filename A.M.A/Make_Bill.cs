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
        // --- custom bill item class-----
        #region custom bill item class
        class bill_list_item 
        {
            public Product Item { get; set; }
            public  int Count { get; set; }
            public double Total { get; set; }
            public bill_list_item(Product Item , int count, double total)
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
                if (obj is bill_list_item)
                {
                    bill_list_item p = obj as bill_list_item;
                    return this.Item.P_ID == p.Item.P_ID && this.Item.P_Name == p.Item.P_Name;
                }
                return false;
            }
           
        }
        #endregion
        //----------------------

        User sys_user;
        Coffee_SystemEntities context ; //---> Singleton  conext in all classes
        Product selected_Item;
        bill_list_item Bill_selected_Item;
        double selected_Item_total, selected_Item_price, Bill_Total_Amount;
        Client selected_Client;
        List<bill_list_item> bill_Item_list;
        

        // constractor
        public Make_Bill(  )
        {
           // this.sys_user = user;
            InitializeComponent();
            Bill_timer1.Start();
            selected_Client=null;
            Bill_selected_Item = null;
            bill_Item_list = new List<bill_list_item>();
            context = new Coffee_SystemEntities();
            All_Cat_comboBox1.Items.Add("All");
            All_Cat_comboBox1.Items.AddRange(context.Categories.Select(c => c.Cat_Name).ToArray());
           var p1 = context.Products.Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            Show_Products_dataGridView1.DataSource = p1;
            var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
            Show_Bills_Items_dataGridView1.DataSource = show_list;

        }

        //----------------------------
        // ** fillter & search in product
        #region fillter & search in product

        // fillter Product by category
        private void All_Cat_comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            string selected_Cat = All_Cat_comboBox1.SelectedItem.ToString();
            if (selected_Cat == "All")
            {
                Show_Products_dataGridView1.DataSource = context.Products.Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            }
            else
            {
                Show_Products_dataGridView1.DataSource = context.Products.Where(p => p.Category.Cat_Name == selected_Cat).Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            }

        }

        // search in product name
        private void Item_search_Txt_TextChanged_1(object sender, EventArgs e)
        {
            string selected_product = Item_search_Txt.Text.ToString();
            if (selected_product != null || selected_product == " ")
            {
                Show_Products_dataGridView1.DataSource = context.Products.Where(p => p.P_Name.Contains(selected_product)).Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            }
            else
            {
                Show_Products_dataGridView1.DataSource = context.Products.Select(p => new { Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            }

        }

        #endregion
        //------------------------------------------------



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



       

       

      
       
        //------------------------
        // **client addtion oprations
        #region client addtion oprations

        // check cient phone
        private void ClientPhone_Txt_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(ClientPhone_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ClientPhone_Txt.Text = ClientPhone_Txt.Text.Remove(ClientPhone_Txt.Text.Length - 1);
            }
        }

        // check client Name
        private void ClientName_Txt_TextChanged(object sender, EventArgs e)
        {
            if (ClientName_Txt.Text != null && ClientName_Txt.Text != "  " && ClientName_Txt.Text != " ")
            {
                Add_Client_Btn.Enabled = true;
            }
            else
            {
                Add_Client_Btn.Enabled = false;
            }
        }

        // add client  button
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
                        selected_Client = new Client { C_Name = ClientName_Txt.Text, C_Address = ClientAddress_Txt.Text, C_Phone_Number = (long)int.Parse(ClientPhone_Txt.Text.ToString()) };
                        var Item = context.Clients.Where(c => c.C_Phone_Number == selected_Client.C_Phone_Number).FirstOrDefault();

                        if (Item == null)
                        {
                            context.Clients.Add(selected_Client);
                            context.SaveChanges();
                            selected_Client.C_ID = context.Clients.Where(c => c.C_Phone_Number == selected_Client.C_Phone_Number).FirstOrDefault().C_ID;
                            ClientName_label15.Text = selected_Client.C_Name;
                            ClientAddress_label13.Text = selected_Client.C_Address;
                            ClientPhone_label14.Text = selected_Client.C_Phone_Number.ToString();
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
        #endregion
        //---------------------------------------------------------


        //------------------------
        // **Add item to bill oprations
        #region  Add item to bill

        // check if there are any item in  Veiw_Item TextBoxt and (enable - disable) Add_Item_ToBill button
        private void Veiw_Item_Txt_TextChanged(object sender, EventArgs e)
        {

            if (Veiw_Item_Txt.Text != null && Veiw_Item_Txt.Text != "")
            {
                Add_Item_ToBill_Btn.Enabled = true;
            }
            else
            {
                Add_Item_ToBill_Btn.Enabled = false;
            }
        }

        // check if count of the selectd product is changed 
        private void ItemAmoun_numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (selected_Item != null)
            {
                selected_Item_price = selected_Item.P_Price;
                if (ApplyOffer_checkBox1.Enabled == true && ApplyOffer_checkBox1.Checked == true)
                {
                    selected_Item_price -= selected_Item_price * selected_Item.Offer.Off_Limit / 100;
                }
                selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
                Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
            }
        }

        // Apply Offer if existed on it 
        private void ApplyOffer_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
                selected_Item_price = selected_Item.P_Price;
                if (ApplyOffer_checkBox1.Enabled == true && ApplyOffer_checkBox1.Checked == true)
                {
                    selected_Item_price -= selected_Item_price * selected_Item.Offer.Off_Limit / 100;
                }
                selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
                Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
            
           
        }

        // Add_Item_ToBill button
        private void Add_Item_ToBill_Btn_Click(object sender, EventArgs e)
        {
            bill_list_item Item = bill_Item_list.Find(i => i.Equals(selected_Item));
            //var Item = bill_Item_list.Where(i=> i.Item.P_ID==selected_Item.P_ID).FirstOrDefault() ;
            if (Item == null || bill_Item_list.Count == 0)
            {
                bill_Item_list.Add(new bill_list_item(selected_Item, ((int)ItemAmoun_numericUpDown1.Value), selected_Item_total));
                var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                Show_Bills_Items_dataGridView1.DataSource = show_list;
                Veiw_Item_Txt.Text = null;
                Veiw_ItemPrice_Txt.Text = "";
                Veiw_ItemCategory_Txt.Text = "";
                Veiw_ItemOffer_Txt.Text = "";
                ItemAmoun_numericUpDown1.Value = 1;
                Veiw_Item_TotalPrice_Txt.Text = "";
                ApplyOffer_checkBox1.Checked = false;
                calc_Bill_Amount();
            }
            else
            {
                MessageBox.Show($" Item {selected_Item.P_Name} is already existed in the Bill ");
            }
        }

        #endregion
        //----------------------------------------------------------

        private void Bill_timer1_Tick(object sender, EventArgs e)
        {
            // Bill_Time_label.Text = DateTime.Now.ToString("HH:mm:ss tt");
            Bill_Date_label13.Text = DateTime.Now.ToString();
        }



        private void Show_Bills_Items_dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            calc_Bill_Amount();
        }

        private void Delete_Bill_Item_Btn_Click(object sender, EventArgs e)
        {
            if (Bill_selected_Item != null)
            {
                string deleted_itemName = Bill_selected_Item.Item.P_Name;
                bill_Item_list.Remove(Bill_selected_Item);
                var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                Show_Bills_Items_dataGridView1.DataSource = show_list;
                MessageBox.Show($"Item {deleted_itemName} Deleted Successfully .");
            }
            else
            {
                MessageBox.Show("No Item Selected to be deleted !!!");

            }


        }

        private void Show_Bills_Items_dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                Bill_selected_Item = bill_Item_list[e.RowIndex];
                Delete_Bill_Item_Btn.Enabled = true;
                Edit_Bill_Item_Btn.Enabled = true;

                //
            }

        }

        private void Show_Bills_Items_dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Bill_selected_Item = null;
            Delete_Bill_Item_Btn.Enabled = false;
            Edit_Bill_Item_Btn.Enabled = false;
        }

       

        private void Edit_Bill_Item_Btn_Click(object sender, EventArgs e)
        {

        }

        void calc_Bill_Amount()
        {
            if (bill_Item_list.Count > 0)
            {
                double amount = 0;
                foreach (bill_list_item i in bill_Item_list)
                {
                    amount += i.Total;
                }
                Bill_Total_Amount = amount;
                Bill_TotalAmount_label15.Text = Bill_Total_Amount.ToString();
            }
        }

    }
}

