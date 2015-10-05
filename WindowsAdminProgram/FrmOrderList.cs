// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This form displays a list of customer orders in a data grid.
// Additional inventory details, such as model name and brand,
// for these orders are displayed in the appropriate textboxes
// when they are selected. To do this, this form must maintain
// a list of customer order numbers to find customer orders.
// Users can then select an order and delete it, after confirmation.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    public partial class FrmOrderList : Form
    {
        // single form instance - singleton pattern
        public static readonly FrmOrderList Instance = new FrmOrderList();
        private List<int> _CustomerOrderNumbers;
        private List<clsCustomerOrder> _CustomerOrders = new List<clsCustomerOrder>();

        public FrmOrderList()
        {
            InitializeComponent();
        }

        private void FrmCustomerOrders_Load(object sender, EventArgs e)
        {
            RefreshFormFromDB();
            // setting a safe, default value as the initial selected order object to display its details in the textboxes.
            if (dgvCustomerOrders.Rows.Count != 0)
                this.dgvCustomerOrders.CurrentCell = this.dgvCustomerOrders[1, 0];
                GrabOrder();
            }

        private void RefreshFormFromDB()
        {
            SetDetails(Program.SvcClient.GetOrders().ToList());
        }

        public void SetDetails(List<int> prCustomerOrderNumbers)
        {
            _CustomerOrderNumbers = prCustomerOrderNumbers;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // resetting the list of customer orders to update the display
            _CustomerOrders.Clear();
            decimal lcOrdersTotal = new decimal();

            foreach (int orderID in _CustomerOrderNumbers)
            {
                clsCustomerOrder lcCustomerOrder = Program.SvcClient.GetOrder(orderID);
                _CustomerOrders.Add(lcCustomerOrder);
                lcOrdersTotal = lcOrdersTotal + lcCustomerOrder.OrderCost;
            }

            //Configuring the order list to be ordered by the customer name from A->Z.
            _CustomerOrders = _CustomerOrders.ToList().OrderBy(order => order.CustomerName).ToList();

            // updating the combined cost of all orders
            lblOrdersTotal.Text = "$"+lcOrdersTotal.ToString();

            // populating the data grid view
            dgvCustomerOrders.DataSource = null;
            dgvCustomerOrders.DataSource = _CustomerOrders;
            // disabling the delete order button if there are no orders
            if (_CustomerOrders.Count < 1)
                btnDeleteOrder.Enabled = false;
            else
                btnDeleteOrder.Enabled = true;
            
            FormatDataGrid();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            // grabbing the order selected by the user from the data grid view.
            int lcSelectedRowIndex = dgvCustomerOrders.SelectedCells[0].RowIndex;
            DataGridViewRow lcSelectedRow = dgvCustomerOrders.Rows[lcSelectedRowIndex];

            if (MessageBox.Show("Are you sure you want to delete this order?", "Deleting Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.SvcClient.DeleteOrder(dgvCustomerOrders.CurrentRow.DataBoundItem as clsCustomerOrder);
                RefreshFormFromDB();
                GrabOrder();
            }
        }

        private void dgvCustomerOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GrabOrder();
        }

        private void GrabOrder()
        {
            // Can't grab an order if none exist to fill out the text boxes
            if (dgvCustomerOrders.Rows.Count > 0)
            {
                // update the textboxes to reflect the order.
                clsCustomerOrder lcCustomerOrder = dgvCustomerOrders.CurrentRow.DataBoundItem as clsCustomerOrder;
                txtBrand.Text = lcCustomerOrder.Inventory.Brand;
                txtModelName.Text = lcCustomerOrder.Inventory.ModelName;
                txtStyle.Text = lcCustomerOrder.Inventory.Style;
            }
        }

        private void FormatDataGrid()
        {
            // hiding the complex inventory column as textboxes will be used to show the fields easier.
            dgvCustomerOrders.Columns[2].Visible = false;

            // formatting column display formats to improve presenation in the data grid view.
            dgvCustomerOrders.Columns[4].DefaultCellStyle.Format = "c"; // format currency
            dgvCustomerOrders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCustomerOrders.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCustomerOrders.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCustomerOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
