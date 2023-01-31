using AutoFixture;

namespace Monitored.API.Data
{
    public static class Seeder
    {

        // This is demo code, don't do this in a production application!
        public static void Seed(this MonitoredAPIDataContext monitoredAPIDataContext)
        {
            if (!monitoredAPIDataContext.WeatherForecasts.Any())
            {
                Fixture fixture = new Fixture();
                fixture.Customize<WeatherForecast>(weatherForecast => weatherForecast.Without(p => p.Id));
                // The next two lines add 100 rows to your database
                List<WeatherForecast> weatherForecasts = fixture.CreateMany<WeatherForecast>(100).ToList();
                monitoredAPIDataContext.AddRange(weatherForecasts);
                monitoredAPIDataContext.SaveChanges();
            }
        }
    }
}
