using BilancoApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Licenses;
using NuGet.Protocol;

namespace BilancoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/{year}")]
        public async Task<ActionResult<IEnumerable<GelirGider>>> GetChart(int id, int year)
        {
            var Kalem = await _context.Kalemler.FindAsync(id);

            var gelirGider = await _context.GelirGider
                            .Where(e => e.KalemlerId == id && e.IslemTarihi.Year == year)
                            .OrderBy(e => e.IslemTarihi)
                            .GroupBy(e => e.IslemTarihi.Month)
                            .Select(g =>  g.Sum(e => e.Tutar))
                            .ToListAsync();

            if (gelirGider == null || gelirGider.Count == 0)
            {
                gelirGider.Add(0);
            }

            var data = gelirGider.ToArray();

            var seriesData = new List<object>();
            seriesData.Add(new { label = Kalem.Name, data = data, toplamData = data.Sum().ToString("#,##.##") });

            return Ok(seriesData);
        }
    }
}
