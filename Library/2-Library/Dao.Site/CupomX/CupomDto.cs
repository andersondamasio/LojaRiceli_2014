using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.CupomX
{
    public class CupomDto
    {
        public int cup_id { get; set; }
        public string cup_chave { get; set; }
        public decimal cup_valor { get; set; }
        public DateTime cup_validade { get; set; }
        public int cup_quantidade { get; set; }
        public int cup_numPedidos { get; set; }
        public int? cli_id { get; set; }
        public string cup_msgErro { get; set; }
    }
}
