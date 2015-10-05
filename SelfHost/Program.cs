// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// Self explanatory class holding the logic for when
// the program runs.

using System;
using System.ServiceModel;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a ServiceHost 
            using (ServiceHost serviceHost = new ServiceHost(typeof(Service1)))
            {
                // Open the ServiceHost to create listeners
                // and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                serviceHost.Close();
            }

        }
    }
}
