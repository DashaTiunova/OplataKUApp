using Microsoft.AspNetCore.Mvc;
using OplataKUWebApp.Models;
using System.Diagnostics;
using System.Text.Json;
using OplataKUWebApp.Models.Client;
using OplataKUWebApp.Models.Pay;
using OplataKUWebApp.Models.ClientViewModels;
using OplataKUWebApp.Services;
using System.Net.Http;

namespace OplataKUApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ClientApiService _clientApiService;


        public ClientController(ILogger<ClientController> logger,ClientApiService clientApiService)
        {
            _logger = logger;
            _clientApiService = clientApiService;
        }
        //открыли страницу но еще фильтр не применен
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
        #region Добавление квартиранта
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
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
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

        public IActionResult Add1()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #region Удаление квартиранта
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _clientApiService.RemoveClient(id);
           
            TempData["Message"] = result ?"Пользователь был удален":"Ошибка удаления";
           
                return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Редактирование квартиранта
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
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
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
        #endregion Редактирование квартиранта



        //[HttpGet]
        //public async Task<IActionResult> Add()
        //{


        //    var viewModel = new ClientAddViewModel()
        //    {
        //        PayInfo = await _clientApiService.GetPayInfo()

        //    };
        //    return View(viewModel);

        //}
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
