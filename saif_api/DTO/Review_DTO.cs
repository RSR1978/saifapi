using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.DTO
{
    public class Review_DTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
