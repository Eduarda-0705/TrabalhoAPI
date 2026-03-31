using Microsoft.EntityFrameworkCore;
using TrabalhoApi;

// Integrantes da Equipe:
// - Maria Eduarda Rosa da Costa
// - Matheus Miguel Barbosa
// - Mariele Leivas de Mattos

var builder = WebApplication.CreateBuilder(args);

// Pega a conexão do appsettings.json
var conn = builder.Configuration.GetConnectionString("ConnPadrao");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, ServerVersion.AutoDetect(conn))); // AutoDetect facilita no XAMPP

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Liberado", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Liberado");
app.UseAuthorization();
app.MapControllers();

app.Run();