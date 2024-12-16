using CoffeShop_Consume_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeShop_Consume_API.Controllers
{
    public class CountryController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7294/api");
        private readonly HttpClient _client;

        public CountryController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]

        public IActionResult GetCountry()
        {
            List<CountryModel> countries = new List<CountryModel>();

            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Country").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                countries = JsonConvert.DeserializeObject<List<CountryModel>>(data);
            }
            else
            {
                ViewBag.Error = "Failed to retrieve data.";
            }
            return View(countries);
        }

        [HttpGet]
        public IActionResult Delete(int CountryId)
        {
            HttpResponseMessage response = 
                _client.DeleteAsync($"{_client.BaseAddress}/Country/{CountryId}").Result;
            if (response.IsSuccessStatusCode) {
                TempData["Message"] = "Country Deleted Successfully..!!";
            }
			else
			{
				TempData["MessageNotDelete"] = "Failed to delete Country..!!!";
			}

			return RedirectToAction("GetCountry");

        }
    }
}
