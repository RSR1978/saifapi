using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using saif_api.Authentication;
using saif_api.Models;

namespace saif_api.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    base.OnModelCreating(modelbuilder);

        //    modelbuilder.Entity<CarAdvertisment>()
        //        .HasOne(x => x.TCategory)
        //        .WithOne()
        //        .OnDelete(DeleteBehavior.Restrict);
            
        //    modelbuilder.Entity<CarAdvertisment>()
        //       .HasOne(x => x.CCategory)
        //       .WithOne()
        //       .OnDelete(DeleteBehavior.Restrict);
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<categories> Categories { get; set; }
        public virtual DbSet<CarAdvertisment> CarAdvertisments { get; set; }
        public virtual DbSet<AccessoriesAdvertisment> AccessoriesAdvertisments { get; set; }
        public virtual DbSet<AnimalAdvertisment> AnimalAdvertisments { get; set; }
        public virtual DbSet<EstateAdvertisment> EstateAdvertisments { get; set; }
        public virtual DbSet<JobAdvertisment> JobAdvertisments { get; set; }
        public virtual DbSet<ServiceAdvertisment> ServiceAdvertisments { get; set; }
        public virtual DbSet<Gover> Govers { get; set; }
        public virtual DbSet<IndustrialAdvertisment> IndustrialAdvertisments { get; set; }
        public virtual DbSet<ProductAdvertisment> ProductAdvertisments { get; set; }
        public virtual DbSet<Gender_Info> Gender_Infos { get; set; }
        //public virtual DbSet<ServiceAdvertisment> Qulification_info { get; set; }
        public DbSet<Qulification_info> Qulification_info { get; set; }
        public virtual DbSet<ReviewAdvertisment> ReviewAdvertisments { get; set; }

        public DbSet<ResetPassword> ResetPasswords { get; set; }
    }
    
}
