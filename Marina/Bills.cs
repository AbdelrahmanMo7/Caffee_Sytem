using Cafffe_Sytem.A.M.A;
using Cafffe_Sytem.D.M.M;
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

namespace Cafffe_Sytem.Marina
{
    public partial class Bills : Templete
    {

       
       
        public int selected;


      
        public Bills()
        {
            InitializeComponent();

            BindDataGridView();


            var bill_list = DBConnection.Context.Bills.Select(b => b.B_ID).GroupBy(d => d).ToList();
           
            var bil = DBConnection.Context.Bills.Select(C => C).ToList();
            dataGridView1.DataSource = bil;
            Is_ExsitedcomboBox2.Items.Add( "Existed");
            Is_ExsitedcomboBox2.Items.Add(  "Deleted" );
            Is_ExsitedcomboBox2.SelectedItem= "Existed";
            LoadData();
        }
        private void LoadData()
        {
            getbills();
        }

        private void init()
        {
            var e = DBConnection.Context.Bills.Select(b => new { b.B_ID, b.B_Date, b.B_Time, b.B_Total_Amount, b.B_Table_Num, b.B_IsDeleted_, b.Creater_Id, b.Client_Id, b.Remover_Id }).ToString();
            dataGridView1.DataSource = e;
        }


        private void BindDataGridView()
        {
            if (DBConnection.Context.Bills != null)
            {
                var bil = DBConnection.Context.Bills
                    .Select(b => new { b.B_ID, b.B_Date, b.B_Time, b.B_Total_Amount, b.B_Table_Num, b.B_IsDeleted_, b.Creater_Id, b.Client_Id, b.Remover_Id })
                    .ToList();

                dataGridView1.DataSource = bil;
            }
            else
            {
               Console.WriteLine ("error");
            }
        }




        private void buttondelete_Click(object sender, EventArgs e)
        {

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
                            // Save changes to the database
                            DBConnection.Context.SaveChanges();
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
       
        
        private void buttonsearch_Click(object sender, EventArgs e)
        {

            if (Bill_idtextBox1.Text != null)
            {
                int selectedBillID = Convert.ToInt32(Bill_idtextBox1.Text);


                var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);

                if (selectedBill != null)
                {
                    // Display the selected bill (You need to implement your display logic here)

                    if (Is_ExsitedcomboBox2.SelectedItem == "Existed")
                    {
                        var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false && b.B_ID == selectedBillID)
                             .Select(b => new
                             {
                                 No = b.B_ID,
                                 Date = b.B_Date,
                                 Time = b.B_Time,
                                 Total = b.B_Total_Amount,
                                 Table = b.B_Table_Num,
                                 Cashier = b.User.U_Name,
                                 Client = b.Client.C_Name
                             }).ToList();
                        dataGridView1.DataSource = query;


                    }
                    else
                    {
                        var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == true && b.B_ID == selectedBillID)
                             .Select(b => new
                             {
                                 No = b.B_ID,
                                 Date = b.B_Date,
                                 Time = b.B_Time,
                                 Total = b.B_Total_Amount,
                                 Table = b.B_Table_Num,
                                 Cashier = b.User.U_Name,
                                 Remover = b.User1.U_Name,
                                 Client = b.Client.C_Name
                             }).ToList();
                        dataGridView1.DataSource = query;

                    }

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Selected bill not found .");
                }
            }
            else
            {
                MessageBox.Show("Please enter valied bill No .");
            }

        }

      
      



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Is_ExsitedcomboBox2.SelectedItem != "Existed")
                 buttondelete.Enabled = false;
            getbills();



        }

      

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selected = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
          
        }

        public void getbills()
        {
            if (Is_ExsitedcomboBox2.SelectedItem == "Existed")
            {
                var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false)
                     .Select(b => new
                     {
                         No = b.B_ID,
                         Date = b.B_Date,
                         Time = b.B_Time,
                         Total = b.B_Total_Amount,
                         Table = b.B_Table_Num,
                         Cashier = b.User.U_Name,
                         Client = b.Client.C_Name
                     }).ToList();
                dataGridView1.DataSource = query;


            }
            else
            {
                var query = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == true)
                     .Select(b => new
                     {
                         No = b.B_ID,
                         Date = b.B_Date,
                         Time = b.B_Time,
                         Total = b.B_Total_Amount,
                         Table = b.B_Table_Num,
                         Cashier = b.User.U_Name,
                         Remover = b.User1.U_Name,
                         Client = b.Client.C_Name
                     }).ToList();
                dataGridView1.DataSource = query;

            }
        }

        private void buttondetails_Click(object sender, EventArgs e)
        {
            if (selected != 0)
            {
                int selectedBillID = selected;
                var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);

                if (selectedBill != null)
                {
                    Detalis_Bill detailsForm = new Detalis_Bill(selectedBill.Client_Id);
                    detailsForm.SetBillDetails(selectedBill.B_ID, selectedBill.B_Date.ToString(), selectedBill.B_Table_Num, (float)selectedBill.B_Total_Amount, selectedBill.User.U_Name);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected = int.Parse(row.Cells["ID"].Value.ToString());
            }
        }

        private void Bill_idtextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(Bill_idtextBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Bill_idtextBox1.Text = Bill_idtextBox1.Text.Remove(Bill_idtextBox1.Text.Length - 1);
            }
        }

       

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedBillID = selected;
            var selectedBill = DBConnection.Context.Bills.FirstOrDefault(b => b.B_ID == selectedBillID);
            Detalis_Bill detailsForm = new Detalis_Bill(selectedBill.Client_Id);
            detailsForm.SetBillDetails(selectedBill.B_ID, selectedBill.B_Date.ToString(), selectedBill.B_Table_Num, (float)selectedBill.B_Total_Amount, selectedBill.User.U_Name);
            detailsForm.Show();
        }
    }
    }


