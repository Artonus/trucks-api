using TrucksApi.Config;
using TrucksApi.ExtensionMethods;
using TrucksApi.Installer;
using TrucksApi.Services;
using TrucksApi.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DbConfig>(builder.Configuration.GetSection(DbConfig.ConfigName));
var dbConfig = builder.Configuration.GetSection(DbConfig.ConfigName).Get<DbConfig>();
builder.Services.AddTrucksDbConnection(dbConfig);
builder.Services.AddScoped<IStartupDataInstaller, StartupDataInstaller>();
builder.Services.AddScoped<ITrucksService, TrucksService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataInstaller = scope.ServiceProvider.GetService<IStartupDataInstaller>();
    await dataInstaller!.Install();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
