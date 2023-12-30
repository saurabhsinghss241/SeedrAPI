using CachingService;
using CachingService.Interfaces;
using HashingService;
using ResilientClient;
using ResilientClient.Intefaces;
using Seedr.Service;
using Seedr.Service.Interfaces;
using TorrentEngine.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var config = builder.Configuration;

builder.Services.AddSingleton<IRequestClient, RequestClient>(sp => new RequestClient(config.GetSection("RequestClientConfig").Get<RequestClientOptions>()));
builder.Services.AddSingleton<IHashingService, BCryptService>();
builder.Services.AddSingleton<ICacheService, RedisCacheService>(sp => new RedisCacheService(config.GetSection("RedisCacheConfig").Get<RedisCacheConfig>()));

builder.Services.AddScoped<ISeedrLogin, SeedrLogin>(sp => new SeedrLogin(config.GetSection("SeedrLoginConfig").Get<SeedrLoginConfig>(), sp.GetRequiredService<IRequestClient>()));
builder.Services.AddScoped<ILogin, Login>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
