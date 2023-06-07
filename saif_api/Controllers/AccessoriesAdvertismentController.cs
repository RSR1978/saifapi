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
    public class AccessoriesAdvertismentController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        public AccessoriesAdvertismentController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/AccessoriesAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessoriesAdvertisment>>> GetAccessoriesAdvertisments()
        {
            return await _context.AccessoriesAdvertisments.ToListAsync();
        }

        // GET: api/AccessoriesAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessoriesAdvertisment>> GetAccessoriesAdvertisment(long id)
        {
            var accessoriesAdvertisment = await _context.AccessoriesAdvertisments.FindAsync(id);

            if (accessoriesAdvertisment == null)
            {
                return NotFound();
            }

            return accessoriesAdvertisment;
        }

        // PUT: api/AccessoriesAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessoriesAdvertisment(long id, AccessoriesAdvertisment accessoriesAdvertisment)
        {
            if (id != accessoriesAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(accessoriesAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoriesAdvertismentExists(id))
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

        private async Task<string> WriteFile(IFormFile file, string fnm, long dd)
        {
            //string fileName;
            try
            {
                //Directory.CreateDirectory("D:\\images\\aliii");
                var pp = "D:\\images\\Accessories\\" + dd;
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

        // POST: api/AccessoriesAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AccessoriesAdvertisment>> PostAccessoriesAdvertisment([FromForm] AccessoriesAdvertisment accessoriesAdvertisment)
        {
            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + accessoriesAdvertisment.File.FileName);
            accessoriesAdvertisment.File.CopyTo(fileStream);

            string ext;
            ext = Path.GetExtension(accessoriesAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;

            fileStream.Flush();
            var stream = accessoriesAdvertisment.File.OpenReadStream();
            accessoriesAdvertisment.m_img = ReadFully(stream);

            _context.AccessoriesAdvertisments.Add(accessoriesAdvertisment);
            await _context.SaveChangesAsync();

            await WriteFile(accessoriesAdvertisment.File, fileName, accessoriesAdvertisment.Id);

            return CreatedAtAction("GetAccessoriesAdvertisment", new { id = accessoriesAdvertisment.Id }, accessoriesAdvertisment);
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


        // DELETE: api/AccessoriesAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccessoriesAdvertisment>> DeleteAccessoriesAdvertisment(long id)
        {
            var accessoriesAdvertisment = await _context.AccessoriesAdvertisments.FindAsync(id);
            if (accessoriesAdvertisment == null)
            {
                return NotFound();
            }

            _context.AccessoriesAdvertisments.Remove(accessoriesAdvertisment);
            await _context.SaveChangesAsync();

            return accessoriesAdvertisment;
        }

        private bool AccessoriesAdvertismentExists(long id)
        {
            return _context.AccessoriesAdvertisments.Any(e => e.Id == id);
        }
    }
}
