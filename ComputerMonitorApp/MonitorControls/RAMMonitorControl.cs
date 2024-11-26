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

namespace ComputerMonitorApp.MonitorControls
{
    public partial class RAMMonitorControl : MonitorControl
    {
        public RAMMonitorControl()
        {
            InitializeComponent();
            ApplyTheme(new BlackTheme());
        }
        public override void ApplyTheme(MonitorControlTheme theme)
        {
            base.ApplyTheme(theme);
            this.nameLabel1.ForeColor = theme.NameColor;
            this.valueLabel1.ForeColor = theme.ValueColor;
        }
        protected override void OnMonitorChanged(MonitorEventArgs e)
        {
            base.OnMonitorChanged(e);
            if(e is RAMMonitorEventArgs args)
            {
                this.SafeInvoke(() => {
                    this.valueLabel1.Text = (int)args.Load + "%";
                });
                
            }
        }
    }
    
} 
