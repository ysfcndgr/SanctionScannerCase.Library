using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace SanctionScannerCase.Common.Logging
{
    public class SerilogLoggingService:ILoggingService
    {
        private readonly Logger _logger;

        public SerilogLoggingService()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File("C:/Logs/Log.txt") 
                .MinimumLevel.Information()
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            _logger.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public void LogError(string message, Exception exception = null)
        {
            _logger.Error(exception, message);
        }
    }
}
