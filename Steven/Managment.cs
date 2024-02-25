using Cafffe_Sytem.A.M.A;
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

namespace Cafffe_Sytem.Steven
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
            
            DataGridViewRow row = Users_dataGridView.Rows[e.RowIndex];
            selectedUserNametxt.Text = row.Cells[1].Value.ToString();
            Selected_ID = int.Parse( row.Cells[0].Value.ToString());
        }

        private void Managment_Load(object sender, EventArgs e)
        {
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

        private void addbtn_Click(object sender, EventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (selectedUserNametxt.Text == "")
                MessageBox.Show("Must Select User To Update");
            else
            {
                DataGridViewRow row = Users_dataGridView.Rows[Selected_ID];
                UpdateUsers updateUsers = new UpdateUsers();
                updateUsers.id = row.Cells[0].Value.ToString();
                updateUsers.name = row.Cells[1].Value.ToString();
                updateUsers.UserName = row.Cells[2].Value.ToString();
                updateUsers.password = row.Cells[3].Value.ToString();
                updateUsers.position = row.Cells[4].Value.ToString();

                updateUsers.ShowDialog();
                Users_dataGridView.Rows.Clear();
                Managment_Load(this,e);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
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

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            string admin;
            if (searchtxt.Text == "")
                MessageBox.Show("write user name to search for");
            else
            {
                User user = DBConnection.Context.Users.FirstOrDefault(u => u.U_UserName == searchtxt.Text);
                if (user == null)
                    MessageBox.Show("Not Found");
                else
                {
                    if (user.U_IsAdmin_ == true)
                        admin = "Admin";
                    else
                        admin = "Cacher";
                    Users_dataGridView.Rows.Clear();
                    Users_dataGridView.Rows.Add(user.U_ID, user.U_Name, user.U_UserName, user.U_PassWord, admin);
                }

            }
        }

        private void editInfobtn_Click(object sender, EventArgs e)
        {
            editInfobtn.Visible = false;
            Savebtn.Visible = true;
            Nametxt.ReadOnly = false;
            phonetxt.ReadOnly = false;
            addresstxt.ReadOnly = false;
            apointmenttxt.ReadOnly = false;
            facebooktxt.ReadOnly = false;
            instatxt.ReadOnly = false;
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (Nametxt.Text == "")
                MessageBox.Show("Name is required");
            else if (phonetxt.Text == "")
                MessageBox.Show("Phone number is required");
            else if (!Regex.Match(phonetxt.Text, @"^(\+[0-9])$").Success)
                MessageBox.Show("Phone number must be numbers only");
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
    }
}
