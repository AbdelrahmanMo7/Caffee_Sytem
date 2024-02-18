using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cafffe_Sytem.ASH;
using System.Text.RegularExpressions;

namespace Cafffe_Sytem.ASH
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
            this.P = AP;
            LoadCategoryCombo();
            LoadOfferCombo();
            this.selectedProduct = selectedProduct;


            // Populate the form with existing product details
            ProductNameTxt.Text = selectedProduct.Name;
            ProductQuantityTxt.Text = selectedProduct.Quantity.ToString();
            ProductPriceTxt.Text = selectedProduct.Price.ToString();
            ProductCodeTxt.Text = selectedProduct.Code;
            CategoryComBox.Text = selectedProduct.Category;
            OfferComBox.Text= selectedProduct.Offer;
            //ProductNameTxt.Text = selectedProduct.P_Name;
            //ProductQuantityTxt.Text = selectedProduct.P_Quantity.ToString();
            //ProductPriceTxt.Text = selectedProduct.P_Price.ToString();
            //ProductCodeTxt.Text = selectedProduct.P_Code;
        }


        public void LoadCategoryCombo()
        {
            using (Coffee_SystemEntities Context = new Coffee_SystemEntities())
            {
                var Q3 = (from e in Context.Categories
                          select e.Cat_Name).ToList();
                // Set the DataSource to the list of categories
                CategoryComBox.DataSource = Q3;
            }
        }
        public void LoadOfferCombo()
        {
            using (Coffee_SystemEntities Context = new Coffee_SystemEntities())
            {
                var Q4 = (from e in Context.Offers
                          select e.Off_Name).ToList();
                // Set the DataSource to the list of offers
                OfferComBox.DataSource = Q4;
            }
        }

        //============================== Events =========================//

        private void button1_Click(object sender, EventArgs e)
        {
            using (Coffee_SystemEntities Context = new Coffee_SystemEntities())
            {
                if (AddBtn.Text == "Add")
                {
                    // Validation 1: Check for unique product names
                    var existingName = Context.Products.Any(p => p.P_Name == ProductNameTxt.Text);
                    if (existingName)
                    {
                        MessageBox.Show("The product name already exists. Please enter a unique name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Validation 2: Ensure the name contains only letters and white spaces
                    if (!Regex.IsMatch(ProductNameTxt.Text, "^[a-zA-Z\\s]+$"))
                    {
                        MessageBox.Show("Product Name must contain only letters and white spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation  3: Check if required fields are not empty
                    if (string.IsNullOrWhiteSpace(ProductNameTxt.Text) || string.IsNullOrWhiteSpace(ProductQuantityTxt.Text) ||
                        string.IsNullOrWhiteSpace(ProductPriceTxt.Text) || string.IsNullOrWhiteSpace(ProductCodeTxt.Text))
                    {
                        MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    // Validation  5: Check for unique product codes
                    var existingProduct = Context.Products.FirstOrDefault(p => p.P_Code == ProductCodeTxt.Text);
                    if (existingProduct != null)
                    {
                        MessageBox.Show("The product code already exists. Please enter a unique code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation  6: Ensure positive quantities
                    if (quantity <= 0)
                    {
                        MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation   7: Check for minimum and maximum length
                    if (ProductNameTxt.Text.Length < 3 || ProductNameTxt.Text.Length > 50)
                    {
                        MessageBox.Show("Product Name must be between  3 and  50 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation   8: Validate price range
                    if (price <= 0)
                    {
                        MessageBox.Show("Price cannot be negative or Zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation   9: Check for alphanumeric characters in product code
                    if (!Regex.IsMatch(ProductCodeTxt.Text, @"^[a-zA-Z0-9]+$"))
                    {
                        MessageBox.Show("Product Code must contain only alphanumeric characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }



                    // Retrieve the selected category from the combo box
                    string selectedCategory = CategoryComBox.Text;

                    // Retrieve the corresponding Category object from the same DbContext
                    Category selectedCategoryObject = Context.Categories.FirstOrDefault(cat => cat.Cat_Name == selectedCategory);

                    // Retrieve the selected offer from the combo box
                    string selectedOfferName = OfferComBox.Text;

                    // Retrieve the corresponding Offer object from the same DbContext
                    Offer selectedOfferObject = Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName);

                    // Create the new product with the selected category and offer
                    Product newProduct = new Product
                    {
                        P_Name = ProductNameTxt.Text,
                        P_Quantity = Convert.ToInt32(ProductQuantityTxt.Text),
                        P_Price = Convert.ToDouble(ProductPriceTxt.Text),
                        P_Code = ProductCodeTxt.Text,
                        Category = selectedCategoryObject,
                        Offer= selectedOfferObject
                    };

                    // Add the new product to the same DbContext and save changes
                    Context.Products.Add(newProduct);
                    Context.SaveChanges();
                    // Refresh ProductsPage
                    P.LoadData();

                    // Close the form or perform any additional actions
                    this.Close();
                    return;
                }
                if (AddBtn.Text == "Update")
                {
                    int productId = selectedProduct.Id;
                    // Retrieve the existing product from the database
                    Product existingProduct = Context.Products.Find(productId);


                    // Retrieve the selected category from the combo box
                    string selectedCategory = CategoryComBox.Text;

                    // Retrieve the corresponding Category object from the same DbContext
                    Category selectedCategoryObject = Context.Categories.FirstOrDefault(cat => cat.Cat_Name == selectedCategory);

                    // Retrieve the selected offer from the combo box
                    string selectedOfferName = OfferComBox.Text;

                    // Retrieve the corresponding Offer object from the same DbContext
                    Offer selectedOfferObject = Context.Offers.FirstOrDefault(offer => offer.Off_Name == selectedOfferName);

                    if (existingProduct != null)
                    {
                        // Update the properties of the existing product with the values from the form
                        existingProduct.P_Name = ProductNameTxt.Text;
                        existingProduct.P_Quantity = Convert.ToInt32(ProductQuantityTxt.Text);
                        existingProduct.P_Price = Convert.ToDouble(ProductPriceTxt.Text);
                        existingProduct.P_Code = ProductCodeTxt.Text;
                        existingProduct.Category = selectedCategoryObject;
                        existingProduct.Offer = selectedOfferObject;
                        // Save changes to the database
                        Context.SaveChanges();

                        // Refresh DataGridView after editing the product
                        P.LoadData();
                        // Close the form or perform any additional actions
                        this.Close();
                    }
                    return;
                }
            }

            // Update :

        }
    }
}
