using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using System.Net.Http;
using System.Net;

namespace MovieAPI.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Movie> movies = new List<Movie>();
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString("http://simonsmoviebooking.azurewebsites.net/api/movie");
                movies = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
            }
            return View(movies);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveTicket(int id)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, "http://simonsmoviebooking.azurewebsites.net/api/movie/BookMovie/" + id.ToString());
            using (var client = new HttpClient())
            {
                await client.SendAsync(req);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
