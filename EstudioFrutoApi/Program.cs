using EstudioFrutoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL do front-end Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuração do DbContext
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Estúdio Fruto API",
        Version = "v1",
        Description = "API para gerenciamento de agenda e instrutores do Estúdio Fruto.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe Estúdio Fruto",
            Email = "contato@estudiofruto.com",
            Url = new Uri("https://estudiofruto.com")
        }
    });
});

var app = builder.Build();

// Habilitar o CORS
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
