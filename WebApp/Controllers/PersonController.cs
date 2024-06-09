using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        //HINT task 8 start

/*        private readonly IConfiguration _config;
        private readonly string _api;
        public PersonController(IConfiguration config)
        {
            _config = config;
            _api = _config.GetValue<string>("");
        }*/

        //HINT task 8 end
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/persons");
            if(message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonInformation> list = JsonConvert.DeserializeObject<List<PersonInformation>>(jstring);
                return View(list);
            }
            else
            return View(new List<PersonInformation>());
        }

        public IActionResult Add()
        {
            Person person = new Person();
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonPerson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:5229/api/persons", content);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorResponse = await message.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"There is an API error: {message.ReasonPhrase} - {errorResponse}");
                    return View(person);
                }

            }
            else
            {
                return View(person);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(Person person)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        HttpClient client = new HttpClient();

        //        var positionResponse = await client.GetStringAsync($"http://localhost:5229/api/positions/{person.PositionId}");
        //        var position = JsonConvert.DeserializeObject<Position>(positionResponse);

        //        var salaryResponse = await client.GetStringAsync($"http://localhost:5229/api/salaries/{person.SalaryId}");
        //        var salary = JsonConvert.DeserializeObject<Salary>(salaryResponse);

        //        // Ensure position and department details are included
        //        if (position.Department == null)
        //        {
        //            var departmentResponse = await client.GetStringAsync($"http://localhost:5229/api/departments/{position.DepartmentId}");


        //            var department = JsonConvert.DeserializeObject<Department>(departmentResponse);
        //            position.Department = new Department { DepartmentName = department.DepartmentName };
        //        }

        //        var requestPayload = new
        //        {
        //            person.Id,
        //            person.Name,
        //            person.Surname,
        //            person.Age,
        //            person.Email,
        //            person.Address,
        //            person.PositionId,
        //            Position = position,
        //            person.SalaryId,
        //            Salary = salary
        //        };

        //        var jsonPayload = JsonConvert.SerializeObject(requestPayload);
        //        StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        //        HttpResponseMessage message = await client.PostAsync("http://localhost:5229/api/persons", content);

        //        if (message.IsSuccessStatusCode)
        //        { 
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            var errorResponse = await message.Content.ReadAsStringAsync();
        //            ModelState.AddModelError("", $"There is an API error: {message.ReasonPhrase} - {errorResponse}");
        //            return View(person);
        //        }
        //    }
        //    else
        //    {
        //        return View(person);
        //    }
        //}


        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/persons/" + Id);

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Person person = JsonConvert.DeserializeObject<Person>(jstring);
                return View(person);
            }
            else
                return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonPerson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync("http://localhost:5229/api/persons", content);

                if(message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorResponse = await message.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"There is an API error: {message.ReasonPhrase} - {errorResponse}");
                    return View(person);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            } return View(person);
               
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync("http://localhost:5229/api/Persons/" + Id);
            if (message.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();

        }
    }
}
