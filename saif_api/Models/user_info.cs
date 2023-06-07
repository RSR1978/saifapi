using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class user_info
    {
        public int usr_id { get; set; }

        public string usr_name { get; set; }

        public string usr_pass{ get; set; }

        public int usr_active { get; set; }

        public int usr_grp { get; set; }

        public String usr_empn { get; set; }

        public String usr_email { get; set; }
    }
}
