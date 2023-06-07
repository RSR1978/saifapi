using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class EstateAdvertisment : AdvertismentBase
    {
        public int Typ { get; set; }
        public int for_adv { get; set; }
        public int Catadv { get; set; }

        [ForeignKey("Catadv")]
        public virtual categories Category { get; set; }

        public string Path { get; set; }
        public string Room { get; set; }
        public string loc { get; set; }
        public int GoverId { get; set; }

        [ForeignKey("GoverId")]
        public virtual Gover Gover { get; set; }
        [StringLength(50)]
        public string Floor { get; set; }
        public string area { get; set; }
        public string Buildarea { get; set; }
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
        public string Buildage { get; set; }
        public string Direction { get; set; }

    }
}
