using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //if (File.Exists("log.txt")) Console.WriteLine("true");
                GetAllPerformanceCounter();
                GetAllNetworkInterfaces();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("press any key to continue...");
            Console.Read();
        }
        private static void GetAllNetworkInterfaces()
        {
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var item in nis)
            {
                Console.WriteLine(item.Name);
            }
        }
        private static void GetAllPerformanceCounter()
        {
            var list = new List<String>();
            foreach(var category in PerformanceCounterCategory.GetCategories())
            {
                if (category.CategoryName != "Network Interface") continue;
                foreach(var instance in category.GetInstanceNames())
                {
                    if (!category.InstanceExists(instance)) continue;
                    foreach(var counter in category.GetCounters(instance))
                    {
                        list.Add($"category:{category.CategoryName},instance:{instance},counter:{counter.CounterName}");
                    }
                }
            }
            list.Sort();
            System.IO.File.WriteAllLines("NetworkInterface.txt", list);
        }
    }
    class TestClass
    {
        public String Property { get; set; }
    }
}
