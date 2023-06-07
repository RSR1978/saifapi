using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class IndustrialAdvertisment : AdvertismentBase
    {
        public int Typ { get; set; }
        public int for_adv { get; set; }
        public int Catadv { get; set; }

        [ForeignKey("Catadv")]
        public virtual categories Category { get; set; }

        public string loc { get; set; }
        public int GoverId { get; set; }

        [ForeignKey("GoverId")]
        public virtual Gover Gover { get; set; }
        public string pic { get; set; }
        [StringLength(1000)]
        public string describe { get; set; }
        [StringLength(1000)]
        public string note1 { get; set; }
        [StringLength(1000)]
        public string note2 { get; set; }
        public double price { get; set; }
        public string Subj { get; set; }
        public string price_curr { get; set; }
        public string Prdcase { get; set; }
        public string Brand { get; set; }
        public string Producttyp { get; set; }
        public string Size { get; set; }
        public string Warranty { get; set; }
        public string Storage { get; set; }
        public string Source { get; set; }
    }
}
