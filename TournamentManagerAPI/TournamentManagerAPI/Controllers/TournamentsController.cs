using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TournamentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Tournaments
        // TODO remove when user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
          if (_context.Tournaments == null)
          {
              return NotFound();
          }
            return await _context.Tournaments.ToListAsync();
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            if (_context.Tournaments == null)
            {
                return NotFound();
            }
            var tournament = await _context.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return tournament;
        }

        // GET: api/Tournaments/5/Players
        [HttpGet("{id}/Players")]
        public async Task<ActionResult<IEnumerable<Player>>> GetTournamentPlayers(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            return await _context.Players
                .Where(p => p.TournamentId == id)
                .ToListAsync();
        }

        // GET: api/Tournaments/5/Matches
        [HttpGet("{id}/Matches")]
        public async Task<ActionResult<IEnumerable<Match>>> GetTournamentMatches(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            return await _context.Matches
                .Where(m => m.TournamentId == id)
                .Include(m => m.Players)
                .Include(m => m.Winner)
                .ToListAsync();
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            _context.Entry(tournament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            if (_context.Tournaments == null)
            {
                return Problem("Entity set 'AppDBContext.Tournaments'  is null.");
            }
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            if (_context.Tournaments == null)
            {
                return NotFound();
            }
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TournamentExists(int id)
        {
            return (_context.Tournaments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
