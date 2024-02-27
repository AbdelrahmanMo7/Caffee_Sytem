using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using Cafffe_Sytem.Pages.SacondryPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages
{
    public partial class Reports_Page : Templete
    { 
        List<Period_Item> ReportPariod_List;

        public Reports_Page()
        {
            InitializeComponent();
            List<string> ReportTypes = new List<string>() { "Bills", "Category", "Products" };
            
            ReportPariod_List = new List<Period_Item>() 
            {
                new Period_Item(1,"1 Day"),new Period_Item(2,"2 Days"),new Period_Item( 3,"3 Days"),new Period_Item(4,"4 Days"),new Period_Item( 5,"5 Days"),new Period_Item(6,"6 Days"),new Period_Item( 7,"7 Days"),new Period_Item(14,"2 Weaks"),new Period_Item( 28,"4 Weaks(1 month)"),new Period_Item(56,"56 Days(2 months)"),new Period_Item(84,"84 Days(3 months)"),new Period_Item(112,"112 Days(4 months)"),new Period_Item(140,"140 Days(5 months)"),new Period_Item(168,"168 Days(6 months)"),new Period_Item(224,"224 Days(8 months)"),new Period_Item(280,"280 Days(10 months)"),new Period_Item(336,"336 Days(12 months)")
            };
            Period_comboBox2.DataSource= ReportPariod_List;
            Period_comboBox2.ValueMember= "Value";
            Period_comboBox2.DisplayMember= "Desc";
            Acounting_Pariods_comboBox2.DataSource = ReportPariod_List;
            Acounting_Pariods_comboBox2.ValueMember = "Value";
            Acounting_Pariods_comboBox2.DisplayMember = "Desc";
            Report_type_comboBox1.DataSource = ReportTypes;

            
            calcExpenses();
        }

       

//----------------------------------------------------------------

        #region Accounting Report


        //Calc expenses 
        void calcExpenses()
        {
            int salary = DBConnection.Context.Employees.Sum(e => e.Emp_Salary).Value;
            int sal_Pariod = ReportPariod_List[Acounting_Pariods_comboBox2.SelectedIndex].Value;
            
            salary = salary * (sal_Pariod/28);
           
            Salaries_Txt.Text = salary.ToString();

            if (Material_Txt.Text == null || Material_Txt.Text.Length == 0)
                Material_Txt.Text="0";
            if (Maintenance_Txt.Text == null || Maintenance_Txt.Text.Length == 0)
                Maintenance_Txt.Text = "0";
            if (Subscriptions_Txt.Text == null || Subscriptions_Txt.Text.Length == 0)
                Subscriptions_Txt.Text = "0";
            if (PettyCash_Txt.Text == null || PettyCash_Txt.Text.Length == 0)
                PettyCash_Txt.Text = "0";
            TotalExpenses_Txt.Text =( (salary+ int.Parse(Material_Txt.Text.ToString())+ int.Parse(Maintenance_Txt.Text.ToString())+ int.Parse(Subscriptions_Txt.Text.ToString())+ int.Parse(PettyCash_Txt.Text.ToString())).ToString());

        }

        //Calc Net Profit
        void calcProfit()
        {
            DateTime newDate = DateTime.Today.AddDays(-(ReportPariod_List[Acounting_Pariods_comboBox2.SelectedIndex].Value));
            List<Bill> bills_list= DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false && b.B_Date >= newDate).ToList();
            if (bills_list == null)
            {
                TotalSales_Txt.Text = "000";
            }
            else
            {
                TotalSales_Txt.Text =((long) bills_list.Sum(b => b.B_Total_Amount)).ToString();
            }
            if (TotalSales_Txt.Text == null || TotalSales_Txt.Text.Length == 0)
                TotalSales_Txt.Text = "0";
            if (TotalExpenses_Txt.Text == null || TotalExpenses_Txt.Text.Length == 0)
                TotalExpenses_Txt.Text = "0";

            Net_Profit_Txt.Text =( long.Parse(TotalSales_Txt.Text.ToString())- long.Parse(TotalExpenses_Txt.Text.ToString())).ToString();
            if (long.Parse(Net_Profit_Txt.Text) < 0)
            {
                Profit_label15.Text = "        Lose :";
                Profit_label15.ForeColor = Color.Red;
                Net_Profit_Txt.ForeColor = Color.Red;
            }

        }

        private void Material_Txt_TextChanged(object sender, EventArgs e)

        {
            if (Material_Txt.Text.Length >= 10)

            {
                MessageBox.Show("You can not enter more than 9 numbers.");
                Material_Txt.Text = Material_Txt.Text.Remove(Material_Txt.Text.Length - 1);
                return;
            }
            if (Regex.IsMatch(Material_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Material_Txt.Text = Material_Txt.Text.Remove(Material_Txt.Text.Length - 1);
                return;
            }
            calcExpenses();


        }

        private void Maintenance_Txt_TextChanged(object sender, EventArgs e)
        {
            if (Maintenance_Txt.Text.Length >= 10)

            {
                MessageBox.Show("You can not enter more than 9 numbers.");
                Maintenance_Txt.Text = Maintenance_Txt.Text.Remove(Maintenance_Txt.Text.Length - 1);
                return;
            }
            if (Regex.IsMatch(Maintenance_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Maintenance_Txt.Text = Maintenance_Txt.Text.Remove(Maintenance_Txt.Text.Length - 1);
                return;
            }
            calcExpenses();
        }

        private void Subscriptions_Txt_TextChanged(object sender, EventArgs e)
        {
            if (Subscriptions_Txt.Text.Length >= 10)

            {
                MessageBox.Show("You can not enter more than 9 numbers.");
                Subscriptions_Txt.Text = Subscriptions_Txt.Text.Remove(Subscriptions_Txt.Text.Length - 1);
                return;
            }
            if (Regex.IsMatch(Subscriptions_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                Subscriptions_Txt.Text = Subscriptions_Txt.Text.Remove(Subscriptions_Txt.Text.Length - 1);
                return;
            }
            calcExpenses();
        }

        private void PettyCash_Txt_TextChanged(object sender, EventArgs e)
        {
            if (PettyCash_Txt.Text.Length >= 10)

            {
                MessageBox.Show("You can not enter more than 9 numbers.");
                PettyCash_Txt.Text = PettyCash_Txt.Text.Remove(PettyCash_Txt.Text.Length - 1);
                return;
            }
            if (Regex.IsMatch(PettyCash_Txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                PettyCash_Txt.Text = PettyCash_Txt.Text.Remove(PettyCash_Txt.Text.Length - 1);
                return;
            }
            calcExpenses();
        }

        private void TotalSales_Txt_TextChanged(object sender, EventArgs e)
        {
            calcProfit();
        }

       
        //----------------
        private void TotalExpenses_Txt_TextChanged(object sender, EventArgs e)
        {
            calcProfit();
        }

        //
        private void Acounting_Pariods_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcExpenses();
        }
    #endregion

    //----------------------------------------------------------------

    //-----
    void getReport( int pariod)
        {
            DateTime newDate = DateTime.Today.AddDays(-pariod);
            string type =Report_type_comboBox1.SelectedItem.ToString();
            //"Category","Products" 
            if (string.IsNullOrEmpty(type))
                return;
            if (type == "Category")
            {
                List<Report_Item> product_list = new List<Report_Item>();
                
                var p_list = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false && b.B_Date >= newDate).Select(b => b.Bill_has_Products).ToList();
                foreach(var i in p_list)
                {

                    product_list.AddRange(i.Select(x=> new Report_Item { Name = x.Product.Category.Cat_Name , Count= x.Product_Count , Total=(x.Product_Count*x.Product.P_Price) }).ToList());

                }
                var Report= product_list.GroupBy(b => b.Name).Select(l=> new {  Category= l.Key, Product_Count=l.Sum(Item=> Item.Count),Total= l.Sum(Item => Item.Total)  }).ToList();
                Reports_conteinar_dataGridView1.DataSource =null ;
                Reports_conteinar_dataGridView1.DataSource =Report ;
                Total_Text_count_label1.Text = "Total Products Count  :";
                TotalCount_textBox2.Text= Report.Sum(x => x.Product_Count).ToString();
                TotalAmount_textBox1.Text= Report.Sum(x => x.Total).ToString();
                Report_name_label1.Text = "Categories Report";
                Report_name_label1.Location = new System.Drawing.Point(495, 13);

            }
            else if (type == "Products")
            {

                List<Report_Item> product_list = new List<Report_Item>();

                var p_list = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false && b.B_Date >= newDate).Select(b => b.Bill_has_Products).ToList();
                foreach (var i in p_list)
                {

                    product_list.AddRange(i.Select(x => new Report_Item { Name = x.Product.P_Name, Count = x.Product_Count, Price= x.Product.P_Price, Total = (x.Product_Count * x.Product.P_Price) }).ToList());

                }
                var Report = product_list.GroupBy(b => b.Name).Select(l => new { Prodect = l.Key,Price= l.Select(item=>item.Price).First(), Product_Count = l.Sum(Item => Item.Count), Total = l.Sum(Item => Item.Total) }).ToList();
                Reports_conteinar_dataGridView1.DataSource = null;
                Reports_conteinar_dataGridView1.DataSource = Report;
                Total_Text_count_label1.Text = "Total Products Count  :";
                TotalCount_textBox2.Text = Report.Sum(x => x.Product_Count).ToString();
                TotalAmount_textBox1.Text = Report.Sum(x => x.Total).ToString();
                Report_name_label1.Text = "Products Report";
                Report_name_label1.Location = new System.Drawing.Point(505, 13);

            }
            else
            {
            
                var Report = DBConnection.Context.Bills.Where(b => b.B_IsDeleted_ == false&& b.B_Date>= newDate).GroupBy(b => b.B_Date).Select(i => new { Date = i.Key, Bill_Count = i.Count(), Total = i.Sum(item => item.B_Total_Amount) }).ToList();
                Reports_conteinar_dataGridView1.DataSource = null;
                Reports_conteinar_dataGridView1.DataSource = Report;
                Total_Text_count_label1.Text = "Total Bills Count  :";
                TotalCount_textBox2.Text = Report.Sum(x => x.Bill_Count).ToString();
                TotalAmount_textBox1.Text = Report.Sum(x => x.Total).ToString();
                Report_name_label1.Text = "Bills Report";
                Report_name_label1.Location = new System.Drawing.Point(515, 13);

            }


        }

       

        private void Accounting_Report_Tab_Enter(object sender, EventArgs e)
        {
            Report_name_label1.Text = "Accounting Summary Report";
            Report_name_label1.Location = new System.Drawing.Point(440, 13);

        }

       
        private void Selling_Reports_Tab_Enter(object sender, EventArgs e)
        {
            getReport(ReportPariod_List[Period_comboBox2.SelectedIndex].Value);
        }

        private void Report_type_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getReport(ReportPariod_List[Period_comboBox2.SelectedIndex].Value);

        }
        private void Period_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Report_type_comboBox1.DataSource==null || Report_type_comboBox1.Items.Count==0)
                return;
           // getReport(int.Parse( Period_comboBox2.ValueMember[Period_comboBox2.SelectedIndex].ToString()));
            getReport(ReportPariod_List[Period_comboBox2.SelectedIndex].Value);
        }

      
    }
}
