using Microsoft.EntityFrameworkCore;
using TrabalhoApi;

var builder = WebApplication.CreateBuilder(args);

// Pega a conexão do appsettings.json
var conn = builder.Configuration.GetConnectionString("ConnPadrao");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, ServerVersion.AutoDetect(conn))); // AutoDetect facilita a vida no XAMPP

// Configuração do CORS (Obrigatório pelo professor)
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

// Swagger só aparece em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Liberado");
app.UseAuthorization();
app.MapControllers();

app.Run();