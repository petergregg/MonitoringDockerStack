using Microsoft.AspNetCore.Mvc.Testing;
using Monitored.FunctionalTests.Base;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Monitored.FunctionalTests
{
    public class WeatherForecastScenarios : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        public WeatherForecastScenarios(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_get_filtered_weather_forecasts_and_response_ok_status_code()
        {
            // Act
            var response = await _client.GetAsync("WeatherForecast?take=10&skip=0");

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
