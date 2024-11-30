using System.ComponentModel.DataAnnotations;

namespace ClientInfoData
{
    public class PayInfo //информация о квартире
    {
        [Key]
        
        public int ApartId { get; set; }
        
        public string Street { get; set; }
        
        public string Housenumber { get; set; }
        
        public string Apartnumber { get; set; }

        

    }
}
