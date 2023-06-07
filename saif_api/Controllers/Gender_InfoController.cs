using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saif_api.Data;
using saif_api.Models;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Gender_InfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Gender_InfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gender_Info
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender_Info>>> GetGender_Infos()
        {
            return await _context.Gender_Infos.ToListAsync();
        }

        // GET: api/Gender_Info/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender_Info>> GetGender_Info(int id)
        {
            var gender_Info = await _context.Gender_Infos.FindAsync(id);

            if (gender_Info == null)
            {
                return NotFound();
            }

            return gender_Info;
        }

        // PUT: api/Gender_Info/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender_Info(int id, Gender_Info gender_Info)
        {
            if (id != gender_Info.Id)
            {
                return BadRequest();
            }

            _context.Entry(gender_Info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gender_InfoExists(id))
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

        // POST: api/Gender_Info
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gender_Info>> PostGender_Info(Gender_Info gender_Info)
        {
            _context.Gender_Infos.Add(gender_Info);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGender_Info", new { id = gender_Info.Id }, gender_Info);
        }

        // DELETE: api/Gender_Info/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gender_Info>> DeleteGender_Info(int id)
        {
            var gender_Info = await _context.Gender_Infos.FindAsync(id);
            if (gender_Info == null)
            {
                return NotFound();
            }

            _context.Gender_Infos.Remove(gender_Info);
            await _context.SaveChangesAsync();

            return gender_Info;
        }

        private bool Gender_InfoExists(int id)
        {
            return _context.Gender_Infos.Any(e => e.Id == id);
        }
    }
}
