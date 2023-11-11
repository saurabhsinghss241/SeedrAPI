using SeedrService.Helpers;
using SeedrService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ISeedrLogin, SeedrLogin>();
builder.Services.AddTransient<ISeedr, Seedr>();

builder.Services.AddHttpClient<HttpClientWrapper>();
builder.Services.AddScoped<HttpClientWrapper>();

//Caching Service
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<MyMemoryCache>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
