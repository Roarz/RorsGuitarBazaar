// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This main page grabs all of the brands in the system and 
// displays these in a data grid view. The user can then 
// select a brand to view all of its inventory items. The
// user can also choose to view all of the customer orders.

using System;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmMain : Form
    {
        // single form instance - singleton pattern
        private static readonly FrmMain _Instance = new FrmMain();

        public static FrmMain Instance
        {
            get { return FrmMain._Instance; }
        } 

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            dgvBrand.DataSource = null;
            string[] lcBrandList = Program.SvcClient.GetBrandNames();
            foreach (string brand in lcBrandList)
            {
                dgvBrand.Rows.Add(brand);
            }
        }

        private void ViewInventory()
        {
            // grabbing the brand the user has selected from the data grid
            int lcSelectedRowIndex = dgvBrand.SelectedCells[0].RowIndex;
            DataGridViewRow lcSelectedRow = dgvBrand.Rows[lcSelectedRowIndex];
            string lcSelectedBrand = Convert.ToString(lcSelectedRow.Cells["Brand"].Value);

            if (lcSelectedBrand != null)
            {
                try
                {
                    FrmInventory.Run(lcSelectedBrand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "This should never occur");
                }
            }
        }

        private void btnViewInventory_Click(object sender, EventArgs e)
        {
            ViewInventory();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            FrmOrderList.Instance.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dgvBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewInventory();
        }

    }
}
