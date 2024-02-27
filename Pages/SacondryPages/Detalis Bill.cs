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
       
        private int clientId;
        public Detalis_Bill(int clientId)
        {
            InitializeComponent();
            Init();
            this.clientId = clientId;
            LoadClientDetails();
        }

        //private void Init()
        //{
        //    var products = caf.Products.ToList();
        //    var totalProducts = products.Count;
        //    var totalPrice = products.Sum(p => p.P_Price);
        //    var averagePrice = totalProducts > 0 ? totalPrice / totalProducts : 0;

        //    var productDetails = products.Select(p => new
        //    {
        //        p.P_Name,
        //        p.P_Price,
        //        //p.P_ID,
        //        //p.P_Code,

        //        p.P_Quantity
        //    }).ToList();

        //    dataGridView1.DataSource = productDetails;

        //}

        private void Init()
        {
            var products = DBConnection.Context.Products.ToList();
            var productDetails = products.Select(p => new
            {
                p.P_Name,
                p.P_Price,
                p.P_Quantity,
                TotalPrice = p.P_Price * p.P_Quantity // Calculate total price for each product
            }).ToList();

            dataGridView1.DataSource = productDetails;

            // Calculate total price for all products
            var totalPriceForAllProducts = productDetails.Sum(p => p.TotalPrice);
            // Now you can print or use totalPriceForAllProducts as needed
            Console.WriteLine("Total price for all products: " + totalPriceForAllProducts);
        }











        public void SetBillDetails(int id, string date, int table, float total, string cashier)
        {
            txtBillId.Text = id.ToString();
            txtDate.Text = date;
            txttable.Text = table.ToString();
            txtTotal.Text = total.ToString();
            txtCashier.Text = cashier;
        }

        private void LoadClientDetails()
        {
            Client client = new Client();
            client = DBConnection.Context.Clients.FirstOrDefault(c => c.C_ID == clientId);
            if (client != null)
            {
                txtClintName.Text = client.C_Name;
                txtClientPhone.Text = client.C_Phone_Number.ToString();
                txtClientAddress.Text = client.C_Address;
            }

        }
        private void Detalis_Bill_Load(object sender, EventArgs e)
        {
            

        }

        // Other event handlers and methods



    }
}
