using Microsoft.EntityFrameworkCore;
using PruebaAppinit.Infrastructure;
using PruebaAppinit.Application.Services;
using PruebaAppinit.Application.Interfaces;
using PruebaAppinit.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppinitDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RpsDatabase"),
        sql => sql.MigrationsAssembly("PruebaAppinit.Infrastructure.Infrastructure")));

builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<IGameRepository, EfGameRepository>();

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
