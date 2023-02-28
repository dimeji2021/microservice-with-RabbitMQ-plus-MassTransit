using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using play.common.Settings;
using play.identity.service.Entities;
using play.identity.service.Settings;

var builder = WebApplication.CreateBuilder(args);
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
var identityServerSettings = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>(); // instance of IdentityServerSettings class

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (
        mongoDbSettings.ConnectionString,
        serviceSettings.ServiceName
    );

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configure IdentityServer
builder.Services.AddIdentityServer(options => {
        options.Events.RaiseSuccessEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseErrorEvents = true;
        // options.Events.RaiseInformationEvents = true;

    })
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
    .AddInMemoryApiResources(identityServerSettings.ApiResources)
    .AddInMemoryClients(identityServerSettings.Clients)
    .AddInMemoryIdentityResources(identityServerSettings.IdentityResources)
    .AddDeveloperSigningCredential();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseIdentityServer(); // IdentityServer Middlewear
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();
