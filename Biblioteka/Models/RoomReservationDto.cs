using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class RoomReservationDto
    {
        [
            Column(TypeName = "NUMERIC(4)"),
            Display(Name = "Id rezerwacji"),
            Range(0, 9999)]
        public int reservationId { get; set; }

        

        //
        public int readerId { get; set; }

       

        //
        public int roomId { get; set; }

        [
            Required(ErrorMessage = "wymagane jest podanie daty"),
            Display(Name = "Data początku rezerwacji")]
        public DateTime begginingOfReservationDate { get; set; }

        [
            Required(ErrorMessage = "wymagane jest podanie daty"),
            Display(Name = "Data końca rezerwacji")]
        public DateTime endOfReservationDate { get; set; }

        [
            Required(ErrorMessage = "wymagane jest podanie godziny"),
            Display(Name = "Godzina rozpoczęczia rezerwacji")]
        public TimeSpan startingTime { get; set; }

        [
            Required(ErrorMessage = "wymagane jest podanie godziny"),
            Display(Name = "Godzina zakończenia rezerwacji")]
        public TimeSpan endingTime { get; set; }


        [
        Display(Name = "Potwierdzenie wydania klucza")]
        public bool Confirmation { get; set; }
    }
}
