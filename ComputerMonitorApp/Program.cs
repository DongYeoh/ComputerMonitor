using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Serilog;

namespace ComputerMonitorApp
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //先初始化日志
                LogManager.InitDefaultLogger();
                Application.EnableVisualStyles();
                //捕获所有异常避免程序崩溃
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //主线程未捕获异常处理
                Application.ThreadException += Application_ThreadException;
                //全局异常处理
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                //异步异常
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch(Exception e)
            {
                //单独用一个日志文件存储程序启动时的错误
                File.AppendAllLines("start_error.txt",
                    new String[] {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +":"+ e.ToString() });
                MessageBox.Show("启动异常："+e.Message);
            }
            finally
            {
                LogManager.Close();
            }
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException($"UnobservedTaskException:{e.Exception.Message}",e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            HandleException($"UnhandledException:{exception?.Message}", exception);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException($"ThreadException:{e.Exception?.Message}", e.Exception);
        }
        private static void HandleException(String message,Exception exception,bool allowExit = true,bool allowLog = true)
        {
            if (allowLog)
            {
                Log.Error(exception, message);
            }
            if (allowExit)
            {
                if(DialogResult.Yes == MessageBox.Show(message+Environment.NewLine+"是否直接关闭程序？", "异常", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show(message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
