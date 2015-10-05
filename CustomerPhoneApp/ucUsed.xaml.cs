// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class is solely responsible for allowing details for a 
// 'used' kind of inventory item to be displayed in the content
// control for the item. To do this, it must implement the 
// IItemControl interface.

using System.Windows.Controls;
using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    public partial class ucUsed : UserControl, IItemControl
    {
        public ucUsed()
        {
            InitializeComponent();
        }

        public void UpdateControl(clsInventory prInventory)
        {
            clsUsed lcUsed = (clsUsed)prInventory;
            txtCondition.Text = lcUsed.ItemCondition;
            txtAge.Text = lcUsed.Age.ToString();
        }
    }
}
