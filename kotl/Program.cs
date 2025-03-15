using Domain.Repositories;
using Domain.Repositories.IUserRepository;
using kotl.RSAkeys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using Model.Repository;
using Model.Repository.AutontificationRepository;
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
        // �������� �� ��������, ��������� � ���������, ��� ���� �������� ������ �� ����.
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // ������ ���������� ��������� � ���������� ���������
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        // ����� �������� ������������� RSA-�����
        IssuerSigningKey = new RsaSecurityKey(keyGetterRSA.GetPublicKey())
    }
    );

builder.Services.AddScoped<DbContext>();

builder.Services.AddScoped<IBoilerRepository, BoilerRepostory>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.AddScoped<IUserAutoantificationService, UserAutoantificationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt => 
{
    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        // ����� ��������� � �����������
        In = ParameterLocation.Header,
        Description = "Enter token",
        // ��� ��������� � ������� ����� ������������ ����� - "Autorization"
        Name = "Autorization",
        // ��� ����� ������������ 
        Type = SecuritySchemeType.Http,
        // ������ ������
        BearerFormat = "Token",
        Scheme = "bearer"
    });
    // ��� ������� ������ ������������ ����� ������������ Bearer
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
}
);



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
