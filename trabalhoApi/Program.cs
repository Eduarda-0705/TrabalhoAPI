using Microsoft.EntityFrameworkCore;
using TrabalhoApi;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("ConnPadrao");

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