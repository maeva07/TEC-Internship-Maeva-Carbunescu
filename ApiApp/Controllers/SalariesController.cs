using Internship.Model;
using Microsoft.AspNetCore.Mvc;

namespace Internship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.Salaries.ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new APIDbContext();
            Salary salary = db.Salaries.FirstOrDefault(x => x.SalaryId == Id);
            if (salary == null)
                return NotFound();
            else
                return Ok(salary);
        }

        [HttpPost]
        public IActionResult Post(Salary salary)
        {
            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                db.Salaries.Add(salary);
                db.SaveChanges();
                return Created("", salary);
            }
            else
                return BadRequest();
        }



        [HttpPut]
        public IActionResult Update(Salary salary)
        {
            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                Salary updateSalary = db.Salaries.Find(salary.SalaryId);
                updateSalary.Amount = salary.Amount;
                db.SaveChanges();
                return NoContent();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var db = new APIDbContext();
            Salary salary = db.Salaries.Find(Id);
            if (salary == null)
                return NotFound();
            else
            {
                db.Salaries.Remove(salary);
                db.SaveChanges();
                return NoContent();
            }
        }
    }
}
