using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.GatPagSeguroX
{
    public class GatPagSeguroDto
    {
        public string gatps_email { get; set; }
        public string gatps_token { get; set; }
        public int gatps_parcelasSemJuros { get; set; }
        public decimal gatps_percentualJuro { get; set; }
        public int loj_id { get; set; }

    }
}
