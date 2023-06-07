using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using saif_api.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class AdvertismentBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModiciationDate { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual categories Category { get; set; }

        public string Att { get; set; }

        public string longit { get; set; }

        public string latit { get; set; }
        public byte[] m_img { get; set; }
        public string firstimg { get; set; }
        public string Own { get; set; }

        //[NotMapped]
        //public IFormFile m_img { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
