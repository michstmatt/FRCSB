using System;
using System.Collections.Generic;
using System.Text;
//using Windows.UI.Popups;
//using Windows.Networking.Connectivity;
namespace FRCSB.FRC
{
    class DebuggerClass
    {
        public  static async  void reportError(object error)
        {
            try
            {
#if DEBUG
               // await new MessageDialog(error.ToString()).ShowAsync();
#endif

            }
            catch {  }
        }
        public static async void showMessage(object message,object messageTitle=null)
        {
            try
            {
              //  await new MessageDialog(message.ToString(),messageTitle.ToString()).ShowAsync();
            }
            catch { }
        }

        public static bool connectedToInternet()
        {
			// ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
			// bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
			//return internet;
			return true;
        }
    }
}
