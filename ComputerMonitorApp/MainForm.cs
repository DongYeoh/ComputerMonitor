
using ComputerMonitorApp.Properties;
using System.Timers;
using System.Collections.Specialized;
using System.Linq;
using ComputerMonitorApp.MonitorControls;
using ComputerMonitorApp.Monitors;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime;
using Serilog;

namespace ComputerMonitorApp
{

    public partial class MainForm : Form
    {
      
        public MainForm()
        {
            InitializeComponent();
            this.mainLayout.Width = this.Width;
            this.mainLayout.Controls.Clear();
            this.mainLayout.RowStyles.Clear();
            this.mainLayout.RowCount = 0;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsInDesignMode()) return;
            //初始化显示位置
            InitLocation();
            //初始化事件监控
            InitEvents();
            InitHeadBar();
            //初始化监视器
            InitMonitorLayouts();
            RefreshDisplayMonitors();
        }
        private void InitEvents()
        {
            ConfigManager.Config.ConfigChanged += Config_ConfigChanged;
        }

        private void Config_ConfigChanged(object sender, ConfigChangedEventArgs eventArgs)
        {
            var property = eventArgs.Property;
            //显示内容的变化
            if(property == nameof(eventArgs.Config.HiddenMonitors))
            {
                RefreshDisplayMonitors();
            }
        }

        private HeadBar headBar;
        private void InitHeadBar()
        {
            headBar = new HeadBar() { MainForm = this };
            headBar.Location = new Point(0, 0);
            headBar.Visible = false;
            this.Controls.Add(headBar);
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += (object sender, EventArgs e) =>
            {
                var cursorPoint = this.PointToClient(Cursor.Position);
                if (this.ClientRectangle.Contains(cursorPoint))
                {
                    if (!headBar.Visible)
                    {
                        headBar.Width = this.Width;
                        headBar.BringToFront();
                        headBar.Show();
                    }
                }
                else
                {
                    if (headBar.Visible)
                        headBar.Hide();
                }
            };
            timer.Start();
            this.SizeChanged += (sender, e) =>
            {
                if (headBar.Visible)
                {
                    headBar.Width = this.Width;
                }
            };
        }
        /// <summary>
        /// 模拟器类型及其显示位置，快速进行显示或隐藏模拟器
        /// </summary>
        private Dictionary<EMonitorType, int> MonitorLayouts = new Dictionary<EMonitorType, int>();
        public void InitMonitorLayouts()
        {
            var allMonitorTypes = new List<EMonitorType>();
            allMonitorTypes.AddRange(Enum.GetValues(typeof(EMonitorType)).Cast<EMonitorType>());
            foreach (var item in allMonitorTypes)
            {
                this.mainLayout.RowCount++;
                this.mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                MonitorLayouts[item] = this.mainLayout.RowCount - 1;
            }
        }
        private void RefreshDisplayMonitors()
        {
            var allMonitorTypes = MonitorLayouts.Keys.ToList();
            //获取显示的内容
            var displayMonitors = allMonitorTypes.Except(ConfigManager.Config.HiddenMonitors).ToHashSet();

            //this.mainLayout.SuspendLayout();

            foreach(var kv in MonitorLayouts)
            {
                var monitorType = kv.Key;
                var row = kv.Value;
                var control = this.mainLayout.GetControlFromPosition(0, row);
                if (displayMonitors.Contains(monitorType) && control == null)
                {
                    var monitorControl = InitMonitorControl(monitorType);
                    monitorControl.Dock = DockStyle.Fill;
                    this.mainLayout.Controls.Add(monitorControl, 0, row);
                }
                else if (!displayMonitors.Contains(monitorType) && control != null)
                {
                    this.mainLayout.Controls.Remove(control);
                }
            }
            //this.mainLayout.ResumeLayout();
            //如果没有显示的了，留个黑框
            if(displayMonitors.Count == 0)
            {
                this.Height = this.headBar.Height;
            }
            else
            {
                this.Height = this.mainLayout.Size.Height;
            }
            
        }

        private MonitorControl InitMonitorControl(EMonitorType monitorType)
        {
            MonitorControl monitorControl = null;

            var controlName = monitorType.ToString() + "MonitorControl";
            var controlTypeName = $"ComputerMonitorApp.MonitorControls.{controlName}";
            var controlType = Type.GetType(controlTypeName);
            monitorControl = Activator.CreateInstance(controlType) as MonitorControl;
            if(monitorControl == null)
            {
                throw new Exception($"无法从类型({controlTypeName})创建MonitorControl实例");
            }
            monitorControl.MonitorType = monitorType;
            //异步关联Monitor，不用等待需要长时间初始化的Monitor
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var monitor = MonitorFactory.GetMonitor(monitorType);
                    monitorControl.BindMonitor(monitor);
                }
                catch(Exception e)
                {
                    Log.Error("获取和绑定Monitor时失败："+e.Message,e);
                }
            });
            return monitorControl;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MonitorFactory.Close();
        }
        private void InitLocation()
        {
            //获取屏幕
            var screen = Screen.PrimaryScreen ?? Screen.AllScreens[0];
            if (!ConfigManager.Config.Screen.IsBlank())
            {
                screen = Screen.AllScreens
                    .Where(item => item.DeviceName == ConfigManager.Config.Screen)
                    .FirstOrDefault() ?? screen;
            }
            var location = ConfigManager.Config.Location;
            if (!ConfigManager.Config.ScreenSize.IsEmpty)
            {
                //如果分辨率调整了，避免位置超出屏幕边界，进行等比例调整
                location = new Point(
                    (int)(location.X * (screen.Bounds.Width * 1.0 / ConfigManager.Config.ScreenSize.Width)),
                    (int)(location.Y * (screen.Bounds.Height * 1.0 / ConfigManager.Config.ScreenSize.Height))
                    );
            }

            //如果超出了范围，再调整
            if (location.X < 0)
                location.X = 0;
            else if (location.X + this.Width > screen.Bounds.Width)
                location.X = screen.Bounds.Width - this.Width;

            if (location.Y < 0)
                location.Y = 0;
            else if (location.Y + this.Height > screen.Bounds.Height)
                location.Y = screen.Bounds.Height - this.Height;

            this.Location = location;
        } 
    }

}