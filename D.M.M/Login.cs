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
    public partial class Login : Form
    {
        Coffee_SystemEntities dbContext = new Coffee_SystemEntities();

        public Login()
        {
            InitializeComponent();
            passward_txt.PasswordChar = '*';
        }

        private void exist_labl_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    

        private void login_btn_MouseLeave(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.DodgerBlue;
        }

        private void login_btn_MouseMove(object sender, MouseEventArgs e)
        {
            login_btn.BackColor = Color.DarkCyan;
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
         
            var use=username_txt.Text.Trim();
            var pass= passward_txt.Text.Trim();
           
            var q= dbContext.Users.Where(c => c.U_UserName ==use).Select(c => c).FirstOrDefault();

            if (q != null)
            {
                //var q2 = dbContext.Users.Where(c => c.U_PassWord == pass).Select(c => c);
               if(q.U_PassWord ==pass)
                {
                    if (q.U_IsAdmin_ == true)
                    {
 CahierForm cahierForm = new CahierForm();
                        cahierForm.Show();
                    }
                    else
                    {

                    }
                    
                }
                else
                {
                    MessageBox.Show("Invalid Passward ");
                }
            }

            else
            {
                MessageBox.Show("Invalid UserName ");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void show_passward_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // Check if the CheckBox is checked
            if (show_passward_checkBox.Checked)
            {
                // If checked, set the PasswordChar property of the TextBox to '\0' (null character)
                passward_txt.PasswordChar = '\0';
            }
            else
            {
                // If not checked, set the PasswordChar property of the TextBox back to '*'
                passward_txt.PasswordChar = '*';
            }
        }

        
    }
}
