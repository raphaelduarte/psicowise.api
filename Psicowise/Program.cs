using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Psicowise.Configuration;
using Psicowise.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Carregar configuração do Kestrel
var kestrelConfig = builder.Configuration.GetSection("Kestrel").Get<KestrelConfiguration>();

builder.WebHost.ConfigureKestrel(options =>
{
    // Configurar HTTP
    if (kestrelConfig.Endpoints.Http != null)
    {
        options.ListenAnyIP(HelperMethods.GetPortFromUrl(kestrelConfig.Endpoints.Http.Url));
    }

    // Configurar HTTPS
    if (kestrelConfig.Endpoints.Https != null)
    {
        options.Listen(IPAddress.Any, HelperMethods.GetPortFromUrl(kestrelConfig.Endpoints.Https.Url), listenOptions =>
        {
            listenOptions.UseHttps(kestrelConfig.Endpoints.Https.Certificate.Path, kestrelConfig.Endpoints.Https.Certificate.Password);
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