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
    public partial class ProductPage : Templete
    {

       
        //private Coffee_Context Context = new Coffee_Context();
        public ProductPage()
        {
            InitializeComponent();
            LoadData();
            LoadFilterComboCategory();
            LoadFilterComboOffer();
        }

        public void LoadData()
        {
            try { 
            var query = (from e in DBConnection.Context.Products
                         select new
                         {
                             Id = e.P_ID,
                             Name = e.P_Name,
                             Price = e.P_Price,
                             Quantity = e.P_Quantity,
                             Code = e.P_Code,
                             Category = e.Category.Cat_Name,
                             Offer = e.Offer.Off_Name
                         }).ToList();


            // Set the DataSource to the list of products
            dataGridView1.DataSource = query;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            AddProductPage add = new AddProductPage(this);
            add.ShowDialog();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                // Get the selected product
                //DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                var selectedProduct = dataGridView1.SelectedRows[0].DataBoundItem as dynamic;

                //int productId = selectedProduct.ProductId;
                //AddProductPage update = new AddProductPage(this, Context, productId);
                //update.ShowDialog();
                //LoadData(); // Refresh DataGridView after editing the product

                // Open the AddProductPage for updating the selected product
                AddProductPage update = new AddProductPage(this, selectedProduct);

                update.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select from row header just one product to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //============================== Delete Functionality =======================//
        public void DeleteData()
        {
            try { 
            if (dataGridView1.SelectedRows.Count == 1)
            {
                // Get the selected product
                var deletedProduct = dataGridView1.SelectedRows[0].DataBoundItem as dynamic;

                if (deletedProduct != null)
                {
                    // Confirm deletion with the user
                    DialogResult result = MessageBox.Show(" Worning!!! if this Product has been deleted this may daleted all it`s data from all biils .   Are you sure you want to delete this product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Find the product by ID and remove it
                        Product productToDelete = DBConnection.Context.Products.Find(deletedProduct.Id);
                        if (productToDelete != null)
                        {
                            productToDelete.Bill_has_Products.Count();
                            productToDelete.Bill_has_Products.Clear();
                            DBConnection.Context.SaveChanges();
                            DBConnection.Context.Products.Remove(productToDelete);
                            DBConnection.Context.SaveChanges();
                            LoadData(); // Refresh DataGridView after deleting the product
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select from row header just one product to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        //============================ Filter stuff ===================//
        public void LoadFilterComboCategory()
        {
            try { 
            List<string> Q2 = new List<string>();
            Q2.Add("All");
            Q2.AddRange((from e in DBConnection.Context.Categories
                         select e.Cat_Name).ToList());
            Q2.Add("No Category");


            // Set the DataSource to the list of products
            FilterComBox.DataSource = Q2;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void LoadFilterComboOffer()
        {
            try { 
            List<string> Q3 = new List<string>();
            Q3.Add("All");
            Q3.AddRange((from e in DBConnection.Context.Offers
                         select e.Off_Name).ToList());
            Q3.Add("No Offer");

            // Set the DataSource to the list of products
            comboBox1.DataSource = Q3;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //===================== Category Filter ====================//
        private void FilterComBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try { 
            // Get the selected category from the combo box
            string selectedCategory = FilterComBox.Text;

            // Check if a category is selected
            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                // Filter the data based on the selected category
                if (selectedCategory == "No Category")
                {
                    // Show products where the Category is null
                    var filteredData = (from product in DBConnection.Context.Products
                                        where product.Category == null
                                        select new
                                        {
                                            Id = product.P_ID,
                                            Name = product.P_Name,
                                            Price = product.P_Price,
                                            Quantity = product.P_Quantity,
                                            Code = product.P_Code,
                                            Category = product.Category == null ? "No Category assigned" : product.Category.Cat_Name,
                                            Offer = product.Offer.Off_Name
                                        }).ToList();

                    // Set the DataSource to the filtered data
                    dataGridView1.DataSource = filteredData;
                }

                else if (selectedCategory == "All")
                {
                    // If "All" is selected, load all data
                    LoadData();
                }
                else
                {
                    // Show products based on the selected Category
                    var filteredData = (from product in DBConnection.Context.Products
                                        where product.Category.Cat_Name == selectedCategory
                                        select new
                                        {
                                            Id = product.P_ID,
                                            Name = product.P_Name,
                                            Price = product.P_Price,
                                            Quantity = product.P_Quantity,
                                            Code = product.P_Code,
                                            Category = product.Category.Cat_Name,
                                            Offer = product.Offer.Off_Name
                                        }).ToList();

                    // Set the DataSource to the filtered data
                    dataGridView1.DataSource = filteredData;
                }
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //======================== Offer Filter ====================//

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
            // Get the selected offer from the combo box
            string selectedOffer = comboBox1.Text;

            // Check if an offer is selected
            if (!string.IsNullOrWhiteSpace(selectedOffer))
            {
                // Filter the data based on the selected category
                if (selectedOffer == "No Offer")
                {
                    // Show products where the Offer is null
                    var filteredData = (from product in DBConnection.Context.Products
                                        where product.Offer == null
                                        select new
                                        {
                                            Id = product.P_ID,
                                            Name = product.P_Name,
                                            Price = product.P_Price,
                                            Quantity = product.P_Quantity,
                                            Code = product.P_Code,
                                            Category = product.Category.Cat_Name,
                                            Offer = product.Offer == null ? "No Offer assigned" : product.Offer.Off_Name
                                        }).ToList();

                    // Set the DataSource to the filtered data
                    dataGridView1.DataSource = filteredData;
                }
                else if (selectedOffer == "All")
                {
                    // If "All" is selected, load all data
                    LoadData();
                }
                else
                {
                    // Show products based on the selected offer
                    var filteredData = (from product in DBConnection.Context.Products
                                        where product.Offer.Off_Name == selectedOffer
                                        select new
                                        {
                                            Id = product.P_ID,
                                            Name = product.P_Name,
                                            Price = product.P_Price,
                                            Quantity = product.P_Quantity,
                                            Code = product.P_Code,
                                            Category = product.Category.Cat_Name,
                                            Offer = product.Offer.Off_Name
                                        }).ToList();

                    // Set the DataSource to the filtered data
                    dataGridView1.DataSource = filteredData;
                }
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //================= Search Stuff ===================//

        private void SearchBtn_Click_1(object sender, EventArgs e)
        {
            try { 
            // Get the search keyword from the search textbox
            string searchKeyword = SearchTxt.Text.ToLower(); // Assuming you have a textbox named SearchTextBox

            // Check if a search keyword is provided
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                // Check if the search keyword is "all" or "All"
                if (searchKeyword == "all")
                {
                    // Load all data again
                    LoadData();
                    return;
                }

                // Filter the data based on the product name 
                var filteredData = (from product in DBConnection.Context.Products
                                    where product.P_Name.ToLower().Contains(searchKeyword)
                                    select new
                                    {
                                        Id = product.P_ID,
                                        Name = product.P_Name,
                                        Price = product.P_Price,
                                        Quantity = product.P_Quantity,
                                        Code = product.P_Code,
                                        Category = product.Category.Cat_Name,
                                        Offer = product.Offer.Off_Name
                                    }).ToList();

                // Set the DataSource to the filtered data
                dataGridView1.DataSource = filteredData;
            }
            else
            {
                LoadData();
                // If no search keyword is provided, load all data
                MessageBox.Show("Please enter the name of product to search.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
