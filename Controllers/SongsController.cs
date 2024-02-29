
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortafolioBack.DB;
using PortafolioBack.Model;

namespace PortafolioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SqliteDB _context;

        public SongsController(SqliteDB context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongI()
        {
            return await _context.SongI.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.SongI.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }
        [HttpGet("apiPage")]
        public async Task<ActionResult<IEnumerable<Song>>> GetPage([FromQuery] int page, [FromQuery] int year, [FromQuery] int limit)
        {
            var songs = await _context.SongI.Where(p=>p.year ==year)
                .OrderBy(s=>s.id)
                .Skip(limit * (page-1))
                .Take(limit)
                .ToListAsync();
            if (songs == null || !songs.Any())
            {
                return NotFound();
            }

            return songs;

        }
        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.SongI.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.id }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.SongI.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.SongI.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return _context.SongI.Any(e => e.id == id);
        }
    }
}
