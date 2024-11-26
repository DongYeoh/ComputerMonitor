using ComputerMonitorApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ComputerMonitorApp
{
    public partial class HeadBar : UserControl
    {
        public MainForm MainForm { get; set; }
        private Dictionary<EMonitorType, ToolStripMenuItem> monitorMenuItems = new Dictionary<EMonitorType, ToolStripMenuItem>();
        public HeadBar()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsInDesignMode()) return;
            InitNotifyIcon();
            InitContextMenuStrip();
            SupportDragging();
        }
        private void InitNotifyIcon()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("退出", null, (object sender, EventArgs e) =>
            {
                Application.Exit();
            });
            var notifyIcon = new NotifyIcon()
            {
                Icon = this.MainForm.Icon,
                Visible = false,
                ContextMenuStrip = contextMenu,
                Text = "资源监控器",
                BalloonTipText = "已最小化，双击可以恢复显示"
            };
            this.MainForm.SizeChanged += (object sender, EventArgs e) =>
            {
                if (this.MainForm.WindowState == FormWindowState.Minimized)
                {
                    this.MainForm.Hide();
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000);
                }
            };
            this.MainForm.FormClosed += (object sender, FormClosedEventArgs e) =>
            {
                notifyIcon.Visible = false;
            };
            notifyIcon.MouseDoubleClick += (object sender, MouseEventArgs e) =>
            {
                this.MainForm.Show();
                this.MainForm.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            };
        }

        private void AddNetworkInterfaces()
        {
            if (!monitorMenuItems.ContainsKey(EMonitorType.Network)) return;
            var networkMenu = monitorMenuItems[EMonitorType.Network];
            networkMenu.DropDownOpening += (object sender, EventArgs e) =>
            {
                var ni_set = NetworkInterfaceHelper.GetCurrentNetworkInterface();
                foreach(ToolStripMenuItem item in networkMenu.DropDownItems)
                {
                    if(item.Tag.ToStringEx() == ni_set.Description)
                    {
                        item.Checked = true;
                        break;
                    }
                }
            };
            var interfaces = NetworkInterfaceHelper.GetNetworkInterfaces();
            foreach(var item in interfaces)
            {
                var menuItem = new ToolStripMenuItem(item.Name) { CheckOnClick = true };
                menuItem.Tag = item.Description;
                menuItem.Click +=(object sender, EventArgs e) => {
                    var clickedItem = (ToolStripMenuItem)sender;
                    var clickedItemTag = clickedItem.Tag.ToStringEx();
                    var ni_set = NetworkInterfaceHelper.GetCurrentNetworkInterface();
                    if (clickedItemTag == ni_set.Description)
                    {
                        clickedItem.Checked = true;
                        return;
                    }
                    //单选设置
                    foreach (ToolStripMenuItem it in networkMenu.DropDownItems)
                    {
                        if(it.Tag.ToStringEx() == clickedItemTag)
                        {
                            it.Checked = true;
                            ConfigManager.Config.NetworkInterface = clickedItemTag;
                            ConfigManager.Save();
                            ConfigManager.Config.OnConfigChanged(this, nameof(ConfigManager.Config.NetworkInterface));
                        }
                        else
                        {
                            it.Checked = false;
                        }
                    }
                };
                networkMenu.DropDownItems.Add(menuItem);
            }
        }

        private void InitContextMenuStrip()
        {
            foreach (EMonitorType monitorType in Enum.GetValues(typeof(EMonitorType)))
            {
                var item = this.mainContextMenuStrip.AddCheckMenuItem(monitorType.ToString());
                item.Tag = monitorType;
                item.Click += MonitorMenuItem_Click;
                monitorMenuItems[monitorType] = item;
            }
            AddNetworkInterfaces();

            this.mainContextMenuStrip.Items.Add(new ToolStripSeparator());

            var closeItem = this.mainContextMenuStrip.AddMenuItem("退出");
            closeItem.Click += (object sender, EventArgs e) =>
            {
                Application.Exit();
            };

            this.mainContextMenuStrip.Opening += MainContextMenuStrip_Opening;
        }

        private void MainContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            this.SuspendLayout();
            foreach (var item in monitorMenuItems.Values)
            {
                item.Checked = true;
            }
            //更改选中状态
            foreach (var item in ConfigManager.Config.HiddenMonitors)
            {
                var menuItem = monitorMenuItems[item];
                menuItem.Checked = false;
            };
            this.ResumeLayout();
        }

        private void MonitorMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item == null) return;
            var monitorType = (EMonitorType)item.Tag;
            if (item.Checked)
            {
                ConfigManager.Config.HiddenMonitors.Remove(monitorType);
            }
            else
            {
                ConfigManager.Config.HiddenMonitors.Add(monitorType);
            }
            ConfigManager.Save();
            ConfigManager.Config.OnConfigChanged(this,nameof(ConfigManager.Config.HiddenMonitors));

        }
        private void SupportDragging()
        {
            var startPoint = Point.Empty;
            var isDragging = false;
            this.MouseDown += (sender, e) =>
            {
                startPoint = e.Location;
                isDragging = true;
            };
            var isDragged = false;
            this.MouseMove += (sender, e) =>
            {
                if (isDragging)
                {
                    var point = e.Location;
                    this.MainForm.Location = new Point(
                        this.MainForm.Location.X + point.X - startPoint.X
                    , this.MainForm.Location.Y + point.Y - startPoint.Y
                    );
                    isDragged = true;
                }
            };
            this.MouseUp += (sender, e) =>
            {
                isDragging = false;
                if (isDragged)
                {
                    var formLocation = this.MainForm.Location;
                    var screen = Screen.FromPoint(formLocation);
                    //多屏幕情况下，计算相对所在屏幕的坐标位置
                    ConfigManager.Config.Location = new Point(formLocation.X - screen.Bounds.X, formLocation.Y - screen.Bounds.Y);
                    ConfigManager.Config.Screen = screen.DeviceName;
                    ConfigManager.Config.ScreenSize = screen.Bounds.Size;
                    ConfigManager.Save();
                }
            };
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            //最小化
            this.MainForm.WindowState = FormWindowState.Minimized;
            //pplication.Exit();
        }
    }
}
