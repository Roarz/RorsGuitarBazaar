// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This form is solely responsible for allowing details for a 
// 'used' kind of inventory item to be set by the user. The user
// is also allowed the opportunity to decide whether they want
// to leave the age field blank. This is reasonable because 
// they may know have this info.

using System;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmUsed : WindowsAdminProgram.FrmItem
    {
        private static readonly string[] _QualityOptions = { "Poor", "Average", "Good", "Like-new" };
        public static readonly FrmUsed Instance = new FrmUsed();

        public FrmUsed()
        {
            InitializeComponent();
            // filling out the quality options in the combo box
            cboCondition.DataSource = _QualityOptions;
            cboCondition.SelectedIndex = 0;
        }

        public static void Run(clsUsed prUsed)
        {
            Instance.SetDetails(prUsed);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            this.Text = "Used Item - " + _InventoryItem.Brand + " Brand";
            clsUsed lcUsed = (clsUsed)_InventoryItem;
            try
            {
                cboCondition.Text = lcUsed.ItemCondition.ToString();
                numAge.Value = Convert.ToInt32(lcUsed.Age);
            }
            catch (Exception)
            {
                cboCondition.SelectedIndex = 1;
                numAge.Value = numAge.Minimum;
            }

        }

        protected override void PushData()
        {
            base.PushData();
            clsUsed lcUsed = (clsUsed)_InventoryItem;
            lcUsed.ItemCondition = cboCondition.Text;
            lcUsed.Age = Convert.ToInt32(numAge.Value);
        }

        // Checks to make sure the user wants to leave the age field empty.
        protected override bool IsValid()
        {
            if (base.IsValid())
            {
                if (String.IsNullOrEmpty(numAge.Text.Trim()))
                {
                    // allowing the user the opportunity to confirm whether they want to leave the age field blank
                    DialogResult lcLeaveAgeEmptyResult = MessageBox.Show("Are you sure you would want to leave the age field blank?",
                        "Confirm Descision", MessageBoxButtons.YesNo);
                    if (lcLeaveAgeEmptyResult == DialogResult.No)
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
