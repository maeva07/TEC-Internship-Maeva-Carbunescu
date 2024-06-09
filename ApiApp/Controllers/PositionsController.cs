using Internship.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {

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
