using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Psicowise.Configuration;

var builder = WebApplication.CreateBuilder(args);

var kestrelConfig = builder.Configuration.GetSection("Kestrel").Get<KestrelConfiguration>();

builder.WebHost.ConfigureKestrel(options =>
{
    var httpEndpoint = kestrelConfig.Endpoints.Http;
    var httpsEndpoint = kestrelConfig.Endpoints.Https;

    options.ListenAnyIP(httpEndpoint.Port);

    if (httpsEndpoint != null)
    {
        options.Listen(IPAddress.Any, httpsEndpoint.Port, listenOptions =>
        {
            listenOptions.UseHttps(httpsEndpoint.Certificate.Path, httpsEndpoint.Certificate.Password);
        });
    }
});

// Add services to the container.
builder.Services.AddControllers();
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