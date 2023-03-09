using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HeroDatingApp.client.Errors;

namespace HeroDatingApp.client.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
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
              _logger.LogError(ex, ex.Message);
              context.Response.ContentType = "application/json";
              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

              var response = _environment.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

              var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

              var json = JsonSerializer.Serialize(response, options);

              await context.Response.WriteAsync(json);
            }
        }
    }
}
