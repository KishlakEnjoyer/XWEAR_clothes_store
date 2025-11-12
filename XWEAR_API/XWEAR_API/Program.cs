using XWEAR_API.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Добавляем DbContext для MySQL
builder.Services.AddDbContext<XwearDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "server=localhost;user=root;password=root;database=xwear_db", 
        new MySqlServerVersion(new Version(9, 0, 1)) 
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors("AllowAll");

app.Run();