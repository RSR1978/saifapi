using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saif_api.Data;
using saif_api.Models;
using saif_api.Interface;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAdvertismentsController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public ServiceAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;

        }

        // GET: api/ServiceAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceAdvertisment>>> GetServiceAdvertisments()
        {
            return await _context.ServiceAdvertisments.ToListAsync();
        }

        // GET: api/ServiceAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceAdvertisment>> GetServiceAdvertisment(long id)
        {
            var serviceAdvertisment = await _context.ServiceAdvertisments.Include(i => i.Gover).FirstOrDefaultAsync(i => i.Id == id);
           // var serviceAdvertisment = await _context.ServiceAdvertisments.FindAsync(id);

            if (serviceAdvertisment == null)
            {
                return NotFound();
            }

            return serviceAdvertisment;
        }

        // PUT: api/ServiceAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceAdvertisment(long id, ServiceAdvertisment serviceAdvertisment)
        {
            if (id != serviceAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceAdvertismentExists(id))
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


        private async Task<string> WriteFile(IFormFile file,string fnm,long dd)
        {
            //string fileName;
            try
            {
                //Directory.CreateDirectory("D:\\images\\aliii");
                var pp = "D:\\images\\Service\\" + dd;
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
        // POST: api/ServiceAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ServiceAdvertisment>> PostServiceAdvertisment([FromForm] ServiceAdvertisment serviceAdvertisment)
        {
            string fileName="";
            
                FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + serviceAdvertisment.File.FileName);
                serviceAdvertisment.File.CopyTo(fileStream);

                string ext;
                ext = Path.GetExtension(serviceAdvertisment.File.FileName);
                
                fileName = "1" + ext;

                fileStream.Flush();
                var stream = serviceAdvertisment.File.OpenReadStream();
                serviceAdvertisment.m_img = ReadFully(stream);
                //Directory.CreateDirectory("D:\\images\\Service\\ffffff");
           
            _context.ServiceAdvertisments.Add(serviceAdvertisment);
            await _context.SaveChangesAsync();
           // if (serviceAdvertisment.File.FileName != null)
            //{
                await WriteFile(serviceAdvertisment.File, fileName, serviceAdvertisment.Id);
            //}
            return CreatedAtAction("GetServiceAdvertisment", new { id = serviceAdvertisment.Id }, serviceAdvertisment);
        }

        // DELETE: api/ServiceAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceAdvertisment>> DeleteServiceAdvertisment(long id)
        {
            var serviceAdvertisment = await _context.ServiceAdvertisments.FindAsync(id);
            if (serviceAdvertisment == null)
            {
                return NotFound();
            }

            _context.ServiceAdvertisments.Remove(serviceAdvertisment);
            await _context.SaveChangesAsync();

            return serviceAdvertisment;
        }

        private bool ServiceAdvertismentExists(long id)
        {
            return _context.ServiceAdvertisments.Any(e => e.Id == id);
        }
    }
}
