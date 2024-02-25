using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.D.M.M
{
    
    
    public partial class MangeClients : Form
    {
        public event EventHandler eva;
        int selectedId;

      
        public MangeClients(string pagetype)
        {
            InitializeComponent();
            if (pagetype == "Add")
            {
                update_btn.Visible = false;
                button1.Visible=true;
                pagetitle_labl.Text = "Add Client";
            }
            else
            {
                update_btn.Visible = true;
                button1.Visible = false;
                pagetitle_labl.Text = "Update Client ";
            }
          
        }

       

        // Define the InitializeForUpdate method
        public void InitializeForUpdate(int clintID, string clintName, string clintAddress, string clintPhone)
        {
            // Update the form controls with the provided client information
            

            selectedId = clintID;
            name_txt.Text = clintName;
            phone_txt.Text = clintPhone;
            address_txt.Text = clintAddress;
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            string clintName = name_txt.Text;
            string clintAddress = address_txt.Text;
            long clintPhone = long.Parse(phone_txt.Text);
            var Item = DBConnection.Context.Clients.Where(c => c.C_Phone_Number == clintPhone).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(clintName))
            {
                MessageBox.Show("Please enter client name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(phone_txt.Text))
            {
                MessageBox.Show("Please enter client phone number.");
                return;
            }
            if (phone_txt.Text.Length < 5)
            {
                MessageBox.Show("Please enter Valiad numbers more than 5 numbers .");
                return;
            }
           
            if (Item != null&&Item.C_ID!= selectedId)
            {
                MessageBox.Show($"This Phone number is already existed for client {Item.C_Name}. please change this number");
                return;
            }

            // Retrieve the Clint entity from the database
            Client q3 = DBConnection.Context.Clients.FirstOrDefault(c => c.C_ID == selectedId);

            // Update the properties of the retrieved Clint entity
            if (q3 != null)
            {
                q3.C_Name = clintName;
                q3.C_Phone_Number =long.Parse( clintPhone.ToString());
                q3.C_Address = clintAddress;

                // Save changes to the database
                DBConnection.Context.Entry(q3).State = EntityState.Detached;
            }
            DBConnection.Context.Clients.Attach(q3);
            DBConnection.Context.Entry(q3).State = EntityState.Modified;
            DBConnection.Context.SaveChanges();
            MessageBox.Show("update Successfull");

            eva?.Invoke(this,e);
            this.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

         if (string.IsNullOrWhiteSpace(name_txt.Text))
            {
                MessageBox.Show("Please enter client name.");
                return;
            }
         if (string.IsNullOrWhiteSpace(phone_txt.Text))
            {
                MessageBox.Show("Please enter client phone number.");
                return;
            }
            if (phone_txt.Text.Length < 5)
            {
                MessageBox.Show("Please enter Valiad numbers more than 5 numbers .");
                return;
            }
            long clintPhone = long.Parse(phone_txt.Text);
            var Item = DBConnection.Context.Clients.Where(c => c.C_Phone_Number == clintPhone).FirstOrDefault();
            if (Item != null)
            {
                MessageBox.Show($"This Phone number is already existed for client {Item.C_Name}. please change this number");
                return;
            }
           
            Client clint = new Client()
            {
                C_Name = name_txt.Text,
                C_Address = address_txt.Text,
                C_Phone_Number = long.Parse(phone_txt.Text)
            };
            DBConnection.Context.Clients.Add(clint);
            DBConnection.Context.SaveChanges();
            MessageBox.Show("add Successfull");
            eva?.Invoke(this, e);
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MangeClients_Load(object sender, EventArgs e)
        {

        }

        private void phone_txt_TextChanged(object sender, EventArgs e)
        {

            if (Regex.IsMatch(phone_txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                phone_txt.Text = phone_txt.Text.Remove(phone_txt.Text.Length - 1);
            }
        }
    }
}
