using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ApiCrud_Core.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:40638/Api/Default");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category1>>(jsonString);
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category1 p)
        {
            var httpClient = new HttpClient();
            var jsonCategory = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("http://localhost:40638/Api/Default", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:40638/Api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonCategory = await responseMessage.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category1>(jsonCategory);
                return View(category);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task< IActionResult> EditCategory(Category1 p)
        {
            var httpClient = new HttpClient();
            var jsonCategory = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("http://localhost:40638/Api/Default/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
    public class Category1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
