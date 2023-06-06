using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSystemApp.API.Data;
using StarSystemApp.API.Models;

namespace StarSystemApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpaceObjectsController : ControllerBase
    {
        private readonly StarSystemDbContext _context;

        public SpaceObjectsController(StarSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/SpaceObjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceObject>>> GetSpaceObjects()
        {
            return await _context.SpaceObjects.ToListAsync();
        }

        // GET: api/SpaceObjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceObject>> GetSpaceObject(int id)
        {
            var spaceObject = await _context.SpaceObjects.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            if (spaceObject == null)
            {
                return NotFound();
            }

            return spaceObject;
        }

        // PUT: api/SpaceObjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceObject(int id, SpaceObject spaceObject)
        {
            if (id != spaceObject.Id)
            {
                return BadRequest();
            }

            var existingSpaseObject = await _context.SpaceObjects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existingSpaseObject == null)
            {
                return NotFound();
            }

            _context.SpaceObjects.Update(spaceObject);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/SpaceObjects
        [HttpPost]
        public async Task<ActionResult<SpaceObject>> PostSpaceObject(SpaceObject spaceObject)
        {
            _context.SpaceObjects.Add(spaceObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpaceObject", new { id = spaceObject.Id }, spaceObject);
        }

        // DELETE: api/SpaceObjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpaceObject(int id)
        {
            var spaceObject = await _context.SpaceObjects.FindAsync(id);
            if (spaceObject == null)
            {
                return NotFound();
            }
            var relatedStarSystems = _context.StarSystems
                .Where(ss => ss.MassCenterId == id);
            foreach (var starSystem in relatedStarSystems)
            {
                starSystem.MassCenterId = null;
            }


            _context.SpaceObjects.Remove(spaceObject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
