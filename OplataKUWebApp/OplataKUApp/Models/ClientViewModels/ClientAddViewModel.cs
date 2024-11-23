using OplataKUWebApp.Models.Client;
using OplataKUWebApp.Models.Pay;
namespace OplataKUWebApp.Models.ClientViewModels
{
    public class ClientAddViewModel
    {
        public ClientAddDto Client {  get; set; }

        public List<PayDto> PayInfo { get; set; }
    }
}
