namespace server.Models
{
    public class Activity
    {
        public int id { get; set; }
        public string content { get; set; } = string.Empty;
        public string details { get; set; } = string.Empty;
        public bool completed { get; set; }
        public DateTime time_assigned { get; set; }

    }
}