using Cafffe_Sytem.Pages;
using Cafffe_Sytem.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cafffe_Sytem.CustomModels;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class updateEmployee : Form
    {
        // Declare the event at the class level
        public event EventHandler DataUpdated;

        Employee employeeToUpdate;
        public updateEmployee(Employee employee, string buttonText)
        {
            InitializeComponent();
            employeeToUpdate = employee;
            btnupdate.Text = buttonText;
            Position_comboBox1.DataSource = DBConnection.Context.Posistions.Select(p => p.Pos_Name).ToList();

        }

        private void btnupdate_Click(object sender, EventArgs e)
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
                if (employeeToUpdate.Emp_Phone_Number != phoneNumber)
                {
                 MessageBox.Show("Phone Number Already Exists To Another Employee.");
                 return;
                }
               
            }

            employeeToUpdate.Emp_Name = textname.Text;
            employeeToUpdate.Emp_Phone_Number = phoneNumber;
            employeeToUpdate.Emp_Age = int.Parse(textage.Text);
            employeeToUpdate.Emp_Gender = radioButton1.Checked; 
            employeeToUpdate.Emp_Salary = int.Parse(textsalary.Text);
            employeeToUpdate.Emp_Address = textaddress.Text;
            employeeToUpdate.P_Id =DBConnection.Context.Posistions.Where(em=>em.Pos_Name==Position_comboBox1.SelectedItem).FirstOrDefault().Pos_ID;
            employeeToUpdate.Emp_ShiftStart = Convert.ToInt32(numericUpDownstart.Value);
            employeeToUpdate.Emp_ShiftEnd = Convert.ToInt32(numericUpDownend.Value);
            Employee Emp= DBConnection.Context.Employees.Find(employeeToUpdate.Emp_ID);
            Emp = employeeToUpdate;
            DBConnection.Context.SaveChanges(); 
            

            DataUpdated?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void updateEmployee_Load(object sender, EventArgs e)
        {
           
            textname.Text = employeeToUpdate.Emp_Name;
            textage.Text = employeeToUpdate.Emp_Age.ToString();
            //radioButton1.Checked = employeeToUpdate.Emp_Gender;
            //radioButton2.Checked = employeeToUpdate.Emp_Gender;
            textphone.Text = employeeToUpdate.Emp_Phone_Number.ToString();
            textsalary.Text = employeeToUpdate.Emp_Salary.ToString();
            textaddress.Text = employeeToUpdate.Emp_Address;
            Position_comboBox1.SelectedItem=employeeToUpdate.Posistion.Pos_Name;
            numericUpDownstart.Value = employeeToUpdate.Emp_ShiftStart.Value;
            numericUpDownend.Value = employeeToUpdate.Emp_ShiftEnd.Value;
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
    }
}
