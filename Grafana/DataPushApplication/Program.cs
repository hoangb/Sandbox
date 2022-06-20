using App.Metrics;
using App.Metrics.Formatters.Prometheus;
using App.Metrics.Gauge;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
IMetricsRoot metrics;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMetrics(metrics = AppMetrics.CreateDefaultBuilder().Build());

var app = builder.Build();



app.UseMetricsAllMiddleware();
app.UseMetricsEndpoint(new MetricsPrometheusTextOutputFormatter());

var gauge = new GaugeOptions
{
    Name = "My gauge",
    MeasurementUnit = Unit.Calls
};

metrics.Measure.Gauge.SetValue(gauge, 1.1);

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
