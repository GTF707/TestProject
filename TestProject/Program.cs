using Microsoft.OpenApi.Models;
using Repository.Repository.Interface;
using Repository.Repository;
using TestProject;
using TestProject.Services;
using TestProject.Services.Interface;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Metrix API", Version = "v1" });
});

builder.Services.AddSignalR();
builder.Services.AddScoped<IMetrixRepository, MetricRepository>();
builder.Services.AddScoped<IDiskSpaceRepository, DiskSpaceRepository>();
builder.Services.AddScoped<IMetrixService, MetrixService>();

CultureInfo customCulture = new CultureInfo("ru-RU");
customCulture.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = customCulture;
CultureInfo.DefaultThreadCurrentUICulture = customCulture;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Metrix API V1");

    });
}

app.MapHub<SignalRChat>("/chatHub");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();