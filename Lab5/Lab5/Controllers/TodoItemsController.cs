using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5.Models; 

namespace Lab5.Controllers 
{
    [Route("api/[controller]")] 
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoDb _context;

        // Constructor nhận DbContext qua Dependency Injection
        public TodoItemsController(TodoDb context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            // Trả về tất cả TodoItems dưới dạng DTO
            return await _context.Todos
                .Select(x => new TodoItemDTO(x))
                .ToListAsync();
        }

        // Đây là đoạn code cuối trang 87.
        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await _context.Todos.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return new TodoItemDTO(todoItem); // Trả về DTO
        }
    }
}