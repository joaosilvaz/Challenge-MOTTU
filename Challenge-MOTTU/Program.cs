using Microsoft.EntityFrameworkCore;
using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Services;
using Challenge_MOTTU.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));



builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPendingService, PendingService>();
builder.Services.AddScoped<IBikeService, BikeService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
