using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models.Client
{
    public class ClientAddDto
    {
        [Required(ErrorMessage="Заполните фамилию,используя буквы русского алфавита")]
        [RegularExpression(@"^[а-яА-Я\s]{1,40}$",ErrorMessage = "Заполните фамилию,используя буквы русского алфавита")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Заполните имя,используя буквы русского алфавита")]
        [RegularExpression(@"^[а-яА-Я\s]{1,40}$", ErrorMessage = "Заполните имя,используя буквы русского алфавита")]
        public string Firstname { get; set; }
        
        [RegularExpression(@"^[а-яА-Я\s]{1,40}$", ErrorMessage = "Заполните отчество,используя буквы русского алфавита")]
        public string? Midname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int ApartId { get; set; }
        //public string Street { get; set; }

        //public int Housenumber { get; set; }

        //public int Apartnumber { get; set; }

    }
}
