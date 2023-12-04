using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BilancoApp.Model;

namespace BilancoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KalemlerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KalemlerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kalemler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kalemler>>> GetKalemler()
        {
          if (_context.Kalemler == null)
          {
              return NotFound();
          }
            

            var kalemler = await _context.Kalemler
                .Select(kalem => new
                {
                    kalem.Id,
                    kalem.Name,
                    kalem.Description,
                    Type = kalem.Type == 0 ? "Gider" :
                           kalem.Type == 1 ? "Gelir" :
                           kalem.Type.ToString(),
                    Tarih = kalem.Tarih.ToString("dd.MM.yyyy")
                }).ToListAsync();

            return Ok(kalemler);

        }

        // GET: Kalemler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kalemler>> GetKalemler(int id)
        {
          if (_context.Kalemler == null)
          {
              return NotFound();
          }
            var kalemler = await _context.Kalemler.FindAsync(id);

            if (kalemler == null)
            {
                return NotFound();
            }

            return kalemler;
        }

        // PUT: Kalemler/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKalemler(int id, Kalemler kalemler)
        {
            if (id != kalemler.Id)
            {
                return BadRequest();
            }

            _context.Entry(kalemler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalemlerExists(id))
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

        // POST: Kalemler
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kalemler>> PostKalemler(Kalemler kalemler)
        {
          if (_context.Kalemler == null)
          {
              return Problem("Entity set 'AppDbContext.Kalemler'  is null.");
          }
            _context.Kalemler.Add(kalemler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKalemler", new { id = kalemler.Id }, kalemler);
        }

        // DELETE: Kalemler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKalemler(int id)
        {
            if (_context.Kalemler == null)
            {
                return NotFound();
            }
            var kalemler = await _context.Kalemler.FindAsync(id);
            if (kalemler == null)
            {
                return NotFound();
            }

            _context.Kalemler.Remove(kalemler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KalemlerExists(int id)
        {
            return (_context.Kalemler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
