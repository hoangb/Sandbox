using App.Metrics;
using App.Metrics.Formatters.Prometheus;
using App.Metrics.Gauge;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MetricsUtil.Metrics = AppMetrics.CreateDefaultBuilder()
                        .OutputMetrics.AsPrometheusPlainText()
                        .OutputMetrics.AsPrometheusProtobuf()
                        .Build();

builder.Services.AddMetrics(MetricsUtil.Metrics);

var app = builder.Build();
app.UseMetricsAllMiddleware();
app.UseMetricsEndpoint(new MetricsPrometheusTextOutputFormatter());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class MetricsUtil
{
    // Singleton
    public static IMetricsRoot Metrics;
}


//var gauge = new GaugeOptions
//{
//    Name = "ATestGauge",
//    MeasurementUnit = Unit.Calls
//};

//MetricsUtil.Metrics.Measure.Gauge.SetValue(gauge, 1.1);