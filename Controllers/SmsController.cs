using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Sms.Controllers
{
    public class SmsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SmsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index2(string x)
        {
            // DÖKÜMANTASYON LİNKİ : https://www.toplusmspaketleri.com/sms-api/csharp-sms-gonderme-api

            var client = _httpClientFactory.CreateClient();

            var body = @"{""api_id"": ""kendi bilgilerinizi giriniz"",""api_key"": ""kendi bilgilerinizi giriniz"",""sender"": ""SMS TEST"",""message_type"": ""normal"",""message"":""Bu bir test mesajıdır son."",""message_content_type"":""bilgi"",""phones"": [""Gönderilecek numara alanı""]}"; // Ticari smsler için ""message_content_type"":""ticari"",

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://api.toplusmspaketleri.com/api/v1/1toN", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("sms gönderildi.");
                return RedirectToAction("Index", "Sms");
            }
            return RedirectToAction("Index", "Sms");
        }

    }
}