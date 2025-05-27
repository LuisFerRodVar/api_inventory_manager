using inventory_manager.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Agrega servicios para controladores
builder.Services.AddControllers(); // Esto es lo importante
builder.Services.AddEndpointsApiExplorer();

// 👇 Agrega tus DAOs o servicios
builder.Services.AddScoped<UserDao>();
builder.Services.AddDbContext<InventoryManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Middleware
app.UseHttpsRedirection();

// ✅ Agrega uso de controladores
app.MapControllers();

app.Run();

