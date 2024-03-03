using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class Detalis_Bill : Form
    {
       
        public Bill selected_bill;
        public event EventHandler editing;
        public Detalis_Bill(Bill selected_bill)
        {
            this.selected_bill = selected_bill;
            InitializeComponent();
            Init();
            SetBillDetails();
            LoadClientDetails();
            DeleteBtn.Visible = false;
            UpdateBtn.Visible = false;
        }
        public Detalis_Bill(Bill selected_bill,int x)
        {
            try { 
            this.selected_bill = selected_bill;
            InitializeComponent();
            Init();
            SetBillDetails();
            LoadClientDetails();
            DeleteBtn.Visible=true;
            UpdateBtn.Visible=true;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void Init()
        {
            try { 
            var productDetails = selected_bill.Bill_has_Products.Select(p => new
            {
               Product_Name= p.Product.P_Name,
                Price= p.Product.P_Price,
                Count=p.Product_Count,
                TotalPrice = p.Product.P_Price * p.Product_Count // Calculate total price for each product
            }).ToList();

            dataGridView1.DataSource = productDetails;
            // Calculate total price for all products
            var totalPriceForAllProducts = selected_bill.B_Total_Amount;
                // Now you can print or use totalPriceForAllProducts as needed
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        public void SetBillDetails()
        {
            try { 
            txtBillId.Text = selected_bill.B_ID.ToString();
            txtDate.Text = selected_bill.B_Date.ToString("dd-MM-yyyy");
            txttable.Text = selected_bill.B_Table_Num.ToString();
            txtTotal.Text = selected_bill.B_Total_Amount.ToString();
            txtCashier.Text = selected_bill.User.U_Name;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void LoadClientDetails()
        {
            try { 
                txtClintName.Text = selected_bill.Client?.C_Name;
                txtClientPhone.Text = selected_bill.Client?.C_Phone_Number.ToString();
                txtClientAddress.Text = selected_bill.Client?.C_Address;

            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Detalis_Bill_Load(object sender, EventArgs e)
        {
            

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try { 
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                var b = DBConnection.Context.Bills.Find(selected_bill.B_ID);
                b.B_IsDeleted_ = true;
                b.Remover_Id = Login.Current_User.U_ID;
                foreach (var pr in b.Bill_has_Products)
                {
                    var p = DBConnection.Context.Products.Find(pr.Product_ID);
                    if (p != null)
                        p.P_Quantity += pr.Product_Count;
                    DBConnection.Context.SaveChanges();
                }

                this.Close();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

            this.editing?.Invoke(this, e);
            this.Close();

        }

        // Other event handlers and methods



    }
}
