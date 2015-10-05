// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This main page grabs all of the brands in the system and 
// displays these in a list. The user can then select a
// brand to view all of its inventory items.

using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class pgMain : PhoneApplicationPage
    {
        public static readonly Service1Client SvcClient = new Service1Client();
        
        public pgMain()
        {
            InitializeComponent();
            SvcClient.GetBrandNamesCompleted += SvcClient_GetBrandNamesCompleted;
        }

        private void SvcClient_GetBrandNamesCompleted(object sender, GetBrandNamesCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                    lstBrands.ItemsSource = e.Result;
                else
                    txbMessage.Text = e.Error.Message;
            }
            catch (Exception ex)
            {
                txbMessage.Text = ex.Message;
            }

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SvcClient.GetBrandNamesAsync();
        }

        private void ViewInventory(){
            if (lstBrands.SelectedItem != null)
                NavigationService.Navigate(new Uri("/pgInventory.xaml?brand=" +
                lstBrands.SelectedItem, UriKind.Relative));
        }

        private void btnBrowseBrand_Click(object sender, RoutedEventArgs e)
        {
            ViewInventory();
        }

        private void lstBrands_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewInventory();
        }
    }
}