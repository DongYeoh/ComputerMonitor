using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp;
public static class Extensions
{
    public static bool IsBlank(this String str)
    {
        return String.IsNullOrWhiteSpace(str);
    }
}
