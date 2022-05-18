using System.Linq;
using CrudOperations.Extensions;
using CrudOperations.Middlewares;
using LoggerService.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlService(builder.Configuration);
builder.Services.AddMediatR(typeof(Application.Assembly.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Program));
// register action filter as a service
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
// disable automatic validation
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    }).AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.Assembly.AssemblyReference).Assembly);
builder.Services.AddCustomMediaTypes();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionMiddlewareHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

NewtonsoftJsonInputFormatter GetJsonPatchInputFormatter() => 
    new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
        .OfType<NewtonsoftJsonInputFormatter>().First();