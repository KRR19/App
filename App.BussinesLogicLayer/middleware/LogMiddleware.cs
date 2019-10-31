using App.BussinesLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Middleware
{
    public class LogMiddleware
    {
        public readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);
                throw;
            }
        }
    }
}
