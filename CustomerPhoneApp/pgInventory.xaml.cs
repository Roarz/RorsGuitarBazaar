// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class recieves a brand name to display all of that brand's 
// inventory items. Users can then select an inventory item from the
// list to view its details in greater depth.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class pgInventory : PhoneApplicationPage
    {
        private clsBrand _Brand;
        private clsInventory _CurrentInventoryItem;

        public pgInventory()
        {
            InitializeComponent();
            pgMain.SvcClient.GetBrandCompleted += SvcClient_GetBrandCompleted;
        }

        private void SvcClient_GetBrandCompleted(object sender, GetBrandCompletedEventArgs e)
        {
            try
            {
                _Brand = e.Result;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                txbMessage.Text = ex.Message;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string lcBrandName;

            if (NavigationContext.QueryString.TryGetValue("brand", out  lcBrandName))
                pgMain.SvcClient.GetBrandAsync(lcBrandName);
            else // no suitable querystring -> new brand!
                _Brand = new clsBrand();
        }

        private void UpdateDisplay()
        {
            lstInventory.ItemsSource = null;
            if (_Brand.Inventory != null)
                lstInventory.ItemsSource = _Brand.Inventory.ToList();
        }

        private void ViewItem(clsInventory prInventory)
        {
            if (prInventory != null)
            {
                _CurrentInventoryItem = prInventory;
                NavigationService.Navigate(new Uri("/pgItemDetails.xaml", UriKind.Relative));
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            ViewItem(lstInventory.SelectedItem as clsInventory);
        }

        private void lstInventory_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewItem(lstInventory.SelectedItem as clsInventory);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
                _CurrentInventoryItem.EditDetails();
            base.OnNavigatedFrom(e);
        }

    }
}