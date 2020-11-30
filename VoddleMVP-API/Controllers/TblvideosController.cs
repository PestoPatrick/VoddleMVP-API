using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoddleMVP_API;

namespace VoddleMVP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblvideosController : ControllerBase
    {
        private readonly voddlemvpContext _context;

        public TblvideosController(voddlemvpContext context)
        {
            _context = context;
        }

        // GET: api/Tblvideos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblvideo>>> GetTblvideos()
        {
            return await _context.Tblvideos.ToListAsync();
        }

        // GET: api/Tblvideos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblvideo>> GetTblvideo(Guid id)
        {
            var tblvideo = await _context.Tblvideos.FindAsync(id);

            if (tblvideo == null)
            {
                return NotFound();
            }

            return tblvideo;
        }

        // PUT: api/Tblvideos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblvideo(Guid id, Tblvideo tblvideo)
        {
            if (id != tblvideo.Videoid)
            {
                return BadRequest();
            }

            _context.Entry(tblvideo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblvideoExists(id))
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

        // POST: api/Tblvideos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblvideo>> PostTblvideo(Tblvideo tblvideo)
        {
            _context.Tblvideos.Add(tblvideo);
            tblvideo.Published = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblvideoExists(tblvideo.Videoid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblvideo", new { id = tblvideo.Videoid }, tblvideo);
        }

        // DELETE: api/Tblvideos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblvideo(Guid id)
        {
            var tblvideo = await _context.Tblvideos.FindAsync(id);
            if (tblvideo == null)
            {
                return NotFound();
            }

            _context.Tblvideos.Remove(tblvideo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblvideoExists(Guid id)
        {
            return _context.Tblvideos.Any(e => e.Videoid == id);
        }
    }
}
