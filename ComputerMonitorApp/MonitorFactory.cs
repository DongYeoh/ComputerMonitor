using ComputerMonitorApp.Monitors;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp
{
    public class MonitorFactory
    {
        private static Computer computer;

        private static void OpenComputer()
        {
            if (computer == null)
            {
                computer = new Computer();
                computer.GPUEnabled = true;
                computer.Open();
            }
        }
        private static Dictionary<EMonitorType, Monitors.Monitor> MonitorCache = new Dictionary<EMonitorType, Monitors.Monitor>();
        public static Monitors.Monitor GetMonitor(EMonitorType monitorType)
        {
            if (!MonitorCache.ContainsKey(monitorType))
            {
                MonitorCache[monitorType] = InitMonitor(monitorType);
            }
            return MonitorCache[monitorType];
        }
        private static Monitors.Monitor InitMonitor(EMonitorType monitorType)
        {
            switch (monitorType)
            {
                case EMonitorType.CPU:
                    return new CPUMonitor();
                case EMonitorType.GPU:
                    return InitGPUMonitor();
                case EMonitorType.RAM:
                    return new RAMMonitor();
                case EMonitorType.HardDisk:
                    return new HardDiskMonitor();
                case EMonitorType.Network:
                    return new NetworkMonitor();
                default:
                    throw new Exception("unknown monitor type:" + monitorType.ToString());
            }
        }
        private static GPUMonitor InitGPUMonitor()
        {
            OpenComputer();
            foreach (var hardare in computer.Hardware)
            {
                if (hardare.HardwareType == HardwareType.GpuNvidia || hardare.HardwareType == HardwareType.GpuAti)
                {
                    return new GPUMonitor(hardare);
                }

            }
            return new GPUMonitor(null);
        }
        public static void Close()
        {
            if (computer != null)
            {
                computer.Close();
            }
            foreach (var monitor in MonitorCache.Values)
            {
                monitor.Dispose();
            }
        }

    }
}
