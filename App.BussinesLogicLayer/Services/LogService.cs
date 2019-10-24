using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class LogService
    {
        public readonly RequestDelegate _next;
       // public readonly ILogger _logger;
        public LogService(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Exception exception = new Exception();
            try
            {
                throw new System.NullReferenceException();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            var token = context.Request.Query["token"];
            if (token != "12345678")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");                
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
