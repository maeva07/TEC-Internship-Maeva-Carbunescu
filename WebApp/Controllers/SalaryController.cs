﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SalaryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Salary> list = new List<Salary>();
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/Salaries");
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Salary>>(jstring);
                return View(list);
            }
            else
                return View(list);
        }

        public IActionResult Add()
        {
            Salary salary = new Salary();
            return View(salary);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Salary salary)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonPerson = JsonConvert.SerializeObject(salary);
                StringContent content = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:5229/api/salaries", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(salary);
                }

            }
            else
            {
                return View(salary);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/salaries/" + Id);

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Salary salary = JsonConvert.DeserializeObject<Salary>(jstring);
                return View(salary);
            }
            else
                return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Salary salary)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonSalary = JsonConvert.SerializeObject(salary);
                StringContent content = new StringContent(jsonSalary, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync("http://localhost:5229/api/salaries", content);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(salary);
            }
            else
                return View(salary);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync("http://localhost:5229/api/Salaries/" + Id);
            if (message.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();

        }
    }
}
