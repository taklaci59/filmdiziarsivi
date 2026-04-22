using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace filmdiziarsivi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Beklenmeyen bir hata oluştu: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Basitçe şık bir hata sayfasına yönlendirme veya JSON dönme
            // MVC uygulamasında genelde /Home/Error 'a yönlendirme de yapabiliriz.
            // Fakat middleware'den direct Response da yazabiliriz.

            context.Response.Redirect("/Home/Error");
            return Task.CompletedTask;
        }
    }
}
