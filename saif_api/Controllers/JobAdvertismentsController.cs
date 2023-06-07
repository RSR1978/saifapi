using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using saif_api.Data;
using saif_api.Models;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdvertismentsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public JobAdvertismentsController(IWebHostEnvironment environment, ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        // GET: api/JobAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobAdvertisment>>> GetJobAdvertisments()
        {
            return await _context.JobAdvertisments.ToListAsync();
        }

        // GET: api/JobAdvertisments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobAdvertisment>> GetJobAdvertisment(long id)
        {
            var jobAdvertisment = await _context.JobAdvertisments.Include(i => i.Gover).Include(i => i.Qulification_info).Include(i => i.Gender_Info).Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
            //var jobAdvertisment = await _context.JobAdvertisments.FindAsync(id);

            if (jobAdvertisment == null)
            {
                return NotFound();
            }

            return jobAdvertisment;
        }

        // PUT: api/JobAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobAdvertisment(long id, JobAdvertisment jobAdvertisment)
        {
            if (id != jobAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAdvertismentExists(id))
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
                var pp = "D:\\images\\Job\\" + dd;
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

                string query = @"insert into sooqak.adv_img
                                values (@id,@Adv_Id,@Adv_img,@comm)";
                //string query = @"update sooqak.jobadvertisments set Att=@att where Id=@dn";
                DataTable table = new DataTable();
                string MysqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                MySqlDataReader myReader;
                using (MySqlConnection myCon = new MySqlConnection(MysqlDataSource))
                {
                    myCon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", 8);
                        myCommand.Parameters.AddWithValue("@Adv_Id", dd);
                        myCommand.Parameters.AddWithValue("@Adv_img", path);
                        myCommand.Parameters.AddWithValue("@comm", "test");
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }

                }


             }
            catch (Exception e)
            {
                return e.Message;
            }
            return fnm;
        }

        // POST: api/JobAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<JobAdvertisment>> PostJobAdvertisment([FromForm] JobAdvertisment jobAdvertisment)
        {

            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + jobAdvertisment.File.FileName);
            jobAdvertisment.File.CopyTo(fileStream);

            string ext;
            ext = Path.GetExtension(jobAdvertisment.File.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = "1" + ext;

            fileStream.Flush();
            var stream = jobAdvertisment.File.OpenReadStream();
            jobAdvertisment.m_img = ReadFully(stream);
            //jobAdvertisment.Att = "D:\\images\\Job\\" + fileName;
           
            _context.JobAdvertisments.Add(jobAdvertisment);
            await _context.SaveChangesAsync();

            await WriteFile(jobAdvertisment.File, fileName, jobAdvertisment.Id);

            return CreatedAtAction("GetJobAdvertisment", new { id = jobAdvertisment.Id }, jobAdvertisment);
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


        // DELETE: api/JobAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobAdvertisment>> DeleteJobAdvertisment(long id)
        {
            var jobAdvertisment = await _context.JobAdvertisments.FindAsync(id);
            if (jobAdvertisment == null)
            {
                return NotFound();
            }

            _context.JobAdvertisments.Remove(jobAdvertisment);
            await _context.SaveChangesAsync();

            return jobAdvertisment;
        }

        private bool JobAdvertismentExists(long id)
        {
            return _context.JobAdvertisments.Any(e => e.Id == id);
        }
    }
}
