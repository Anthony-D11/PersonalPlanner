namespace server.DTOs
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActiveDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public IEnumerable<int> TagIds { get; set; } = [];
        public int? CategoryId { get; set; }
    }
    public class NewTodoDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActiveDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public IEnumerable<int> TagIds { get; set; } = [];
        public int? CategoryId { get; set; }
    }
}