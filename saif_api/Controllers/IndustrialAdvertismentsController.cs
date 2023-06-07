using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saif_api.Data;
using saif_api.Models;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustrialAdvertismentsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public IndustrialAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/IndustrialAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndustrialAdvertisment>>> GetIndustrialAdvertisments()
        {
            return await _context.IndustrialAdvertisments.ToListAsync();
        }

        // GET: api/IndustrialAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IndustrialAdvertisment>> GetIndustrialAdvertisment(long id)
        {
            var industrialAdvertisment = await _context.IndustrialAdvertisments.Include(i => i.Gover).FirstOrDefaultAsync(i => i.Id == id);

            if (industrialAdvertisment == null)
            {
                return NotFound();
            }

            return industrialAdvertisment;
        }

        // PUT: api/IndustrialAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndustrialAdvertisment(long id, IndustrialAdvertisment industrialAdvertisment)
        {
            if (id != industrialAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(industrialAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndustrialAdvertismentExists(id))
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


        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async Task<string> WriteFile(IFormFile file, string fnm, long dd)
        {
            //string fileName;
            try
            {
                //Directory.CreateDirectory("D:\\images\\aliii");
                var pp = "D:\\images\\Industrial\\" + dd;
                Directory.CreateDirectory(pp);
                bool folderExists = Directory.Exists(pp);
                if (!folderExists)
                    Directory.CreateDirectory(pp);
                //var extension = new StringBuilder(".")
                //    .Append(file.FileName.Split(".")[file.FileName.Split('.').Length - 1]);
                //fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                //var path = Path.Combine(Directory.GetCurrentDirectory(),
                //    "wwwroot\\images", fileName);
                var path = Path.Combine(
                  pp, fnm);
                using (var bits = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(bits);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fnm;
        }

        // POST: api/IndustrialAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IndustrialAdvertisment>> PostIndustrialAdvertisment([FromForm] IndustrialAdvertisment industrialAdvertisment)
        {
            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + industrialAdvertisment.File.FileName);
            industrialAdvertisment.File.CopyTo(fileStream);

            string ext;
            ext = Path.GetExtension(industrialAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;

            fileStream.Flush();
            var stream = industrialAdvertisment.File.OpenReadStream();
            industrialAdvertisment.m_img = ReadFully(stream);

            _context.IndustrialAdvertisments.Add(industrialAdvertisment);
            await _context.SaveChangesAsync();

            await WriteFile(industrialAdvertisment.File, fileName, industrialAdvertisment.Id);

            return CreatedAtAction("GetIndustrialAdvertisment", new { id = industrialAdvertisment.Id }, industrialAdvertisment);
        }

        // DELETE: api/IndustrialAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IndustrialAdvertisment>> DeleteIndustrialAdvertisment(long id)
        {
            var industrialAdvertisment = await _context.IndustrialAdvertisments.FindAsync(id);
            if (industrialAdvertisment == null)
            {
                return NotFound();
            }

            _context.IndustrialAdvertisments.Remove(industrialAdvertisment);
            await _context.SaveChangesAsync();

            return industrialAdvertisment;
        }

        private bool IndustrialAdvertismentExists(long id)
        {
            return _context.IndustrialAdvertisments.Any(e => e.Id == id);
        }
    }
}
