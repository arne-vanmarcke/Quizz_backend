﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApp.Models;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private readonly HighscoreContext _context;

        public HighscoresController(HighscoreContext context)
        {
            _context = context;
        }

        // GET: api/Highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores()
        {
            return await _context.Highscores.ToListAsync();
        }

        // GET: api/Highscores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Highscore>> GetHighscore(long id)
        {
            var highscore = await _context.Highscores.FindAsync(id);

            if (highscore == null)
            {
                return NotFound();
            }

            return highscore;
        }

        // PUT: api/Highscores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHighscore(long id, Highscore highscore)
        {
            if (id != highscore.Id)
            {
                return BadRequest();
            }

            _context.Entry(highscore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HighscoreExists(id))
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

        // POST: api/Highscores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Highscore>> PostHighscore(Highscore highscore)
        {
            _context.Highscores.Add(highscore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHighscore", new { id = highscore.Id }, highscore);
        }

        // DELETE: api/Highscores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHighscore(long id)
        {
            var highscore = await _context.Highscores.FindAsync(id);
            if (highscore == null)
            {
                return NotFound();
            }

            _context.Highscores.Remove(highscore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HighscoreExists(long id)
        {
            return _context.Highscores.Any(e => e.Id == id);
        }
    }
}
