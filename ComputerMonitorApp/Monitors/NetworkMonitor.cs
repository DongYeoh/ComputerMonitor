using ComputerMonitorApp.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors
{
    public class NetworkMonitor : Monitors.Monitor
    {
        private PerformanceCounter performanceCounterDownload;
        private PerformanceCounter performanceCounterUpload;
        private String currentInterface;
        public NetworkMonitor() : base(EMonitorType.Network)
        {
            var ni = GetNetworkInterface() ;
            if (ni == null) return;
            currentInterface = ni.Description;
            performanceCounterDownload = new PerformanceCounter("Network Interface", "Bytes Received/sec", currentInterface);
            performanceCounterUpload = new PerformanceCounter("Network Interface", "Bytes Sent/sec", currentInterface);

            EnableTimer(1000, Timer_Elapsed);

            ConfigManager.Config.ConfigChanged += Config_ConfigChanged;
        }
        
        private void Config_ConfigChanged(object sender, ConfigChangedEventArgs eventArgs)
        {
            if (eventArgs.Property != nameof(ConfigManager.Config.NetworkInterface)) return;
            var ni_set = ConfigManager.Config.NetworkInterface;
            if (currentInterface == ni_set)
            {
                return;
            }
            if (performanceCounterDownload != null)
            {
                performanceCounterDownload.Dispose();
            }
            if (performanceCounterUpload != null)
            {
                performanceCounterUpload.Dispose();
            }
            currentInterface = ni_set;
            performanceCounterDownload = new PerformanceCounter("Network Interface", "Bytes Received/sec", currentInterface);
            performanceCounterUpload = new PerformanceCounter("Network Interface", "Bytes Sent/sec", currentInterface);
        }

        private NetworkInterface GetNetworkInterface()
        {
            var ni = NetworkInterfaceHelper.GetCurrentNetworkInterface();
            if (ni.Name != ConfigManager.Config.NetworkInterface)
            {
                ConfigManager.Config.NetworkInterface = ni.Description;
                ConfigManager.Save();
            }
            return ni;
        }

        private void Timer_Elapsed()
        {
            var downloadValue = performanceCounterDownload.NextValue();
            var uploadValue = performanceCounterUpload.NextValue();
            OnMonitorChanged(new NetworkMonitorEventArgs() { ReceiveSpeed = downloadValue,SendSpeed = uploadValue });
        }
    }
    public class NetworkMonitorEventArgs : MonitorEventArgs
    {
        public float ReceiveSpeed { get; set; }

        public float SendSpeed { get; set; }
    }
}
