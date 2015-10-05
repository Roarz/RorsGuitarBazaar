// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class maintains the logic of data transferrable objects
// for all tables needed by the client applications. Important
// methods include mapping a class to an entity.

// Order of DTO classes:
//  - clsBrand
//  - clsCustomerOrder
//  - clsInventory
//  - clsNew
//  - clsUsed

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SelfHost.DTO
{
    [DataContract]
    public class clsBrand
    {
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string Slogan { get; set; }
        [DataMember]
        public string Founded { get; set; }
        [DataMember]
        public List<clsInventory> Inventory { get; set; }

        public tblbrand MapToEntity()
        {
            return new tblbrand() { Brand = this.Brand, Slogan = this.Slogan, Founded = this.Founded };
        }
    }

    [DataContract]
    public class clsCustomerOrder
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal OrderCost { get; set; }
        [DataMember]
        public System.DateTime OrderTimestamp { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerPhone { get; set; }
        [DataMember]
        public clsInventory Inventory { get; set; }

        public tblcustomerorder MapToEntity()
        {
            return new tblcustomerorder()
            {
                OrderID = this.OrderID,
                ItemID = this.ItemID,
                Quantity = this.Quantity,
                OrderCost = this.OrderCost,
                OrderTimestamp = this.OrderTimestamp,
                CustomerName = this.CustomerName,
                CustomerPhone = this.CustomerPhone
            };
        }
    }
   
    [DataContract]
    [KnownType(typeof(clsNew))]
    [KnownType(typeof(clsUsed))]
    public abstract class clsInventory
    {
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string ModelName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public System.DateTime LastModified { get; set; }
        [DataMember]
        public int StockQuantity { get; set; }
        [DataMember]
        public string Style { get; set; }
        
       [DataMember]
        public List<clsCustomerOrder> CustomerOrders { get; set; }

        public tblinventory MapToEntity()
        {
            tblinventory lcInventoryEntity = GetEntity();
            lcInventoryEntity.ItemID = ItemID;
            lcInventoryEntity.Brand = Brand;
            lcInventoryEntity.ModelName = ModelName;
            lcInventoryEntity.Price = Price;
            lcInventoryEntity.LastModified = LastModified;
            lcInventoryEntity.StockQuantity = StockQuantity;
            lcInventoryEntity.Style = Style;
            return lcInventoryEntity;
        }

        // method allowing the different kinds of inventory items to add their fields to a concrete inventory item.
        public abstract tblinventory GetEntity();
    }

    [DataContract]
    public partial class clsNew : clsInventory
    {
        [DataMember]
        public Nullable<System.DateTime> DateProduced { get; set; }
        [DataMember]
        public string CountryProduced { get; set; }

        public override tblinventory GetEntity()
        {
            return new New() { DateProduced = this.DateProduced, CountryProduced = this.CountryProduced };
        }
    }

    [DataContract]
    public partial class clsUsed : clsInventory
    {
        [DataMember]
        public string ItemCondition { get; set; }
        [DataMember]
        public Nullable<int> Age { get; set; }

        public override tblinventory GetEntity()
        {
            return new Used() { ItemCondition = this.ItemCondition, Age = this.Age };
        }
    }

}
