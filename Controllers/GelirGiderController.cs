using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BilancoApp.Model;
using BilancoApp.Model.request;

namespace BilancoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GelirGiderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GelirGiderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GelirGider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GelirGider>>> GetGelirGider()
        {
          if (_context.GelirGider == null)
          {
              return NotFound();
          }

            var gelirGider = from GelirGider in _context.GelirGider
                             join Kalemler in _context.Kalemler
                                  on GelirGider.KalemlerId equals Kalemler.Id
                             select new
                             {
                                Id = GelirGider.Id,
                                Kalem = Kalemler.Name,
                                Tutar = GelirGider.Tutar,
                                IslemTarihi = GelirGider.IslemTarihi.ToString("dd.MM.yyyy"),
                             };

            var gelirGiderList = await gelirGider.ToListAsync();

            var result = gelirGiderList.Select(item => new GelirGiderJoinKalem
            {
                Id = item.Id,
                Kalem = item.Kalem,
                Tutar = item.Tutar,
                IslemTarihi = item.IslemTarihi
            });
            return Ok(result);
        }

        // GET: GelirGider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GelirGider>> GetGelirGider(int id)
        {
          if (_context.GelirGider == null)
          {
              return NotFound();
          }
            var gelirGider = await _context.GelirGider.FindAsync(id);

            if (gelirGider == null)
            {
                return NotFound();
            }

            return gelirGider;
        }

        // PUT: GelirGider/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGelirGider(int id, GelirGiderPost gelirGider)
        {
            
            var gelirGiderModel = new GelirGider();
            gelirGiderModel.Id = id;
            gelirGiderModel.KalemlerId = gelirGider.Kalem;
            gelirGiderModel.Tutar = gelirGider.Tutar;
            gelirGiderModel.IslemTarihi = gelirGider.IslemTarihi;


            _context.Entry(gelirGiderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GelirGiderExists(id))
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

        // POST: GelirGider
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GelirGiderPost>> PostGelirGider(GelirGiderPost gelirGiderPost)
        {
          if (_context.GelirGider == null)
          {
              return Problem("Entity set 'AppDbContext.GelirGider'  is null.");
          }

            var gelirGider = new GelirGider();
            gelirGider.KalemlerId = gelirGiderPost.Kalem;
            gelirGider.Tutar = gelirGiderPost.Tutar;
            gelirGider.IslemTarihi = gelirGiderPost.IslemTarihi;

            _context.GelirGider.Add(gelirGider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGelirGider", new { id = gelirGider.Id }, gelirGider);
        }

        // DELETE: GelirGider/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGelirGider(int id)
        {
            if (_context.GelirGider == null)
            {
                return NotFound();
            }
            var gelirGider = await _context.GelirGider.FindAsync(id);
            if (gelirGider == null)
            {
                return NotFound();
            }

            _context.GelirGider.Remove(gelirGider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GelirGiderExists(int id)
        {
            return (_context.GelirGider?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
