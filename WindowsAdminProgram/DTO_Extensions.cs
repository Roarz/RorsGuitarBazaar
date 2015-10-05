// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class is used to harness logic for the inventory, new and used classes. This 
// is useful for presenting inventory items better in a list, holding a factory method
// to control which kind of inventory item to create and choosing which form should be
// loaded when an item's EditDetails() method is invoked.

namespace WindowsAdminProgram.ServiceReference1
{
    public abstract partial class clsInventory
    {
        // edit details method overriden by inventory subclasses.
        public abstract void EditDetails();

        // Factory method selecting which type of item to make
        public static clsInventory NewItem(char prItemChoice)
        {
            switch (char.ToUpper(prItemChoice))
            {
                case 'N': return new clsNew();
                case 'U': return new clsUsed();
                default: return null;
            }
        }
    }

    public partial class clsNew
    {
        public delegate void LoadNewFormDelegate(clsNew prNew);
        public static LoadNewFormDelegate LoadNewForm;

        public override void EditDetails()
        {
            // loading which ever form is written in the static variable.
            LoadNewForm(this);
        }
    }

    public partial class clsUsed
    {
        public delegate void LoadUsedFormDelegate(clsUsed prUsed);
        public static LoadUsedFormDelegate LoadUsedForm;

        public override void EditDetails()
        {
            // loading which ever form is written in the static variable.
            LoadUsedForm(this);
        }
    }

}
