using XWEAR_API.Models; // Убедитесь, что пространство имён правильное
using Microsoft.EntityFrameworkCore; // Необходимо для UseMySql
using Pomelo.EntityFrameworkCore.MySql; // Необходимо для UseMySql

var builder = WebApplication.CreateBuilder(args);

// Добавляем DbContext для MySQL
builder.Services.AddDbContext<XwearDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection") // Рекомендуется хранить строку подключения здесь
        ?? "server=localhost;user=root;password=root;database=xwear_db", // Резервная строка, как в вашем XwearDbContext.cs
        new MySqlServerVersion(new Version(9, 0, 1)) // Укажите версию вашего MySQL сервера, как в вашем файле
    ));

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

app.Run();