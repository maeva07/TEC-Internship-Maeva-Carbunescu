using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonDetailsController : Controller
    {

        private readonly IConfiguration _config;
        private readonly string _api;

        public PersonDetailsController(IConfiguration config)
        {
            _config = config;
            _api = _config.GetValue<string>("ApiSettings:ApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync($"{_api}/personsdetails");
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonDetails> list = JsonConvert.DeserializeObject<List<PersonDetails>>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonDetails>());
        }

        public IActionResult Add()
        {
            PersonDetails personDetails = new PersonDetails();
            return View(personDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsondepartment = JsonConvert.SerializeObject(personDetails);
                StringContent content = new StringContent(jsondepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync($"{_api}/personsdetails", content);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "There is an API error");
                    var errorResponse = await message.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"There is an API error: {message.ReasonPhrase} - {errorResponse}");
                    return View(personDetails);

                }
            }
            else
            {

                return View(personDetails);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync($"{_api}/personsdetails/" + Id);

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails personDetails = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(personDetails);
            }
            else
                return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsondepartment = JsonConvert.SerializeObject(personDetails);
                StringContent content = new StringContent(jsondepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync($"{_api}/personsdetails", content);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorResponse = await message.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"There is an API error: {message.ReasonPhrase} - {errorResponse}");
                    return View(personDetails);

                }
            }
            else
                return View(personDetails);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync($"{_api}/personsdetails/" + Id);
            if (message.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }

    }
}
