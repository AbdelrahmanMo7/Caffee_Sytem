using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.D.M.M
{
    public partial class Offers:Templete
    {

        public Offers()
        {
            InitializeComponent();
            dbContext = new Coffee_SystemEntities();
            init();
        }


        private void search_btn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White; // Set background color to default
                }
            }

            // Get the search keyword from the search box
            string keyword = searchvalue_txt.Text.Trim().ToLower();

            // Ensure the keyword is not empty
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                bool matchFound = false;

                // Iterate through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Iterate through each cell in the current row
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        // Check if the cell value contains the keyword
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(keyword))
                        {
                            // Highlight the cell
                            cell.Style.BackColor = Color.Yellow;
                            matchFound = true; // Set flag to indicate at least one match found
                        }
                    }
                }

                if (!matchFound)
                {
                    // If no matches were found, show a message to the user
                    MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Display a message to the user if the search keyword is empty
                MessageBox.Show("Please enter a search keyword.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void refresh(object obj,EventArgs e)
        {
            init();
        }
        private void update_btn_Click(object sender, EventArgs e)
        {
            Cafffe_Sytem.D.M.M.MangeOffer mangeOffer = new Cafffe_Sytem.D.M.M.MangeOffer();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the data from the selected row
                var selectedRow = dataGridView1.SelectedRows[0];
                var offerOff_ID = (int)selectedRow.Cells["Off_ID"].Value;
                var offerOff_Name = selectedRow.Cells["Off_Name"].Value.ToString();
                var offerOff_Limit = selectedRow.Cells["Off_Limit"].Value.ToString();
                var offerOff_Start = selectedRow.Cells["Off_Start"].Value.ToString();
                var offerOff_End = selectedRow.Cells["Off_End"].Value.ToString();

                // Create an instance of the MangeClients form

                // Pass the data to the MangeClients form for updating
                mangeOffer.InitializeForUpdate(offerOff_ID, offerOff_Name, offerOff_Limit, offerOff_Start, offerOff_End);

                // Show the MangeClients form
                mangeOffer.Show();
                mangeOffer.eva += this.refresh;

                init();
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }
        private void add_btn_Click(object sender, EventArgs e)
        {
            Cafffe_Sytem.D.M.M.MangeOffer mangeOffer = new Cafffe_Sytem.D.M.M.MangeOffer();
            mangeOffer.Show();
            mangeOffer.eva += this.refresh;
        }

        private void delet_btn_Click(object sender, EventArgs e)

        {// Show the message box asking the user to confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);

            // Check if the user clicked OK to confirm deletion
            if (result == DialogResult.OK)
            {
                // User confirmed deletion
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the ID of the selected Clint
                    int selectedofferID = (int)dataGridView1.SelectedRows[0].Cells["Off_ID"].Value;

                    // Retrieve the corresponding Clint entity from the database
                    var selectedoffer = dbContext.Offers.FirstOrDefault(c => c.Off_ID == selectedofferID);

                    if (selectedoffer != null)
                    {
                        // Remove the selected Clint entity from the dbContext.Clints collection
                        dbContext.Offers.Remove(selectedoffer);

                        // Save changes to the database
                        dbContext.SaveChanges();

                        // Refresh the DataGridView to reflect the changes
                        init();
                    }
                    else
                    {
                        MessageBox.Show("Selected Clint not found in the database.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }

        }

        private void init()
        {
            var x = dbContext.Offers.Select(c => new { c.Off_ID, c.Off_Name, c.Off_Limit, c.Off_Start,c.Off_End }).ToList();
            dataGridView1.DataSource = x;
        }
        private void PopulateComboBox()
        {
            comboBox1.Items.Clear(); // Clear existing items in the ComboBox

            // Add "All Users" option as the first item in the ComboBox
            comboBox1.Items.Add("All offers");

            // Get unique usernames from the Clients table
            var uniqueUsernames = dbContext.Offers.Select(c => c.Off_Name).Distinct().ToList();

            // Add unique usernames to the ComboBox
            comboBox1.Items.AddRange(uniqueUsernames.ToArray());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected username from ComboBox
            string selectedUsername = comboBox1.SelectedItem.ToString();

            // Filter clients based on the selected username
            var filteredClients = dbContext.Offers.AsQueryable(); // Start with all clients

            if (selectedUsername != "All offers")
            {
                // If a specific user is selected, filter by username
                filteredClients = filteredClients.Where(c => c.Off_Name == selectedUsername);
            }

            // Update DataGridView with filtered results
            var filteredClientList = filteredClients.Select(c => new { c.Off_ID, c.Off_Name, c.Off_Limit, c.Off_Start,c.Off_End }).ToList();
            dataGridView1.DataSource = filteredClientList;
        }
        // Event handler for ComboBox's TextChanged event
        private void combobox_textchange(object sender, EventArgs e)
        {
            string enteredText = comboBox1.Text.Trim().ToLower(); // Get the entered text and convert to lowercase

            try
            {
                if (!string.IsNullOrEmpty(enteredText))
                {
                    // Filter clients whose names contain the entered text
                    var filteredClients = dbContext.Offers
                        .Where(c => c.Off_Name.ToLower().Contains(enteredText))
                        .Select(c => new { c.Off_ID, c.Off_Name, c.Off_Limit, c.Off_Start, c.Off_End })
                        .ToList();

                    // Update DataGridView with filtered results
                    dataGridView1.DataSource = filteredClients;
                }
                else
                {
                    // If no text is entered, display all clients
                    init();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filtering clients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
