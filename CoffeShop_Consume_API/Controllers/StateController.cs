using CoffeShop_Consume_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeShop_Consume_API.Controllers
{
    public class StateController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7294/api");
        private readonly HttpClient _client;

        public StateController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult GetState()
        {
            List<StateModel> states = new List<StateModel>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/State").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                states = JsonConvert.DeserializeObject<List<StateModel>>(data);
            }
            return View(states);
        }

		[HttpGet]
		public IActionResult Delete(int StateID)
		{
			HttpResponseMessage response =
				_client.DeleteAsync($"{_client.BaseAddress}/State/{StateID}").Result;
			if (response.IsSuccessStatusCode)
			{
				TempData["Message"] = "State Deleted Successfully..!!";
			}
			else
			{
				TempData["MessageNotDelete"] = "Failed to Delete State..!!!";
			}

			return RedirectToAction("GetState");

		}
	}
}
