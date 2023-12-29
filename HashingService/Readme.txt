# To Register 
	builder.Services.AddSingleton<IHashingService, BCryptService>();

#To customise the HashingService we can inject a config from appsettings and handle that in constructor.