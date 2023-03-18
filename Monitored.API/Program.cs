using Elastic.Apm.SerilogEnricher;
using Microsoft.EntityFrameworkCore;
using Monitored.API.Data;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) => logger
       .WriteTo.Console()
       .MinimumLevel.Verbose()
       .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:ServerUrl"]))
       {
           AutoRegisterTemplate = true,
           AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
           FailureCallback = e => Console.WriteLine($"Unable to submit event to Elasticsearch {e.MessageTemplate}"),
       })
       .Enrich.WithElasticApmCorrelationInfo()
       .ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddDbContext<MonitoredAPIDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("monitoredapidb")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
