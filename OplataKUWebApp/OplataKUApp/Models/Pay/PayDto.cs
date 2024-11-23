using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models.Pay
{
    public class PayDto
    {
        [Key]

        public int ApartId { get; set; }

        public string? Street { get; set; }

        public string? Housenumber { get; set; }

        public string? Apartnumber { get; set; }
    }
}
