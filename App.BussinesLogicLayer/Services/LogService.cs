using Microsoft.Extensions.Logging;
using System;

namespace App.BussinesLogicLayer.Services
{
    public class LogService
    {
        public static void LogError(Exception ex)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateLogger("Exception");

            string exeption = $"{ex.Data} + {ex.InnerException} + {ex.StackTrace} + {ex.Source}+ {ex.Message} + {ex.HelpLink}";
            logger.LogInformation(exeption);
        }
    }
}