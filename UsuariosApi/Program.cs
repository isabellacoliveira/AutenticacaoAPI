using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConnectionStrings:UsuarioConnection"];

// o identity será de um usuario
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    // dizer que estamos usando esse identity para nos comunicarmos com o banco de dados 
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// o cadastro sempre vai ser instanciado quando houver uma requisição nova, sempre a mesma requisição
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();
// fara sempre uma instancia nova , mesmo que seja a mesma requisição
// builder.Services.AddTransient<CadastroService>()
// ia ser um unico cadastro service para todas as requisições que chegassem, a mesma instancia  
// builder.Services.AddSingleton<CadastroService>()

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    // quais os parametros que vamos validar no nosso token 
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        // campo a campo que queremos colocar 
        ValidateIssuerSigningKey  = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration ["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

// precisamos definir a policy e como ela será cumprida 
// nome da policy e qual a condição de funcionamento 
builder.Services.AddAuthorization(options => {
    options.AddPolicy("IdadeMinima", policy => 
    // ela tem que ter uma classe para representar esse requisito 
        policy.AddRequirements(new IdadeMinima(18))
    );
});
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

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
app.UseAuthentication();

app.MapControllers();

app.Run();


