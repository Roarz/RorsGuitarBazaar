// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// Interface providing a method for any classes in contract to implement the
// update control method. Classes in contract include pgNew and pgUsed.

using CustomerPhoneApp.ServiceReference1;

namespace CustomerPhoneApp
{
    interface IItemControl
    {
        void UpdateControl(clsInventory prInventory);
    }
}
