using Cafffe_Sytem.A.M.A;
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

namespace Cafffe_Sytem.D.M.M
{
    public partial class MangeOffer : Form
    {
        public event EventHandler eva;
        int selectedId;
        public MangeOffer(string pagetype)
        {
            InitializeComponent();
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
      public void  InitializeForUpdate(int offerOff_ID,string offerOff_Name,string offerOff_Limit,string offerOff_Start,string offerOff_End)
        {
            selectedId= offerOff_ID;
            offname_txt.Text = offerOff_Name;
            offlimit_txt.Text =( offerOff_Limit).ToString();
            offstart_txt.Text = offerOff_Start.ToString();
            offend_txt.Text = offerOff_End.ToString();
        }
        private void update_btn_Click(object sender, EventArgs e)
        {

           
            string offerOff_Name = offname_txt.Text;
            string offerOff_Limit = offlimit_txt.Text;
            DateTime offerOff_Start = DateTime.Parse(offstart_txt.Text);
            DateTime offerOff_End = DateTime.Parse(offend_txt.Text);

            // Retrieve the Clint entity from the database
            Offer q3 = DBConnection.Context.Offers.FirstOrDefault(c => c.Off_ID == selectedId);

            // Update the properties of the retrieved Clint entity
            if (q3 != null)
            {
                q3.Off_Name = offerOff_Name;
                q3.Off_Limit = int.Parse( offerOff_Limit);
                q3.Off_Start = offerOff_Start;
               // q3.Off_End = offerOff_End;

                // Save changes to the database
                DBConnection.Context.Entry(q3).State = EntityState.Detached;
            }
            DBConnection.Context.Offers.Attach(q3);
            DBConnection.Context.Entry(q3).State = EntityState.Modified;
            DBConnection.Context.SaveChanges();
            MessageBox.Show("update Successfull");

            eva?.Invoke(this, e);
        }
        private void add_btn_Click(object sender, EventArgs e)
        {



            Offer offer = new Offer()
            {
                Off_Name = offname_txt.Text,
                Off_Limit = int.Parse(offlimit_txt.Text),
                Off_Start =DateTime.Parse( offstart_txt.Text),
                Off_End = DateTime.Parse( offend_txt.Text),
            };
            DBConnection.Context.Offers.Add(offer);
            DBConnection.Context.SaveChanges();
            MessageBox.Show("add Successfull");
            eva?.Invoke(this, e);
        }

        private void MangeOffer_Load(object sender, EventArgs e)
        {

        }

        private void offstart_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
