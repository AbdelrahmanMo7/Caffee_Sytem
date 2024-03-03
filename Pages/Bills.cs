using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Pages.SacondryPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages
{
    public partial class Bills : Templete
    {

        public int selected;
      
        public Bills()
        {
            InitializeComponent();

            BindDataGridView();


           
            
            Is_ExsitedcomboBox2.Items.Add( "Existed");
            Is_ExsitedcomboBox2.Items.Add(  "Deleted" );
            Is_ExsitedcomboBox2.SelectedItem= "Existed";
            LoadData();
        }
        private void LoadData()
        {
            getbills();
        }

       


        private void BindDataGridView()
        {
            getbills();
        }




        private void buttondelete_Click(object sender, EventArgs e)
        {
            try {

            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int selectedBillID = (int)dataGridView1.Rows[selectedRowIndex].Cells["No"].Value;

                    var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);
                    if (selectedBill != null)
                    {
                        
                        
                            selectedBill.B_IsDeleted_ = true;
                            selectedBill.Remover_Id = Login.Current_User.U_ID;
                        foreach (var pr in selectedBill.Bill_has_Products)
                        {
                            var p = DBConnection.Context.Products.Find(pr.Product_ID);
                            if (p != null)
                                p.P_Quantity += pr.Product_Count;
                            DBConnection.Context.SaveChanges();
                        }
                        // Save changes to the database
                            MessageBox.Show("Row marked as deleted successfully.");
                        getbills();


                    }
                    else
                    {
                        MessageBox.Show("Bill not found in the database.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
       
        
        private void buttonsearch_Click(object sender, EventArgs e)
        {
            try { 
            if (!string.IsNullOrWhiteSpace( Bill_idtextBox1.Text))
            {
                int selectedBillID = int.Parse(Bill_idtextBox1.Text.ToString());


                var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);

                if (selectedBill != null)
                {
                    // Display the selected bill (You need to implement your display logic here)

                    if (Is_ExsitedcomboBox2.SelectedItem == "Existed")
                    {
                        var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false && b.B_ID == selectedBillID).ToList()
                             .Select(b => new
                             {
                                 No = b.B_ID,
                                 Date = b.B_Date,
                                 Time = b.B_Time,
                                 Total = b.B_Total_Amount,
                                 Table = b.B_Table_Num,
                                 Cashier = b.User.U_Name,
                                 Client = b.Client?.C_Name
                             }).ToList();
                        dataGridView1.DataSource = query;


                    }
                    else
                    {
                        var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == true && b.B_ID == selectedBillID).ToList()
                             .Select(b => new
                             {
                                 No = b.B_ID,
                                 Date = b.B_Date,
                                 Time = b.B_Time,
                                 Total = b.B_Total_Amount,
                                 Table = b.B_Table_Num,
                                 Cashier = b.User.U_Name,
                                 Remover = b.User1?.U_Name,
                                 Client = b.Client?.C_Name
                             }).ToList();
                        dataGridView1.DataSource = query;

                    }

                    
                }
                else
                {
                    MessageBox.Show("Please enter valied bill No .");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Selected bill not found in this List.");
               
                LoadData();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

      
      



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
            if(Is_ExsitedcomboBox2.SelectedItem != "Existed")
                 buttondelete.Enabled = false;
            else
                buttondelete.Enabled = true;
            getbills();

            
           }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }

}

      

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
            if(e.RowIndex>-1)
            selected = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void getbills()
        {
            try { 
            if (Is_ExsitedcomboBox2.SelectedItem == "Existed")
            {
                var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false).ToList()
                     .Select(b => new
                     {
                         No = b.B_ID,
                         Date = b.B_Date,
                         Time = b.B_Time,
                         Total = b.B_Total_Amount,
                         Table = b.B_Table_Num,
                         Cashier = b.User.U_Name,
                         Client = b.Client?.C_Name
                     }).ToList();
                dataGridView1.DataSource = query;


            }
            else
            {
                var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == true).ToList()
                     .Select(b => new
                     {
                         No = b.B_ID,
                         Date = b.B_Date,
                         Time = b.B_Time,
                         Total = b.B_Total_Amount,
                         Table = b.B_Table_Num,
                         Cashier = b.User.U_Name,
                         Remover = b.User1?.U_Name,
                         Client = b.Client?.C_Name
                     }).ToList();
                dataGridView1.DataSource = query;

            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttondetails_Click(object sender, EventArgs e)
        {
            try { 
            if (selected != 0)
            {
                int selectedBillID = selected;
                var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);

                if (selectedBill != null)
                {
                    Detalis_Bill detailsForm = new Detalis_Bill(selectedBill);
                   
                    detailsForm.Show();
                }
                else
                {
                    MessageBox.Show("Selected bill not found");
                }
            }
            else
            {
                MessageBox.Show("Please select a bill first.");
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected = int.Parse(row.Cells["ID"].Value.ToString());
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Bill_idtextBox1_TextChanged(object sender, EventArgs e)
        {
            try {
            if (Regex.IsMatch(Bill_idtextBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Bill_idtextBox1.Text = Bill_idtextBox1.Text.Remove(Bill_idtextBox1.Text.Length - 1);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {  if (e.RowIndex >= 0)
                {
                    Detalis_Bill detailsForm = new Detalis_Bill(DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selected));

                    detailsForm.Show();
                }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
    }


