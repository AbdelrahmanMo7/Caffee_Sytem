using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.A.SH
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        #region SideBar
        bool isexpend;

        private void SideBar_timer_Tick22(object sender, EventArgs e)
        {
            if (isexpend)
            {
                sidebar_layout.Width -= 10;
                if (sidebar_layout.Width == sidebar_layout.MinimumSize.Width)
                {
                    isexpend = false;
                    SideBar_timer.Stop();
                }
            }
            else
            {
                sidebar_layout.Width += 10;
                if (sidebar_layout.Width == sidebar_layout.MaximumSize.Width)
                {
                    isexpend = true;
                    SideBar_timer.Stop();
                }
            }
        }

        private void SideBar_btn_Click22(object sender, EventArgs e)
        {
            SideBar_timer.Start();
            if (isexpend)
            {
                SideBar_btn.Image = Cafffe_Sytem.Properties.Resources.icons8_menu_36;
                SideBar_btn.Refresh();
            }
            else
            {
                SideBar_btn.Image = Cafffe_Sytem.Properties.Resources.icons8_activity_feed_33;
                SideBar_btn.Refresh();
            }
        }


        #endregion

       
    }
}
