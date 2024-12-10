using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()    // Permite qualquer origem
               .AllowAnyMethod()    // Permite qualquer método HTTP (GET, POST, PUT, DELETE...)
               .AllowAnyHeader();   // Permite qualquer cabeçalho
    });
});

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tarefas_categoria.db"));

// Adicionando controladores (API)
builder.Services.AddControllers();

var app = builder.Build();

// Usar CORS com a política configurada
app.UseCors("AllowAll");

// Configuração do pipeline de requisição
app.UseAuthorization();

// Mapear os controladores
app.MapControllers();

// Iniciar a aplicação
app.Run();
