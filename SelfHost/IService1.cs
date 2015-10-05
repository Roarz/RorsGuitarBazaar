// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This interface maintains a list of all the service
// methods that any class implementing this interface
// must implement. For this project, this refers to 
// the Service1 class.

using SelfHost.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace SelfHost
{
    [ServiceContract]
    interface IService1
    {
        // Getting a list of strings containing all the brand names
        [OperationContract]
        List<string> GetBrandNames();

        // Returns a brand object including all its associated inventory records.
        [OperationContract]
        clsBrand GetBrand(string prBrand);

        // Getting a list of integers containing all the order IDs
        [OperationContract]
        List<int> GetOrders();

        // Returns a customer order object including its associated inventory.
        [OperationContract]
        clsCustomerOrder GetOrder(int prItemID);


        // Inventory item CRUD
        [OperationContract]
        int AddItem(clsInventory prItem);

        [OperationContract]
        int UpdateItem(clsInventory prItem);

        [OperationContract]
        int DeleteItem(clsInventory prItem);


        // Order CRUD
        [OperationContract]
        int AddOrder(clsCustomerOrder prCustomerOrder);

        [OperationContract]
        int DeleteOrder(clsCustomerOrder prCustomerOrder);
    }
}
