namespace OplataKUWebApi.Models
{
    public class ClientInfoModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Midname { get; set; }

        public string Street { get; set; }

        public int Housenumber { get; set; }

        public int Apartnumber { get; set; }
        public int ApartId { get; set; }

    }
}
