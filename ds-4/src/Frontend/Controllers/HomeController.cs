using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;
using StackExchange.Redis;
using System.Threading;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TextDetails(string id)
        {
            Console.WriteLine("\n\n\n\n\n\n\n\nid:" + id + "\n\n\n\n\n\n\n\n");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            var response = await client.GetAsync("api/Values/TextDetails/" + id);
            ViewBag.Value = await response.Content.ReadAsStringAsync();
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("value", data)
            });

            var response = await client.PostAsync("api/Values/Post", content);
            var id = await response.Content.ReadAsStringAsync();

            Console.WriteLine("\n\n\n\n\n\n\n\nid:" + id + "\n\n\n\n\n\n\n\n");
            return RedirectToAction("TextDetails", new { Id = id });

            //return RedirectToAction("TextDetails", new RouteValueDictionary(
            //   new { controller = "Home", action = "TextDetails", Id = id }));
            //return new RedirectToRouteResult("TextDetails", "Home");
            //   return Ok(id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
