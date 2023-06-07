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

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class advertiseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        //private readonly IConfiguration _configuration;

        public advertiseController(ApplicationDbContext context, IMapper mapper)//IConfiguration configuration, 
        {
            //_configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        //[Route("api/advertise/{ad_cat1?}/{ad_cat2?}/{ad_cat3?}/{ad_cat4?}")]
        //[HttpGet("{ad_cat1}/{ad_cat2}/{ad_cat3}/{ad_cat4}")]

        //public JsonResult Get(int ad_cat1,int ad_cat2,int ad_cat3,int ad_cat4)

        [Route("api/advertise/{ad_cat1?}")]
        [HttpGet("{ad_cat1}")]

        public JsonResult Get(int ad_cat1)
        {
            var list = new List<SearchResultDTO>();

            //string query="";
            switch (ad_cat1)
            {
                case 1:
                    var cars = _context.CarAdvertisments.AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(cars);
                    break;
                case 2:

                    var estates = _context.EstateAdvertisments.AsNoTracking()
                          .Where(x => x.CategoryId == ad_cat1)
                          .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(estates);
                    //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,note1,price from EstateAdvertisments ";
                    break;

                case 3:
                    //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,describe,note1,price from EstateAdvertisments where CategoryId=@ad_cat2 ";
                    var estates_r = _context.EstateAdvertisments.AsNoTracking()
                  .Where(x => x.CategoryId == ad_cat1)
                  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(estates_r);
                    break;

                case 4:
                    var prod_n = _context.AccessoriesAdvertisments.AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(prod_n);
                    break;

                case 5:
                    var access_s = _context.AccessoriesAdvertisments.AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(access_s);
                    break;

                case 7:
                    var serv = _context.ServiceAdvertisments.AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(serv);
                    break;

                case 8:

                    var jobs = _context.JobAdvertisments.AsNoTracking()
                         .Where(x => x.CategoryId == ad_cat1)
                         .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(jobs);
                    break;

                case 9:
                    var anim = _context.AnimalAdvertisments.AsNoTracking()
                  .Where(x => x.CategoryId == ad_cat1)
                  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(anim);
                    break;
            }

                    //string query = @"select ad_ser,ad_date,ad_price,ad_title,ad_loc,ad_fold_img,ad_space,ad_space_net,ad_room,ad_balcony,ad_age,ad_floor,ad_floor_no,ad_bath_no,ad_from,ad_tel,ad_furnished,ad_heating,ad_prop,ad_desc,ad_loc_at,ad_loc_long from advertise where ad_cat1=@ad_cat1 and ad_cat2=@ad_cat2 and ad_cat3=@ad_cat3 and ad_cat4=@ad_cat4 order by ad_ser";

            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        //myCommand.Parameters.AddWithValue("@ad_cat1", ad_cat1);
            //        //myCommand.Parameters.AddWithValue("@ad_cat2", ad_cat2);
            //        //myCommand.Parameters.AddWithValue("@ad_cat3", ad_cat3);
            //        //myCommand.Parameters.AddWithValue("@ad_cat4", ad_cat4);
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);
            //        myReader.Close();
            //    }
            //}

            return new JsonResult(list);

        }




        [Route("api/advertise/{ad_cat1?}/{ad_cat3?}")]
        [HttpGet("{ad_cat1}/{ad_cat3}")]

        public JsonResult Get(int ad_cat1, int ad_cat3)
        {
            var list = new List<SearchResultDTO>();

            //string query="";
            switch (ad_cat1)
            {
                case 1:
                    var cars = _context.CarAdvertisments.Include(i => i.Gover).AsNoTracking()
                   .Where(x => x.Type == ad_cat3)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();
                    
                    list.AddRange(cars);
                    break;
                case 2:

                    var estates = _context.EstateAdvertisments.Include(i => i.Gover).AsNoTracking()
                          .Where(x => x.CategoryId == ad_cat1 &  x.for_adv == ad_cat3)
                          .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(estates);
                    //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,note1,price from EstateAdvertisments ";
                    break;

                case 3:
                    //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,describe,note1,price from EstateAdvertisments where CategoryId=@ad_cat2 ";
                    var estates_r = _context.EstateAdvertisments.Include(i => i.Gover).AsNoTracking()
                  .Where(x => x.CategoryId == ad_cat1)
                  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(estates_r);
                    break;

                case 4:
                    var prod_n = _context.ProductAdvertisments.Include(i => i.Gover).AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1 & x.for_adv == ad_cat3)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(prod_n);
                    break;

                case 5:
                    var access_s = _context.AccessoriesAdvertisments.Include(i => i.Gover).AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(access_s);
                    break;


                case 6:
                    var indust_s = _context.IndustrialAdvertisments.Include(i => i.Gover).AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1 & x.for_adv == ad_cat3)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(indust_s);
                    break;

                case 7:
                    var serv = _context.ServiceAdvertisments.Include(i => i.Gover).AsNoTracking()
                   .Where(x => x.CategoryId == ad_cat1)
                   .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(serv);
                    break;

                case 8:

                    var jobs = _context.JobAdvertisments.Include(i => i.Gover).AsNoTracking()
                         .Where(x => x.Catadv == ad_cat3)
                         .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(jobs);
                    break;

                case 9:
                    var anim = _context.AnimalAdvertisments.Include(i => i.Gover).AsNoTracking()
                  .Where(x => x.CategoryId == ad_cat1 & x.Catadv == ad_cat3)
                  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

                    list.AddRange(anim);
                    break;
            }

            //string query = @"select ad_ser,ad_date,ad_price,ad_title,ad_loc,ad_fold_img,ad_space,ad_space_net,ad_room,ad_balcony,ad_age,ad_floor,ad_floor_no,ad_bath_no,ad_from,ad_tel,ad_furnished,ad_heating,ad_prop,ad_desc,ad_loc_at,ad_loc_long from advertise where ad_cat1=@ad_cat1 and ad_cat2=@ad_cat2 and ad_cat3=@ad_cat3 and ad_cat4=@ad_cat4 order by ad_ser";

            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        //myCommand.Parameters.AddWithValue("@ad_cat1", ad_cat1);
            //        //myCommand.Parameters.AddWithValue("@ad_cat2", ad_cat2);
            //        //myCommand.Parameters.AddWithValue("@ad_cat3", ad_cat3);
            //        //myCommand.Parameters.AddWithValue("@ad_cat4", ad_cat4);
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);
            //        myReader.Close();
            //    }
            //}

            return new JsonResult(list);

        }


        //[Route("api/advertiseall/{ad_cat1?}")]
        //[HttpGet("{ad_cat1}")]

        //public JsonResult Getall(int ad_cat1)
        //{
        //    var list = new List<SearchResultDTO>();

        //    //string query="";
        //    switch (ad_cat1)
        //    {
        //        case 1:
        //            var cars = _context.CarAdvertisments.AsNoTracking()
        //           .Where(x => x.CategoryId == ad_cat1)
        //           .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(cars);
        //            break;
        //        case 2:

        //            var estates = _context.EstateAdvertisments.AsNoTracking()
        //                  .Where(x => x.CategoryId == ad_cat1)
        //                  .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(estates);
        //            //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,note1,price from EstateAdvertisments ";
        //            break;

        //        case 3:
        //            //query = @"select Id,CreateDate,UserId,CategoryId,Typ,for_adv,Catadv,Path,Room,loc,GoverId,Floor,area,Buildarea,describe,note1,price from EstateAdvertisments where CategoryId=@ad_cat2 ";
        //            var estates_r = _context.EstateAdvertisments.AsNoTracking()
        //          .Where(x => x.CategoryId == ad_cat1)
        //          .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(estates_r);
        //            break;

        //        case 4:
        //            var prod_n = _context.AccessoriesAdvertisments.AsNoTracking()
        //           .Where(x => x.CategoryId == ad_cat1)
        //           .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(prod_n);
        //            break;

        //        case 5:
        //            var access_s = _context.AccessoriesAdvertisments.AsNoTracking()
        //           .Where(x => x.CategoryId == ad_cat1)
        //           .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(access_s);
        //            break;

        //        case 7:
        //            var serv = _context.ServiceAdvertisments.AsNoTracking()
        //           .Where(x => x.CategoryId == ad_cat1)
        //           .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(serv);
        //            break;

        //        case 8:

        //            var jobs = _context.JobAdvertisments.AsNoTracking()
        //                 .Where(x => x.CategoryId == ad_cat1)
        //                 .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(jobs);
        //            break;

        //        case 9:
        //            var anim = _context.AnimalAdvertisments.AsNoTracking()
        //          .Where(x => x.CategoryId == ad_cat1)
        //          .Select(x => _mapper.Map<SearchResultDTO>(x)).ToList();

        //            list.AddRange(anim);
        //            break;
        //    }

        //    //string query = @"select ad_ser,ad_date,ad_price,ad_title,ad_loc,ad_fold_img,ad_space,ad_space_net,ad_room,ad_balcony,ad_age,ad_floor,ad_floor_no,ad_bath_no,ad_from,ad_tel,ad_furnished,ad_heating,ad_prop,ad_desc,ad_loc_at,ad_loc_long from advertise where ad_cat1=@ad_cat1 and ad_cat2=@ad_cat2 and ad_cat3=@ad_cat3 and ad_cat4=@ad_cat4 order by ad_ser";

        //    //DataTable table = new DataTable();
        //    //string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
        //    //SqlDataReader myReader;
        //    //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    //{
        //    //    myCon.Open();
        //    //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //    //    {
        //    //        //myCommand.Parameters.AddWithValue("@ad_cat1", ad_cat1);
        //    //        //myCommand.Parameters.AddWithValue("@ad_cat2", ad_cat2);
        //    //        //myCommand.Parameters.AddWithValue("@ad_cat3", ad_cat3);
        //    //        //myCommand.Parameters.AddWithValue("@ad_cat4", ad_cat4);
        //    //        myReader = myCommand.ExecuteReader();
        //    //        table.Load(myReader);
        //    //        myReader.Close();
        //    //    }
        //    //}

        //    return new JsonResult(list);

        //}

    }
}
