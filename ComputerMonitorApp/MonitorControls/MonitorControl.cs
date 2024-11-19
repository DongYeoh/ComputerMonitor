﻿using ComputerMonitorApp.Monitors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp.MonitorControls;
public partial class MonitorControl : UserControl
{
    public Monitors.Monitor? Monitor { get; protected set; }
    public MonitorControl()
    {
        InitializeComponent();
    }
    protected void SafeInvoke(Action action)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(action);
        }
        else
        {
            action();
        }
    }
}