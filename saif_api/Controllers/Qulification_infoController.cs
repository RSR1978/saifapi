using System;
using System.Collections.Generic;
using System.IO;
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
    public class Qulification_infoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Qulification_infoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Qulification_info
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qulification_info>>> GetQulification_info()
        {
            //try
            //{
            //    long dd = 7;
            //    var pp = "c:\\images\\Service";
            //    bool folderExists = Directory.Exists(pp);
            //    if (!folderExists)
            //        Directory.CreateDirectory(pp);
            //}
            //catch (Exception e)
            //{

            //}

            return await _context.Qulification_info.ToListAsync();
        }

        // GET: api/Qulification_info/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Qulification_info>> GetQulification_info(int id)
        {
            var qulification_info = await _context.Qulification_info.FindAsync(id);

            if (qulification_info == null)
            {
                return NotFound();
            }

            return qulification_info;
        }

        // PUT: api/Qulification_info/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQulification_info(int id, Qulification_info qulification_info)
        {
            if (id != qulification_info.Id)
            {
                return BadRequest();
            }

            _context.Entry(qulification_info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Qulification_infoExists(id))
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

        // POST: api/Qulification_info
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Qulification_info>> PostQulification_info(Qulification_info qulification_info)
        {
            _context.Qulification_info.Add(qulification_info);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQulification_info", new { id = qulification_info.Id }, qulification_info);
        }

        // DELETE: api/Qulification_info/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Qulification_info>> DeleteQulification_info(int id)
        {
            var qulification_info = await _context.Qulification_info.FindAsync(id);
            if (qulification_info == null)
            {
                return NotFound();
            }

            _context.Qulification_info.Remove(qulification_info);
            await _context.SaveChangesAsync();

            return qulification_info;
        }

        private bool Qulification_infoExists(int id)
        {
            return _context.Qulification_info.Any(e => e.Id == id);
        }
    }
}
