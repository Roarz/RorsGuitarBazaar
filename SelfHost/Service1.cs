// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// Self explanatory class holding the logic for when
// the program runs.

// Order of service methods:
//  - GetBrandNames()
//  - GetBrand(string prBrand)
//  - GetOrders()
//  - GetOrder(int prOrderID)
//  - Process entity states method
//  - Inventory item CRUD methods
//  - Order CRUD methods

using System.Collections.Generic;
using System.Linq;
using SelfHost.DTO;

namespace SelfHost
{
    class Service1 : IService1
    {
        // Getting a list of strings containing all the brand names
        public List<string> GetBrandNames()
        {
            using (rorsGuitarBazaarEntities lcContext = new rorsGuitarBazaarEntities())
                return lcContext.tblbrands
                .Select(lcBrand => lcBrand.Brand)
                .ToList();
        }

        // Returns a brand object including all its associated inventory records.
        public clsBrand GetBrand(string prBrand)
        {
            using (rorsGuitarBazaarEntities lcContext = new rorsGuitarBazaarEntities())
            {
                tblbrand lcBrand = lcContext.tblbrands
                .Include("tblinventories")
                .Where(tblbrand => tblbrand.Brand == prBrand)
                .FirstOrDefault();

                clsBrand lcBrandDTO = new clsBrand()
                {
                    Brand = lcBrand.Brand,
                    Slogan = lcBrand.Slogan,
                    Founded = lcBrand.Founded,
                    Inventory = new List<clsInventory>()
                };
                foreach (tblinventory item in lcBrand.tblinventories)
                {
                    lcBrandDTO.Inventory.Add(item.MapToDTO());
                }
                    

                lcBrandDTO.Inventory.OrderBy(item => item.ItemID);
                return lcBrandDTO;
            }
        }

        // Getting a list of integers containing all the order IDs
        public List<int> GetOrders()
        {
            using (rorsGuitarBazaarEntities lcContext = new rorsGuitarBazaarEntities())
                return lcContext.tblcustomerorders
                .Select(lcOrderID => lcOrderID.OrderID)
                .ToList();
        }

        // Returns a customer order object including its associated inventory.
        public clsCustomerOrder GetOrder(int prOrderID)
        {
            using (rorsGuitarBazaarEntities lcContext = new rorsGuitarBazaarEntities())
            {
                tblcustomerorder lcCustomerOrder = lcContext.tblcustomerorders
                .Include("tblinventory")
                .Where(tblcustomerorder => tblcustomerorder.OrderID == prOrderID)
                .FirstOrDefault();

                clsCustomerOrder lcCustomerOrderDTO = new clsCustomerOrder()
                {
                    OrderID = lcCustomerOrder.OrderID,
                    ItemID = lcCustomerOrder.ItemID,
                    CustomerName = lcCustomerOrder.CustomerName,
                    OrderTimestamp = lcCustomerOrder.OrderTimestamp,
                    OrderCost = lcCustomerOrder.OrderCost,
                    Quantity = lcCustomerOrder.Quantity,
                    CustomerPhone = lcCustomerOrder.CustomerPhone,
                    Inventory = lcCustomerOrder.tblinventory.MapToDTO()
                };
               
                return lcCustomerOrderDTO;
            }
        }

        // Generic method used to process CRUD actions against entities in the database.
        private int process<TEntity>(TEntity prItem, System.Data.EntityState prState) where TEntity : class
        {
            using (rorsGuitarBazaarEntities lcContext = new rorsGuitarBazaarEntities())
            {
                lcContext.Entry(prItem).State = prState;
                int lcCount = lcContext.SaveChanges();
                return lcCount;
            }
        }

        // Inventory Item CRUD
        public int AddItem(clsInventory prItem)
        {
            return process(prItem.MapToEntity(), System.Data.EntityState.Added);
        }

        public int UpdateItem(clsInventory prItem)
        {
            return process(prItem.MapToEntity(), System.Data.EntityState.Modified);
        }

        public int DeleteItem(clsInventory prItem)
        {
            return process(prItem.MapToEntity(), System.Data.EntityState.Deleted);
        }

        // Orders CRUD
        public int AddOrder(clsCustomerOrder prCustomerOrder)
        {
            return process(prCustomerOrder.MapToEntity(), System.Data.EntityState.Added);
        }

        public int DeleteOrder(clsCustomerOrder prCustomerOrder)
        {
            return process(prCustomerOrder.MapToEntity(), System.Data.EntityState.Deleted);
        }

    }
}
