using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using Cafffe_Sytem.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class AddCategoryPage : Form
    {
        private CategoryPage C;
        private dynamic selectedProduct;
        public AddCategoryPage(CategoryPage AC)
        {
            InitializeComponent();
            C = AC;
            // Hide the text & OfferComBox
            label2.Visible = false;
            OfferComBox.Visible = false;
            //LoadOfferCombo();
        }
        // Constructor for updating an existing product
        public AddCategoryPage(CategoryPage AC, dynamic selectedProduct)
        {
            InitializeComponent();
            this.AddBtn.Text = "Update";
            this.AddBtn.BackColor = Color.FromArgb(11, 96, 176);
            this.C = AC;
            //// UnHide the text & OfferComBox
            //label2.Visible = true;
            //OfferComBox.Visible = true;
            LoadOfferCombo();
            this.selectedProduct = selectedProduct;


            // Populate the form with existing product details

            CategoryTxtBox.Text = selectedProduct.Category;
            if (selectedProduct.Offer == null)
                OfferComBox.SelectedItem = "No Offer assigned";
            else
                OfferComBox.SelectedItem = selectedProduct.Offer;

            //ProductNameTxt.Text = selectedProduct.P_Name;
            //ProductQuantityTxt.Text = selectedProduct.P_Quantity.ToString();
            //ProductPriceTxt.Text = selectedProduct.P_Price.ToString();
            //ProductCodeTxt.Text = selectedProduct.P_Code;
        }



        public void LoadOfferCombo()
        {
            try {
            List<string> Q4 = new List<string>();

            Q4.AddRange((from e in DBConnection.Context.Offers
                         select e.Off_Name).ToList());
            Q4.Add("No Offer assigned");

            // Set the DataSource to the list of offers
            OfferComBox.DataSource = Q4;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //============================== Events =========================//

        

       

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try { 
            // Hide the OfferComBox
            OfferComBox.Visible = false;

            if (AddBtn.Text == "Add")
            {
                //============================== Validations =============================//
                // Validation  1: Check if required fields are not empty
                if (string.IsNullOrWhiteSpace(CategoryTxtBox.Text))
                {
                    MessageBox.Show("fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 2: Check for unique category names
                var existingName = DBConnection.Context.Categories.Any(c => c.Cat_Name == CategoryTxtBox.Text);
                if (existingName)
                {
                    MessageBox.Show("The Category name already exists. Please enter a unique name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 3: Ensure the name contains only letters and white spaces
                if (!Regex.IsMatch(CategoryTxtBox.Text, "^[a-zA-Z\\s]+$"))
                {
                    MessageBox.Show("Product Name must contain only letters and white spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //============================== Logic =============================//

                //// Hide the OfferComBox
                //OfferComBox.Visible = false;

                //// Retrieve the selected offer from the combo box
                //string selectedOfferName = OfferComBox.Text;

                //// Retrieve the corresponding Offer object from the same DbContext
                //Offer selectedOfferObject = DBConnection.Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName || (offer.Off_Name == null && selectedOfferName == "No Offer assigned"));

                // Create the new product with the selected category and offer
                Category newCategory = new Category
                {
                    Cat_Name = CategoryTxtBox.Text
                    //Offer = selectedOfferObject
                };

                // Add the new product to the same DbContext and save changes
                DBConnection.Context.Categories.Add(newCategory);
                DBConnection.Context.SaveChanges();

                // Refresh ProductsPage
                C.LoadData();

                // Close the form or perform any additional actions
                this.Close();
                return;
            }


            if (AddBtn.Text == "Update")
            {
                // UnHide the text & OfferComBox
                label2.Visible = true;
                OfferComBox.Visible = true;
                //============================== Validations =============================//

                // Validation  1: Check if required fields are not empty
                if (string.IsNullOrWhiteSpace(CategoryTxtBox.Text))
                {
                    MessageBox.Show("fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Validation 2: Check for unique product names excluding the current product
                int pro_ID = selectedProduct.Id;
                var existingNamee = DBConnection.Context.Categories.Any(c => c.Cat_Name == CategoryTxtBox.Text && c.Cat_ID != pro_ID);
                if (existingNamee)
                {
                    MessageBox.Show("The Category name already exists. Please enter a unique name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 3: Ensure the name contains only letters and white spaces
                if (!Regex.IsMatch(CategoryTxtBox.Text, "^[a-zA-Z\\s]+$"))
                {
                    MessageBox.Show("Product Name must contain only letters and white spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //============================== Logic =============================//

                // Retrieve the existing category from the database
                int categoryId = selectedProduct.Id;
                Category updatedCategory = DBConnection.Context.Categories.Find(categoryId);

                // Retrieve the selected offer from the combo box
                string selectedOfferName = OfferComBox.Text;

                // Explicitly load the Offer navigation property
                DBConnection.Context.Entry(updatedCategory).Reference(c => c.Offer).Load();

                // Check if "No Offer assigned" is selected
                Offer selectedOfferObject = selectedOfferName == "No Offer assigned"
                    ? null
                    : DBConnection.Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName);

                // Update the properties of the existing category with the values from the form
                updatedCategory.Cat_Name = CategoryTxtBox.Text;
                updatedCategory.Offer = selectedOfferObject;

                // Save changes to the database
                DBConnection.Context.SaveChanges();

                // Apply changes to products of the updated category
                UpdateProductsForCategory(updatedCategory);

                // Refresh DataGridView after editing the category
                C.LoadData();
                // Close the form or perform any additional actions
                this.Close();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        // Method to update products of the specified category
        private void UpdateProductsForCategory(Category category)
        {
            try { 
            var productsToUpdate = DBConnection.Context.Products
                .Where(p => p.Category.Cat_ID == category.Cat_ID /*&& p.Offer != null*/)
                .ToList();

            foreach (var product in productsToUpdate)
            {
                // Update or modify properties of each product if needed
                product.Offer = category.Offer;
            }

            // Save changes to the database
            DBConnection.Context.SaveChanges();
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}





