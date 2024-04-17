using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class EmployeeDataDto
    {
        [
            Required,
            Display(Name = "Id pracownika"),
            ]
        public int employeeId { get; set; }

        [
            Required,
            Display(Name = "PESEL"),
            Range(10000000000, 99999999999, ErrorMessage = "PESEL składa się z 11 cyfr"),]
        public long? pesel { get; set; }

        [
            Required,
            Display(Name = "Ulica"),
            MaxLength(40, ErrorMessage = "Ulica nie może zaweirać więcej niż 40 znaków")]
        public string street { get; set; }

        [

            Display(Name = "Nr mieszkania"),
            Range(0, 999, ErrorMessage = "Numer mieszkania nie może być większy niż 999")]
        public int? houseNumber { get; set; }

        [
            Required,
            Display(Name = "Miasto"),
            MaxLength(30, ErrorMessage = "Miasto nie może zawierać więcej niż 30 znaków")]
        public string town { get; set; }

        [
            Required,
            Display(Name = "Kod pocztowy"),
            MaxLength(6, ErrorMessage = "Kod pocztowy nie może zawierać więcej niż 6 znaków")]
        public string zipCode { get; set; }

        [
            Phone,
            Required,
            Display(Name = "Nr telefonu")]
        public string phoneNumber { get; set; }

        [
            Required,
            Display(Name = "Data urodzenia")]
        public DateTime birthDate { get; set; }

    } 
}
