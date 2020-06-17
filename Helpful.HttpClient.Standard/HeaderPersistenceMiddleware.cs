using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpful.Logging.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Helpful.HttpClient.Standard
{
    public class HeaderPersistenceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HelpfulLoggingConfiguration _configuration;

        public HeaderPersistenceMiddleware(RequestDelegate next, IOptions<HelpfulLoggingConfiguration> configuration)
        {
            _next = next;
            _configuration = configuration.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            foreach (var header in _configuration.HeadersToPersist)
            {
                if (context.Request.Headers.TryGetValue(header, out StringValues headerValue))
                {
                    LoggingContext.Set(header, headerValue);
                }
            }

            await _next(context);
        }
    }

    public class HelpfulLoggingConfiguration
    {
        public const string APP_SETTINGS_KEY = "Helpful.Logging:Configuration";

        public string[] HeadersToPersist { get; set; }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderPersistenceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderPersistenceMiddleware>();
        }
    }
}
