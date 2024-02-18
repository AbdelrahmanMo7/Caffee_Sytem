using Cafffe_Sytem.A.M.A;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cafffe_Sytem.D.M.M
{
    public partial class CahierForm : Templete
    {
      

        public CahierForm()
        {
            InitializeComponent();
           
            InitializeForm();
            PopulateComboBox();
        }

        private void InitializeForm()
        {
            LoadUserBillsTotal();
        }

        private void LoadUserBillsTotal()
        {
            var userBillsTotal = DBConnection.Context.Users
     .Where(user => user.U_UserName.Contains("cashier"))
     .Select(user => new
     {
         CashierID = user.U_ID,
         CashierName = user.U_Name,
         BillsCount = user.Bills.Select(b=>b).Count(), // Use nullable double
         TotalAmount = user.Bills.Sum(b => (double?)b.B_Total_Amount) // Use nullable double
     })
     .ToList();
            dataGridView1.DataSource = userBillsTotal;
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
    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the value of the UserID column from the clicked row
                object userIdCellValue = dataGridView1.Rows[e.RowIndex].Cells["CashierID"].Value;

                // Check if the value is not null and can be parsed to an integer
                if (userIdCellValue != null && int.TryParse(userIdCellValue.ToString(), out int selectedUserId))
                {
                    using (var dbContext = DBConnection.Context)
                    {
                        try
                        {
                            // Query bills for the selected user
                            var userBillsDetails = from bill in dbContext.Bills
                                                   where bill.User.U_ID == selectedUserId
                                                   select new
                                                   {
                                                       BillID = bill.B_ID,
                                                       Time = bill.B_Time,
                                                       Date = bill.B_Date,
                                                       Total = bill.B_Total_Amount,
                                                       TableNumber = bill.B_Table_Num,
                                                       IsDeleted = bill.B_IsDeleted_,
                                                       UserID = bill.User.U_ID,
                                                       ClintID = bill.Creater_Id,
                                                       // Include other bill details as needed
                                                   };

                            // Create a new instance of CashierDetailsBills
                            Cafffe_Sytem.D.M.M.CashierDetailsBills cashierDetailsBills = new CashierDetailsBills();

                            // Bind the query result to the DataGridView in CashierDetailsBills
                            cashierDetailsBills.dataGridView1.DataSource = userBillsDetails.ToList();

                            // Show the CahierForm
                            cashierDetailsBills.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error retrieving bills for user: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid user ID value.");
                }
            }
        }

        private void init()
        {
            var x = DBConnection.Context.Users.Select(c => new { c.U_ID, c.U_Name, c.U_UserName, c.U_IsAdmin_ }).ToList();
            dataGridView1.DataSource = x;
        }

        private void PopulateComboBox()
        {
            comboBox1.Items.Clear(); // Clear existing items in the ComboBox

            // Add "All Users" option as the first item in the ComboBox
            comboBox1.Items.Add("All Users");

            // Get unique usernames from the Clients table
            var uniqueUsernames = DBConnection.Context.Users.Select(c => c.U_UserName).Distinct().ToList();

            // Add unique usernames to the ComboBox
            comboBox1.Items.AddRange(uniqueUsernames.ToArray());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected username from ComboBox
            string selectedUsername = comboBox1.SelectedItem.ToString();

            // Filter clients based on the selected username
            var filteredClients = DBConnection.Context.Users.AsQueryable(); // Start with all clients

            if (selectedUsername != "All Users")
            {
                // If a specific user is selected, filter by username
                filteredClients = filteredClients.Where(c => c.U_UserName == selectedUsername);
            }

            // Update DataGridView with filtered results
            var filteredClientList = filteredClients.Select(c => new { c.U_ID, c.U_Name,c.U_UserName ,c.U_IsAdmin_ }).ToList();
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
                    var filteredClients = DBConnection.Context.Users
                        .Where(c => c.U_UserName.ToLower().Contains(enteredText))
                        .Select(c => new { c.U_ID, c.U_Name, c.U_UserName, c.U_IsAdmin_ })
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
