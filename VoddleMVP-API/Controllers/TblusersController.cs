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
    public class TblusersController : ControllerBase
    {
        private readonly voddlemvpContext _context;

        public TblusersController(voddlemvpContext context)
        {
            _context = context;
        }

        // GET: api/Tblusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tbluser>>> GetTblusers()
        {
            return await _context.Tblusers.ToListAsync();
        }

        // GET: api/Tblusers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tbluser>> GetTbluser(Guid id)
        {
            var tbluser = await _context.Tblusers.FindAsync(id);

            if (tbluser == null)
            {
                return NotFound();
            }

            return tbluser;
        }

        // PUT: api/Tblusers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbluser(Guid id, Tbluser tbluser)
        {
            if (id != tbluser.Userid)
            {
                return BadRequest();
            }

            _context.Entry(tbluser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbluserExists(id))
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

        // POST: api/Tblusers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tbluser>> PostTbluser(Tbluser tbluser)
        {
            _context.Tblusers.Add(tbluser);
            tbluser.Userid = Guid.NewGuid();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TbluserExists(tbluser.Userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTbluser", new { id = tbluser.Userid }, tbluser);
        }

        // DELETE: api/Tblusers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbluser(Guid id)
        {
            var tbluser = await _context.Tblusers.FindAsync(id);
            if (tbluser == null)
            {
                return NotFound();
            }

            _context.Tblusers.Remove(tbluser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbluserExists(Guid id)
        {
            return _context.Tblusers.Any(e => e.Userid == id);
        }
    }
}
