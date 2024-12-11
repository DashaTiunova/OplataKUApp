using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models.Pay
{
    public class PayAddDto
    {
       [Required(ErrorMessage = "Укажите улицу,используя буквы русского алфавита")]
        [RegularExpression(@"^[а-яА-Я\s]{1,40}$", ErrorMessage = "Заполните улицу,используя буквы русского алфавита")]
        public string? Street  { get; set; }
       [Required(ErrorMessage = "Укажите номер улицы")]
       [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Недопустимое представление номера улицы")]
        [StringLength(4, ErrorMessage = "Номер улицы не может быть более четырехзначного числа") ]
        public string? Housenumber { get; set; }
        [Required(ErrorMessage = "Укажите номер дома")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Недопустимое представление номера дома")]
        [StringLength(4, ErrorMessage = "Номер дома не может быть более четырехзначного числа")]
        public string? Apartnumber { get; set; }
    }
}
