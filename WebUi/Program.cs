using System.Reflection;
using Application._Common.Interfaces.Infrastructure.Services;
using Application._Common.Interfaces.Persistence;
using AutoMapper.EquivalencyExpression;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using WebUi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserIdService, SimpleUserIdService>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps("Application");
    cfg.AddCollectionMappers();
});
var sqlConnString = builder.Configuration.GetConnectionString("FinanceTrackerContext");
builder.Services.AddTransient<IFinanceTrackerContext, FinanceTrackerContext>();
builder.Services.AddDbContext<FinanceTrackerContext>(options =>
{
    options.UseNpgsql(sqlConnString, builder =>
    {
        builder.CommandTimeout(120);
        builder.UseNetTopologySuite();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FinanceTrackerContext>();
        var env = services.GetRequiredService<IHostEnvironment>();

        if (!env.IsProduction())
        {
            await context.Database.MigrateAsync();
        };

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


