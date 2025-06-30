using System.Text.Json.Serialization;
using AluguelImoveis.Data;
using AluguelImoveis.Repositories;
using AluguelImoveis.Repositories.Interfaces;
using AluguelImoveis.Services;
using AluguelImoveis.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IImovelRepository, ImovelRepository>();
builder.Services.AddScoped<IAluguelRepository, AluguelRepository>();
builder.Services.AddScoped<ILocatarioRepository, LocatarioRepository>();

builder.Services.AddScoped<ILocatarioService, LocatarioService>();
builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<IAluguelService, AluguelService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
