namespace server.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public IEnumerable<int> TodoIds { get; set; } = [];
    }
    public class NewTagDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

    }
}