using ClientInfoData;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace OplataKUWebApi.Models
{
    public class ClientAddDto
    {
     
       
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Midname { get; set; }

        


        public string Email { get; set; }

        public string Password { get; set; }

        public int ApartId { get; set; }
        //public string Street { get; set; }

        //public int Housenumber { get; set; }

        //public int Apartnumber { get; set; }





    }
}
