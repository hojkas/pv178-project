using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PlayersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
          if (_context.Players == null)
          {
              return NotFound();
          }
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // GET: api/Players/5/Matches
        [HttpGet("{id}/Matches")]
        public async Task<ActionResult<IEnumerable<Match>>> GetPlayerMatches(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches
                .Where(m => 
                    m.Players.Any(p => !p.IsEmpty && p.IsPlayer && p.Player != null && p.Player.Id == id)
                )
                .Include(m => m.Players)
                .ThenInclude(p => p.Player)
                .Include(m => m.Players)
                .ThenInclude(p => p.Match)
                .Include(m => m.Winner)
                .ToListAsync();

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
          if (_context.Players == null)
          {
              return Problem("Entity set 'AppDBContext.Players'  is null.");
          }
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            if ((await GetPlayerMatches(id)).Value?.Count() > 0) {
                return BadRequest("Cannot delete player when they are in existing match.");
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
