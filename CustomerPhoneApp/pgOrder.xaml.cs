// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class allows a customer to create an order for the 
// inventory item they have selected. To do this, it must
// know the inventory item they have chosen and allow them
// to fill out an order form.

using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class pgOrder : PhoneApplicationPage
    {
        private clsInventory _Inventory;
        private clsCustomerOrder _Order = new clsCustomerOrder();

        public pgOrder()
        {
            InitializeComponent();
            clsNew.LoadNewForm = UpdatePage;
            clsUsed.LoadUsedForm = UpdatePage;
            pgMain.SvcClient.AddOrderCompleted += SvcClient_AddOrderCompleted;
            txtCustomerName.MaxLength = 40;
            txtCustomerPhone.MaxLength = 40;
        }

        private void SvcClient_AddOrderCompleted(object sender, AddOrderCompletedEventArgs e)
        {
            try
            {
                // reducing the available stock by the number of items that were ordered.
                _Inventory.StockQuantity = _Inventory.StockQuantity - _Order.Quantity;
                // updating the inventory item that the customer has just ordered to reflect the stock changes.
                pgMain.SvcClient.UpdateItemAsync(_Inventory);
                MessageBox.Show("Thank you \"" + _Order.CustomerName + "\" for your order. Press OK to browse more brands at Ror's Guitar Bazaar.",
                        "Order Created",
                        MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/pgMain.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Processing Order",
                    MessageBoxButton.OK);
            }
        }

        private void UpdatePage(clsInventory prInventory)
        {
            _Inventory = null;
            _Inventory = prInventory;
            txbBrand.Text = _Inventory.Brand;
            txbModel.Text = _Inventory.ModelName;
            txbStock.Text = _Inventory.StockQuantity.ToString();
            txbPrice.Text = _Inventory.Price.ToString();
        }

        private void UpdateDisplay()
        {
            txtCustomerName.Text = _Order.CustomerName;
            txtCustomerPhone.Text = _Order.CustomerPhone;
            txbOrderCost.Text = _Order.OrderCost.ToString();
            txtQuantity.Text = _Order.Quantity.ToString();
        }

        private void PushData()
        {
            // trying to parse the quantity text box to prevent users from entering non-numeric input
            int lcResult;
            bool lcIsNumeric = int.TryParse(txtQuantity.Text, out lcResult);
            if (string.IsNullOrEmpty(txtQuantity.Text) || lcIsNumeric == false)
                _Order.Quantity = 0;
            else
                _Order.Quantity = Convert.ToInt32(txtQuantity.Text);
            _Order.CustomerName = txtCustomerName.Text.Trim();
            _Order.CustomerPhone = txtCustomerPhone.Text.Trim();
            _Order.OrderCost = _Order.Quantity * _Inventory.Price;
            _Order.OrderTimestamp = DateTime.Now;
            _Order.ItemID = _Inventory.ItemID;
            UpdateDisplay();
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            PushData();
            if (IsValid())
            {
                try
                {
                    pgMain.SvcClient.AddOrderAsync(_Order);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Processing Order",
                        MessageBoxButton.OK);
                }
            }
        }

        private void txtQuantity_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PushData();
        }

        private bool IsValid()
        {
            if (txtCustomerName.TextboxIsValid() && txtCustomerPhone.TextboxIsValid())
            {
                if (_Order.OrderCost <= 0 || _Order.Quantity > _Inventory.StockQuantity)
                {
                    MessageBox.Show("Please enter an appropriate number for order quantity.", "Quantity Error",
                        MessageBoxButton.OK);
                    return false;
                }
                else
                    return true;
            }
            return false;
        }
    }
}