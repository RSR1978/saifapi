using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class CarAdvertisment : AdvertismentBase
    {
        public int Year { get; set; }
        public int Type { get; set; }

        [ForeignKey("Type")]
        //public int CategoryId { get; set; }
        public virtual categories TCategory { get; set; }


        //[ForeignKey("CategoryId")]
        //public virtual categories Category2 { get; set; }
        //CategoryId

        public int Cat { get; set; }
        //public string SCatDesc { get; set; }

        [ForeignKey("Cat")]
        public virtual categories CCategory { get; set; }

        [StringLength(50)]
        public string Color { get; set; }
        public string Cbody { get; set; }
        public string Case { get; set; }
        public double price { get; set; }
        public string Kilo { get; set; }
        public string pic { get; set; }
        [StringLength(1000)]
        public string describe { get; set; }
        [StringLength(1000)]
        public string note1 { get; set; }
        public int GoverId { get; set; }

        [ForeignKey("GoverId")]
        public virtual Gover Gover { get; set; }

        public string Subj { get; set; }

        public string price_curr { get; set; }

        public string Model { get; set; }

        public string Source { get; set; }
        public string Geartyp { get; set; }
        public string Cylinder { get; set; }
        public string Engsize { get; set; }
        public string EngHorse { get; set; }
        public string Turbo { get; set; }
        public string Guaranty { get; set; }
        public string PushSys { get; set; }
        public string Window { get; set; }
        public string Petrol { get; set; }

        public string Upsys { get; set; }

        public string Carroof { get; set; }
        public string Seattyp { get; set; }
        public string ElecMirror { get; set; }
        public string Reflection { get; set; }
        public string Seats { get; set; }
        public string Electricseat { get; set; }
        public string Airbag { get; set; }
        public string Screen { get; set; }
        public string Refrig { get; set; }
        public string Soundsys { get; set; }
        public string Autopark { get; set; }
        public string Cruisecontrol { get; set; }
        public string Doors { get; set; }
        public string Revcamera { get; set; }

        public string Tiresize { get; set; }
        public string Lamps { get; set; }
        public string Sensors { get; set; }
        public string NonDeviation { get; set; }
        public string Plate { get; set; }
        public string Platecity { get; set; }
        public string Mortgage { get; set; }
        public string Clss { get; set; }
        public string Spec { get; set; }
        public string pay { get; set; }
        public string CountTyp { get; set; }

    }
}
