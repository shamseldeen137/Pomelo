using Microsoft.EntityFrameworkCore;
using Pomelo;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Server=localhost;Database=pomelotest;User=root;Password=root;";


// Register DbContext using your Pomelo extension
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.ConfigureMySql(connectionString, retryCount: 3)
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

