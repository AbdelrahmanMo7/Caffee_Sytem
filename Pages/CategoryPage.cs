using Cafffe_Sytem.CustomModels;
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
    public partial class CategoryPage : Templete
    {
        public CategoryPage()
        {
            InitializeComponent();
            LoadData();
            LoadFilterComboOffer();
        }

        public void LoadData()
        {
            var query = (from e in DBConnection.Context.Categories
                         select new
                         {
                             Id =e.Cat_ID,
                             Category = e.Cat_Name,
                             Offer = e.Offer.Off_Name
                         }).ToList();


            // Set the DataSource to the list of products
            dataGridView1.DataSource = query;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddCategoryPage add = new AddCategoryPage(this);
            add.ShowDialog();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
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
                AddCategoryPage update = new AddCategoryPage(this, selectedProduct);

                update.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select from row header just one Category to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //============================== Delete Functionality =======================//
        public void DeleteData()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                // Get the selected category
                var deletedCategory = dataGridView1.SelectedRows[0].DataBoundItem as dynamic;

                if (deletedCategory != null)
                {
                    int CategoryId=deletedCategory.Id;
                    // Check if there are associated products
                    var productsWithCategory = DBConnection.Context.Products.Where(p => p.P_Cat_Id == CategoryId).ToList();

                    if (productsWithCategory.Any())
                    {
                        // Ask for confirmation
                        DialogResult result = MessageBox.Show("Deleting this category will remove its association with products. Do you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Update associated products with null category
                            foreach (var product in productsWithCategory)
                            {
                                product.Category = null;
                                //product.Category.Of_Id = null;
                            }

                            // Save changes
                            DBConnection.Context.SaveChanges();
                        }
                        else
                        {
                            // User chose not to proceed
                            return;
                        }
                    }

                    // Find the category by ID and remove it
                    var categoryToDelete = DBConnection.Context.Categories.Find(deletedCategory.Id);
                    if (categoryToDelete != null)
                    {
                        DBConnection.Context.Categories.Remove(categoryToDelete);
                        DBConnection.Context.SaveChanges();
                        LoadData(); // Refresh DataGridView after deleting the category
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select from row header just one category to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            DeleteData();
        }


        //============================ Filter stuff ===================//

        public void LoadFilterComboOffer()
        {
            List<string> Q3 = new List<string>();
            Q3.Add("All");
            Q3.AddRange((from e in DBConnection.Context.Offers
                         select e.Off_Name).ToList());
            Q3.Add("No Offer");

            // Set the DataSource to the list of categories
            comboBox1.DataSource = Q3;
        }

        //======================== Offer Filter ====================//

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Get the selected offer from the combo box
            string selectedOffer = comboBox1.Text;

            // Check if an offer is selected
            if (!string.IsNullOrWhiteSpace(selectedOffer))
            {
                // Filter the data based on the selected category
                if (selectedOffer == "No Offer")
                {
                    // Show products where the Offer is null
                    var filteredData = (from Category in DBConnection.Context.Categories
                                        where Category.Offer == null
                                        select new
                                        {
                                            Id = Category.Cat_ID,
                                            Name = Category.Cat_Name,
                                            Offer = Category.Offer == null ? "No Offer assigned" : Category.Offer.Off_Name
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
                    var filteredData = (from Category in DBConnection.Context.Categories
                                        where Category.Offer.Off_Name == selectedOffer
                                        select new
                                        {
                                            Id = Category.Cat_ID,
                                            Name = Category.Cat_Name,
                                            Offer = Category.Offer.Off_Name
                                        }).ToList();

                    // Set the DataSource to the filtered data
                    dataGridView1.DataSource = filteredData;
                }
            }
        }

        //================= Search Stuff ===================//

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            // Get the search keyword from the search textbox
            string searchKeyword = SearchTxt.Text.ToLower(); // Assuming you have a textbox named SearchTextBox

            // Check if a search keyword is provided
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                // Filter the data based on the product name 
                var filteredData = (from Category in DBConnection.Context.Categories
                                    where Category.Cat_Name.ToLower().Contains(searchKeyword)
                                    select new
                                    {
                                        Id = Category.Cat_ID,
                                        Name = Category.Cat_Name,
                                        Offer = Category.Offer.Off_Name
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

       
    }
}
