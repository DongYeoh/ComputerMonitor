using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors
{
    public class RAMMonitor : Monitor
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        private MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
        public RAMMonitor() : base(EMonitorType.RAM)
        {
            EnableTimer(1000, Timer_Elapsed);
        }

        private void Timer_Elapsed()
        {
            if (GlobalMemoryStatusEx(memStatus))
            {
                float totalMemory = memStatus.ullTotalPhys;
                float availableMemory = memStatus.ullAvailPhys;
                float usagePercentage = ((totalMemory - availableMemory) / totalMemory) * 100;
                OnMonitorChanged(new RAMMonitorEventArgs() { Load = usagePercentage });
            }
        }

    }
    public class RAMMonitorEventArgs : MonitorEventArgs
    {
        public float Load { get; set; }
    }
}
