# Register Service Program.cs

builder.Services.AddScoped<ISeedrLogin, SeedrLogin>(sp => new SeedrLogin(config.GetSection("SeedrLoginConfig").Get<SeedrLoginConfig>(), sp.GetRequiredService<IRequestClient>()));
