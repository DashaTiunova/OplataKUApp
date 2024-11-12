using Microsoft.AspNetCore.Mvc;
using OplataKUWebApp.Models;
using System.Diagnostics;
using System.Text.Json;
using OplataKUWebApp.Models.Client;

namespace OplataKUApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

       

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
          
        }

        public async Task<IActionResult> Index()
        {
            var httpclient=new HttpClient();
            var response = await httpclient.GetAsync("http://localhost:5012/Client/GetAll");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData=JsonSerializer.Deserialize<List<ClientDto>>(responseText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //PropertyNameCaseInsensitive = true нечувствител.к регистру
            }
                );
            return View(responseData);
        }
        #region Добавление квартиранта
        [HttpPost]
        public async Task<IActionResult> Add(ClientAddDto adddto)
        {
            if (!ModelState.IsValid)
            {
                
                return View(adddto);
            }
            var httpclient = new HttpClient();
            var response = await httpclient.PutAsJsonAsync("http://localhost:5012/Client/Add", adddto);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
                return View(adddto);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }
        #region Удаление квартиранта
        public async Task<IActionResult> Remove(int id)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.DeleteAsync($"http://localhost:5012/Client/Delete?id={id}");
            
            if (!response.IsSuccessStatusCode)
            {
                //удаление не удалось
            }
            TempData["Message"] = "Пользователь был удален";

                return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Edit(ClientEditDto editdto)
        {
            if (!ModelState.IsValid)
            {

                return View(editdto);
            }

            var httpclient = new HttpClient();
            var response = await httpclient.PostAsJsonAsync("http://localhost:5012/Client/Post", editdto);//client id летит 0
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
                return View(editdto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync($"http://localhost:5012/Client/Get/?id={id}");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<ClientDto>(responseText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //PropertyNameCaseInsensitive = true нечувствител.к регистру
            }
                );
            return View(responseData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
