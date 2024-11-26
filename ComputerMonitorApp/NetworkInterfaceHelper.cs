using ComputerMonitorApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp
{
    public class NetworkInterfaceHelper
    {
        public static List<NetworkInterface> GetNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .ToList().Where(item => item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up)
                .ToList();
        }

        public static NetworkInterface GetCurrentNetworkInterface()
        {
            var nis = NetworkInterfaceHelper.GetNetworkInterfaces();
            if (nis.Count == 0) return null;

            var ni_set = ConfigManager.Config.NetworkInterface;
            var ni = nis.FirstOrDefault(item => !ni_set.IsBlank() && item.Description == ni_set) ?? nis[0];//默认第一个
            
            return ni;
        }
    }
}
