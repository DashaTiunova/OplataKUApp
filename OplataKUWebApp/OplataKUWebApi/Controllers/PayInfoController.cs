using Microsoft.AspNetCore.Mvc;
using OplataKUWebApi.Models;
using ClientInfoData;
using OplataKUWebApi.Models.Pay;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
namespace OplataKUWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PayInfoController : ControllerBase
    {
        private readonly ILogger<PayInfoController> _logger;

        private readonly ClientInfoContext _context;
        private readonly IMapper _mapper;

        public PayInfoController(ILogger<PayInfoController> logger, ClientInfoContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
    
        [HttpPut]
        public PayInfo Add2([FromBody] PayAddDto model)
        {
            var client = _mapper.Map<PayInfo>(model);
            _context.PayInfos.Add(client);
            _context.SaveChanges();

            return client;
        }

        [HttpGet]
        public PayInfo? Get(int id)
        {
            var payinfo = _context.PayInfos.FirstOrDefault(x => x.ApartId == id);

            return payinfo;
        }

        [HttpPost]
        public List<PayGetDto> GetAll(PayFilterDto model)
        {
            var query = _context.PayInfos.AsQueryable();
            if (!string.IsNullOrEmpty(model.Street) ) query=query.Where(x => x.Street.Contains( model.Street));
            if (!string.IsNullOrEmpty(model.Housenumber)) query = query.Where(x => x.Housenumber.Contains(model.Housenumber));
            if(!string.IsNullOrEmpty(model.Apartnumber)) query = query.Where(x => x.Apartnumber.Contains(model.Apartnumber));
           

            return query
                .ProjectTo<PayGetDto>(_mapper.ConfigurationProvider)
                .ToList();
        }


        [HttpDelete]
        public void Delete(int id)
        {
            var payinfo = _context.PayInfos.FirstOrDefault(x => x.ApartId == id);
            if (payinfo != null)
            {
                _context.PayInfos.Remove(payinfo);
                _context.SaveChanges();
            }

        }

        [HttpPost]
        public PayInfo? Post([FromBody] PayInfo model)
        {
            var payinfo = _context.PayInfos.FirstOrDefault(x => x.ApartId == model.ApartId);
            if (payinfo != null)
            {
                payinfo.ApartId = model.ApartId;
                payinfo.Street = model.Street;
                payinfo.Housenumber = model.Housenumber;
                payinfo.Apartnumber = model.Apartnumber;
                

                _context.PayInfos.Update(payinfo);
                _context.SaveChanges();



            }
            return payinfo;
        }

    }
}
