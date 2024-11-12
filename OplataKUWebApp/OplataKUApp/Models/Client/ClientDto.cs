namespace OplataKUWebApp.Models.Client
{
    public class ClientDto
    {  
        public int Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string? Midname { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int ApartId { get; set; }

        public string? Street { get; set; }

        public string? Housenumber { get; set; }

        public string? Apartnumber { get; set; }

    }
}
