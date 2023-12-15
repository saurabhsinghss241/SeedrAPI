using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ResilientClient.Intefaces;
using ResilientClient;
using APITesting.Service;
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.DependencyInjection;
using CachingService;
using CachingService.Interfaces;
using HashingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var config = builder.Configuration;
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.Register(c => new RequestClient(config.GetSection("LoadTestConfig").Get<RequestClientOptions>())).Keyed<IRequestClient>("LoadTestConfigKey").SingleInstance();
    builder.RegisterType<LoadService>().As<ILoadService>().WithAttributeFiltering();

    //builder.Register(c => new RedisCacheService(config.GetSection("RedisCacheConfig").Get<RedisCacheConfig>())).As<ICacheService>().SingleInstance();
});


builder.Services.AddControllers();
//builder.Services.AddDistributedMemoryCache();

builder.Services.AddSingleton<ICacheService, RedisCacheService>(sp => new RedisCacheService(config.GetSection("RedisCacheConfig").Get<RedisCacheConfig>()));
builder.Services.AddSingleton<IHashingService, BCryptService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
