using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using Cafffe_Sytem.Pages.SacondryPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages
{
    public partial class Managment : Templete
    {
        int Selected_ID;
      
        public Managment()
        {
            InitializeComponent();
        }

        private void Reports_conteinar_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            if (e.RowIndex <= -1)
                return;
            DataGridViewRow row = Users_dataGridView.Rows[e.RowIndex];
            selectedUserNametxt.Text = row.Cells[1].Value.ToString();
            Selected_ID = int.Parse( row.Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Managment_Load(object sender, EventArgs e)
        {
            try { 
            string admin;
            List<User> users = DBConnection.Context.Users.Select(p => p).ToList();
            foreach (User item in users)
            {
                if (item.U_IsAdmin_ == true)
                    admin = "Admin";
                else
                    admin = "Cacher";
                Users_dataGridView.Rows.Add(item.U_ID, item.U_Name, item.U_UserName, item.U_PassWord, admin);
            }
            //system info display
            Sys_Info info = (from d in DBConnection.Context.Sys_Info select d).FirstOrDefault();
            Nametxt.Text = info.Coffee_Name;
            phonetxt.Text = info.Coffee_Phone_Number.ToString();
            addresstxt.Text = info.Coffee_Address;
            apointmenttxt.Text = info.Coffee_Apointment;
            facebooktxt.Text = info.Coffee_FaceBook_Link;
            instatxt.Text = info.Coffee_Insta_Link;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try { 
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
            Users_dataGridView.Rows.Clear();
            Managment_Load(this, e);
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
           try{if (selectedUserNametxt.Text == "")
                MessageBox.Show("Must Select User To Update");
            else
            {
                User row = DBConnection.Context.Users.Find(Selected_ID);
                UpdateUsers updateUsers = new UpdateUsers();
                updateUsers.id = row.U_ID.ToString();
                updateUsers.name = row.U_Name;
                updateUsers.UserName = row.U_UserName;
                updateUsers.password = row.U_PassWord;
                if (row.U_IsAdmin_ == true)
                    updateUsers.position = "Admin";
                else
                    updateUsers.position = "Cashier";

                updateUsers.ShowDialog();
                Users_dataGridView.Rows.Clear();
                Managment_Load(this,e);
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try { 
            if (selectedUserNametxt.Text == "")
                MessageBox.Show("Must Select User To Remove");
            else
            {
                
                
                DBConnection.Context.Users.Remove(DBConnection.Context.Users.Find(Selected_ID));
                try
                {
                    DBConnection.Context.SaveChanges();
                    MessageBox.Show("User Removed");
                }
                catch(Exception  ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       
        private void editInfobtn_Click(object sender, EventArgs e)
        {
            try { 
            editInfobtn.Visible = false;
            Savebtn.Visible = true;
            Nametxt.ReadOnly = false;
            phonetxt.ReadOnly = false;
            addresstxt.ReadOnly = false;
            apointmenttxt.ReadOnly = false;
            facebooktxt.ReadOnly = false;
            instatxt.ReadOnly = false;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try { 
            if (Nametxt.Text == "")
                MessageBox.Show("Name is required");
            else if (phonetxt.Text == "")
                MessageBox.Show("Phone number is required");
            //else if (!Regex.Match(phonetxt.Text, @"^(\+[0-9])$").Success)
            //    MessageBox.Show("Phone number must be numbers only");
            else { 
                Sys_Info info = (from d in DBConnection.Context.Sys_Info select d).FirstOrDefault();
                info.Coffee_Name = Nametxt.Text;
                info.Coffee_Phone_Number = long.Parse(phonetxt.Text);
                info.Coffee_Address = addresstxt.Text;
                info.Coffee_Apointment = apointmenttxt.Text;
                info.Coffee_FaceBook_Link = facebooktxt.Text;
                info.Coffee_Insta_Link = instatxt.Text;
                DBConnection.Context.Entry(info).State = System.Data.Entity.EntityState.Modified;
                DBConnection.Context.SaveChanges();
                MessageBox.Show("System Information Updated");
                editInfobtn.Visible = true;
                Savebtn.Visible = false;
                Nametxt.ReadOnly = true;
                phonetxt.ReadOnly = true;
                addresstxt.ReadOnly = true;
                apointmenttxt.ReadOnly = true;
                facebooktxt.ReadOnly = true;
                instatxt.ReadOnly = true;
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {
           try{ string admin;
            if (string.IsNullOrWhiteSpace(searchtxt.Text))
            {
                List<User> users = DBConnection.Context.Users.Select(u => u).ToList();
               
                    Users_dataGridView.Rows.Clear();
                    foreach (User user in users)
                    {
                        if (user.U_IsAdmin_ == true)
                            admin = "Admin";
                        else
                            admin = "Cacher";

                        Users_dataGridView.Rows.Add(user.U_ID, user.U_Name, user.U_UserName, user.U_PassWord, admin);
                    }
                
            }
               
            else
            {
               List< User> users = DBConnection.Context.Users.Where(u => u.U_UserName.Contains( searchtxt.Text)).ToList();
                if (users == null)
                    MessageBox.Show("Not Found");
                else
                {
                    Users_dataGridView.Rows.Clear();
                    foreach (User user in users)
                    {
                        if (user.U_IsAdmin_ == true)
                            admin = "Admin";
                        else
                            admin = "Cacher";
                        
                        Users_dataGridView.Rows.Add(user.U_ID, user.U_Name, user.U_UserName, user.U_PassWord, admin);
                    }
                }

            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
