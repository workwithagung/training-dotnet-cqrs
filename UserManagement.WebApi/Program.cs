using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Queries;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// register db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// register mediatr
builder.Services.AddMediatR(cfg 
    => cfg.RegisterServicesFromAssembly(typeof(GetPegawaiByIdQuery).Assembly));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();