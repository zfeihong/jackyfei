using Iot.Application;
using Iot.Data;
using Iot.Identity;
using Iot.Identity.Helper;
using Iot.Shared;
using Iot.WebApi.Extensions;
using Iot.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
//using VueCliMiddleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructureData(builder.Configuration);
builder.Services.AddInfrastructureShared(builder.Configuration);
builder.Services.AddInfrastructureIdentity(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Extension
builder.Services.AddApiVersioningExtension();
builder.Services.AddVersionedApiExplorerExtension();
builder.Services.AddSwaggerGenExtension();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
 

//添加一个单页（SPA）的静态文件服务，确保放在SwaggerGenOptions下。
//builder.Services.AddSpaStaticFiles(configuration =>
//{
//    configuration.RootPath = "../vue-app/dist";
//});

//builder.Services.AddCors();
builder.Services.AddCors(c =>
{

    //一般采用这种方法
    c.AddPolicy("LimitRequests", policy =>
    {
        // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
        // 注意，http://127.0.0.1:5401 和 http://localhost:5401 是不一样的，尽量写两个
        policy
        .WithOrigins("http://127.0.0.1:8080", "http://localhost:8080", "http://127.0.0.1:3000", "http://localhost:3000")
        .AllowAnyHeader()//允许任何标头
        .AllowAnyMethod();//允许任何方法
    });
});

#region ApiVersion
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
});
# endregion 

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension(apiVersionDescriptionProvider);
}

app.UseStaticFiles();
//if (!app.Environment.IsDevelopment())
//{
//    app.UseSpaStaticFiles();
//}

//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。
app.UseCors("LimitRequests");

//app.UseCors(b => {
//    b.AllowAnyOrigin();
//    b.AllowAnyHeader();
//    b.AllowAnyMethod();
//});


app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "../vue-app";

//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseVueCli(npmScript: "serve");
//    }
//});

app.MapControllers();

var name = Assembly.GetExecutingAssembly().GetName();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", $"{name.Name}")
    .Enrich.WithProperty("Assembly", $"{name.Version}")
    .WriteTo.SQLite(
            Environment.CurrentDirectory + @"/Logs/log.db",
            restrictedToMinimumLevel: LogEventLevel.Information,
            storeTimestampInUtc: true)
    .WriteTo.File(
            new CompactJsonFormatter(),
            Environment.CurrentDirectory + @"/Logs/log.json",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();

// Wrap creating and running the host in a try-catch block
try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
