using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages.SacondryPages
{

    public partial class addEmployee : Form
    {


       
        Employee selected_item;
        string buttontext;

        bool gender = false;
        public event EventHandler DataUpdated;
        public addEmployee( string buttontext)
        {
            InitializeComponent();
            Position_comboBox1.DataSource = DBConnection.Context.Posistions.Select(p=>p.Pos_Name).ToList();
        }



        #region      add


        private void button1_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textname.Text))
            {
                MessageBox.Show("Please enter Employee name.");
                return;
            }
            if (string.IsNullOrEmpty(textage.Text))
            {
                MessageBox.Show("Please enter Employee age.");
                return;
            }
            if (string.IsNullOrEmpty(textphone.Text))
            {
                MessageBox.Show("Please enter Employee phone.");
                return;
            }
            if (string.IsNullOrEmpty(textsalary.Text))
            {
                MessageBox.Show("Please enter Employee salary.");
                return;
            }
            if (string.IsNullOrEmpty(textaddress.Text))
            {
                MessageBox.Show("Please enter Employee address.");
                return;
            }
            if (numericUpDownend.Value < 1 && numericUpDownend.Value > 24)
            {
                MessageBox.Show("Please enter valied Shift End Hour .");
                return;
            }
            if (numericUpDownstart.Value < 1 && numericUpDownstart.Value > 24)
            {
                MessageBox.Show("Please enter valied Shift Start Hour .");
                return;
            }

            long phoneNumber = long.Parse(textphone.Text);

            // Check if the phone number already exists
            if (DBConnection.Context.Employees.Any(emp => emp.Emp_Phone_Number == phoneNumber))
            {
                MessageBox.Show("Phone Number Already Exists To Another Employee.");
                return;
            }

            Employee newEmployee = new Employee
            {

                Emp_Name = textname.Text.ToString(),
                Emp_Phone_Number = (long)int.Parse(textphone.Text.ToString()),
                Emp_Age = int.Parse(textage.Text.ToString()),
                Emp_Gender = gender,
                P_Id= DBConnection.Context.Posistions.Find(Position_comboBox1.SelectedItem).Pos_ID,
                Emp_Salary = int.Parse(textsalary.Text.ToString()),
                Emp_Address = textaddress.Text.ToString(),
                Emp_ShiftStart = int.Parse(numericUpDownstart.Value.ToString()),
                Emp_ShiftEnd = int.Parse(numericUpDownend.Value.ToString())
            };

            DBConnection.Context.Employees.Add(newEmployee);
            DBConnection.Context.SaveChanges();

            DataUpdated?.Invoke(this, EventArgs.Empty);
            DialogResult = DialogResult.OK;
            MessageBox.Show("Add Successful");


        }


        #endregion

        private void addEmployee_Load(object sender, EventArgs e)
        {

        }

        private void textage_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textage.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textage.Text = textage.Text.Remove(textage.Text.Length - 1);
            }
        }

        private void textphone_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textphone.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textphone.Text = textphone.Text.Remove(textphone.Text.Length - 1);
            }
        }

        private void textsalary_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textsalary.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textsalary.Text = textsalary.Text.Remove(textsalary.Text.Length - 1);
            }
        }

        private void textname_TextChanged(object sender, EventArgs e)
        {

        }
    }

}