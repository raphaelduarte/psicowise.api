var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on the specified URL
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(49999); // Porta desejada
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();