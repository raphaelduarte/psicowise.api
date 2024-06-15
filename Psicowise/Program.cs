using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        options.ListenAnyIP(kestrelConfig.Endpoints.Http.Port);
    }

    if (kestrelConfig.Endpoints.Https != null)
    {
        var certificatePath = kestrelConfig.Endpoints.Https.Certificate.Path;
        var certificatePassword = kestrelConfig.Endpoints.Https.Certificate.Password;

        // Ajustar o caminho do certificado para o caminho dentro do contêiner, se necessário
        if (builder.Environment.IsProduction())
        {
            certificatePath = "/app/certs/https/aspnetapp.pfx";
        }

        options.Listen(IPAddress.Any, kestrelConfig.Endpoints.Https.Port, listenOptions =>
            {
                listenOptions.UseHttps(certificatePath, certificatePassword);
                Console.WriteLine("Successfully configured HTTPS", certificatePath);
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