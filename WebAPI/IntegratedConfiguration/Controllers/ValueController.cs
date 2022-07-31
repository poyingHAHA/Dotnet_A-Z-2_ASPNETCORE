using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace IntegratedConfiguration.Controllers
{

    public class ValueController : Controller
    {
        private readonly IOptions<SmtpSettings> optSmpt;
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public ValueController(IOptions<SmtpSettings> optSmpt, IConnectionMultiplexer connectionMultiplexer)
        {
            this.optSmpt = optSmpt;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        [HttpGet]
        public string Demo1()
        {
            return optSmpt.Value.ToString();
        }
    }
}
