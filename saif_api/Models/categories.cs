using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public String SCatDesc { get; set; }
        public String SCatAdesc { get; set; }
        public int SCatParent { get; set; }
        public int SCatActive { get; set; }
        public String SCatDet { get; set; }
        public String SCatAdet { get; set; }
        public int STerminal { get; set; }

     

    }
}
