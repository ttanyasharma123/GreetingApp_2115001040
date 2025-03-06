using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Middleware_Layer.GlobalExceptionHandler
{
    public class GlobalExceptionFilter
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionFilter(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next)); 
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = exception.Message,
                ExceptionType = exception.GetType().Name
            };

            string jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
