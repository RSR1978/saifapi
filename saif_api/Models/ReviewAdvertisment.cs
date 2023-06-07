using Microsoft.AspNetCore.Identity;
using saif_api.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class ReviewAdvertisment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int AdvId { get; set; }

        //[ForeignKey("Id")]
        //public virtual AdvertismentBase AdvertismentBase { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime RevDate { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual categories Category { get; set; }

    }
}
