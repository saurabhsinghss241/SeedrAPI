For best performance HttpClientWrapper instance lifecycle should be Signleton.

#Register Request Client in Service.
  Using Autofac, add in Program.cs
  builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
  var config = builder.Configuration;
  builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
  {
    builder.Register(c => new RequestClient(config.GetSection("ServiceConfig").Get<RequestClientOptions>())).Keyed<IRequestClient>("LoadTestConfigKey").SingleInstance();
    builder.RegisterType<ServiceName>().As<IServiceName>().WithAttributeFiltering();
  });

#Add Config in your Service appsettings.json

"ServiceNameConfig": {
    "BaseUrl": "http://httpstat.us/",
    "TimeoutPolicyConfig": {
      "Seconds": 180
    },
    "RetryPolicyConfig": {
      "RetryCount": 2
    },
    "CircuitBreakerPolicyConfig": {
      "AllowExceptions": 10,
      "BreakDuration": 30
    }
}