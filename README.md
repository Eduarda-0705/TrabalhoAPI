# 🚀 API REST com ASP.NET Core + MySQL

## 📌 Descrição

Este projeto consiste em uma API REST desenvolvida com ASP.NET Core, utilizando Entity Framework Core e banco de dados MySQL (via XAMPP).

A API implementa operações CRUD completas para as entidades:

* Produto
* Categoria
* Cliente

Além disso, utiliza:

* Data Annotation para validações
* Swagger para documentação
* CORS para permitir acesso externo

---

## 🛠️ Tecnologias utilizadas

* C#
* ASP.NET Core Web API
* Entity Framework Core
* MySQL (XAMPP)
* Pomelo.EntityFrameworkCore.MySql
* Swagger (Swashbuckle)

---

## ⚙️ Como criar o projeto do zero

### 1. Criar o projeto

```bash
dotnet new webapi -n NomeDoProjeto
cd NomeDoProjeto
```

---

### 2. Instalar pacotes necessários

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Swashbuckle.AspNetCore
```

---

### 3. Estrutura de pastas

```txt
Controllers/
Models/
Data/
Program.cs
appsettings.json
```

---

## 🧱 Models (Entidades)

Criar as classes dentro da pasta `Models` usando Data Annotation:

Exemplo:

```csharp
using System.ComponentModel.DataAnnotations;

public class Produto
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Range(typeof(decimal), "0.01", "999999")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue)]
    public int Estoque { get; set; }
}
```

---

## 🗄️ DbContext

Criar na pasta `Data`:

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
}
```

---

## 🔌 Configuração do banco

### appsettings.json

```json
{
  "ConnectionStrings": {
    "ConnPadrao": "Server=127.0.0.1;Port=3307;Database=trabalho;User=root;Password="
  }
}
```

---

## ⚙️ Program.cs

```csharp
using Microsoft.EntityFrameworkCore;

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Liberado");

app.MapControllers();

app.Run();
```

---

## 📡 Controllers (CRUD)

Cada entidade deve ter um controller com:

* GET (listar)
* GET por ID
* POST (criar)
* PUT (atualizar)
* DELETE (remover)

---

## 🧪 Banco de dados (XAMPP)

1. Iniciar MySQL no XAMPP
2. Criar banco:

```sql
CREATE DATABASE trabalho;
```

---

## 🔄 Migrations

```bash
dotnet ef migrations add Inicial
dotnet ef database update
```

---

## ▶️ Executar o projeto

```bash
dotnet run
```

Acessar:

```txt
http://localhost:porta/swagger
```

---

## ✅ Funcionalidades implementadas

* CRUD completo
* Validação com Data Annotation
* Integração com MySQL
* Swagger funcionando
* CORS habilitado

---

## 📌 Observações importantes

* O banco deve se chamar **trabalho**
* Não usar validação manual com `if/else` nas models
* Cada entidade gera uma tabela no banco
* Utilizar Data Annotation para validação

---

## 📚 Aprendizados

Este projeto reforça conceitos como:

* API REST
* Entity Framework Core
* Migrations
* Integração com banco de dados
* Validação de dados
* Estrutura de projeto em camadas

---

