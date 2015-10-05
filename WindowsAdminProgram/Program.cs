// -----------------------
// Author: Rorie McPherson
// Date: 24 June 2014
// -----------------------

// This class powers the whole windows application. It
// maintains delegates for new and used inventory item
// forms and ensures the connection to the service is
// closed after the application has finished running.

using System;
using System.Windows.Forms;
using WindowsAdminProgram.ServiceReference1;

namespace WindowsAdminProgram
{
    static class Program
    {
        public static ServiceReference1.Service1Client SvcClient =
            new ServiceReference1.Service1Client();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            clsNew.LoadNewForm = new clsNew.LoadNewFormDelegate(FrmNew.Run);
            clsUsed.LoadUsedForm = new clsUsed.LoadUsedFormDelegate(FrmUsed.Run);
            Application.Run(FrmMain.Instance);
            if (SvcClient != null &&
            SvcClient.State != System.ServiceModel.CommunicationState.Closed)
                SvcClient.Close();
        }
    }
}
