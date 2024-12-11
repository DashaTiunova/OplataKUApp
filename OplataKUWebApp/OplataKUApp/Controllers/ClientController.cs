using Microsoft.AspNetCore.Mvc;
using OplataKUWebApp.Models;
using System.Diagnostics;
using System.Text.Json;
using OplataKUWebApp.Models.Client;
using OplataKUWebApp.Models.Pay;
using OplataKUWebApp.Models.ClientViewModels;
using OplataKUWebApp.Models.PayViewModels;
using OplataKUWebApp.Services;
using System.Net.Http;

namespace OplataKUApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ClientApiService _clientApiService;
        private readonly PayApiService _payApiService;


        public ClientController(ILogger<ClientController> logger,ClientApiService clientApiService, PayApiService payApiService)
        {
            _logger = logger;
            _clientApiService = clientApiService;
            _payApiService = payApiService;
        }
        //������� �������� �� ��� ������ �� ��������
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var viewModel = new ClientViewModel
            {
                Clients = await _clientApiService.GetClients(new ClientFilterDto()),
                PayInfo = await _clientApiService.GetPayInfo()
            };
            return View(viewModel);
        }

       

        [HttpPost]
        public async Task<IActionResult> Index(ClientFilterDto filter)
        {
            var viewModel = new ClientViewModel
            {
                Clients = await _clientApiService.GetClients(filter),
                PayInfo = await _clientApiService.GetPayInfo()
            };
            
            return View(viewModel);
        }
        #region ���������� �����������
        [HttpPost]
        public async Task<IActionResult> Add(ClientAddDto adddto)
        {
            var viewModel = new ClientAddViewModel()
            {
                PayInfo = await _clientApiService.GetPayInfo(),
                Client=adddto

            };
            if (!ModelState.IsValid)
            {
                
                return View(viewModel);
            }

            var result = await _clientApiService.AddClient(adddto);
            
            if (!result)
            {
                ModelState.AddModelError("api_error", "������ ��������� ������");
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new ClientAddViewModel()
            {
                PayInfo = await _clientApiService.GetPayInfo()
            };

            return View(viewModel);
        }

        #endregion

        

        public IActionResult Privacy()
        {
            return View();
        }
        #region �������� �����������
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _clientApiService.RemoveClient(id);
           
            TempData["Message"] = result ?"������������ ��� ������":"������ ��������";
           
                return RedirectToAction(nameof(Index));
        }
        #endregion


        #region �������������� �����������
        [HttpPost]
        public async Task<IActionResult> Edit(ClientEditDto editdto)
        {
            var viewModel = new ClientEditViewModel
            {
                PayInfo = await _clientApiService.GetPayInfo(),
            };
            if (!ModelState.IsValid)
            {

                return View(viewModel);
            }
            var result=await _clientApiService.EditClient(editdto);

            if (!result)
            {
                ModelState.AddModelError("api_error", "������ ��������� ������");
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var client = await _clientApiService.GetClient(id);
            
            var viewModel = new ClientEditViewModel
            {
                PayInfo = await _clientApiService.GetPayInfo(),
                Client= await _clientApiService.GetClient(id)
            };
            return View(viewModel);
        }
        #endregion �������������� �����������




        #region ��������
        //����� �������
        
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            var payinfo = await _clientApiService.GetPayInfos(new PayFilterDto());
            
            return View(payinfo);
        }
        //���������� �������
        [HttpPost]
        public async Task<IActionResult> Index2(PayFilterDto filter)
        {
            var payinfo= await _clientApiService.GetPayInfos(filter);
            return View(payinfo);
        }

        #region ���������� ��������

        [HttpPost]
        public async Task<IActionResult> Add2(PayAddDto paydto)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }

            var result = await _clientApiService.AddApart(paydto);
            if (!result)
            {
                ModelState.AddModelError("api_error", "������ ��������� ������");
                return View(paydto);
            }

            return RedirectToAction("Index2");

        }
        [HttpGet]
        public IActionResult Add2()
        {
            
            var model = new PayAddDto();


            return View(model);


        }

        #endregion ���������� ��������

        #region �������� ��������


        public async Task<IActionResult> Remove2(int id)
        {
            var result = await _clientApiService.RemoveApart(id);
            TempData["Message"] = result ? "�������� ���� �������" : "������ ��������";

            return RedirectToAction(nameof(Index2));

        }

        #endregion �������� ��������
        #region �������������� ��������

        [HttpPost]
        public async Task<IActionResult> Edit2(PayDto paydto)
        {
            
            var result = await _clientApiService.EditApart(paydto);

            if (!result)
            {
                ModelState.AddModelError("api_error", "������ ��������� ������");
                return View(paydto);
            }
            return RedirectToAction("Index2");

        }
        [HttpGet]
        public async Task<IActionResult> Edit2(int id)
        {
            var client = await _clientApiService.GetApart(id);

            
            return View(client);
        }

        #endregion �������������� ��������
        #endregion ��������
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
