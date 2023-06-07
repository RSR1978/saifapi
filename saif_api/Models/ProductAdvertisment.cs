using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class ProductAdvertisment : AdvertismentBase
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
        public string prd_case { get; set; }
        public string gen_desc { get; set; }
        public string brand { get; set; }
        public string producttyp { get; set; }
        public string size { get; set; }
        public string warranty { get; set; }
        public string storage { get; set; }
        public string operatsys { get; set; }
        public string ram { get; set; }
        public string screensize { get; set; }
        public string screencard { get; set; }
        public string generation { get; set; }
        public string buyer { get; set; }
        public string prd_t1 { get; set; }
    }
}
