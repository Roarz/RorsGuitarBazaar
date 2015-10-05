// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class holds extra partial classes responsible for
// facilitating the conversion between data transfer objects
// and entities.

using SelfHost.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost
{
    public abstract partial class tblinventory
    {
        public clsInventory MapToDTO()
        {
            clsInventory lcInventoryDTO = GetDTO();
            lcInventoryDTO.ItemID = ItemID;
            lcInventoryDTO.Brand = Brand;
            lcInventoryDTO.ModelName = ModelName;
            lcInventoryDTO.Price = Price;
            lcInventoryDTO.LastModified = LastModified;
            lcInventoryDTO.StockQuantity = StockQuantity;
            lcInventoryDTO.Style = Style;
            return lcInventoryDTO;
        }

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

        protected abstract clsInventory GetDTO();

        protected abstract tblinventory GetEntity();
    }

    public partial class New
    {
        protected override clsInventory GetDTO()
        {
            return new clsNew() { DateProduced = this.DateProduced, CountryProduced = this.CountryProduced };
        }

        protected override tblinventory GetEntity()
        {
            return new New() { DateProduced = this.DateProduced, CountryProduced = this.CountryProduced };
        }
    }

    public partial class Used
    {
        protected override clsInventory GetDTO()
        {
            return new clsUsed() { ItemCondition = this.ItemCondition, Age = this.Age };
        }

        protected override tblinventory GetEntity()
        {
            return new Used() { ItemCondition = this.ItemCondition, Age = this.Age };
        }
    }

   

}
