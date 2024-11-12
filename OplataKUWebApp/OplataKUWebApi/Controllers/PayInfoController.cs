using Microsoft.AspNetCore.Mvc;
using OplataKUWebApi.Models;
using ClientInfoData;

namespace OplataKUWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PayInfoController : ControllerBase
    {
        private readonly ILogger<PayInfoController> _logger;

        private readonly ClientInfoContext _context;

        public PayInfoController(ILogger<PayInfoController> logger, ClientInfoContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPut]
        public PayInfo Add([FromBody] PayInfo model)
        {
            _context.PayInfos.Add(model);
            _context.SaveChanges();

            return model;
        }

        [HttpGet]
        public PayInfo? Get(int id)
        {
            var payinfo = _context.PayInfos.FirstOrDefault(x => x.ApartId == id);

            return payinfo;
        }

        [HttpGet]
        public List<PayInfo> GetAll()
        {
            var payinfos = _context.PayInfos.ToList();

            return payinfos;
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
