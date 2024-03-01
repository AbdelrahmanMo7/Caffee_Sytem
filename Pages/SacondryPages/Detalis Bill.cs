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
       
        private Bill selected_bill;
        public Detalis_Bill(Bill selected_bill)
        {
            this.selected_bill = selected_bill;
            InitializeComponent();
            Init();
            SetBillDetails();
            LoadClientDetails();
        }

       

        private void Init()
        {
           
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


        public void SetBillDetails()
        {
            txtBillId.Text = selected_bill.B_ID.ToString();
            txtDate.Text = selected_bill.B_Date.ToString("dd-MM-yyyy");
            txttable.Text = selected_bill.B_Table_Num.ToString();
            txtTotal.Text = selected_bill.B_Total_Amount.ToString();
            txtCashier.Text = selected_bill.User.U_Name;
        }

        private void LoadClientDetails()
        {
            
                txtClintName.Text = selected_bill.Client?.C_Name;
                txtClientPhone.Text = selected_bill.Client?.C_Phone_Number.ToString();
                txtClientAddress.Text = selected_bill.Client?.C_Address;
           

        }
        private void Detalis_Bill_Load(object sender, EventArgs e)
        {
            

        }

        // Other event handlers and methods



    }
}
