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
    public class PlayerOrMatchResultsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PlayerOrMatchResultsController(AppDBContext context)
        {
            _context = context;
        }

        // Empties the playerOrMatchResult
        internal async Task EmptyPlayerOrMatchResult(int id)
        {
            Match? match = null;

            var pomr = await _context.PlayerOrMatchResults
                .Include(p => p.Match)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pomr != null && !pomr.IsEmpty)
            {
                pomr.IsPlayer = true;
                pomr.PlayerId = null;
                pomr.Player = null;
                pomr.MatchId = null;

                if (pomr.Match != null)
                {
                    pomr.Match.PlayerRequiringResult = null;
                    _context.Matches.Update(pomr.Match);
                }

                pomr.Match = null;
                pomr.IsEmpty = true;
                _context.Entry(pomr).State = EntityState.Modified;
                if (match != null) _context.Entry(match).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
        }

        // GET: api/PlayerOrMatchResults/5
        // TODO delete only for debug
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerOrMatchResult>> GetPlayerOrMatchResult(int id)
        {
          if (_context.PlayerOrMatchResults == null)
          {
              return NotFound();
          }
            var playerOrMatchResult = await _context.PlayerOrMatchResults
                .Include(p => p.Match)
                .Include(p => p.OriginalMatch)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playerOrMatchResult == null)
            {
                return NotFound();
            }

            return playerOrMatchResult;
        }

        // PUT: api/PlayerOrMatchResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerOrMatchResult(int id, PlayerOrMatchResult playerOrMatchResult)
        {
            if (id != playerOrMatchResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerOrMatchResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerOrMatchResultExists(id))
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

        // POST: api/PlayerOrMatchResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerOrMatchResult>> PostPlayerOrMatchResult(PlayerOrMatchResult playerOrMatchResult)
        {
          if (_context.PlayerOrMatchResults == null)
          {
              return Problem("Entity set 'AppDBContext.PlayerOrMatchResults'  is null.");
          }
            _context.PlayerOrMatchResults.Add(playerOrMatchResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerOrMatchResult", new { id = playerOrMatchResult.Id }, playerOrMatchResult);
        }

        // DELETE: api/PlayerOrMatchResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerOrMatchResult(int id)
        {
            if (_context.PlayerOrMatchResults == null)
            {
                return NotFound();
            }
            var playerOrMatchResult = await _context.PlayerOrMatchResults.FindAsync(id);
            if (playerOrMatchResult == null)
            {
                return NotFound();
            }

            _context.PlayerOrMatchResults.Remove(playerOrMatchResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerOrMatchResultExists(int id)
        {
            return (_context.PlayerOrMatchResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
