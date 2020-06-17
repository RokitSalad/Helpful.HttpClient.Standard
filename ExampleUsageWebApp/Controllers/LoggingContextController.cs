using System.Collections.Generic;
using Helpful.Logging.Standard;
using Microsoft.AspNetCore.Mvc;

namespace ExampleUsageWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoggingContextController : ControllerBase
    {
        [HttpGet]
        public IDictionary<string, object> Get()
        {
            return LoggingContext.LoggingScope;
        }
    }
}
