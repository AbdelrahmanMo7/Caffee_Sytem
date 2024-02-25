using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Steven
{
    public partial class UpdateUsers : Form
    {

        public string id { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string position { get; set; }
        public UpdateUsers()
        {
            InitializeComponent();
        }

        private void UpdateUsers_Load(object sender, EventArgs e)
        {
            nametxt.Text = name;
            usernametxt.Text = UserName;
            passwordtxt.Text = password;
            if (position == "Admin")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nametxt.Text == "")
                MessageBox.Show("Name is required");
            else if (usernametxt.Text == "")
                MessageBox.Show("UserName is required");
            else if (passwordtxt.Text == "")
                MessageBox.Show("Password is required");
            else if (passwordtxt.Text.Length <= 8)
                MessageBox.Show("Password must be 8 digits or more");
            else
            {
                int x = int.Parse(id);
                User user = DBConnection.Context.Users.Find(int.Parse(id));
                user.U_Name = nametxt.Text;
                user.U_UserName = usernametxt.Text;
                user.U_PassWord = passwordtxt.Text;
                if (checkBox1.Checked)
                    user.U_IsAdmin_ = true;
                else
                    user.U_IsAdmin_ = false;
                DBConnection.Context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                DBConnection.Context.SaveChanges();
                MessageBox.Show("User Updated");
                Managment managment = new Managment();
                managment.Update();
                managment.Refresh();
                this.Close();
            }
        }
    }
}
