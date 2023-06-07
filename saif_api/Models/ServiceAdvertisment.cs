using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class ServiceAdvertisment : AdvertismentBase
    {
        public int Typ { get; set; }

        public int Catadv { get; set; }

        [ForeignKey("Catadv")]
        public virtual categories Category { get; set; }
        public string Subj { get; set; }
        public string Desc { get; set; }
        [StringLength(50)]
        public string loc { get; set; }
        public double price { get; set; }
        public int GoverId { get; set; }

        [ForeignKey("GoverId")]
        public virtual Gover Gover { get; set; }
        public string pic { get; set; }

        public string price_curr { get; set; }
    }
}
