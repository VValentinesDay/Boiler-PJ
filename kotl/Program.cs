using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Repository;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>
    (optons => optons.UseNpgsql(builder.Configuration.GetConnectionString("db")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        // ѕроверка на издател€, аудиторию и установка, что срок действи€ токена не истЄк.
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // —писок допустимых издателей и допустима€ аудитори€
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        // €вное указание использовани€ RSA-ключа
        IssuerSigningKey = new RsaSecurityKey(GetPublicKey())
    }
    );

static RSA GetPublicKey() 
    {
        var rsaReadFile = File.ReadAllText("/RSA/public_key.pem");
        var rsa = RSA.Create();
        rsa.ImportFromPem(rsaReadFile);
        return rsa;
    }



builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<IBoilerRepository, BoilerRepostory>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
