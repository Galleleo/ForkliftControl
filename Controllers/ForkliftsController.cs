using Microsoft.AspNetCore.Mvc;
using ForkliftControl.Data;
using ForkliftControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ForkliftControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForkliftsController : ControllerBase
    {
        private readonly ForkliftContext _context;

        public ForkliftsController(ForkliftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forklift>>> GetForklifts()
        {
            return await _context.Forklifts.ToListAsync();
        }
    }
}