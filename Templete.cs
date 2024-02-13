using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem
{
    public partial class Templete : Form
    {
        public Templete()
        {
            InitializeComponent();
        }

        #region SideBar
        protected bool isexpend;

        protected void SideBar_timer_Tick(object sender, EventArgs e)
        {
            if (isexpend)
            {
                sidebar_layout.Width -= 40;
                if (sidebar_layout.Width == sidebar_layout.MinimumSize.Width)
                {
                    isexpend = false;
                    SideBar_timer.Stop();
                }
            }
            else
            {
                sidebar_layout.Width += 40;
                if (sidebar_layout.Width == sidebar_layout.MaximumSize.Width)
                {
                    isexpend = true;
                    SideBar_timer.Stop();
                }
            }
        }

        protected void SideBar_btn_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
