using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models.Pay
{
    public class PayAddDto
    {
        [RegularExpression(@"^[А-Яа-яЁё]*$",ErrorMessage ="Некорректное название")]
        public string? Street  { get; set; }
       
        public string? Housenumber { get; set; }
      
        public string? Apartnumber { get; set; }
    }
}
