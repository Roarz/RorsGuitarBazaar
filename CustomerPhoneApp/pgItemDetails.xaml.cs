// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class hold an inventory item for the purposes of display all
// of its details to the user. If the user likes what they see, they
// can go to the order page to start the order process.

using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class pgItemDetails : PhoneApplicationPage
    {
        private clsInventory _Inventory;

        public pgItemDetails()
        {
            InitializeComponent();
            clsNew.LoadNewForm = RunNew;
            clsUsed.LoadUsedForm = RunUsed;
        }

        private void UpdatePage(clsInventory prInventory)
        { 
            _Inventory = prInventory;
            txtBrand.Text = _Inventory.Brand;
            txtModel.Text = _Inventory.ModelName;
            txtStock.Text = _Inventory.StockQuantity.ToString();
            txtPrice.Text = _Inventory.Price.ToString();
            txtStyle.Text = _Inventory.Style;
            (ctcItemSpecs.Content as IItemControl).UpdateControl(prInventory);
        }

        private void RunNew(clsInventory prInventory)
        {
            ctcItemSpecs.Content = new ucNew();
            PageTitle.Text = "New Item";
            UpdatePage(prInventory);
        }

        private void RunUsed(clsInventory prInventory)
        {
            ctcItemSpecs.Content = new ucUsed();
            PageTitle.Text = "Used Item";
            UpdatePage(prInventory);
        }

        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_Inventory != null)
            {
                NavigationService.Navigate(new Uri("/pgOrder.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
                _Inventory.EditDetails();
            base.OnNavigatedFrom(e);
        }

    }
}