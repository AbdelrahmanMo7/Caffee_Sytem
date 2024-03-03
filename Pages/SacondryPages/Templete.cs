
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
    public partial class Templete : Form
    {

        public Templete()
        {
            InitializeComponent();
            try { 
            UserName_label1.Text = Login.Current_User.U_Name;
            if (Login.Current_User.U_IsAdmin_ == true)
            {
                Is_Admin_label2.Text = "Admin";
            }
            else
            {
                Is_Admin_label2.Text = "Cashier";
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        #region SideBar
        protected bool isexpend =true;

      

        protected void SideBar_btn_Click(object sender, EventArgs e)
        {
            try { 
            //SideBar_timer.Start();
            if (isexpend)
            {
                isexpend = false;
                sidebar_layout.Width = 55;
                SideBar_btn.Image = Cafffe_Sytem.Properties.Resources.icons8_menu_36;
                SideBar_btn.Refresh();
            }
            else
            {
                sidebar_layout.Width = 215;
                isexpend = true;
                SideBar_btn.Image = Cafffe_Sytem.Properties.Resources.icons8_activity_feed_33;
                SideBar_btn.Refresh();
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sButton11_Click(object sender, EventArgs e)
        {
           
            Offers offers = new Offers();
            offers.Show();
          
            this.Hide();

        }

        private void sButton2_Click(object sender, EventArgs e)
        {
            Reports_Page reports_Page = new Reports_Page();
            reports_Page.Show();
           
            this.Hide();
        }

        private void sButton8_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();
            
            this.Hide();
        }

        private void sButton6_Click(object sender, EventArgs e)
        {
            CashierForm form = new CashierForm();   
            form.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            Login login =new Login();
            login.Show();
            this.Hide();
        }

        private void sButton10_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();  
            dashboard.Show();
            this.Hide();
        }

        private void sButton9_Click(object sender, EventArgs e)
        {
            ProductPage products= new ProductPage();
            products.Show();
            this.Hide();
            
        }

        private void sButton3_Click(object sender, EventArgs e)
        {
            CategoryPage categories= new CategoryPage();
            categories.Show();
            this.Hide();
        }

        private void sButton5_Click(object sender, EventArgs e)
        {
            Employees employee = new Employees();
            employee.Show();
            this.Hide();
        }

        private void sButton7_Click(object sender, EventArgs e)
        {
            Bills bills = new Bills();
            bills.Show();
            this.Hide();
        }

        private void sButton1_Click(object sender, EventArgs e)
        {
            Managment managment = new Managment();
            managment.Show();
            this.Hide();
        }
    }
}
