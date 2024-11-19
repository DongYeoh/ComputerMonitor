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

namespace ComputerMonitorApp;
public partial class HeadBar : UserControl
{
    private MainForm mainForm;
    private Dictionary<EMonitorType, ToolStripMenuItem> monitorMenuItems = new Dictionary<EMonitorType, ToolStripMenuItem>();
    public HeadBar(MainForm mainForm)
    {
        this.mainForm = mainForm;
        InitializeComponent();
        InitNotifyIcon();
        InitContextMenuStrip();
        SupportDragging();
    }
    private void InitNotifyIcon()
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("退出", null, (object? sender, EventArgs e) => {
            Application.Exit();
        });
        var notifyIcon = new NotifyIcon()
        {
            Icon = this.mainForm.Icon,
            Visible = false,
            ContextMenuStrip = contextMenu,
            Text = "资源监控器",
            BalloonTipText = "已最小化"
        };
        this.mainForm.SizeChanged += (object? sender, EventArgs e) => {
            if (this.mainForm.WindowState == FormWindowState.Minimized)
            {
                this.mainForm.Hide();
                notifyIcon.Visible = true;
            }
        };
        this.mainForm.FormClosed += (object? sender, FormClosedEventArgs e) => {
            notifyIcon.Visible = false;
        };
        notifyIcon.MouseDoubleClick += (object? sender, MouseEventArgs e)=>{
            this.mainForm.Show();
            this.mainForm.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        };
    }

    private void InitContextMenuStrip()
    {
        
        foreach (var monitorType in Enum.GetValues<EMonitorType>())
        {
            var item = this.mainContextMenuStrip.AddCheckMenuItem(monitorType.ToString());
            item.Tag = monitorType;
            item.Click += MonitorMenuItem_Click;
            monitorMenuItems[monitorType] = item;
        }
        this.mainContextMenuStrip.Items.Add(new ToolStripSeparator());
        var closeItem = this.mainContextMenuStrip.AddMenuItem("退出");
        closeItem.Click += (object? sender, EventArgs e) =>
        {
            Application.Exit();
        };
    }

    private void MonitorMenuItem_Click(object? sender, EventArgs e)
    {
        var item = sender as ToolStripMenuItem;
        if (item == null) return;
        var monitorType = (EMonitorType)item.Tag!;
        if (item.Checked)
        {
            this.mainForm.DisplayMonitor(monitorType);
        }
        else
        {

            //如果还剩一下一个，就不要隐藏了
            if (this.mainForm.GetMonitors().Count <= 1)
            {
                item.Checked = true;
            }
            else
            {
                this.mainForm.HideMonitor(monitorType);
            }
        }
        
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
                this.mainForm.Location = new Point(
                    this.mainForm.Location.X + point.X - startPoint.X
                , this.mainForm.Location.Y + point.Y - startPoint.Y
                );
                isDragged = true;
            }
        };
        this.MouseUp += (sender, e) =>
        {
            isDragging = false;
            if (isDragged)
            {
                this.mainForm.SaveLocation();
            }
        };
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        //最小化
        this.mainForm.WindowState = FormWindowState.Minimized;
    }

    private void mainContextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
        foreach (var item in monitorMenuItems.Values)
        {
            item.Checked = false;
        }
        //更改选中状态
        foreach (var item in mainForm.GetMonitors())
        {
            var menuItem = monitorMenuItems[item.MonitorType];
            menuItem.Checked = true;
        };
        
    }
}
