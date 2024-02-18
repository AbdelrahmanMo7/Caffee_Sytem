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
    public partial class Clients :Templete
    {

        public Clients()
        {
            InitializeComponent();
            PopulateComboBox();
            PopulateComboBoxphone();

            init();
        }
        private void PopulateComboBox()
        {
            comboBox1.Items.Clear(); // Clear existing items in the ComboBox

            // Add "All Users" option as the first item in the ComboBox
            comboBox1.Items.Add("All Users");

            // Get unique usernames from the Clients table
            var uniqueUsernames = dbContext.Clients.Select(c => c.C_Name).Distinct().ToList();

            // Add unique usernames to the ComboBox
            comboBox1.Items.AddRange(uniqueUsernames.ToArray());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected username from ComboBox
            string selectedUsername = comboBox1.SelectedItem.ToString();

            // Filter clients based on the selected username
            var filteredClients = dbContext.Clients.AsQueryable(); // Start with all clients

            if (selectedUsername != "All Users")
            {
                // If a specific user is selected, filter by username
                filteredClients = filteredClients.Where(c => c.C_Name == selectedUsername);
            }

            // Update DataGridView with filtered results
            var filteredClientList = filteredClients.Select(c => new { c.C_ID, c.C_Name, c.C_Address, c.C_Phone_Number }).ToList();
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
                    var filteredClients = dbContext.Clients
                        .Where(c => c.C_Name.ToLower().Contains(enteredText))
                        .Select(c => new { c.C_ID, c.C_Name, c.C_Address, c.C_Phone_Number })
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

        private void init()
        {
            var x = dbContext.Clients.Select(c => new { c.C_ID, c.C_Name, c.C_Address, c.C_Phone_Number }).ToList();
            dataGridView1.DataSource = x;
        }

        private void button3_Click(object sender, EventArgs e)

        {// Show the message box asking the user to confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);

            // Check if the user clicked OK to confirm deletion
            if (result == DialogResult.OK)
            {
                // User confirmed deletion
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the ID of the selected Clint
                    int selectedClintID = (int)dataGridView1.SelectedRows[0].Cells["C_ID"].Value;

                    // Retrieve the corresponding Clint entity from the database
                    var selectedClint = dbContext.Clients.FirstOrDefault(c => c.C_ID == selectedClintID);

                    if (selectedClint != null)
                    {
                        // Remove the selected Clint entity from the dbContext.Clints collection
                        dbContext.Clients.Remove(selectedClint);

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
    
        public void refresh(object obj,EventArgs e)
        {
            init();
        }
        private void update_btn_Click(object sender, EventArgs e)
        {
            Cafffe_Sytem.D.M.M.MangeClients mangeClients = new Cafffe_Sytem.D.M.M.MangeClients();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the data from the selected row
                var selectedRow = dataGridView1.SelectedRows[0];
                var clintID = (int)selectedRow.Cells["C_ID"].Value;
                var clintName = selectedRow.Cells["C_Name"].Value.ToString();
                var clintAddress = selectedRow.Cells["C_Address"].Value.ToString();
                var clintPhone = selectedRow.Cells["C_Phone_Number"].Value.ToString();

                // Create an instance of the MangeClients form
               
                // Pass the data to the MangeClients form for updating
                mangeClients.InitializeForUpdate(clintID, clintName, clintAddress, clintPhone);

                // Show the MangeClients form
                mangeClients.Show();
                mangeClients.eva += this.refresh;

                init();
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cafffe_Sytem.D.M.M.MangeClients mangeClients = new Cafffe_Sytem.D.M.M.MangeClients();
            mangeClients.Show();
            mangeClients.eva += this.refresh;
        }

        //private void search_btn_Click(object sender, EventArgs e)
        //{
        //    // Get the search keyword from the search box
        //    string keyword = textBox1.Text.Trim();

        //    // Ensure the keyword is not empty
        //    if (!string.IsNullOrWhiteSpace(keyword))
        //    {
        //        // Iterate through each row in the DataGridView
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            // Iterate through each cell in the current row
        //            foreach (DataGridViewCell cell in row.Cells)
        //            {
        //                // Check if the cell value contains the keyword
        //                if (cell.Value != null && cell.Value.ToString().Contains(keyword))
        //                {
        //                    // Highlight the row and scroll to it
        //                    dataGridView1.CurrentCell = cell;
        //                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
        //                    return; // Exit the method after finding the first match
        //                }
        //            }
        //        }

        //        // If the keyword is not found in any cell, show a message to the user
        //        MessageBox.Show("No matching record found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        // Display a message to the user if the search keyword is empty
        //        MessageBox.Show("Please enter a search keyword.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void search_btn_Click(object sender, EventArgs e)
        {
            // Clear previous highlights
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White; // Set background color to default
                }
            }

            // Get the search keyword from the search box
            string keyword = textBox1.Text.Trim().ToLower();

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
        private void phone_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected phone number from ComboBox
                string selectedPhoneNumberStr = phone_comboBox.SelectedItem?.ToString();

                if (selectedPhoneNumberStr != "All Users")
                {
                    // Convert the selected phone number from string to long
                    long selectedPhoneNumber;
                    if (long.TryParse(selectedPhoneNumberStr, out selectedPhoneNumber))
                    {
                        var filteredClients = dbContext.Clients.AsQueryable(); // Start with all clients

                        // If a specific phone number is selected, filter by phone number
                        filteredClients = filteredClients.Where(c => c.C_Phone_Number == selectedPhoneNumber);

                        // Update DataGridView with filtered results
                        var filteredClientList = filteredClients.Select(c => new { c.C_ID, c.C_Name, c.C_Address, c.C_Phone_Number }).ToList();
                        dataGridView1.DataSource = filteredClientList;
                    }
                    else
                    {
                        MessageBox.Show("Invalid phone number selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // If "All Users" is selected, display all clients
                    init(); // Assuming init() is a method that displays all clients
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filtering clients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void phone_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string enteredText = phone_comboBox.Text.Trim().ToLower();

                if (!string.IsNullOrEmpty(enteredText))
                {
                    // Convert the entered text from string to long
                   
                        // Filter clients whose phone numbers contain the entered text
                        var filteredClients = dbContext.Clients
                            .Where(c => c.C_Phone_Number.ToString().Contains( enteredText))
                            .Select(c => new { c.C_ID, c.C_Name, c.C_Address, c.C_Phone_Number })
                            .ToList();

                        // Update DataGridView with filtered results
                        dataGridView1.DataSource = filteredClients;
                    }
               
               
                else
                {
                    // If no text is entered, display all clients
                    phone_comboBox_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filtering clients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateComboBoxphone()
        {
            phone_comboBox.Items.Clear(); // Clear existing items in the ComboBox

            // Add "All Users" option as the first item in the ComboBox
            phone_comboBox.Items.Add("All Users");

            // Get unique phone numbers from the Clients table
            var uniquePhoneNumbers = dbContext.Clients.Select(c => c.C_Phone_Number).Distinct().ToList();

            // Add unique phone numbers to the ComboBox
            phone_comboBox.Items.AddRange(uniquePhoneNumbers.Select(p => p.ToString()).ToArray());
        }

    }
}