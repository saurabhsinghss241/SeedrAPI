#Adding InMemoryCache to your service.

Add these in Program.cs
	builder.Services.AddDistributedMemoryCache();
	builder.Services.AddSingleton<ICacheService,InMemoryCacheService>();

#Adding RedisCache to your service.
	Add these in your Program.cs, pick any one of the mentioned ways

	1. Basic Way
	builder.Services.AddSingleton<ICacheService, RedisCacheService>(sp => new RedisCacheService(config.GetSection("RedisCacheConfig").Get<RedisCacheConfig>()));

	2. Using AutoFac

	builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
	var config = builder.Configuration;
	builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
	{
		builder.Register(c => new RedisCacheService(config.GetSection("RedisCacheConfig").Get<RedisCacheConfig>())).As<ICacheService>().SingleInstance();
	});

#Add Config in your service appsettings.json

"RedisCacheConfig": {
    "Hostname": "redis-****.****.us-east-1-2.ec2.cloud.redislabs.com",
    "Port": "18863",
    "Username": "default",
    "Password": "yourpassword"
  },