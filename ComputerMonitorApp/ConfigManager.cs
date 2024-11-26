using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using ComputerMonitorApp.Monitors;

namespace ComputerMonitorApp
{
    public class ConfigManager
    {

        private const String configFileName = "config.json";

        private static Config config;
        public static Config Config
        {
            get
            {
                if (config == null)
                {
                    if (!File.Exists(configFileName))
                    {
                        config = new Config();
                    }
                    else
                    {
                        var json = File.ReadAllText(configFileName);
                        config = JsonConvert.DeserializeObject<Config>(json);

                    }
                }
                return config;
            }
        }
        public static void Save()
        {
            var json = JsonConvert.SerializeObject(Config);
            File.WriteAllText(configFileName, json);
        }
    }

    public class ConfigChangedEventArgs : EventArgs
    {
        public Config Config { get; private set; }
        public String Property { get; private set; }
        public ConfigChangedEventArgs(Config config,String property)
        {
            this.Config = config;
            this.Property = property;
        }
    }

    public class Config
    {
        public Config()
        {
            this.HiddenMonitors = new HashSet<EMonitorType>();
        }
        /// <summary>
        /// 上次位置
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// 上次显示的屏幕
        /// </summary>
        public String Screen { get; set; }
        /// <summary>
        /// 屏幕大小
        /// </summary>
        public Size ScreenSize { get; set; }
        /// <summary>
        /// 隐藏的监视器
        /// </summary>
        public HashSet<EMonitorType> HiddenMonitors { get; set; }
        /// <summary>
        /// 网络监视器使用的网络接口
        /// </summary>
        public String NetworkInterface { get; set; }

        public delegate void ConfigChangedHandler(object sender, ConfigChangedEventArgs eventArgs);

        public event ConfigChangedHandler ConfigChanged;
        public void OnConfigChanged(object sender, String property)
        {
            ConfigChanged?.Invoke(sender, new ConfigChangedEventArgs(this,property));
        }
    }
}
