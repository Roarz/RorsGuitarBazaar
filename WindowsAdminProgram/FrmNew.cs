// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This form is solely responsible for allowing details for a 
// 'new' kind of inventory item to be set by the user. The user
// is also allowed the opportunity to decide whether they want
// to leave the country produced field blank. This is reasonable 
// because they may know have this info.

using System;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmNew : WindowsAdminProgram.FrmItem
    {
        public static readonly FrmNew Instance = new FrmNew();

        public FrmNew()
        {
            InitializeComponent();
            // ensuring the user enters a country produced value that can actually fit in the database
            txtCountryProduced.MaxLength = 40;
        }

        public static void Run(clsNew prNew)
        {
            Instance.SetDetails(prNew);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            this.Text = "New Item - " + _InventoryItem.Brand + " Brand";
            clsNew lcNew = (clsNew)_InventoryItem;
            try
            {
                dtpDateProduced.Value = Convert.ToDateTime(lcNew.DateProduced);
                txtCountryProduced.Text = lcNew.CountryProduced.ToString();
            }
            catch (Exception)
            {
                dtpDateProduced.Value = Convert.ToDateTime(DateTime.Now);
                txtCountryProduced.Text = null;
            }
        }

        protected override void PushData()
        {
            base.PushData();
            clsNew lcNew = (clsNew)_InventoryItem;
            lcNew.DateProduced = dtpDateProduced.Value;
            lcNew.CountryProduced = txtCountryProduced.Text.Trim();
        }

        // Checks to make sure the user wants to leave the country produced field empty.
        protected override bool IsValid()
        {
            if (base.IsValid())
            {
                if (String.IsNullOrEmpty(txtCountryProduced.Text.Trim()))
                {
                    // allowing the user the opportunity to confirm whether they want to leave the country produced field blank
                    DialogResult lcLeaveCountryEmptyResult = MessageBox.Show("Are you sure you would want to leave the country produced field blank?",
                        "Confirm Descision", MessageBoxButtons.YesNo);
                    if (lcLeaveCountryEmptyResult == DialogResult.No)
                        return false;
                    else
                        return true;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
