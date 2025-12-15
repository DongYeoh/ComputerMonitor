using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp.Modules
{
    /// <summary>
    /// 置顶适配器
    /// </summary>
    public class TopMostAdapter : IDisposable
    {
        private ForegroundWatcher watcher;
        private bool isTopMostApplied;
        private Form form;
        public TopMostAdapter(Form form) {
            if (form == null)
            {
                 throw new ArgumentNullException(nameof(form)); 
            }
            this.form = form;
        }
        public void Init()
        {
            // 默认置顶
            ApplyTopMost(true);

            watcher = new ForegroundWatcher();
            watcher.ForegroundChanged += hwnd =>
            {
                if (!this.form.IsHandleCreated) return;
                // 如果前台就是自己，不必处理
                if (hwnd == this.form.Handle) return;

                if (this.form.InvokeRequired)
                {
                    this.form.Invoke(new Action(() => HandleForegroundChanged(hwnd)));
                }
                else
                {
                    HandleForegroundChanged(hwnd);
                }
            };
        }
        private void HandleForegroundChanged(IntPtr fgHwnd)
        {
            var procName = TryGetProcessName(fgHwnd);
            bool shouldGiveWayToMstsc = string.Equals(procName, "mstsc", StringComparison.OrdinalIgnoreCase);

            ApplyTopMost(!shouldGiveWayToMstsc);
        }

        private void ApplyTopMost(bool enable)
        {
            if (isTopMostApplied == enable) return; // 避免反复设置导致闪烁

            // 1) WinForms 属性
            this.form.TopMost = enable;

            // 2) 再用 SetWindowPos 强化一次，避免某些情况下不生效
            SetWindowPos(this.form.Handle,
                enable ? HWND_TOPMOST : HWND_NOTOPMOST,
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);

            isTopMostApplied = enable;
        }

        private static string TryGetProcessName(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) return null;

            GetWindowThreadProcessId(hwnd, out uint pid);
            if (pid == 0) return null;

            try { return Process.GetProcessById((int)pid).ProcessName; }
            catch { return null; }
        }

        // P/Invoke
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        public void Dispose()
        {
            if (watcher != null)
            {
                watcher.Dispose();
                watcher = null;
            }
        }
    }
    /// <summary>
    /// 监控前台窗口变化的类
    /// </summary>
    public sealed class ForegroundWatcher : IDisposable
    {
        // 前台窗口变化事件
        private const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
        private const uint WINEVENT_OUTOFCONTEXT = 0x0000;

        private readonly WinEventDelegate _callback;
        private IntPtr _hook;

        public event Action<IntPtr> ForegroundChanged;

        public ForegroundWatcher()
        {
            _callback = WinEventProc;
            _hook = SetWinEventHook(
                EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND,
                IntPtr.Zero, _callback, 0, 0, WINEVENT_OUTOFCONTEXT);
            if (_hook == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
            int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd != IntPtr.Zero)
                ForegroundChanged?.Invoke(hwnd);
        }

        public void Dispose()
        {
            if (_hook != IntPtr.Zero)
            {
                UnhookWinEvent(_hook);
                _hook = IntPtr.Zero;
            }
        }

        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
            int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWinEventHook(
            uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc,
            uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);
    }
}
