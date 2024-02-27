using Cafffe_Sytem.CustomModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Cafffe_Sytem.Models;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class AddProductPage : Form
    {
        private ProductPage P;
        private int productId;
        private dynamic selectedProduct;

        // Constructor for adding a new product
        public AddProductPage(ProductPage AP)
        {
            InitializeComponent();
            this.P = AP;
            LoadCategoryCombo();
            LoadOfferCombo();
        }
        // Constructor for updating an existing product
        public AddProductPage(ProductPage AP, dynamic selectedProduct)
        {
            InitializeComponent();
            this.AddBtn.Text = "Update";
            this.AddBtn.BackColor = Color.FromArgb(11, 96, 176);
            this.P = AP;
            LoadCategoryCombo();
            LoadOfferCombo();
            this.selectedProduct = selectedProduct;


            // Populate the form with existing product details
            ProductNameTxt.Text = selectedProduct.Name;
            ProductQuantityTxt.Text = selectedProduct.Quantity.ToString();
            ProductPriceTxt.Text = selectedProduct.Price.ToString();
            ProductCodeTxt.Text = selectedProduct.Code;

            if (selectedProduct.Category == null)
                CategoryComBox.SelectedItem = "No Category assigned";
            else
                CategoryComBox.SelectedItem = selectedProduct.Category;
            if (selectedProduct.Offer == null)
                OfferComBox.SelectedItem = "No Offer assigned";
            else
                OfferComBox.SelectedItem = selectedProduct.Offer;


            //ProductNameTxt.Text = selectedProduct.P_Name;
            //ProductQuantityTxt.Text = selectedProduct.P_Quantity.ToString();
            //ProductPriceTxt.Text = selectedProduct.P_Price.ToString();
            //ProductCodeTxt.Text = selectedProduct.P_Code;
        }


        public void LoadCategoryCombo()
        {
            //using (Coffee_SystemEntities Context = new Coffee_SystemEntities())

            var Q3 = (from e in DBConnection.Context.Categories
                      select e.Cat_Name).ToList();
            Q3.Add("No Category assigned");

            // Set the DataSource to the list of categories
            CategoryComBox.DataSource = Q3;

        }
        public void LoadOfferCombo()
        {
            List<string> Q4 = new List<string>();
            
            Q4.AddRange((from e in DBConnection.Context.Offers
                         select e.Off_Name).ToList());
            Q4.Add("No Offer assigned");

            // Set the DataSource to the list of offers
            OfferComBox.DataSource = Q4;

        }

        //============================== Events =========================//

        private void AddBtn_Click(object sender, EventArgs e)
        {
            //using (Coffee_SystemEntities Context = DBConnection.Context)

            if (AddBtn.Text == "Add")
            {
                // Validation  1: Check if required fields are not empty
                if (string.IsNullOrWhiteSpace(ProductNameTxt.Text) || string.IsNullOrWhiteSpace(ProductQuantityTxt.Text) ||
                    string.IsNullOrWhiteSpace(ProductPriceTxt.Text) || string.IsNullOrWhiteSpace(ProductCodeTxt.Text))
                {
                    MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 2: Check for unique product names
                var existingName = DBConnection.Context.Products.Any(p => p.P_Name == ProductNameTxt.Text);
                if (existingName)
                {
                    MessageBox.Show("The product name already exists. Please enter a unique name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 3: Ensure the name contains only letters and white spaces
                if (!Regex.IsMatch(ProductNameTxt.Text, "^[a-zA-Z\\s]+$"))
                {
                    MessageBox.Show("Product Name must contain only letters and white spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation  4: Validate numeric inputs
                int quantity;
                double price;
                if (!int.TryParse(ProductQuantityTxt.Text, out quantity) || !double.TryParse(ProductPriceTxt.Text, out price))
                {
                    MessageBox.Show("Quantity and Price must be valid numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation  5: Ensure positive quantities
                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation   6: Validate price range
                if (price <= 0)
                {
                    MessageBox.Show("Price cannot be negative or Zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation  7: Check for unique product codes
                var existingProduct = DBConnection.Context.Products.FirstOrDefault(p => p.P_Code == ProductCodeTxt.Text);
                if (existingProduct != null)
                {
                    MessageBox.Show("The product code already exists. Please enter a unique code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation   8: Check for alphanumeric characters in product code
                if (!Regex.IsMatch(ProductCodeTxt.Text, @"^[a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("Product Code must contain only alphanumeric characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //============================= Seconday =========================//

                // Validation   9: Check for minimum and maximum length
                if (ProductNameTxt.Text.Length < 3 || ProductNameTxt.Text.Length > 50)
                {
                    MessageBox.Show("Product Name must be between  3 and  50 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                

                //============================== Logic =============================//


                // Retrieve the selected category from the combo box
                string selectedCategoryName = CategoryComBox.Text;

                // Retrieve the corresponding Category object from the same DbContext
                Category selectedCategoryObject = DBConnection.Context.Categories.FirstOrDefault(cat => cat.Cat_Name == selectedCategoryName || (cat.Cat_Name == null && selectedCategoryName == "No Category assigned"));

                // Retrieve the selected offer from the combo box
                string selectedOfferName = OfferComBox.Text;

                // Retrieve the corresponding Offer object from the same DbContext
                Offer selectedOfferObject = DBConnection.Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName || (offer.Off_Name == null && selectedOfferName == "No Offer assigned")); 

                // Create the new product with the selected category and offer
                Product newProduct = new Product
                {
                    P_Name = ProductNameTxt.Text,
                    P_Quantity = Convert.ToInt32(ProductQuantityTxt.Text),
                    P_Price = Convert.ToDouble(ProductPriceTxt.Text),
                    P_Code = ProductCodeTxt.Text,
                    Category = selectedCategoryObject,
                    Offer = selectedOfferObject
                };

                // Add the new product to the same DbContext and save changes
                DBConnection.Context.Products.Add(newProduct);
                DBConnection.Context.SaveChanges();
                // Refresh ProductsPage
                P.LoadData();

                // Close the form or perform any additional actions
                this.Close();
                return;
            }

            //================================= Update ================================//

            if (AddBtn.Text == "Update")
            {
                // Validation  1: Check if required fields are not empty
                if (string.IsNullOrWhiteSpace(ProductNameTxt.Text) || string.IsNullOrWhiteSpace(ProductQuantityTxt.Text) ||
                    string.IsNullOrWhiteSpace(ProductPriceTxt.Text) || string.IsNullOrWhiteSpace(ProductCodeTxt.Text))
                {
                    MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Validation 2: Check for unique product names excluding the current product
                int pro_ID = selectedProduct.Id;
                var existingName = DBConnection.Context.Products.Any(p => p.P_Name == ProductNameTxt.Text && p.P_ID != pro_ID);
                if (existingName)
                {
                    MessageBox.Show("The product name already exists. Please enter a unique name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 3: Ensure the name contains only letters and white spaces
                if (!Regex.IsMatch(ProductNameTxt.Text, "^[a-zA-Z\\s]+$"))
                {
                    MessageBox.Show("Product Name must contain only letters and white spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation  4: Validate numeric inputs
                int quantity;
                double price;
                if (!int.TryParse(ProductQuantityTxt.Text, out quantity) || !double.TryParse(ProductPriceTxt.Text, out price))
                {
                    MessageBox.Show("Quantity and Price must be valid numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation  5: Ensure positive quantities
                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation   6: Validate price range
                if (price <= 0)
                {
                    MessageBox.Show("Price cannot be negative or Zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validation 7: Check for unique product codes excluding the current product
                var existingProductCode = DBConnection.Context.Products.Any(p => p.P_Code == ProductCodeTxt.Text && p.P_ID != pro_ID);

                if (existingProductCode)
                {
                    MessageBox.Show("The product code already exists. Please enter a unique code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Validation   8: Check for alphanumeric characters in product code
                if (!Regex.IsMatch(ProductCodeTxt.Text, @"^[a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("Product Code must contain only alphanumeric characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //============================= Seconday =========================//

                // Validation   9: Check for minimum and maximum length
                if (ProductNameTxt.Text.Length < 3 || ProductNameTxt.Text.Length > 50)
                {
                    MessageBox.Show("Product Name must be between  3 and  50 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                

                //============================== Logic =============================//

                // Retrieve the existing product from the database
                int productId = selectedProduct.Id;
                Product updatedProduct = DBConnection.Context.Products.Find(productId);

                // Retrieve the selected category from the combo box
                string selectedCategoryName = CategoryComBox.Text;

                // Retrieve the corresponding Category object from the same DbContext
                Category selectedCategoryObject = DBConnection.Context.Categories.FirstOrDefault(cat => cat.Cat_Name == selectedCategoryName); /*|| (cat.Cat_Name == null && selectedCategoryName == "No Category assigned"));*/

                // Retrieve the selected offer from the combo box
                string selectedOfferName = OfferComBox.Text;

                // Explicitly load the Category navigation property
                DBConnection.Context.Entry(updatedProduct).Reference(c => c.Category).Load();

                // Explicitly load the Offer navigation property
                DBConnection.Context.Entry(updatedProduct).Reference(c => c.Offer).Load();

                // Retrieve the corresponding Offer object from the same DbContext
                Offer selectedOfferObject = DBConnection.Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName); /*|| (offer.Off_Name == null && selectedOfferName == "No Offer assigned"));*/

                

                if (updatedProduct != null)
                {
                    // Update the properties of the existing product with the values from the form
                    updatedProduct.P_Name = ProductNameTxt.Text;
                    updatedProduct.P_Quantity = Convert.ToInt32(ProductQuantityTxt.Text);
                    updatedProduct.P_Price = Convert.ToDouble(ProductPriceTxt.Text);
                    updatedProduct.P_Code = ProductCodeTxt.Text;
                    updatedProduct.Category = selectedCategoryObject;
                    updatedProduct.Offer = selectedOfferObject;

                    // Save changes to the database
                    DBConnection.Context.SaveChanges();
                    // Refresh DataGridView after editing the product
                    P.LoadData();
                    // Close the form or perform any additional actions
                    this.Close();
                }
                return;
            }
        }

        private void AddProductPage_Load(object sender, EventArgs e)
        {

        }
    }
}
