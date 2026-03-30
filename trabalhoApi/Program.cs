using Microsoft.EntityFrameworkCore;
using TrabalhoApi;

var builder = WebApplication.CreateBuilder(args);

var conn = "Server=127.0.0.1;Port=3307;Database=trabalho;User=root;Password="; //Essa parte foi colocada para funcionar o xampp que estava dando muito erro.
Console.WriteLine(conn);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 36))));

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

app.UseHttpsRedirection();
app.UseCors("Liberado");
app.UseAuthorization();
app.MapControllers();

app.Run();