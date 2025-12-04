using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lab2.Data;
// THÊM DÒNG NÀY ĐỂ SỬ DỤNG LỚP SeedData
using Lab2.Models;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext (Đã có)
builder.Services.AddDbContext<Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Lab2Context") ?? throw new InvalidOperationException("Connection string 'Lab2Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ====================================================================
// KHỐI LỆNH GỌI SEED DATA KHI ỨNG DỤNG KHỞI ĐỘNG (Start-up)
// ====================================================================

// Tạo một phạm vi dịch vụ để lấy DbContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Lấy DbContext
        var context = services.GetRequiredService<Lab2Context>();

        // Khởi tạo và điền dữ liệu mẫu
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Ghi log lỗi nếu có vấn đề khi tạo hoặc seed DB
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
// ====================================================================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();