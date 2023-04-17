using Application._Common.Interfaces.Infrastructure.Services;
using Application._Common.Interfaces.Persistence;
using Application.Users.Queries;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Persistence;
using WebUi.Helpers;
using WebUi.Utils.Extensions;
using WebUi.Utils.Middleware;

var builder = WebApplication.CreateBuilder(args);
const string AllowAll = "_allowAll";

// Add services to the container.
var currentEnvironment = builder.Environment;
builder.Services.AddDistributedMemoryCache();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddSessionStateTempDataProvider();

builder.Services.AddCors(cors =>
{
    cors.AddPolicy(AllowAll, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddSession(b => { b.IdleTimeout = TimeSpan.FromDays(1); });

//Валидаторы
builder.Services /*.AddFluentValidationAutoValidation()*/
    .AddValidatorsFromAssemblyContaining<IFinanceTrackerContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMediatR(typeof(GetUsersQuery).Assembly);
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps("Application");
    cfg.AddCollectionMappers();
});


if (!currentEnvironment.IsDevelopment())
{
    // Добавить реальный сервис, после добавления авторизации
    //builder.Services.AddScoped<IUserIdService, UserIdService>();
}
else
{
    //Mock Data with no SSO
    builder.Services.AddScoped<IUserIdService, SimpleUserIdService>();
}


var sqlConnString = builder.Configuration.GetConnectionString("FinanceTrackerContext");
builder.Services.AddTransient<IFinanceTrackerContext, FinanceTrackerContext>();
builder.Services.AddDbContext<FinanceTrackerContext>(options =>
{
    options.UseNpgsql(sqlConnString, builder =>
    {
        builder.CommandTimeout(120);
        //builder.UseNetTopologySuite();
    });
});


var app = builder.Build();

app.UseCustomExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*app.UseSwagger();
    app.UseSwaggerUI();*/
    app.UseOpenApi(configure =>
    {
        configure.PostProcess = (doc, httpReq) =>
        {
            var a = doc.ToJson();
            Console.WriteLine(a);
        };
    });
    app.UseSwaggerUi3(settings =>
    {
        settings.Path = "/api";
        settings.DocumentPath = "/api/specification.json";
    });
}

app.UseSecurity();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
if (currentEnvironment.IsDevelopment())
{
    app.UseCors(AllowAll);
}

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.MapControllers();


if (currentEnvironment.IsProduction() || currentEnvironment.IsTesting() || currentEnvironment.IsStaging())
{
    /*app.MapWhen(x => x.Request.Path.Value.StartsWith("/admin"), builder =>
    {
        builder.UseSpa(spa =>
        {
            var adminAppPath = Path.Combine(currentEnvironment.ContentRootPath, "adminapp");
            spa.Options.SourcePath = adminAppPath;

            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(currentEnvironment.WebRootPath, "admin"))
            };
        });
    });*/

    app.MapWhen(x => !x.Request.Path.Value.StartsWith("/admin"), builder =>
    {
        builder.UseSpa(spa =>
        {
            var frontAppPath = Path.Combine(currentEnvironment.ContentRootPath, "frontapp");
            //moved to content root folder
            spa.Options.SourcePath = currentEnvironment.ContentRootPath;

            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(currentEnvironment.WebRootPath, "frontapp"))
            };
        });
    });
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<FinanceTrackerContext>();
        var env = services.GetRequiredService<IHostEnvironment>();

        if (!env.IsProduction()) await context.Database.MigrateAsync();

        await SeedContextHelper.Seed(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        throw;
    }
}

app.Run();