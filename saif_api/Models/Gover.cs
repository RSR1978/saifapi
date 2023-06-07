using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class Gover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoverId { get; set; }
        public string GoverADesc { get; set; }
        public string GoverAdesc { get; set; }
        public int GoverAct { get; set; }
    }
}
