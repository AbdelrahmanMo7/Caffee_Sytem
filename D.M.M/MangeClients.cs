using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.D.M.M
{
    
    
    public partial class MangeClients : Form
    {
        public event EventHandler eva;

        

        public MangeClients()
        {
            InitializeComponent();
        }

       

        // Define the InitializeForUpdate method
        public void InitializeForUpdate(int clintID, string clintName, string clintAddress, string clintPhone)
        {
            // Update the form controls with the provided client information
            

            ID_txt.Text = clintID.ToString();
            name_txt.Text = clintName;
            phone_txt.Text = clintPhone;
            address_txt.Text = clintAddress;
        }

        private void update_btn_Click(object sender, EventArgs e)
        {

            int clintID = int.Parse(ID_txt.Text);
            string clintName = name_txt.Text;
            string clintAddress = address_txt.Text;
            decimal clintPhone = decimal.Parse(phone_txt.Text);

            // Retrieve the Clint entity from the database
            Client q3 = DBConnection.Context.Clients.FirstOrDefault(c => c.C_ID == clintID);

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
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

         

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
        }

        
    }
}
