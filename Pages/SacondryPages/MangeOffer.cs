using Cafffe_Sytem.CustomModels;
using Cafffe_Sytem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem.Pages.SacondryPages
{
    public partial class MangeOffer : Form
    {
        public event EventHandler eva;
        int selectedId;
        public MangeOffer(string pagetype)
        {
           
            InitializeComponent();
            try { 
            off_End_dateTimePicker2.CustomFormat = " dd - MM - yyyy";
            off_Start_dateTimePicker1.CustomFormat = " dd - MM - yyyy";
            if (pagetype == "Add")
            {
                update_btn.Visible = false;
                add_btn.Visible = true;
                pagetitle_labl.Text = "Add new offer";
            }
            else
            {
                update_btn.Visible = true;
                add_btn.Visible = false;
                pagetitle_labl.Text = "Update offer data";
            }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
      public void  InitializeForUpdate(int offerOff_ID,string offerOff_Name,int offerOff_Limit,DateTime offerOff_Start, DateTime offerOff_End)
        {
           try{ selectedId= offerOff_ID;
            offname_txt.Text = offerOff_Name;
            Limit_numericUpDown1.Value =offerOff_Limit;
            off_Start_dateTimePicker1.Value =offerOff_Start;
            off_End_dateTimePicker2.Value =offerOff_End;
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void update_btn_Click(object sender, EventArgs e)
        {
            try { 
           
            string offerOff_Name = offname_txt.Text;
            int offerOff_Limit =(int) Limit_numericUpDown1.Value;
            DateTime offerOff_Start = off_Start_dateTimePicker1.Value;
            DateTime offerOff_End = off_End_dateTimePicker2.Value;

            // Retrieve the Clint entity from the database
            Offer q3 = DBConnection.Context.Offers.FirstOrDefault(c => c.Off_ID == selectedId);

            // Update the properties of the retrieved Clint entity
            if (q3 != null)
            {
                q3.Off_Name = offerOff_Name;
                q3.Off_Limit = offerOff_Limit;
                q3.Off_Start = offerOff_Start;
                q3.Off_End = offerOff_End;

                // Save changes to the database
                DBConnection.Context.Entry(q3).State = EntityState.Detached;
            }
            DBConnection.Context.Offers.Attach(q3);
            DBConnection.Context.Entry(q3).State = EntityState.Modified;
            DBConnection.Context.SaveChanges();
            MessageBox.Show("update Successfull");

            eva?.Invoke(this, e);
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void add_btn_Click(object sender, EventArgs e)
        {
            try { 
            if (string.IsNullOrEmpty(offname_txt.Text))
            {
                MessageBox.Show("Please, Enter name for the Offer !!!");
                return;
            }

           

            Offer offer = new Offer()
            {
                Off_Name = offname_txt.Text,
                Off_Limit = (int)Limit_numericUpDown1.Value,
                Off_Start = off_Start_dateTimePicker1.Value,
                Off_End = off_End_dateTimePicker2.Value,
            };
            DBConnection.Context.Offers.Add(offer);
            DBConnection.Context.SaveChanges();
            MessageBox.Show("add Successfull");
            eva?.Invoke(this, e);
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show("System Error : " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void MangeOffer_Load(object sender, EventArgs e)
        {

        }

        private void offstart_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
