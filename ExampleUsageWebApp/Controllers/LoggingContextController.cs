using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpful.Logging.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleUsageWebApp.Controllers
{
    [Route("api/[controller]")]
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
