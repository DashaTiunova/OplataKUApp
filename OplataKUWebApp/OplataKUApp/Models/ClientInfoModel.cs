using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models
{
    public class ClientInfoModel
    {
        [Required(ErrorMessage = "Заполните фамилию")]
        [RegularExpression(@"^[а-яА-Я\s\-]{1,64}$", ErrorMessage="Некорректная фамилия")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Заполните имя")]
        [RegularExpression(@"^[а-яА-Я\s\-]{1,64}$", ErrorMessage = "Некорректное имя")]
        public string Firstname { get; set; }
        public string? Midname { get; set; }

        [Required(ErrorMessage = "Укажите Id квартиры")]
        public int ApartId { get; set; }

        
        [RegularExpression(@"^[а-яА-Я\s\-]{1,64}$", ErrorMessage = "Некорректное название улицы")]
        public string Street { get; set; }
       
        public int Housenumber { get; set; }
       
        public int Apartnumber { get; set; }
    }
}
