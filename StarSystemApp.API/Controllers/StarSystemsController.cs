using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSystemApp.API.Data;
using StarSystemApp.API.Models;

namespace StarSystemApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarSystemsController : ControllerBase
    {
        private readonly StarSystemDbContext _context;

        public StarSystemsController(StarSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/StarSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StarSystem>>> GetStarSystems()
        {
            return await _context.StarSystems.ToListAsync();
        }

        // GET: api/StarSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StarSystem>> GetStarSystem(int id)
        {
            var starSystem = await _context.StarSystems
                                           .Include(s => s.SpaceObjects)
                                           .Include(s=>s.MassCenter)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(s => s.Id == id);

            if (starSystem == null)
            {
                return NotFound();
            }

            return starSystem;
        }

        // PUT: api/StarSystems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStarSystem(int id, StarSystem starSystem)
        {
            if (id != starSystem.Id)
            {
                return BadRequest();
            }
            var existingStarSystem = await _context.StarSystems.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id);

            if (existingStarSystem == null)
            {
                return NotFound();
            }

            _context.StarSystems.Update(starSystem);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/StarSystems
        [HttpPost]
        public async Task<ActionResult<StarSystem>> PostStarSystem(StarSystem starSystem)
        {
            _context.StarSystems.Add(starSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStarSystem", new { id = starSystem.Id }, starSystem);
        }

        // DELETE: api/StarSystems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStarSystem(int id)
        {
            var starSystem = await _context.StarSystems.FindAsync(id);
            if (starSystem == null)
            {
                return NotFound();
            }

            var relatedSpaceObjects = _context.SpaceObjects
                .Where(so => so.StarSystemId == id);
            foreach (var spaceObject in relatedSpaceObjects)
            {
                _context.SpaceObjects.Remove(spaceObject);
            }

            _context.StarSystems.Remove(starSystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
