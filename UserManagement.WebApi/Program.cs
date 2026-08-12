using System.Security.Cryptography;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using UserManagement.Application.Commands;
using UserManagement.Application.Common.Behaviours;
using UserManagement.Application.Queries;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Persistence.Interceptors;
using UserManagement.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// jwt setup
var iamPubKey = builder.Configuration["Jwt:PublicKeyPem"];
var rsa = RSA.Create();
rsa.ImportFromPem(iamPubKey);
var securityKey = new RsaSecurityKey(rsa);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
        };
    });

builder.Services.AddAuthorization();

// auditable interceptor
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<AuditableEntityInterceptor>();

// problem detail enhancement
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement                                 
    {                                                                                            
        [new OpenApiSecuritySchemeReference("Bearer", doc)] = []
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// register db context
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
    
    options
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(auditableInterceptor);
});

// register mediatr and validator pipeline
builder.Services.AddValidatorsFromAssembly(typeof(CreatePegawaiCommand).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetPegawaiByIdQuery).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

// register repository
builder.Services.AddScoped<IPegawaiRepository, PegawaiRepository>();
builder.Services.AddScoped<IJabatanRepository, JabatanRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() | app.Environment.IsEnvironment("Local"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();