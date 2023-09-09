using CommandApp.Interfaces;
using Core.Settings;
using Framework.Extensions;
using Framework.Extensions.Swagger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryApp.Interfaces;
using SqlRepository.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddServices();
builder.Services.InitSettings(builder.Configuration);
builder.Services.AddMediatR(typeof(ICommandApp).Assembly);
builder.Services.AddMediatR(typeof(IQueryApp).Assembly);
builder.Services.AddDbContext<TaxDbContext>(options =>
    options.UseSqlite(Settings.AllSettings.SqlSetting.DefaultConnection));


var app = builder.Build();

app.UseSwagger();


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
    c.RoutePrefix = string.Empty;
});


app.UseAuthorization();

app.MapControllers();

app.Run();