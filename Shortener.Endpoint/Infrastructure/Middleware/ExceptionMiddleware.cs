using System.Net;
using System.Text.Json;

namespace Shortener.Endpoint.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ShortenerException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(
                                        JsonSerializer.Serialize(new
                                        {
                                            Message = exception.Message
                                        })
                                    );
            }
            else
            {
                await context.Response.WriteAsync(
                                JsonSerializer.Serialize(new
                                {
                                    Message = "Internal Server Error from the custom middleware."
                                })
                            );
            }


        }
    }
}
