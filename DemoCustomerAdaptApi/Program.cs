using System;
using System.IO;
using System.Reflection;
using DemoCustomerAdaptApi.DataAccess;
using DemoCustomerAdaptApi.Domain.Configs;
using DemoCustomerAdaptApi.Domain.Logic;
using DemoCustomerAdaptApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration.GetConnectionString("local");
builder.Configuration["Serilog:WriteTo:0:Args:connectionString"] = dbConnectionString;
var generalConfig = builder.Configuration.GetSection("GeneralConfig").Get<GeneralConfiguration>();
// Add services to the container.

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddControllers();
builder.Services.AddControllers(options => { options.Filters.Add<ApiExceptionFilterAttribute>(); });
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
            {Title = "Demo Customer API", Version = "v1", Description = "Customer Api"});
    c.EnableAnnotations();
});
builder.Services.AddOptions();
builder.Services.Configure<GeneralConfiguration>(builder.Configuration.GetSection("GeneralConfig"));

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
builder.Services.AddDataAccess(builder.Configuration, generalConfig.UseInMemoryDatabase);
builder.Services.AddDomainLogic();
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            "AllowAnyCorsPolicy",
            builder => builder.WithOrigins(
                    "http://localhost:4200",
                    "http://127.0.0.1:4200",
                    "http://127.0.0.1:4201",
                    "https://127.0.0.1:4201")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    });


var app = builder.Build();
app.UseCors("AllowAnyCorsPolicy");
// Midlleware

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Migrate SQL

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    if (generalConfig.UseInMemoryDatabase)
        context.Database.EnsureDeleted();
    if (context.Database.IsSqlServer()) context.Database.Migrate();
}


app.Run();