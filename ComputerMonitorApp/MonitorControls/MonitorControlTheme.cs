using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ComputerMonitorApp.MonitorControls
{
    public abstract class MonitorControlTheme
    {
        /// <summary>
        /// 背景颜色
        /// </summary>
        public abstract Color BackgroundColor { get; }
        /// <summary>
        /// 名称标签颜色
        /// </summary>
        public abstract Color NameColor { get; }
        /// <summary>
        /// 值颜色
        /// </summary>
        public abstract Color ValueColor { get; }
        /// <summary>
        /// 折线图颜色
        /// </summary>
        public abstract Color ChartLineColor { get; }

        public abstract Color MonitorSplitColor { get; }

    }

}