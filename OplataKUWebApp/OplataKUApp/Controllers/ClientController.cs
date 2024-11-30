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




        #region КВАРТИРЫ
        //вывод квартир
        
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            var payinfo = await _clientApiService.GetPayInfos(new PayFilterDto());
            
            return View(payinfo);
        }
        //добавление фильтра
        [HttpPost]
        public async Task<IActionResult> Index2(PayFilterDto filter)
        {
            var payinfo= await _clientApiService.GetPayInfos(filter);
            return View(payinfo);
        }

        #region Добавление квартиры

        [HttpPost]
        public async Task<IActionResult> Add2(PayAddDto paydto)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync("http://localhost:5012/PayInfo/Add2", paydto);
            if(!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
                return View();
            }
            return RedirectToAction("Index2");

        }
        [HttpGet]
        public IActionResult Add2()
        {

            return View();
        }

        #endregion добавление квартиры

        #region удаление квартиры

        
        public async Task<IActionResult> Remove2(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"http://localhost:5012/PayInfo/Delete?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                //удаление не удалось
            }
            TempData["Message"] = "Пользователь был удален";
            return RedirectToAction(nameof(Index2));

        }

        #endregion удаление квартиры
        #region Редактирование квартиры

        [HttpPost]
        public async Task<IActionResult> Edit2(PayDto paydto)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("http://localhost:5012/PayInfo/Post", paydto);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
                return View(paydto);
            }
            return RedirectToAction("Index2");

        }
        [HttpGet]
        public async Task<IActionResult> Edit2(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5012/PayInfo/Get?id={id}");
            var responseText = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<PayDto>(responseText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return View(responseData);
        }

        #endregion Редактирование квартиры
        #endregion КВАРТИРЫ
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
