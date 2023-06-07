using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.DTO
{
    public class SearchResultDTO
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int Typ { get; set; }
        public int Catadv { get; set; }
        public string describe { get; set; }
        public string descTxt { get; set; }
        public double Price { get; set; }
        public string price_curr { get; set; }
        public int GoverId { get; set; }
        public string GoverName { get; set; }
        public string Controller { get; set; }
        public string Subj { get; set; }
        public string Att { get; set; }
        public byte[] m_img { get; set; }

    }
}
