namespace server.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public bool Completed { get; set; }

    }
}