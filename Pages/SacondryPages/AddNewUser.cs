using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class AddNewUser : Form
    {
       
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            if (nametxt.Text == "")
                MessageBox.Show("Name is required");
            else if (usernametxt.Text == "")
                MessageBox.Show("UserName is required");
            else if (passwordtxt.Text == "")
                MessageBox.Show("Password is required");
            else if (passwordtxt.Text.Length <= 8)
                MessageBox.Show("Password must be 8 digits or more");
            else { 
                string name = nametxt.Text;
                string username = usernametxt.Text;
                string password = passwordtxt.Text;
                bool admin;
                if (checkBox1.Checked)
                    admin = true;
                else
                    admin = false;
                User user = new User() { U_Name = name,U_UserName = username,U_PassWord = password,U_IsAdmin_ = admin};
                DBConnection.Context.Users.Add(user);
                DBConnection.Context.SaveChanges();
                MessageBox.Show("User added");
                this.Close();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       
    }
}
