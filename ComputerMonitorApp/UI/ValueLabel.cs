using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ComputerMonitorApp.UI
{
    public class ValueLabel : Label
    {
        public ValueLabel()
        {
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}