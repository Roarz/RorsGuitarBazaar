// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class is solely responsible for allowing details for a 
// 'new' kind of inventory item to be displayed in the content
// control for the item. To do this, it must implement the 
// IItemControl interface.

using System.Windows.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class ucNew : UserControl, IItemControl 
    {
        public ucNew()
        {
            InitializeComponent();
        }

        public void UpdateControl(clsInventory prInventory)
        {
            clsNew lcNew = (clsNew)prInventory;
            txtCountryProduced.Text = lcNew.CountryProduced;
            txtDateProduced.Text = lcNew.DateProduced.ToString();
        }

    }
}
