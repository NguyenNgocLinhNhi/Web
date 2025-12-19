using Lab8.Data;
using Lab8.Models;
using Lab8.Repository.Implementations;
// Thống nhất sử dụng namespace số ít "Repository" và "Service"
using Lab8.Repository.Interfaces;
using Lab8.Service.Implementations;
using Lab8.Services.Interfaces;
using Lab8.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- ĐĂNG KÝ DỊCH VỤ (Trước builder.Build) ---

// 1. Cấu hình DbContext kết nối SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Cấu hình Identity (Xác thực người dùng)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Đăng ký Repositoriy
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// 4. Đăng ký Service
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- CẤU HÌNH PIPELINE (Sau builder.Build) ---



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 5. Khởi tạo dữ liệu mẫu (Seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        // Đảm bảo class DbSeeder nằm trong namespace Lab8.Data
        DbSeeder.Seed(db);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Một lỗi đã xảy ra khi seeding dữ liệu.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Load tài nguyên từ wwwroot (SB Admin)

app.UseRouting();

app.UseAuthentication(); // Xác thực danh tính
app.UseAuthorization();  // Kiểm tra quyền hạn

// Cấu hình Route mặc định trỏ thẳng vào giao diện Admin Dashboard
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.MapRazorPages();

app.Run();