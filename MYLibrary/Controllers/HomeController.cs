using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MYLibrary.Handlers;
using MYLibrary.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MYLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IConfiguration configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();

        }
        [Route("/Home/BookDetails")]
        public async Task<IActionResult> BookDetails(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var errorMessage = "Sayfa detayları getirlirken bir hata oluştu.";

                var url = "https://localhost:7161/api/Library/getBookById/" + id;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogInformation(errorMessage);
                    return View("Error"); //TODO Error Sayfası eklenecek.
                }

                string content = await response.Content.ReadAsStringAsync();
                if (content is null)
                {
                    logger.LogInformation(errorMessage);
                    return View("Error");
                }

                var book = JsonConvert.DeserializeObject<Book>(content);
                return View(book);
            }


        }

    }
}