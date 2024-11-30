using System.ComponentModel.DataAnnotations;

namespace OplataKUWebApp.Models.Pay
{
    public class PayFilterDto
    {
        
        public string? Street { get; set; }
        
        public string? Housenumber { get; set; }
       
        public string? Apartnumber { get; set; }
    }
}
