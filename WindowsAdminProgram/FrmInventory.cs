// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This form recieves a brand name to display all of that brand's 
// inventory items. Users can then select an inventory item from the
// data grid to view & edit its details or even delete the inventory 
// item. Users can also add new inventory items. A dictionary containing
// inventory forms is maintained allowing multiple different brand
// inventories to be displayed at once.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmInventory : Form
    {
        private static readonly string[] _ItemTypes = { "New", "Used" };
        // dictionary holding all inventory forms allowing multiple brands to be manipulated at the same time
        private static Dictionary<string, FrmInventory> _InventoryFormList =
            new Dictionary<string, FrmInventory>(); 
        
        private clsBrand _Brand;
        // private variable to hold the brand's inventory
        private List<clsInventory> _BrandInventory;
        

        public FrmInventory()
        {
            InitializeComponent();
            // populating the combo box with the item types available
            cboItemType.DataSource = _ItemTypes;
            // setting the selected index to the first option available in the item type string
            cboItemType.SelectedIndex = 0;
        }

        public static void Run(string prBrand)
        {
            FrmInventory lcInventoryForm;

            if (string.IsNullOrEmpty(prBrand) || !_InventoryFormList.TryGetValue(prBrand, out lcInventoryForm))
            {
                lcInventoryForm = new FrmInventory();
                if (string.IsNullOrEmpty(prBrand))
                    lcInventoryForm.SetDetails(new clsBrand());
                else
                {
                    // adding this new form to the dictionary.
                    _InventoryFormList.Add(prBrand, lcInventoryForm);
                    // refresh this new form to show the brand's inventory.
                    lcInventoryForm.RefreshFormFromDB(prBrand);
                }
            }
            else
            {
                lcInventoryForm.Show();
                lcInventoryForm.Activate();
            }
        }

        private void RefreshFormFromDB(string prBrand)
        {
            SetDetails(Program.SvcClient.GetBrand(prBrand));
        }

        public void SetDetails(clsBrand prBrand)
        {
            _Brand = prBrand;
            UpdateDisplay();
            UpdateForm();
            Show();
        }

        private void UpdateDisplay()
        {
            //Configuring the inventory of the brand to be ordered by the model name from A->Z.
            _BrandInventory = _Brand.Inventory.ToList().OrderBy(item => item.ModelName).ToList();
            // populating inventory data grid view
            dgvInventory.DataSource = null;
            if (_Brand.Inventory != null)
                dgvInventory.DataSource = _BrandInventory;
            // if there are inventory items, the edit and delete buttons can be enabled.
            if (dgvInventory.Rows.Count != 0) {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            // if there are no inventory items, the edit and delete buttons must be disabled.
            else {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void UpdateForm()
        {
            // update the form to reflect the brand's details
            lblFounded.Text = "est " + _Brand.Founded;
            lblSlogan.Text = _Brand.Slogan;
            this.Text = "Inventory - " + _Brand.Brand + " Brand";
            FormatDataGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lcItemChoice = cboItemType.SelectedItem.ToString().Substring(0, 1);
            clsInventory lcInventoryItem = clsInventory.NewItem(lcItemChoice[0]);
            if (lcInventoryItem != null) // valid inventory item created?
            {
                // setting safe, default values for the new item that will be shown in the form.
                lcInventoryItem.Brand = _Brand.Brand;
                lcInventoryItem.Price = 500;
                lcInventoryItem.LastModified = DateTime.Now;
                lcInventoryItem.Style = "Electric";

                // editing this new item in the appropriate form.
                lcInventoryItem.EditDetails();
                // checks if the user canceled item creation.
                if (!string.IsNullOrEmpty(lcInventoryItem.ModelName))
                {
                    RefreshFormFromDB(_Brand.Brand);
                }
            }
        }

        private void EditItem()
        {
            try
            {
                int lcSelectedRowIndex = dgvInventory.SelectedCells[0].RowIndex;
                // need to refresh the inventory form so that it shows up to date info on all items.
                RefreshFormFromDB(_Brand.Brand);
                // grabbing the inventory item selected by the user from the data grid view.
                DataGridViewRow lcSelectedRow = dgvInventory.Rows[lcSelectedRowIndex];
                // editing the details of the selected inventory item.
                (lcSelectedRow.DataBoundItem as clsInventory).EditDetails();
                RefreshFormFromDB(_Brand.Brand);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to edit item. Please try again.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // grabbing the inventory item selected by the user from the data grid view.
                int lcSelectedRowIndex = dgvInventory.SelectedCells[0].RowIndex;
                DataGridViewRow lcSelectedRow = dgvInventory.Rows[lcSelectedRowIndex];

                if (MessageBox.Show("Are you sure?", "Deleting Inventory Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.SvcClient.DeleteItem(lcSelectedRow.DataBoundItem as clsInventory);
                    RefreshFormFromDB(_Brand.Brand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvInventory_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditItem();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void FormatDataGrid()
        {
            // formatting column display formats to improve presenation in the data grid view.
            dgvInventory.Columns[4].DefaultCellStyle.Format = "c"; // format currency
            // number columns aligned to the right
            dgvInventory.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventory.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventory.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // renaming the column headers for better readability
            dgvInventory.Columns[6].HeaderText = "Guitar Style";
            dgvInventory.Columns[5].HeaderText = "In Stock";
            dgvInventory.Columns[3].HeaderText = "Model Name";
            dgvInventory.Columns[1].HeaderText = "Item Code";

            // hiding columns we don't need to see (brand and last modified).
            dgvInventory.Columns[0].Visible = false;
            dgvInventory.Columns[2].Visible = false;
        }

    }
}
