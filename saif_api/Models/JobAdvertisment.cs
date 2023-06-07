using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class JobAdvertisment : AdvertismentBase
    {
        public int Typ { get; set; }
        public string Subj { get; set; }
        public string Desc { get; set; }
        public string JobTyp { get; set; }
        public int Catadv { get; set; }

        [ForeignKey("Catadv")]
        public virtual categories Category { get; set; }
        public int gender { get; set; }
        [ForeignKey("gender")]
        public virtual Gender_Info Gender_Info { get; set; }
        public int exp_yy { get; set; }
        public int qulif { get; set; }
        [ForeignKey("qulif")]
        public virtual Qulification_info Qulification_info { get; set; }

        [StringLength(50)]
        public string loc { get; set; }
        public double Sal { get; set; }
        public int health { get; set; }
        public int Social { get; set; }
        public int Commis { get; set; }
        public int Other { get; set; }
        public int GoverId { get; set; }

        [ForeignKey("GoverId")]
        public virtual Gover Gover { get; set; }
        public string pic { get; set; }

        public string sal_curr { get; set; }
        public string WorkHour { get; set; }
    }
}
