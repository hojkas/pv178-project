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
    public class MatchesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly PlayerOrMatchResultsController _pomrController;

        public MatchesController(AppDBContext context)
        {
            _context = context;
            _pomrController = new PlayerOrMatchResultsController(context);
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches
                .Include(m => m.Players)
                .ThenInclude(p => p.Player)
                .Include(m => m.Players)
                .ThenInclude(p => p.Match)
                .Include(m => m.Winner)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.Id)
            {
                return BadRequest();
            }

            if (match.WinnerId != null && match.PlayerRequiringResultId != null)
            {
                // if result was put in, updates dependent player match
                var pomr = await _context.PlayerOrMatchResults.FindAsync((int)match.PlayerRequiringResultId);
                if (pomr != null && !pomr.IsEmpty && !pomr.IsPlayer)
                {
                    pomr.IsPlayer = true;
                    pomr.PlayerId = match.WinnerId;
                    pomr.MatchId = null;
                    match.PlayerRequiringResultId = null;
                    _context.Entry(pomr).State = EntityState.Modified;
                }
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'AppDBContext.Matches'  is null.");
            }

            if(match.Players.Count > 2)
            {
                return BadRequest("Match cannot have more than 2 players");
            } 

            // add empty POMR (player or match result) to fill matches players
            if(match.Players.Count < 2)
            {
                while(match.Players.Count < 2)
                {
                    match.Players.Add(new PlayerOrMatchResult()
                    {
                        IsEmpty = true,
                        OriginalMatchId = match.Id
                    });
                }
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.Id }, match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches
                .Where(m => m.Id == id)
                .Include(m => m.Players)
                .FirstOrDefaultAsync();

            if (match == null)
            {
                return NotFound();
            }
            
            if (match.PlayerRequiringResultId != null)
            {
                return BadRequest("Cannot delete match when its result is to be used by another match.");
            }
            
            match.WinnerId = null;
            match.Winner = null;

            foreach(var pomr in match.Players)
            {
                if (!pomr.IsEmpty) await _pomrController.EmptyPlayerOrMatchResult(pomr.Id);
            }
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task DeleteAllPlayersOrMatchResultsWithOriginMatch(int id)
        {
            var players = await _context.PlayerOrMatchResults.Where(p => p.OriginalMatchId == id).ToListAsync();
            foreach (var player in players) _context.PlayerOrMatchResults.Remove(player);
            await _context.SaveChangesAsync();
        }

        private bool MatchExists(int id)
        {
            return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
