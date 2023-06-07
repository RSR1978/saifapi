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
    public class ProductAdvertismentsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public ProductAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ProductAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAdvertisment>>> GetProductAdvertisments()
        {
            return await _context.ProductAdvertisments.ToListAsync();
        }

        // GET: api/ProductAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAdvertisment>> GetProductAdvertisment(long id)
        {
            //var industrialAdvertisment = await _context.IndustrialAdvertisments.Include(i => i.Gover).FirstOrDefaultAsync(i => i.Id == id);
            var productAdvertisment = await _context.ProductAdvertisments.Include(i => i.Gover).FirstOrDefaultAsync(i => i.Id == id);

            if (productAdvertisment == null)
            {
                return NotFound();
            }

            return productAdvertisment;
        }

        // PUT: api/ProductAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAdvertisment(long id, ProductAdvertisment productAdvertisment)
        {
            if (id != productAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(productAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAdvertismentExists(id))
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
                var pp = "D:\\images\\Product\\" + dd;
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
        // POST: api/ProductAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductAdvertisment>> PostProductAdvertisment([FromForm] ProductAdvertisment productAdvertisment)
        {
            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + productAdvertisment.File.FileName);
            productAdvertisment.File.CopyTo(fileStream);
            
            string ext;
            ext = Path.GetExtension(productAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;
            fileStream.Flush();
            var stream = productAdvertisment.File.OpenReadStream();
            productAdvertisment.m_img = ReadFully(stream);

            _context.ProductAdvertisments.Add(productAdvertisment);
            await _context.SaveChangesAsync();

            //fileName = "1" + (productAdvertisment.File.FileName.Split('.').Length - 1);
            
            await WriteFile(productAdvertisment.File, fileName, productAdvertisment.Id);
            
            return CreatedAtAction("GetProductAdvertisment", new { id = productAdvertisment.Id }, productAdvertisment);
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

        // DELETE: api/ProductAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductAdvertisment>> DeleteProductAdvertisment(long id)
        {
            var productAdvertisment = await _context.ProductAdvertisments.FindAsync(id);
            if (productAdvertisment == null)
            {
                return NotFound();
            }

            _context.ProductAdvertisments.Remove(productAdvertisment);
            await _context.SaveChangesAsync();

            return productAdvertisment;
        }

        private bool ProductAdvertismentExists(long id)
        {
            return _context.ProductAdvertisments.Any(e => e.Id == id);
        }
    }
}
