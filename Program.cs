using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal;
using newsletter_form_api.Dal.Repositories.Implementations;
using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Services.Implementations;
using newsletter_form_api.Services.Interfaces;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Define common resource attributes for all telemetry
var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(
        serviceName: "newsletter-form-api",
        serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
        serviceInstanceId: Environment.MachineName)
    .AddAttributes(new Dictionary<string, object>
    {
        ["deployment.environment"] = builder.Environment.EnvironmentName
    });
    
// Configure OpenTelemetry logging
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(resourceBuilder)
        .AddOtlpExporter(a =>
        {
            a.Endpoint = new Uri(builder.Configuration["OpenTelemetry:Endpoint"]??"http://localhost:5341/ingest/otlp/v1/logs");
            a.Protocol = Enum.Parse<OtlpExportProtocol>(builder.Configuration["OpenTelemetry:Protocol"]??"HttpProtobuf");
            a.Headers = builder.Configuration["OpenTelemetry:Headers"];
        });

    // Add console exporter for local debugging
    if (builder.Environment.IsDevelopment())
    {
        options.AddConsoleExporter();
    }
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NewsletterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLoggerFactory(null) //disable EF Core logging
);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
builder.Services.AddScoped<ICommunicationPreferenceRepository, CommunicationPreferenceRepository>();

// Register services
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ICommunicationPreferenceService, CommunicationPreferenceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
