using Microsoft.EntityFrameworkCore;
using Lab5.Models; 
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình JSON (Tùy chọn: Đảm bảo enum được serialize dưới dạng chuỗi)
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// --- Cấu hình Middleware và Endpoints ---

// Endpoint GET đơn giản (Trang chào mừng)
app.MapGet("/", () => "Hello World!");

/*// Endpoint GET tất cả các mục to-do (Sử dụng DTO để ngăn over-posting)
app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync());*/

// Endpoint GET các mục to-do đã hoàn thành (Sử dụng DTO)
/*app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).Select(x => new TodoItemDTO(x)).ToListAsync());

// Endpoint GET mục to-do theo ID (Sử dụng DTO)
app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(new TodoItemDTO(todo))
            : Results.NotFound());
*/
// Endpoint POST: Thêm mục to-do mới (Nhận DTO)
/*app.MapPost("/todoitems", async (TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todoItem = new Todo
    {
        IsComplete = todoItemDTO.IsComplete,
        Name = todoItemDTO.Name
        // Trường 'Secret' bị bỏ qua, ngăn chặn over-posting
    };

    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
});
*/
// Endpoint PUT: Cập nhật mục to-do (Nhận DTO)
app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    // Chỉ cập nhật các trường từ DTO
    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Endpoint DELETE: Xóa mục to-do
app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(new TodoItemDTO(todo));
    }
    return Results.NotFound();
});

app.Run();

// Thêm services cho Controllers
builder.Services.AddControllers();

// Thêm services cho Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (Giữ lại cấu hình DbContext đã làm ở Lab 5)
builder.Services.AddDbContext<Lab5.Models.TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


