using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientInfoData
{
    public class ClientInfo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(PayInfo))]
        public int ApartId { get; set; }
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string? Midname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public PayInfo? PayInfo { get; set; }
    }
}
