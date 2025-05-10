using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal;
using newsletter_form_api.Dal.Repositories.Implementations;
using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Services.Implementations;
using newsletter_form_api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NewsletterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
