namespace server.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActiveDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public bool Completed { get; set; } = false;
        public List<TodoTag> TodosTags { get; set; } = [];
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}