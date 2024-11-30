using Microsoft.AspNetCore.Mvc;
using ClientInfoData;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OplataKUWebApi.Models.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


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

        [HttpPost]
        public List<ClientGetDto> GetAll(ClientFilterDto model)
        {
            var query = _context.ClientInfos.AsQueryable();
            if (!string.IsNullOrEmpty(model.Lastname)) query = query.Where(x => x.Lastname.Contains(model.Lastname));
            if (!string.IsNullOrEmpty(model.Firstname)) query = query.Where(x => x.Firstname.Contains(model.Firstname));
            if (!string.IsNullOrEmpty(model.Midname)) query = query.Where(x => x.Midname.Contains(model.Midname));
            if (!string.IsNullOrEmpty(model.Email)) query = query.Where(x => x.Email.Contains(model.Email));
            if (model.ApartId !=null) query = query.Where(x => x.ApartId==model.ApartId);
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

            return query.Include(p => p.PayInfo)
            .ProjectTo<ClientGetDto>(_mapper.ConfigurationProvider)

            .ToList(); ;
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
