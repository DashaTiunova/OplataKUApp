using OplataKUWebApp.Models.Client;
using OplataKUWebApp.Models.Pay;

namespace OplataKUWebApp.Models.ClientViewModels
{
    public class ClientEditViewModel
    {
        public ClientDto? Client { get; set; }
        public List<PayDto> PayInfo { get; set; }
    }
}
