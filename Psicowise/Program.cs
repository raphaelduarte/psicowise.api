var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on the specified URL
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(49152); // Porta desejada
    // Caso queira adicionar mais portas, você pode configurar mais listeners aqui
    // options.ListenAnyIP(5001);
});

// Add services to the container.
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();