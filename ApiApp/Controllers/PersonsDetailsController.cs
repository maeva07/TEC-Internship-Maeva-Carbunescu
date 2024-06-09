using ApiApp.Model;
using Internship.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsDetailsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.PersonDetails.ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new APIDbContext();
            PersonDetail personDetails = db.PersonDetails.Find(Id);
            if (personDetails == null)
                return NotFound();
            else
                return Ok(personDetails);
        }
        
        [HttpPost]
        public IActionResult Add(PersonDetail personDetails)
        {
            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                db.PersonDetails.Add(personDetails);
                db.SaveChanges();
                return Created("", personDetails);
            }
            else
                return BadRequest();

        }

        [HttpPut]
        public IActionResult Update(PersonDetail personDetails)
        {

            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                PersonDetail updateDetails = db.PersonDetails.Find(personDetails.Id);
                updateDetails.BirthDay = personDetails.BirthDay;
                updateDetails.PersonCity = personDetails.PersonCity;
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
            PersonDetail personDetails = db.PersonDetails.Find(Id);
            if (personDetails == null)
                return NotFound();
            else
            {
                db.PersonDetails.Remove(personDetails);
                db.SaveChanges();
                return NoContent();
            }
        }
    }

}
