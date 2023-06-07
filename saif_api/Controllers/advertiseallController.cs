using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

using Microsoft.Extensions.Configuration;
using saif_api.DTO;
using saif_api.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using saif_api.Models;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class advertiseallController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        //private readonly IConfiguration _configuration;

        public advertiseallController(ApplicationDbContext context, IMapper mapper)//IConfiguration configuration, 
        {
            //_configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
         
        [Route("api/advertiseall/{fserch}")]
        [HttpGet("{fserch}")]

        public JsonResult Get(string fserch)
        {
            var list = new List<SearchResultDTO>();


            var cars = _context.CarAdvertisments.AsNoTracking()
               .Where(x => (x.Case != null && x.Case.Contains(fserch)) || (x.Color != null && x.Color.Contains(fserch)) || x.describe.Contains(fserch) || x.price.ToString() == fserch)
               .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();


            //var estates = _context.EstateAdvertisments.AsNoTracking()
            //             .Where(x => x.CategoryId == 2)
            //             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
            var estates = _context.EstateAdvertisments.AsNoTracking().AsEnumerable()
                .Where(x => x.describe.Contains(fserch) || (x.area != null && x.area.Contains(fserch)) ||( x.Buildarea != null && x.Buildarea.Contains(fserch)) || x.price.ToString() == fserch)
                .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var products = _context.ProductAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.describe.Contains(fserch) || (x.prd_case != null && x.prd_case.Contains(fserch)) || (x.producttyp != null && x.producttyp.Contains(fserch)) || (x.brand != null && x.brand.Contains(fserch)) || (x.storage != null && x.storage.Contains(fserch)) || x.Subj.Contains(fserch) || x.price.ToString() == fserch )
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var Industs = _context.IndustrialAdvertisments.AsNoTracking().AsEnumerable()
            .Where(x => x.describe.Contains(fserch) || (x.Producttyp != null && x.Producttyp.Contains(fserch)) || (x.Prdcase != null && x.Prdcase.Contains(fserch)) || (x.Brand != null && x.Brand.Contains(fserch)) || x.Subj.Contains(fserch) || x.price.ToString() == fserch )
            .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var jobs = _context.JobAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.Subj.Contains(fserch) || x.exp_yy.ToString() == fserch || x.Desc.Contains(fserch) || x.Sal.ToString()== fserch)
              .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
            
            //var services = _context.ServiceAdvertisments.AsNoTracking().AsEnumerable()
            //  .Where(x => x.Desc.Contains(fserch) || x.Subj.Contains(fserch) || x.price.ToString() == fserch || x.Desc.Contains(fserch))
            //  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var animals = _context.AnimalAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.Desc.Contains(fserch) || x.Subj.Contains(fserch) || x.price.ToString() == fserch)
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            list.AddRange(cars);
            list.AddRange(estates);
            list.AddRange(products);
            list.AddRange(jobs);
            list.AddRange(Industs);
            //list.AddRange(services);
            list.AddRange(animals);
            
            return new JsonResult(list);

        }


        [Route("Myadv/{fserch}")]
        [HttpGet]
        //[HttpGet("{MID}")]

        public JsonResult Myadv(string fserch)
        {
            var list = new List<SearchResultDTO>();


            var cars = _context.CarAdvertisments.AsNoTracking()
               .Where(x => x.UserId.Contains(fserch) )
               .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();


            //var estates = _context.EstateAdvertisments.AsNoTracking()
            //             .Where(x => x.CategoryId == 2)
            //             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
            var estates = _context.EstateAdvertisments.AsNoTracking().AsEnumerable()
                .Where(x => x.UserId.Contains(fserch) )
                .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var accesors = _context.AccessoriesAdvertisments.AsNoTracking().AsEnumerable()
            .Where(x => x.UserId.Contains(fserch))
            .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var products = _context.ProductAdvertisments.AsNoTracking().AsEnumerable()
            .Where(x => x.UserId.Contains(fserch))
            .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var industs = _context.IndustrialAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.UserId.Contains(fserch))
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var jobs = _context.JobAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch) )
              .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var services = _context.ServiceAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch) )
              .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var animals = _context.AnimalAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.UserId.Contains(fserch))
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            list.AddRange(cars);
            list.AddRange(estates);
            list.AddRange(accesors);
            list.AddRange(products);
            list.AddRange(industs);
            list.AddRange(jobs);
            list.AddRange(services);
            list.AddRange(animals);

            return new JsonResult(list);

        }


        [Route("Myadv/{fserch}")]
        [HttpGet]
        //[HttpGet("{MID}")]

        public JsonResult Myadv_rev(string fserch)
        {
            var list = new List<SearchResultDTO>();


            var cars = _context.CarAdvertisments.AsNoTracking()
               .Where(x => x.UserId.Contains(fserch))
               .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();


            //var estates = _context.EstateAdvertisments.AsNoTracking()
            //             .Where(x => x.CategoryId == 2)
            //             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
            var estates = _context.EstateAdvertisments.AsNoTracking().AsEnumerable()
                .Where(x => x.UserId.Contains(fserch))
                .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var accesors = _context.AccessoriesAdvertisments.AsNoTracking().AsEnumerable()
            .Where(x => x.UserId.Contains(fserch))
            .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var products = _context.ProductAdvertisments.AsNoTracking().AsEnumerable()
            .Where(x => x.UserId.Contains(fserch))
            .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var industs = _context.IndustrialAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.UserId.Contains(fserch))
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var jobs = _context.JobAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch))
              .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var services = _context.ServiceAdvertisments.AsNoTracking().AsEnumerable()
              .Where(x => x.UserId.Contains(fserch))
              .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            var animals = _context.AnimalAdvertisments.AsNoTracking().AsEnumerable()
             .Where(x => x.UserId.Contains(fserch))
             .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

            list.AddRange(cars);
            list.AddRange(estates);
            list.AddRange(accesors);
            list.AddRange(products);
            list.AddRange(industs);
            list.AddRange(jobs);
            list.AddRange(services);
            list.AddRange(animals);

            return new JsonResult(list);

        }







    }
}
