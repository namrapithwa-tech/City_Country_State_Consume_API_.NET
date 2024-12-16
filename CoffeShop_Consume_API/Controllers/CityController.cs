using CoffeShop_Consume_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeShop_Consume_API.Controllers
{
    public class CityController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7294/api");
        private readonly HttpClient _client;

        public CityController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult GetCity()
        {
            List<CityModel> cities = new List<CityModel>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/City").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cities = JsonConvert.DeserializeObject<List<CityModel>>(data);
            }
            return View(cities);
        }

        public IActionResult Delete(int CityID)
        {
            HttpResponseMessage response =
                _client.DeleteAsync($"{_client.BaseAddress}/City/{CityID}").Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "City Deleted Successfully..!!";
            }
            else
            {
                TempData["MessageNotDelete"] = "Failed to Delete ..!!!";
            }

            return RedirectToAction("GetCity");

        }
    }
}
