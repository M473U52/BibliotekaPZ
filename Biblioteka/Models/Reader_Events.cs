namespace Biblioteka.Models
{
    public class Reader_Events
    {
        public int readerId { get; set; }
        public int eventId { get; set; }

        public Event eventt { get; set; }
        public Reader reader { get; set; }
    }
}
