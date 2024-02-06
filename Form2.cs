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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = Color.Aqua;
            button2.Text = " save ";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.BurlyWood;
            button2.Text = " Update ";

        }

        private void button2_TextChanged(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
        }
    }
}
