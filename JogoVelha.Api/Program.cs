using JogoVelha.Api.Data;
using JogoVelha.Api.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ng", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200"));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("ng");
app.MapControllers();
app.Run();