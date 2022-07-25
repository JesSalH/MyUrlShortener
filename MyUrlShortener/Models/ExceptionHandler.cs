using Newtonsoft.Json;
using System.Net;

namespace MyUrlShortener.Models
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, env, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, Exception exception)
        {
            string result;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = new
            {
                message = exception.Message
            };

            result = JsonConvert.SerializeObject(errorMessage);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
