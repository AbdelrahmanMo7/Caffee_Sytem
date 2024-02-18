using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.A.M.A
{
    public partial class Bill_Templete : Form
    {

        public string Bill_DateTime { get; set; }
        public List<bill_list_item> bill_Item_list { get; set; }
        public Client selected_Client { get; set; }
        public double Bill_Total_Amount { get; set; }
        
        public Bill_Templete( List<bill_list_item> _bill_Item_list , Client _selected_Client,double _Bill_Total_Amount )
        {
            InitializeComponent();
            // time = DateTime.Now.ToString("HH:mm:ss tt");
            // date = DateTime.Now.ToString("dd/M/yyyy");
            Bill_DateTime = DateTime.Now.ToString();
            Bill_DateTime_label15.Text = Bill_DateTime;
            this.bill_Item_list =new List<bill_list_item>( _bill_Item_list);
            this.selected_Client = _selected_Client;
            this.Bill_Total_Amount = _Bill_Total_Amount;
            Sys_Info Caffee_info = DBConnection.Context.Sys_Info.Select(i => i).FirstOrDefault();
            var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
            Bill_Items_dataGridView1.DataSource = show_list;
        }

        private void Bill_Templete_Load(object sender, EventArgs e)
        {
            // to get logo here 
            //  DBConnection.Context
            Sys_Info Caffee_info = DBConnection.Context.Sys_Info.Select(i => i).FirstOrDefault();
            var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
            Bill_Items_dataGridView1.DataSource = show_list;

            var q= DBConnection.Context.Bills.Where(b => b.B_ID == 1).Select(b => b).FirstOrDefault();

            
        }

        

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Print Bill ");
        }
        private void print(Panel p)
        {
            PrinterSettings ps = new PrinterSettings();
            Bill_Temp_panel2 = p;
            getPrintArea(p);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_printPage);
            printPreviewDialog1.ShowDialog();

        }
        void printDocument1_printPage(object obj , PrintPageEventArgs e)
        {
            Rectangle pageArea = e.PageBounds;
            e.Graphics.DrawImage(memoryImg,(pageArea.Width/2)-(this.Bill_Temp_panel2.Width/2),this.Bill_Temp_panel2.Location.Y);
        }

        private Bitmap memoryImg;

         void getPrintArea(Panel p)
        {
            memoryImg = new Bitmap(p.Width,p.Height);
            p.DrawToBitmap(memoryImg, new Rectangle(0, 0, p.Width, p.Height));
        }
        private void pictureBox1_Click( object obj , EventArgs e)
        {
            print(this.Bill_Temp_panel2);
        }

        private void Bill_Temp_panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
