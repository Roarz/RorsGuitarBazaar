// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This form holds an inventory item that can be edited by its
// subclasses - FrmNew and FrmUsed. Important methods for this 
// form include the ability to grab data entered by users as well
// as put already existing data from the inventory item onto the
// item form. If the inventory item is new, it will be added to
// the database. If the inventory item was edited, it will be
// updated in the database.

using System;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmItem : Form
    {
        protected clsInventory _InventoryItem;

        public FrmItem()
        {
            InitializeComponent();
            // ensuring the user enters a model name value that can actually fit in the database.
            txtModelName.MaxLength = 40;
        }

        public void SetDetails(clsInventory prInventoryItem)
        {
            _InventoryItem = prInventoryItem;
            UpdateForm();
            ShowDialog();
        }

        protected virtual void UpdateForm()
        {
            // if the item has a brand, the brand textbox will be disabled.
            txtBrand.Enabled = string.IsNullOrEmpty(_InventoryItem.Brand);
            txtBrand.Text = _InventoryItem.Brand;
            // if the item has a model name, the model name textbox will be disabled.
            txtModelName.Enabled = string.IsNullOrEmpty(_InventoryItem.ModelName);
            txtModelName.Text = _InventoryItem.ModelName;
            numPrice.Value = _InventoryItem.Price;
            dtpLastModified.Value = _InventoryItem.LastModified;
            numStock.Value = _InventoryItem.StockQuantity;
            cboGuitarStyle.Text = _InventoryItem.Style.ToString();
        }

        protected virtual void PushData()
        {
            // if the user cleared the stock control, we will ensure that it is understood as being = '0'
            if (string.IsNullOrEmpty(numStock.Text))
                _InventoryItem.StockQuantity = 0;
            else
                _InventoryItem.StockQuantity = Convert.ToInt32(numStock.Text);
            _InventoryItem.ModelName = txtModelName.Text.Trim();
            _InventoryItem.Price = numPrice.Value;
            // updating the last modified value automatically
            _InventoryItem.LastModified = DateTime.Now;
            _InventoryItem.Style = cboGuitarStyle.Text.Trim();
        }

        protected virtual bool IsValid()
        {
            if (String.IsNullOrEmpty(txtModelName.Text.Trim()))
            {
                MessageBox.Show("Please enter a model name for the inventory item.", "Please Enter Model Name");
                txtModelName.Focus();
                return false;
            }
            else if (numPrice.Value < 1 || String.IsNullOrEmpty(numPrice.Text.Trim()))
            {
                DialogResult lcPriceZeroResult = MessageBox.Show("You must enter a price of at least $1 for the item",
                   "Enter Price", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                PushData();
                // using the model name as a means to detect if a new item was created or an existing item was modified.
                if (txtModelName.Enabled)
                    Program.SvcClient.AddItem(_InventoryItem);
                else
                    Program.SvcClient.UpdateItem(_InventoryItem);
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
