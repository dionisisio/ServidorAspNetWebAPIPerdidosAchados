using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServidorAsp.Codigo;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conex�o com o PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar o DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Adicionar servi�os ao cont�iner
builder.Services.AddControllers();

// Configura��o de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem
              .AllowAnyMethod()  // Permite qualquer m�todo (GET, POST, etc.)
              .AllowAnyHeader(); // Permite qualquer cabe�alho
    });
});


// Configurar Swagger para a API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configurar o Kestrel para escutar em todas as interfaces (0.0.0.0)
app.Urls.Add("http://0.0.0.0:5155");  // HTTP
app.Urls.Add("https://0.0.0.0:7099"); // HTTPS

// Aplicar as migra��es automaticamente ao iniciar a aplica��o
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Aplica as migra��es pendentes automaticamente
}

// Configurar o pipeline de solicita��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
