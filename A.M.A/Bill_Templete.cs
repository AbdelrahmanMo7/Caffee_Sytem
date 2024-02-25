using Cafffe_Sytem.D.M.M;
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
        public int Table_Num { get; set; }
        public List<bill_list_item> bill_Item_list { get; set; }
        public Client selected_Client { get; set; }
        public double Bill_Total_Amount { get; set; }
        
        public event EventHandler BillCompleted;
        public Bill_Templete( List<bill_list_item> _bill_Item_list , Client _selected_Client,double _Bill_Total_Amount ,int Table_Num )
        {
            InitializeComponent();
            this.bill_Item_list =new List<bill_list_item>( _bill_Item_list);
            this.selected_Client = _selected_Client;
            this.Bill_Total_Amount = _Bill_Total_Amount;
            this.Table_Num = Table_Num;
            Login.Current_User = DBConnection.Context.Users.OrderByDescending(U => U.U_ID).FirstOrDefault();
        }

        private void Bill_Templete_Load(object sender, EventArgs e)
        {
            // time = DateTime.Now.ToString("HH:mm:ss tt");
            // date = DateTime.Now.ToString("dd/M/yyyy");
            Bill_DateTime = DateTime.Now.ToString();
            Bill_DateTime_label15.Text = Bill_DateTime;
            // to get logo here 
            //  DBConnection.Context
            Sys_Info Caffee_info = DBConnection.Context.Sys_Info.Select(i => i).FirstOrDefault();
           if (Caffee_info != null)
            {
                CoffeeName_label1.Text = Caffee_info.Coffee_Name;
                CoffeeAddres_label4.Text = Caffee_info.Coffee_Address;
                CoffeePhone_label2.Text = Caffee_info.Coffee_Phone_Number.ToString();
                Caffee_Appointment_label10.Text = Caffee_info.Coffee_Apointment;
                Link_FaceBook_label14.Text = Caffee_info.Coffee_FaceBook_Link;
                Link_Insagram_label14.Text = Caffee_info.Coffee_Insta_Link;
                if (!string.IsNullOrEmpty(Caffee_info.Coffee_Logo_Path))
                {

                }
            }
            else
            {
                MessageBox.Show("Please, Enter your Caffee Information !!!");
            }
           
            //------
            var show_list = bill_Item_list.Select(b => new { Item = b.Item.P_Name, Count = b.Count, Total = b.Total }).ToList();
            Bill_Items_dataGridView1.DataSource = show_list;
            Bill_TotalAmount_label15.Text = Bill_Total_Amount.ToString();
            //
            if (Login.Current_User!=null)
            CashierName_label9.Text=Login.Current_User.U_Name;

            Client_Name_label10.Text = selected_Client.C_Name;
            Client_Address_label4.Text = selected_Client.C_Address;
            Client_Phone_label9.Text = selected_Client.C_Phone_Number.ToString();


           
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

        // print image that save dill data to server and print bill
        private void pictureBox1_Click( object obj , EventArgs e)
        {
            DBConnection.Context.Bills.Add(new Bill { B_Date= (DateTime.Now.Date),B_Time= (DateTime.Now.ToString("HH:mm:ss tt")),B_IsDeleted_=false,B_Table_Num=Table_Num,B_Total_Amount=Bill_Total_Amount,Client_Id= selected_Client.C_ID, Creater_Id=Login.Current_User.U_ID });
            DBConnection.Context.SaveChanges();

            //
            Bill last_Bill = DBConnection.Context.Bills.OrderByDescending(b=>b.B_ID).FirstOrDefault();
            BillNumber22_label14.Text = "Bill.No: " + last_Bill.B_ID;
            Bill_Number_label15.Text = "Bill.No: " + last_Bill.B_ID;
            last_Bill.Bill_has_Products= bill_Item_list.Select(i=> new Bill_has_Products { Bill_ID=last_Bill.B_ID,Product=i.Item,Product_Count=i.Count }).ToList();
            DBConnection.Context.SaveChanges();
            BillCompleted?.Invoke(this, e);

            print(this.Bill_Temp_panel2);

            this.Close();
        }

       

       
    }
}
