using OplataKUWebApp.Models.Pay;
using OplataKUWebApp.Models.Client;


namespace OplataKUWebApp.Models.PayViewModels
{
    public class PayViewModel
    {
        public List<ClientDto> Clients { get; set; }
        public List<PayDto> PayInfo { get; set; }
    }
}
