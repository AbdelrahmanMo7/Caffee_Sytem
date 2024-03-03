using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using Cafffe_Sytem.Pages;
using Cafffe_Sytem.Pages.SacondryPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Cafffe_Sytem.Pages
{

    public partial class Make_Bill : Form
    {

        int selectedTable_Number;
        Product selected_Item;
        bill_list_item Bill_selected_Item;
        double selected_Item_total, selected_Item_price, Bill_Total_Amount;
        Client selected_Client;
        List<bill_list_item> bill_Item_list;
        Bill selectedBill_edit;
        List<Bill_has_Products> oldProducts;


        // constractor
        public Make_Bill()
        {
            
            InitializeComponent();
            start();
            UserName_label1.Text = Login.Current_User.U_Name;
            if (Login.Current_User.U_IsAdmin_ == true)
            {
                Is_Admin_label2.Text = "Admin";
            }
            else
            {
                Is_Admin_label2.Text = "Cashier";
            }
        }
        void start()
        {
            try
            {
            Bill_timer1.Start();
            oldProducts = new List<Bill_has_Products>();
            selected_Client = null;
            Bill_selected_Item = null;
            selectedTable_Number = 0;
            bill_Item_list = new List<bill_list_item>();
            ClientName_label15.Text ="-----------------";
            ClientAddress_label13.Text = "-----------------";
            ClientPhone_label14.Text = "-----------------";
            Table_textBox1.Text = "";

            All_Cat_comboBox1.Items.Add("All");
            All_Cat_comboBox1.Items.AddRange(DBConnection.Context.Categories.Select(c => c.Cat_Name).ToArray());
            var p1 = DBConnection.Context.Products.Select(p => new { p.P_ID, Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
            Show_Products_dataGridView1.DataSource = p1;
            Show_Products_dataGridView1.Columns[0].Visible = false;
            var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
            Show_Bills_Items_dataGridView1.DataSource = show_list;
            //Where(b => b.User1.U_ID == Login.Current_User.U_ID && b.B_Date == (DateTime.Today.Date)).
            DateTime newdate = DateTime.Today.Date.AddDays(-7);
            Show_Bills_dataGridView2.DataSource = DBConnection.Context.Bills.Where(b => b.B_Date >= newdate && b.B_IsDeleted_==false).ToList().Select(b => new { ID=b.B_ID ,Total=b.B_Total_Amount,Client=b.Client?.C_Name  }).OrderByDescending(b=>b.ID).ToList();
            }catch(Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
             }
        }

        //---------------------------------------------------------
        // ** fillter & search in product
        #region fillter & search in product

        // fillter Product by category
        private void All_Cat_comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try { 
            string selected_Cat = All_Cat_comboBox1.SelectedItem.ToString();
            if (selected_Cat == "All")

            {
                Show_Products_dataGridView1.DataSource = DBConnection.Context.Products.Select(p => new { p.P_ID, Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();

                Show_Products_dataGridView1.Columns[0].Visible = false;

            }
            else
            {
                Show_Products_dataGridView1.DataSource = DBConnection.Context.Products.Where(p => p.Category.Cat_Name == selected_Cat).Select(p => new { p.P_ID, Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
                Show_Products_dataGridView1.Columns[0].Visible = false;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // search in product name
        private void Item_search_Txt_TextChanged_1(object sender, EventArgs e)
        {
            try
            { 
            string selected_product = Item_search_Txt.Text.ToString();
            if (selected_product != null || selected_product == " ")
            {
                Show_Products_dataGridView1.DataSource = DBConnection.Context.Products.Where(p => p.P_Name.Contains(selected_product)).Select(p => new { p.P_ID, Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
                Show_Products_dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                Show_Products_dataGridView1.DataSource = DBConnection.Context.Products.Select(p => new { p.P_ID, Name = p.P_Name, Category = p.Category.Cat_Name, Price = p.P_Price, Offer = p.Offer.Off_Name }).ToList();
                Show_Products_dataGridView1.Columns[0].Visible = false;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion
        //------------------------------------------------



        private void Show_Products_dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
            if (e.RowIndex >= 0)
            {
                Add_Item_ToBill_Btn.Text = "Add Item";
                int selected_id = int.Parse(Show_Products_dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //  int selected_id = int.Parse(Show_Products_dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                selected_Item = DBConnection.Context.Products.Find(selected_id);
                Veiw_Item_Txt.Text = selected_Item.P_Name;
                Veiw_ItemPrice_Txt.Text = selected_Item.P_Price.ToString();

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
                calc_item_total();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        private void Show_Products_dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            refresh();
        }

        //------------------------
        // **client addtion oprations
        #region client addtion oprations

        // check cient phone
        private void ClientPhone_Txt_TextChanged(object sender, EventArgs e)
        {
            try { 
            if (Regex.IsMatch(ClientPhone_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ClientPhone_Txt.Text = ClientPhone_Txt.Text.Remove(ClientPhone_Txt.Text.Length - 1);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // check client Name
        private void ClientName_Txt_TextChanged(object sender, EventArgs e)
        {
            try { 
            if (ClientName_Txt.Text != null && ClientName_Txt.Text != "  " && ClientName_Txt.Text != " ")
            {
                Add_Client_Btn.Enabled = true;
            }
            else
            {
                Add_Client_Btn.Enabled = false;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Table_textBox1_TextChanged(object sender, EventArgs e)
        {
            try { 
            if (Regex.IsMatch(Table_textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Table_textBox1.Text = Table_textBox1.Text.Remove(Table_textBox1.Text.Length - 1);
            }
            if (string.IsNullOrEmpty(Table_textBox1.Text.ToString()))
               return;
            if (int.Parse(Table_textBox1.Text.ToString()) > 100)
            {
                Table_textBox1.Text = "100";
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // add client  button
        private void Add_Client_Btn_Click(object sender, EventArgs e)
        {
            try { 
            if (string.IsNullOrEmpty(ClientName_Txt.Text))
            {
                MessageBox.Show("Please client name .");
                return;
            }               

            if (ClientPhone_Txt.Text.Length < 5)
            {
                MessageBox.Show("Please enter Valiad numbers more than 5 numbers .");
                return;
            }
            
            if (string.IsNullOrEmpty(Table_textBox1.Text.ToString()) || int.Parse(Table_textBox1.Text.ToString()) <= 0)
            {
                MessageBox.Show(" Please enter Valid Table number !!!");
                return;
            }
            
            selected_Client = new Client { C_Name = ClientName_Txt.Text, C_Address = ClientAddress_Txt.Text, C_Phone_Number = long.Parse(ClientPhone_Txt.Text.ToString()) };
            var Item = DBConnection.Context.Clients.Where(c => c.C_Phone_Number == selected_Client.C_Phone_Number).FirstOrDefault();

            if (Item == null)
            {
                DBConnection.Context.Clients.Add(selected_Client);
                DBConnection.Context.SaveChanges();
                selected_Client.C_ID = DBConnection.Context.Clients.Where(c => c.C_Phone_Number == selected_Client.C_Phone_Number).FirstOrDefault().C_ID;
                get_client();
            }
            else
            {

                DialogResult result = MessageBox.Show($"This number is already existed for client {Item.C_Name}.                                     Are you want to select this client?  ", "client Phone Number", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    if (!string.IsNullOrEmpty(Table_textBox1.Text.ToString()) || int.Parse(Table_textBox1.Text.ToString()) > 0)
                    {
                        selected_Client = Item;
                        get_client();
                    }
                    else
                    {
                        MessageBox.Show(" Please enter Valid Table number !!!");
                    }

                }
                else
                {
                    selected_Client = null;
                }
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        void get_client()
        {
            try { 
            ClientName_label15.Text = selected_Client.C_Name;
            ClientAddress_label13.Text = selected_Client.C_Address;
            ClientPhone_label14.Text = selected_Client.C_Phone_Number.ToString();
            selectedTable_Number = int.Parse(Table_textBox1.Text.ToString());
            MessageBox.Show($"Client {selected_Client.C_Name} is Add Successfully .");
            if (bill_Item_list.Count > 0)
            {
                PrintBill_Btn.Enabled = true;
            }
            else
            {
                PrintBill_Btn.Enabled = false;
            }
            ClientName_Txt.Text = "";
            ClientAddress_Txt.Text = "";
            ClientPhone_Txt.Text= "";
            Add_Client_Btn.Enabled = false;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            try { 
           
            if (Veiw_Item_Txt.Text != null && Veiw_Item_Txt.Text != "")
            {
                Add_Item_ToBill_Btn.Enabled = true;
            }
            else
            {
                Add_Item_ToBill_Btn.Enabled = false;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // check if count of the selectd product is changed 
        private void ItemAmoun_numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            calc_item_total();
        }

        // Apply Offer if existed on it 
        private void ApplyOffer_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            calc_item_total();
        }

        // Add_Item_ToBill button
        private void Add_Item_ToBill_Btn_Click(object sender, EventArgs e)
        {
            try { 
            if (Add_Item_ToBill_Btn.Text == "Edit Item")
            {

                bill_list_item Item = bill_Item_list.Find(i => i.Equals(Bill_selected_Item));
                if (Item != null)
                {
                    Item.Count = (int)ItemAmoun_numericUpDown1.Value;
                    Item.Total = selected_Item_total;
                    Add_Item_ToBill_Btn.Text = "Add Item";
                    var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                    Show_Bills_Items_dataGridView1.DataSource = show_list;
                    refresh();
                    calc_Bill_Amount();
                }
                else
                {
                    Item = bill_Item_list.Find(i => i.Equals(selected_Item));
                    //var Item = bill_Item_list.Where(i=> i.Item.P_ID==selected_Item.P_ID).FirstOrDefault() ;
                    if (Item == null || bill_Item_list.Count == 0)
                    {
                        bill_Item_list.Add(new bill_list_item(selected_Item, ((int)ItemAmoun_numericUpDown1.Value), selected_Item_total));
                        var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                        Show_Bills_Items_dataGridView1.DataSource = show_list;
                        refresh();
                        calc_Bill_Amount();
                    }
                    else
                    {
                        MessageBox.Show($" Item {selected_Item.P_Name} is already existed in the Bill ");
                    }
                }

            }
            else
            {
                bill_list_item Item = bill_Item_list.Find(i => i.Equals(selected_Item));
                //var Item = bill_Item_list.Where(i=> i.Item.P_ID==selected_Item.P_ID).FirstOrDefault() ;
                if (Item == null || bill_Item_list.Count == 0)
                {
                    bill_Item_list.Add(new bill_list_item(selected_Item, ((int)ItemAmoun_numericUpDown1.Value), selected_Item_total));
                    var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                    Show_Bills_Items_dataGridView1.DataSource = show_list;
                    refresh();
                    calc_Bill_Amount();
                }
                else
                {
                    MessageBox.Show($" Item {selected_Item.P_Name} is already existed in the Bill ");
                }
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message   , "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            try { 
            calc_Bill_Amount();
            Bill_selected_Item = null;
            selected_Item_total = 0;
            selected_Item_price = 0;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        private void Show_Bills_Items_dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
            if (e.RowIndex >= 0)
            {

                Bill_selected_Item = bill_Item_list[e.RowIndex];
                Delete_Bill_Item_Btn.Enabled = true;
                Edit_Bill_Item_Btn.Enabled = true;

                //
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void Show_Bills_Items_dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Bill_selected_Item = null;
            Delete_Bill_Item_Btn.Enabled = false;
            Edit_Bill_Item_Btn.Enabled = false;
        }

        private void PrintBill_Btn_Click(object sender, EventArgs e)
        {
            try { 
            if (PrintBill_Btn.Text == "Edit Bill")
            {
                calc_Bill_Amount();
                Bill newB = DBConnection.Context.Bills.Find(selectedBill_edit.B_ID);
                if(newB != null)
                {
                    newB.Client = selected_Client;
                    newB.B_Table_Num = selectedTable_Number;
                    newB.B_IsDeleted_ = false;
                    newB.B_Date = DateTime.Now.Date;
                    newB.B_Time = DateTime.Now.ToString("HH:mm:ss tt");
                    newB.B_Total_Amount = Bill_Total_Amount;
                     newB.Creater_Id = Login.Current_User.U_ID;
                    
                    newB.Bill_has_Products= bill_Item_list.Select(i => new Bill_has_Products { Bill_ID = newB.B_ID, Product = i.Item, Product_Count = i.Count }).ToList();
                    foreach (var pr in bill_Item_list)
                    {
                        Product p = DBConnection.Context.Products.Select(a=>a).Where(s=>s.P_ID==pr.Item.P_ID).FirstOrDefault();
                        if(p != null)
                        p.P_Quantity -= pr.Count;
                        DBConnection.Context.SaveChanges();
                    }
                    PrintBill_Btn.Text = "Print Bill";
                    this.start();

                }
            }
            else
            {
                calc_Bill_Amount();
                Bill_Templete bill_Templete1 = new Bill_Templete(bill_Item_list, selected_Client, Bill_Total_Amount, selectedTable_Number);
                bill_Templete1.BillCompleted += (obj2, e2) =>
                {
                    this.start();

                };

                bill_Templete1.ShowDialog();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // edit button to edit selected bill item
        private void Edit_Bill_Item_Btn_Click(object sender, EventArgs e)
        {
            try { 
            if (Bill_selected_Item != null)
            {
                Veiw_Item_Txt.Text = Bill_selected_Item.Item.P_Name;
                Veiw_ItemPrice_Txt.Text = Bill_selected_Item.Item.P_Price.ToString();
                if (Bill_selected_Item.Item.P_Cat_Id != null)
                {
                    Veiw_ItemCategory_Txt.Text = Bill_selected_Item.Item.Category.Cat_Name;
                }
                if (Bill_selected_Item.Item.P_Of_Id != null)
                {
                    Veiw_ItemOffer_Txt.Text = Bill_selected_Item.Item.Offer.Off_Name;
                    ApplyOffer_checkBox1.Enabled = true;
                    ApplyOffer_checkBox1.Visible = true;
                    if ((Bill_selected_Item.Count * Bill_selected_Item.Item.P_Price) == Bill_selected_Item.Total)
                    {
                        ApplyOffer_checkBox1.Checked = false;
                    }
                    else
                    {
                        ApplyOffer_checkBox1.Checked = true;
                    }

                }

                calc_item_total();
                ItemAmoun_numericUpDown1.Value = Bill_selected_Item.Count;
                Add_Item_ToBill_Btn.Text = "Edit Item";

            }
            else
            {
                MessageBox.Show("No Item Selected to be edited !!!");

            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        // Delete button to delete selected bill item
        private void Delete_Bill_Item_Btn_Click(object sender, EventArgs e)
        {
            try { 
            if (Bill_selected_Item != null)
            {
                string deleted_itemName = Bill_selected_Item.Item.P_Name;
                bill_Item_list.Remove(Bill_selected_Item);
                var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                Show_Bills_Items_dataGridView1.DataSource = show_list;
                calc_Bill_Amount();
                MessageBox.Show($"Item {deleted_itemName} Deleted Successfully .");
            }
            else
            {
                MessageBox.Show("No Item Selected to be deleted !!!");

            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //
        //--------------------------------------------------------------
        //

        //
        void refresh()
        {
            Veiw_Item_Txt.Text = null;
            Veiw_ItemPrice_Txt.Text = "";
            Veiw_ItemCategory_Txt.Text = "";
            Veiw_ItemOffer_Txt.Text = "";
            Veiw_Item_TotalPrice_Txt.Text = "";
            ItemAmoun_numericUpDown1.Value = 1;
            ApplyOffer_checkBox1.Enabled = false;
            ApplyOffer_checkBox1.Visible = false;
        }

      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Show_Bills_dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
             if(e.RowIndex<0) return;
            int selectedBillID = int.Parse(Show_Bills_dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
             var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);
            Detalis_Bill detailsForm = new Detalis_Bill(selectedBill,1);
            detailsForm.editing+= (obj2, e2) =>
            {
                selectedBill_edit = detailsForm.selected_bill;
                Show_Bills_Items_dataGridView1.DataSource = null;
                bill_Item_list=selectedBill_edit.Bill_has_Products.Select(b=> new bill_list_item {Item=b.Product,Count= b.Product_Count,Total=(b.Product_Count*b.Product.P_Price)  }).ToList();
                var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
                Show_Bills_Items_dataGridView1.DataSource = show_list;
                selected_Client= selectedBill_edit.Client;
                selectedTable_Number= selectedBill_edit.B_Table_Num;
                oldProducts = selectedBill_edit.Bill_has_Products.ToList();
                foreach (var pr in oldProducts)
                {
                    var p = DBConnection.Context.Products.Find(pr.Product_ID);
                    if (p != null)
                        p.P_Quantity += pr.Product_Count;
                    DBConnection.Context.SaveChanges();
                }
                refresh();
                calc_Bill_Amount();
                PrintBill_Btn.Text = "Edit Bill";

            };
            detailsForm.Show();
            start();
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        //
        void calc_item_total()
        {
            try {
            if (Add_Item_ToBill_Btn.Text == "Edit Item")
            {
                if (Bill_selected_Item != null && Add_Item_ToBill_Btn.Enabled)
                {
                    selected_Item_price = Bill_selected_Item.Item.P_Price;
                    if (ApplyOffer_checkBox1.Enabled == true && ApplyOffer_checkBox1.Checked == true)
                    {
                        selected_Item_price -= selected_Item_price * Bill_selected_Item.Item.Offer.Off_Limit / 100;
                    }
                    selected_Item_total = (selected_Item_price) * ((int)ItemAmoun_numericUpDown1.Value);
                    Veiw_Item_TotalPrice_Txt.Text = (selected_Item_total).ToString();
                }
            }
            else
            {
                if (selected_Item != null && Add_Item_ToBill_Btn.Enabled)
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
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //      
        void calc_Bill_Amount()
        {
            try { 
            if (bill_Item_list.Count > 0)
            {
                double amount = 0;
                foreach (bill_list_item i in bill_Item_list)
                {
                    amount += i.Total;
                }
                Bill_Total_Amount = amount;
                Bill_TotalAmount_label15.Text = Bill_Total_Amount.ToString();
                if (selected_Client != null)
                {
                    if (selected_Client.C_ID != 0)
                    {
                        PrintBill_Btn.Enabled = true;
                    }
                    else
                    {
                        PrintBill_Btn.Enabled = false;
                    }
                }
            }
            else
            {
                Bill_Total_Amount = 00.00;
                Bill_TotalAmount_label15.Text = Bill_Total_Amount.ToString();
                PrintBill_Btn.Enabled = false;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}

