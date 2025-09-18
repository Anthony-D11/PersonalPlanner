namespace server.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public List<TodoTag> TodosTags { get; set; } = [];

    }
}