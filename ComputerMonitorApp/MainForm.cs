
using ComputerMonitorApp.Properties;
using System.Timers;
using OpenHardwareMonitor.Hardware;
using System.Collections.Specialized;
using System.Linq;
using ComputerMonitorApp.MonitorControls;
using ComputerMonitorApp.Monitors;
using System.Configuration;
using System.Threading;

namespace ComputerMonitorApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        InitForm();
    }
    private void InitForm()
    {
        this.mainLayout.Width = this.Width;
        InitHeadBar();
        //初始化显示位置
        InitLocation();
        //初始化监视器
        InitMonitors();
    }
    private void InitHeadBar()
    {
        var headBar = new HeadBar(this);
        headBar.Location = new Point(0,0);
        headBar.Visible = false;
        this.Controls.Add(headBar);
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 100;
        timer.Tick += (object? sender, EventArgs e) =>
        {
            var cursorPoint = this.PointToClient(Cursor.Position);
            if(this.ClientRectangle.Contains(cursorPoint))
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
                if(headBar.Visible)
                    headBar.Hide();
            }
        };
        timer.Start();
        this.SizeChanged += (sender, e) => {
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
    public void InitMonitors()
    {
        //获取显示的内容
        var hiddenMonitors = new List<EMonitorType>();
        foreach (var item in (Settings.Default.HiddenMonitors ?? new StringCollection()))
        {
            EMonitorType monitorType;
            if (Enum.TryParse<EMonitorType>(item, out monitorType))
            {
                hiddenMonitors.Add(monitorType);
            }
        }
        var displayMonitors = Enum.GetValues<EMonitorType>().Except(hiddenMonitors).ToList();

        displayMonitors = new List<EMonitorType>() { EMonitorType.CPU };

        this.mainLayout.SuspendLayout();    

        this.mainLayout.Controls.Clear();
        this.mainLayout.RowStyles.Clear();
        this.mainLayout.RowCount = 0;

        foreach (var item in Enum.GetValues<EMonitorType>())
        {
            this.mainLayout.RowCount++;
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            MonitorLayouts[item] = this.mainLayout.RowCount - 1;
        }
        foreach(var monitorType in displayMonitors)
        {
            var monitorControl = InitMonitorControl(monitorType);
            this.mainLayout.Controls.Add(monitorControl, 0, MonitorLayouts[monitorType]);
        }
        this.mainLayout.ResumeLayout();
        this.Size = this.mainLayout.Size;
    }

    private MonitorControl InitMonitorControl(EMonitorType monitorType)
    {
        switch (monitorType)
        {
            case EMonitorType.CPU:
                return new CPUMonitorControl();
            default:
                throw new Exception("unknown monitor type to init monitor control!");
        }
    }
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
    }
    private void InitLocation()
    {
        //获取屏幕
        var screen = Screen.PrimaryScreen ?? Screen.AllScreens[0];
        if (!Settings.Default.Screen.IsBlank())
        {
            screen = Screen.AllScreens
                .Where(item => item.DeviceName == Settings.Default.Screen)
                .FirstOrDefault() ?? screen;
        }
        var location = Settings.Default.Location;
        if (!Settings.Default.ScreenSize.IsEmpty)
        {
            //如果分辨率调整了，避免位置超出屏幕边界，进行等比例调整
            location = new Point(
                (int)(location.X * (screen.Bounds.Width*1.0/Settings.Default.ScreenSize.Width)),
                (int)(location.Y * (screen.Bounds.Height * 1.0 / Settings.Default.ScreenSize.Height))
                );
        }
        this.Location = location;
    }
    internal void SaveLocation()
    {
        Settings.Default.Location = this.Location;
        var screen = Screen.FromPoint(this.Location);
        Settings.Default.Screen = screen.DeviceName;
        Settings.Default.ScreenSize = screen.Bounds.Size;
        Settings.Default.Save();
    }
    internal void DisplayMonitor(EMonitorType monitorType)
    {
        var row = MonitorLayouts[monitorType];
        if (this.mainLayout.GetControlFromPosition(0, row) != null)
        {
            throw new Exception("duplicate！");
        }
        var monitorControl = InitMonitorControl(monitorType);
        this.mainLayout.Controls.Add(monitorControl, 0, row);
        SaveMonitors();
    }
    internal void HideMonitor(EMonitorType monitorType)
    {
        var row = MonitorLayouts[monitorType];
        var control = this.mainLayout.GetControlFromPosition(0, row);
        if (control == null)
        {
            throw new Exception("none to hide！");
        }
        this.mainLayout.Controls.Remove(control);
        SaveMonitors();
    }
    internal void SaveMonitors()
    {
        var monitors = this.GetMonitors().Select(item => item.MonitorType.ToString()).ToArray();
        var collection = new StringCollection();
        collection.AddRange(monitors);
        Settings.Default.HiddenMonitors = collection;
        Settings.Default.Save();
    }
    internal List<Monitors.Monitor> GetMonitors()
    {
        var monitors = new List<Monitors.Monitor>();
        for (var i = 0; i < this.mainLayout.RowCount; i++)
        {
            var item = this.mainLayout.GetControlFromPosition(0, i);
            var control = item as MonitorControl;
            if (control == null) continue;
            var monitor = control.Monitor;
            monitors.Add(monitor);
        }
        return monitors;
    }
}
