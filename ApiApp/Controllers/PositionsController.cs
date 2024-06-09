using Internship.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.Positions.ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new APIDbContext();
            Position position = db.Positions.FirstOrDefault(x => x.PositionId == Id);
            if (position == null)
                return NotFound();
            else
                return Ok(position);

        }
    }
}
