using Cafffe_Sytem.A.M.A;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cafffe_Sytem.Marina
{
    public partial class Employees : Templete
    {



        public Employees()
        {

            InitializeComponent();

          
            // Bind the DataGridView once with the initial data


            // filter shiftStart  
            var shift_list = DBConnection.Context.Employees.Select(m => m.Emp_ShiftStart).GroupBy(f => f).ToList();
            Start_com.Items.Add("All");
            foreach (var item in shift_list)
            {
                Start_com.Items.Add(item.Key.ToString());
            }




            // SALARY
            var salary_list = DBConnection.Context.Employees.Select(m => m.Emp_Salary).GroupBy(E => E).ToList();
            comboBoxsalary.Items.Add("All");
            foreach (var item in salary_list)
            {
                comboBoxsalary.Items.Add(item.Key.ToString());

            }

            getShowList(DBConnection.Context.Employees.ToList());

        }

         void getShowList( IEnumerable<Employee> emp_list)
        {
             

            var ShowList = emp_list.Select(m => new
            {
                ID = m.Emp_ID,
                Name = m.Emp_Name,
                Job = m.Posistion.Pos_Name,
                Salary = m.Emp_Salary,
                ShiftStart = m.Emp_ShiftStart,
                ShiftEnd = m.Emp_ShiftEnd,
                Phone = m.Emp_Phone_Number,
                Address = m.Emp_Address,
                Age = m.Emp_Age,
                Gender = (m.Emp_Gender.ToString())

            }).ToList();
            var ShowList2 = DBConnection.Context.Employees.Take(2).Select(m => new
            {
                ID = m.Emp_ID,
                Name = m.Emp_Name,
                Job = m.Posistion.Pos_Name,
                Salary = m.Emp_Salary,
                ShiftStart = m.Emp_ShiftStart,
                ShiftEnd = m.Emp_ShiftEnd,
                Phone = m.Emp_Phone_Number,
                Address = m.Emp_Address,
                Age = m.Emp_Age,
                Gender = (m.Emp_Gender.ToString())

            }).ToList();
            ShowList2.Clear();

            foreach (var item in ShowList)
            {
                if (item.Gender == "true")
                {
                    ShowList2.Add(new
                    {
                        item.ID,
                        item.Name,
                        item.Job,
                        item.Salary,
                        item.ShiftStart,
                        item.ShiftEnd,
                        item.Phone,
                        item.Address,
                        item.Age,
                        Gender = "Female"

                    });
                }

                else
                {
                    ShowList2.Add(new
                    {
                        item.ID,
                        item.Name,
                        item.Job,
                        item.Salary,
                        item.ShiftStart,
                        item.ShiftEnd,
                        item.Phone,
                        item.Address,
                        item.Age,
                        Gender = "Male"

                    });
                }

            }
            dataGridView1.DataSource = ShowList2;
        }

      



        #region salary filter
       
        private void comboBoxsalary_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedSalary = comboBoxsalary.SelectedItem.ToString();

            if (string.IsNullOrEmpty(selectedSalary))
            
                return;
            if (selectedSalary == "All")
            {
                getShowList(DBConnection.Context.Employees.ToList());
            }
            else
            {


                int selectedsalaryInt = int.Parse(selectedSalary);

                getShowList(DBConnection.Context.Employees.Where(a => a.Emp_Salary == selectedsalaryInt).ToList());
              
           }
            

        }
        #endregion

        #region    name Search
        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameSearch_Txt.Text))
            {
                getShowList(DBConnection.Context.Employees.ToList());
                return;
            }
            getShowList(DBConnection.Context.Employees.Where(m=>m.Emp_Name==NameSearch_Txt.Text).ToList());
        }


        #endregion

        #region filter shiftStart

        private void Start_com_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedShiftStart = Start_com.SelectedItem.ToString();


            if (string.IsNullOrEmpty(selectedShiftStart))

                return;
            if (selectedShiftStart == "All")
            {
                getShowList(DBConnection.Context.Employees.ToList());
            }
            else
            {


                int selectedShiftStartInt = int.Parse(selectedShiftStart);

                getShowList(DBConnection.Context.Employees.Where(a => a.Emp_ShiftStart == selectedShiftStartInt).ToList());

            }

        }
        #endregion


        public void RefreshDataGridView()
        {
            getShowList(DBConnection.Context.Employees.ToList());
        }

          #region add

        private void btnadd_Click(object sender, EventArgs e)
        {
            addEmployee emp = new addEmployee("Add");
            emp.DataUpdated += (s, args) =>
            {
                // Refresh the dataGridView after data is updated
                RefreshDataGridView();
                DBConnection.Context.SaveChanges();
            };
            emp.Show();
        }


        #endregion

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEmployeeID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                if (selectedEmployeeID != null)
                {
                    Employee selectedEmployee = DBConnection.Context.Employees.FirstOrDefault(E => E.Emp_ID == selectedEmployeeID);
                    if (selectedEmployee != null)
                    {
                        updateEmployee emp = new updateEmployee(selectedEmployee, btnupdate.Text);

                        emp.DataUpdated += (s, args) =>
                        {
                            // Refresh the dataGridView after data is updated
                            RefreshDataGridView();
                            DBConnection.Context.SaveChanges();
                        };

                        emp.Show();
                       

                        //Cafe_employee.Employees.AddOrUpdate(selectedEmployee);
                        //Cafe_employee.SaveChanges();

                    }
                    else
                    {
                        MessageBox.Show("Employee not found");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select row to update");
            }
        }










       





        //private readonly Coffee_System cafffe = new Coffee_System();
        //    Employees employ = new Employees();







            #region  delete




        private void btndelet_Click(object sender, EventArgs e)
        {



            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int empsId;
                    if (row.Cells["ID"].Value != null && int.TryParse(row.Cells["ID"].Value.ToString(), out empsId))
                    {
                        var q = DBConnection.Context.Employees.FirstOrDefault(r => r.Emp_ID == empsId);

                        if (q != null)
                        {
                            DBConnection.Context.Employees.Remove(q);
                        }
                    }
                }


                // Save changes to the database
                DBConnection.Context.SaveChanges();

                // Refresh the DataGridView
               RefreshDataGridView();
            }

        }

       #endregion


        
    }
}



