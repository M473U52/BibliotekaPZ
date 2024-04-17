using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class EventDto
    {
        [
            Display(Name = "Id wydarzenia")
            ]
        public int eventId { get; set; }

        [
            Required,
            Display(Name = "Nazwa"),
            MaxLength(100, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
        public string name { get; set; }

        [
            Required,
            Display(Name = "Opis"),
            MaxLength(400, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
        public string description { get; set; }

        [
        Required,
        Display(Name = "Data")]
        public DateTime eventDate { get; set; }

        
    }
}
