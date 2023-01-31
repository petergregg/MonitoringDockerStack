using Microsoft.EntityFrameworkCore;

namespace Monitored.API.Data
{
    public class MonitoredAPIDataContext : DbContext
    {
        public MonitoredAPIDataContext(DbContextOptions<MonitoredAPIDataContext> options) : base(options)
        {
            Database.EnsureCreated();
            this.Seed();
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
