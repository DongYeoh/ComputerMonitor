using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ComputerMonitorApp.MonitorControls
{
    public class BlackTheme : MonitorControlTheme
    {
        public override Color BackgroundColor => ColorTranslator.FromHtml("#121212");

        public override Color NameColor => ColorTranslator.FromHtml("#FF9800");

        public override Color ValueColor => ColorTranslator.FromHtml("#F5F5F5");

        public override Color ChartLineColor => ColorTranslator.FromHtml("#4CAF50");

        public override Color MonitorSplitColor => Color.LightGray;
    }
}
