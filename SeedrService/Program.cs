using Microsoft.Extensions.Configuration;
using ResilientClient;
using SeedrService.Helpers;
using SeedrService.Service;
using System.ComponentModel.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ISeedrLogin, SeedrLogin>();
builder.Services.AddTransient<ISeedr, Seedr>();
builder.Services.AddTransient<IStreamTape, StreamTape>();

builder.Services.AddHttpClient<SeedrService.Helpers.HttpClientWrapper>();
builder.Services.AddScoped<SeedrService.Helpers.HttpClientWrapper>();

//builder.Services.AddHttpClient("SeedrAuthClient", client =>
//{
//    client.BaseAddress = new Uri("https://www.seedr.cc/oauth_test/token.php");
//});

//builder.Services.AddHttpClient("SeedrServiceClient", client =>
//{
//    client.BaseAddress = new Uri("https://www.seedr.cc/oauth_test/resource.php");
//});

//builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();


//Caching Service
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<MyMemoryCache>();
//builder.Services.AddSingleton<ICaching,RedisCaching>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<Program>();

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

app.Run();
