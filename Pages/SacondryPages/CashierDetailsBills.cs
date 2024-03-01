using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
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
    public partial class CashierDetailsBills : Form
    {

        List<Bill> bills = new List<Bill>();
        public CashierDetailsBills(List<Bill> bills)
        {
            InitializeComponent();
            this.bills=bills;
            dataGridView1.DataSource = bills.Select( bill=> new
            {
                ID = bill.B_ID,
                Time = bill.B_Time,
                Date = bill.B_Date,
                Total = bill.B_Total_Amount,
                TableNumber = bill.B_Table_Num,
                Cashier = bill.User.U_Name,
                Client = bill.Client?.C_Name,
                // Include other bill details as needed
            }).ToList() ;

        }
    

      
        private void datetimepicker_filter_selected(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;

            // Filter the rows based on the selected date
           

            // Update the DataGridView with the filtered data
            dataGridView1.DataSource = bills.Where(bill=>bill.B_Date==selectedDate).Select(bill => new
            {
                ID = bill.B_ID,
                Time = bill.B_Time,
                Date = bill.B_Date,
                Total = bill.B_Total_Amount,
                TableNumber = bill.B_Table_Num,
                Cashier = bill.User.U_Name,
                Client = bill.Client.C_Name,
                // Include other bill details as needed
            }).ToList();
            ;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
