using Microsoft.AspNetCore.Mvc;
using OplataKUWebApp.Models.Client;
using OplataKUWebApp.Models.Pay;
using System.Text.Json;

namespace OplataKUWebApp.Services
{
    public class ClientApiService
    {
        private JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //PropertyNameCaseInsensitive = true нечувствител.к регистру
        };
        //чтоб не создавать httpclient исп."фабрика сервисов", + подключ. ее в program 

        private HttpClient _httpClient;
        public ClientApiService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ClientApi");
        }

        //private string _host= "http://localhost:5012";
        public async Task<List<ClientDto>?> GetClients(ClientFilterDto filter)
        {
           
            var response = await _httpClient.PostAsJsonAsync("/Client/GetAll", filter);
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<List<ClientDto>>(responseText,  _jsonSerializerOptions  );
            //    JsonSerializerOptions  мы вынесли это в приват.метод на ур. класса,чтоб не писать одно и то же
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    //PropertyNameCaseInsensitive = true нечувствител.к регистру
            //}

            return responseData;
        }

        /// <summary>
        //
        /// </summary>

        public async Task<List<PayDto>?> GetPayInfos(PayFilterDto filter)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("http://localhost:5012/PayInfo/GetAll", filter);
            var responseText = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<List<PayDto>>(responseText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return responseData;
        }

        public async Task<List<PayDto>?> GetPayInfo()
        {
            
            var response = await _httpClient.PostAsJsonAsync("/PayInfo/GetAll", new PayFilterDto());
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<List<PayDto>>(responseText, _jsonSerializerOptions);
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    //PropertyNameCaseInsensitive = true нечувствител.к регистру
            //}
            //    );

            return responseData;
        }
        #region удаление
        public async Task<bool> RemoveClient(int id)
        {
            
            var response = await _httpClient.DeleteAsync($"/Client/Delete?id={id}");

           return response.IsSuccessStatusCode;
            
        }
        public async Task<bool> RemoveApart(int id)
        {

            var response = await _httpClient.DeleteAsync($"/PayInfo/Delete?id={id}");

            return response.IsSuccessStatusCode;

        }
        #endregion удаление

        #region редактирование
        public async Task<ClientDto?> GetClient(int id)
        {
            
            var response = await _httpClient.GetAsync($"/Client/Get/?id={id}");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<ClientDto>(responseText, _jsonSerializerOptions);
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    //PropertyNameCaseInsensitive = true нечувствител.к регистру
            //});
                
         return responseData;
        }

        public async Task<bool> EditClient(ClientEditDto editdto)
        {
            
            var response = await _httpClient.PostAsJsonAsync("/Client/Post", editdto);

            return response.IsSuccessStatusCode;
           
        }

        public async Task<PayDto?> GetApart(int id)
        {

            var response = await _httpClient.GetAsync($"/PayInfo/Get/?id={id}");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<PayDto>(responseText, _jsonSerializerOptions);
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    //PropertyNameCaseInsensitive = true нечувствител.к регистру
            //});

            return responseData;
        }

        public async Task<bool> EditApart(PayDto editdto)
        {

            var response = await _httpClient.PostAsJsonAsync("PayInfo/Post", editdto);

            return response.IsSuccessStatusCode;

        }

        #endregion редактирование

        #region добавление
        public async Task<bool> AddClient(ClientAddDto adddto)
        {
            
            var response = await _httpClient.PutAsJsonAsync("/Client/Add", adddto);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddApart(PayAddDto adddto)
        {

            var response = await _httpClient.PutAsJsonAsync("/PayInfo/Add2", adddto);

            return response.IsSuccessStatusCode;

        }

        #endregion добавление
    }
}
