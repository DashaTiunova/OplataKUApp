using Microsoft.AspNetCore.Mvc;
using OplataKUWebApi.Models;
using ClientInfoData;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace OplataKUWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientController(ILogger<ClientController> _logger,
        ClientInfoContext _context,
        IMapper _mapper):ControllerBase
    {
        
        [HttpPut]
        public ClientInfo Add([FromBody] ClientAddDto model)
        {
            var client= _mapper.Map<ClientInfo>(model);
            _context.ClientInfos.Add(client);
            _context.SaveChanges();

            return client;
        }

        [HttpGet]
        public ClientGetDto? Get(int id)
        {
            var client = _context.ClientInfos.FirstOrDefault(x => x.Id == id);
            var clientDto = _mapper.Map<ClientGetDto>(client);
            return clientDto;
        }

        [HttpGet]
        public List<ClientGetDto> GetAll()
        {
            var clients = _context.ClientInfos
                .Include(p => p.PayInfo)
                .ProjectTo<ClientGetDto>(_mapper.ConfigurationProvider)
                
                .ToList();
           // var clientsDto=_mapper.Map<List<ClientGetDto>>(clients);

            //var clientsDto = clients.Select(x => new ClientGetDto
            //{
            //    Id = x.Id,
            //    Lastname=x.Lastname,
            //    Firstname=x.Firstname,
            //    Midname=x.Midname,
            //    Email=x.Email,
            //    Street=x.Street,
            //    Housenumber=x.Housenumber,
            //    Apartnumber=x.Apartnumber

            //})
            //.ToList();

            return clients;
        }


        [HttpDelete]
        public void Delete(int id)
        {
            var client = _context.ClientInfos.FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                _context.ClientInfos.Remove(client);
                _context.SaveChanges();
            }

        }

        [HttpPost]
        public ClientInfo? Post([FromBody] ClientInfo model)
        {
            var client = _context.ClientInfos.FirstOrDefault(x => x.Id == model.Id);
            if (client != null)
            {
                client.Firstname = model.Firstname;
                client.Lastname = model.Lastname;
                client.Midname = model.Midname;
                client.ApartId = model.ApartId;
                client.Email = model.Email;
                client.Password = model.Password;
                _context.ClientInfos.Update(client);
                _context.SaveChanges();



            }
            return client;
        }

    }
}
