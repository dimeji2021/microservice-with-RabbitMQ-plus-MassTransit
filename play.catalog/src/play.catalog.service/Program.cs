using play.catalog.service.Entities;
using play.common.MongoDB;
using play.common.MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using play.common.Settings;
using play.common.Identity;

var builder = WebApplication.CreateBuilder(args);
var configValue = builder.Configuration.GetSection("AllowedOrigin").Get<string[]>();
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
// Add services to the container.

builder.Services.AddMongo()
    .AddMongoRepository<Item>("items")
    .AddMassTransitWithRabbitMq()
    .AddJwtBearerAuthentication();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // .AddJwtBearer(options =>
    // {
        // options.Authority = "https://localhost:7211";
        // options.Audience = serviceSettings.ServiceName;
    // });
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder =>
    {
        builder.WithOrigins(configValue) // To allow the origin specified in this appsettings.development, can either take in a string or an array of strings
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
    // app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); // To allow any origin
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
