using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp
{
    public static class Extensions
    {
        public static bool IsBlank(this String str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        public static String ToStringEx(this object obj)
        {
            if (obj == null) return "";
            return obj.ToString();
        }

        public static bool IsInDesignMode(this Component component)
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   component.Site?.DesignMode == true;
        }

        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}