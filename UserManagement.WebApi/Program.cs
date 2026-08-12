using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Commands;
using UserManagement.Application.Common.Behaviours;
using UserManagement.Application.Queries;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Persistence.Interceptors;
using UserManagement.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// auditable interceptor
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<AuditableEntityInterceptor>();

// problem detail enhancement
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();