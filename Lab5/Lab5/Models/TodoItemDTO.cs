namespace Lab5.Models
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItemDTO() { }

        // Constructor để ánh xạ từ model Todo sang DTO (loại bỏ trường Secret)
        public TodoItemDTO(Todo todoItem) =>
            (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    }
}