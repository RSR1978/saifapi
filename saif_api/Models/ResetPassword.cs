using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using saif_api.Authentication;

namespace saif_api.Models
{
    public class ResetPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(5000)]
        public string Token { get; set; }

        [StringLength(10)]
        public string OTP { get; set; }

        public DateTime InsertDateTimeUTC { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public ApplicationUser User { get; set; }
    }
}
