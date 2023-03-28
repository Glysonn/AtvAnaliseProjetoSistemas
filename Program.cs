using Microsoft.EntityFrameworkCore;
using AttAnalise.Context;

var builder = WebApplication.CreateBuilder(args);


// configurando o CORS para permitir as requisições do frontend

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://127.0.0.1:5500")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adicionando o banco em memória do entity framework na API
builder.Services.AddDbContext<LojaContext> (options => options.UseInMemoryDatabase("Loja"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
