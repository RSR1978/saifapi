using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saif_api.Data;
using saif_api.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using saif_api.DTO;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewAdvertismentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ReviewAdvertismentsController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/ReviewAdvertisments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewAdvertisment>>> GetReviewAdvertisments()
        {
            return await _context.ReviewAdvertisments.ToListAsync();
        }

        // GET: api/ReviewAdvertisments/5
        //[Route("api/CATEGORIES/{SCatParent?}")]
        //[HttpGet("{SCatParent}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewAdvertisment>> GetReviewAdvertisment(long id)
        {
            var jobAdvertisment = await _context.ReviewAdvertisments.FindAsync(id);

            if (jobAdvertisment == null)
            {
                return NotFound();
            }

            return jobAdvertisment;
        }



        [Route("revadv/{fserch}")]
        [HttpGet]
        //[HttpGet("{MID}")]

        public JsonResult Myadv(string fserch)
        {
            var list = new List<Review_DTO>();
            var rev_s =0;

            var cars = _context.CarAdvertisments.AsNoTracking()
               .Where(x => x.UserId.Contains(fserch))
               .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

          

            //var estates = _context.EstateAdvertisments.AsNoTracking()
            //             .Where(x => x.CategoryId == 2)
            //             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
            var estates = _context.EstateAdvertisments.AsNoTracking().AsEnumerable()
                .Where(x => x.UserId.Contains(fserch))
                .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

            var products = _context.ProductAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch))
              .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

            var jobs = _context.JobAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch))
              .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

           // var services = _context.                     ServiceAdvertisments.AsNoTracking().AsEnumerable()
            //  .Where(x => x.UserId.Contains(fserch))
            //  .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

            var animals = _context.AnimalAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.UserId.Contains(fserch))
             .Select(x => _mapper.Map<Review_DTO>(x)).ToList();

            list.AddRange(cars);
            list.AddRange(estates);
            list.AddRange(products);
            list.AddRange(jobs);
            //list.AddRange(services);
            list.AddRange(animals);

            foreach (var G in list)
            {
                rev_s = rev_s + _context.ReviewAdvertisments.AsQueryable().Count(p => p.AdvId == G.Id & p.CategoryId == G.CategoryId);


                //Console.WriteLine("Amount is {0} and type is {1}", money.amount, money.type);
            }


            return new JsonResult(rev_s);

        }






        // PUT: api/ReviewAdvertisments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviewAdvertisment(long id, ReviewAdvertisment ReviewAdvertisment)
        {
            if (id != ReviewAdvertisment.Id)
            {
                return BadRequest();
            }

            _context.Entry(ReviewAdvertisment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewAdvertismentExists(id))
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

        // POST: api/ReviewAdvertisments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReviewAdvertisment>> PostReviewAdvertisment(ReviewAdvertisment ReviewAdvertisment)
        {
            _context.ReviewAdvertisments.Add(ReviewAdvertisment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviewAdvertisment", new { id = ReviewAdvertisment.Id }, ReviewAdvertisment);
        }

        // DELETE: api/ReviewAdvertisments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReviewAdvertisment>> DeleteReviewAdvertisment(long id)
        {
            var ReviewAdvertisment = await _context.ReviewAdvertisments.FindAsync(id);
            if (ReviewAdvertisment == null)
            {
                return NotFound();
            }

            _context.ReviewAdvertisments.Remove(ReviewAdvertisment);
            await _context.SaveChangesAsync();

            return ReviewAdvertisment;
        }

        private bool ReviewAdvertismentExists(long id)
        {
            return _context.ReviewAdvertisments.Any(e => e.Id == id);
        }
    }
}
