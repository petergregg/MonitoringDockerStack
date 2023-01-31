using Microsoft.AspNetCore.Mvc;
using Monitored.API.Data;

namespace Monitored.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly MonitoredAPIDataContext _monitoredAPIDataContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MonitoredAPIDataContext monitoredAPIDataContext)
        {
            _logger = logger;
            _monitoredAPIDataContext = monitoredAPIDataContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult Get(int take = 10, int skip = 0)
        {
            _logger.LogInformation("Starting Get request");

            return Ok(_monitoredAPIDataContext.WeatherForecasts.OrderBy(p => p.Id).Skip(skip).Take(take));

        }
    }
}