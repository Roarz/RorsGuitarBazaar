// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class is currently used to make the "TextboxIsValid" extension method available
// to any class or page. Only pgOrder currently makes use of this class.

using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomerPhoneApp
{
    public static class clsUtil
    {
        // This extension method simply returns a boolean value representing whether the
        // textbox is valid. In this case, valid means it is not empty!
        public static bool TextboxIsValid(this TextBox prTextbox)
        {
            if (String.IsNullOrEmpty(prTextbox.Text.Trim()))
            {
                MessageBox.Show("Please fill out: " + prTextbox.Name.Substring(3), "More Customer Info Required",
                        MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}
