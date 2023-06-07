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
    public class CarAdvertismentsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public CarAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/CarAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAdvertisment>>> GetCarAdvertisments()
        {
            return await _context.CarAdvertisments.ToListAsync();
        }

        // GET: api/CarAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAdvertisment>> GetCarAdvertisment(long id)
        {
            //var carAdvertisment = await _context.CarAdvertisments.FindAsync(id);
            var carAdvertisment = await _context.CarAdvertisments.Include(i => i.Gover).Include(i => i.TCategory).Include(i => i.CCategory).FirstOrDefaultAsync(i => i.Id == id);
            //return await _context.EstateAdvertisments.Include(u => u.Gover).ToListAsync();

            if (carAdvertisment == null)
            {
                return NotFound();
            }

            
            //await _context.Entry(carAdvertisment.Cat).Collection(i => i.).LoadAsync();



            return carAdvertisment;
        }

        // PUT: api/CarAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAdvertisment(long id, CarAdvertisment carAdvertisment)
        {
            if (id != carAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(carAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarAdvertismentExists(id))
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
                var pp = "D:\\images\\Car\\" + dd;
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


        // POST: api/CarAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CarAdvertisment>> PostCarAdvertisment([FromForm] CarAdvertisment carAdvertisment)
        {
            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + carAdvertisment.File.FileName);
            carAdvertisment.File.CopyTo(fileStream);
            
            string ext;
            ext = Path.GetExtension(carAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;

            fileStream.Flush();
            var stream = carAdvertisment.File.OpenReadStream();
            carAdvertisment.m_img = ReadFully(stream);

            _context.CarAdvertisments.Add(carAdvertisment);
            await _context.SaveChangesAsync();

            await WriteFile(carAdvertisment.File, fileName, carAdvertisment.Id);

            return CreatedAtAction("GetCarAdvertisment", new { id = carAdvertisment.Id }, carAdvertisment);
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

        // DELETE: api/CarAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarAdvertisment>> DeleteCarAdvertisment(long id)
        {
            var carAdvertisment = await _context.CarAdvertisments.FindAsync(id);
            if (carAdvertisment == null)
            {
                return NotFound();
            }

            _context.CarAdvertisments.Remove(carAdvertisment);
            await _context.SaveChangesAsync();

            return carAdvertisment;
        }

        private bool CarAdvertismentExists(long id)
        {
            return _context.CarAdvertisments.Any(e => e.Id == id);
        }
    }
}
