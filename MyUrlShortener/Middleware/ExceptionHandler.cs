using MyUrlShortener.Services.UrlShortener;
using Newtonsoft.Json;
using System.Net;

namespace MyUrlShortener.Middleware
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
            string exceptionMessage;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (env.IsDevelopment())
            {
                if (exception is UrlShortenerServiceException)
                {

                    exceptionMessage = $"Shortener Service Exception: {exception.Message}";
                } 
                else
                {
                    exceptionMessage = exception.Message;
                }
            } 
            else
            {
                exceptionMessage = "Could not process request";
            }

            var errorMessage = new
            {
                message = exceptionMessage
            };

            result = JsonConvert.SerializeObject(errorMessage);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
