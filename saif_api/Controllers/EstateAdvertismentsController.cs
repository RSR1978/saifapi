using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saif_api.Data;
using saif_api.Models;
using static System.Net.Mime.MediaTypeNames;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAdvertismentsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public EstateAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/EstateAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstateAdvertisment>>> GetEstateAdvertisments()
        {
            return await _context.EstateAdvertisments.Include(u => u.Gover ).ToListAsync();
        }

        // GET: api/EstateAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstateAdvertisment>> GetEstateAdvertisment(long id)
        {
            //var estateAdvertisment = await _context.EstateAdvertisments.FindAsync(id);
            var estateAdvertisment = await _context.EstateAdvertisments.Include(i => i.Gover).FirstOrDefaultAsync(i => i.Id == id);
            if (estateAdvertisment == null)
            {
                return NotFound();
            }

            return estateAdvertisment;
        }

        // PUT: api/EstateAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Route("PutEstateAdvertisment")]
        public async Task<IActionResult> PutEstateAdvertisment([FromForm] long id, [FromForm] EstateAdvertisment estateAdvertisment)
        {
            if (id != estateAdvertisment.Id)
            {
                return BadRequest();
            }
            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + estateAdvertisment.File.FileName);
            estateAdvertisment.File.CopyTo(fileStream);

            fileStream.Flush();
            var stream = estateAdvertisment.File.OpenReadStream();
            estateAdvertisment.m_img = ReadFully(stream);
            //System.Drawing.ImageConverter _imageConverter = new ImageConverter();
            //estateAdvertisment.m_img = (byte[])_imageConverter.ConvertTo(estateAdvertisment.File, typeof(byte[]));
            // estateAdvertisment.m_img = System.IO.File.ReadAllBytes(estateAdvertisment.File.ToString());
            _context.Entry(estateAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateAdvertismentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(estateAdvertisment.Id);
            //NoContent();
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
                var pp = "D:\\images\\Estate\\" + dd;
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


        // POST: api/EstateAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EstateAdvertisment>> PostEstateAdvertisment([FromForm] EstateAdvertisment estateAdvertisment)
        {

            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + estateAdvertisment.File.FileName);
            estateAdvertisment.File.CopyTo(fileStream);
            
            string ext;
            ext = Path.GetExtension(estateAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;

            fileStream.Flush();
            var stream = estateAdvertisment.File.OpenReadStream();
            estateAdvertisment.m_img = ReadFully(stream);

            _context.EstateAdvertisments.Add(estateAdvertisment);
            await _context.SaveChangesAsync();

            await WriteFile(estateAdvertisment.File, fileName, estateAdvertisment.Id);

            return CreatedAtAction("GetEstateAdvertisment", new { id = estateAdvertisment.Id }, estateAdvertisment);
        }

        // DELETE: api/EstateAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EstateAdvertisment>> DeleteEstateAdvertisment(long id)
        {
            var estateAdvertisment = await _context.EstateAdvertisments.FindAsync(id);
            if (estateAdvertisment == null)
            {
                return NotFound();
            }

            _context.EstateAdvertisments.Remove(estateAdvertisment);
            await _context.SaveChangesAsync();

            return estateAdvertisment;
        }

        private bool EstateAdvertismentExists(long id)
        {
            return _context.EstateAdvertisments.Any(e => e.Id == id);
        }
    }
}