using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class tasks
    {
        public int tsk_id { get; set; }

        public DateTime  tsk_date { get; set; }

        public string tsk_note { get; set; }

        public int tsk_usr { get; set; }
        
        public int tsk_to { get; set; }
        
        public int tsk_to_dept { get; set; }

        public int tsk_active { get; set; }

        public DateTime tsk_comp_dt { get; set; }

        public int tsk_usr_comp { get; set; }

        public int tsk_oper_ser { get; set; }

    }
}
