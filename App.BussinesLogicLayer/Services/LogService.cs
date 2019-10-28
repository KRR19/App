using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class LogService
    {
        public readonly RequestDelegate _next;
        public readonly ILogger logger;
        public LogService(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            logger = logFactory.CreateLogger("Exception");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string exeption = $"{ex.Data} + {ex.InnerException} + {ex.StackTrace} + {ex.Source}+ {ex.Message} + {ex.HelpLink}";
                logger.LogInformation(exeption);
            }
        }
    }
}