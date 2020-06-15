using System.Linq;
using System.Threading.Tasks;
using Helpful.Logging.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Helpful.HttpClient.Standard
{
    public class HeaderPersistenceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _headersToPersist;

        public HeaderPersistenceMiddleware(RequestDelegate next, params string[] headersToPersist)
        {
            _next = next;
            _headersToPersist = headersToPersist;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            foreach (var header in _headersToPersist)
            {
                if (context.Response.Headers.TryGetValue(header, out StringValues headerValue))
                {
                    LoggingContext.Set(header, headerValue);
                }
            }

            await _next(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderPersistenceMiddleware(this IApplicationBuilder builder, params string[] headersToPersist)
        {
            return builder.UseMiddleware<HeaderPersistenceMiddleware>(headersToPersist);
        }
    }
}
