using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;

namespace ConsoleApp
{
    public class LogManager
    {
        public static void InitDefaultLogger()
        {
            var configuration = new LoggerConfiguration();
            foreach (LogEventLevel level in Enum.GetValues(typeof(LogEventLevel)))
            {
                configuration.WriteTo.Logger(
                    lc=>lc
                    .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Debug)
                    .WriteTo.File(
                        path: $"logs/{level.ToString()}.txt", 
                        fileSizeLimitBytes: 10 * 1024 * 1024, //10M, 
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}][{SourceContext}-] {Message:lj}{NewLine}{Exception}", 
                        rollOnFileSizeLimit: true, 
                        retainedFileCountLimit: 10)
                    );
            }
            Log.Logger = configuration.CreateLogger();
        }
        public static void Close()
        {
            Log.CloseAndFlush();
        }
    }
}
