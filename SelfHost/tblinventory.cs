//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SelfHost
{
    using System;
    using System.Collections.Generic;
    
    public abstract partial class tblinventory
    {
        public tblinventory()
        {
            this.tblcustomerorders = new HashSet<tblcustomerorder>();
        }
    
        public int ItemID { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public System.DateTime LastModified { get; set; }
        public int StockQuantity { get; set; }
        public string Style { get; set; }
    
        public virtual ICollection<tblcustomerorder> tblcustomerorders { get; set; }
    }
}
