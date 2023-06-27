using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("UsuarioConnection");

// o identity será de um usuario
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    // dizer que estamos usando esse identity para nos comunicarmos com o banco de dados 
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// o cadastro sempre vai ser instanciado quando houver uma requisição nova, sempre a mesma requisição
builder.Services.AddScoped<CadastroService>();
// fara sempre uma instancia nova , mesmo que seja a mesma requisição
// builder.Services.AddTransient<CadastroService>()
// ia ser um unico cadastro service para todas as requisições que chegassem, a mesma instancia  
// builder.Services.AddSingleton<CadastroService>()

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<UsuarioDbContext>
    (opts => 
    {
       opts.UseMySql(connectionString, 
       ServerVersion.AutoDetect(connectionString)); 
    });
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
