using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.CorreioX
{
    public class FreteDto
    {
        public int fre_id { get; set; }
        public string fre_nome { get; set; }
        public string fre_regiao { get; set; }
        public string fre_cepOrigem { get; set; }
        public int fre_cepDestinoInicio { get; set; }
        public int fre_cepDestinoFim { get; set; }
        public decimal? fre_valor { get; set; }
        public int? fre_prazo { get; set; }
        public decimal? fre_peso { get; set; }
        public DateTime? fre_dataHoraAtualizacao { get; set; }
    }
}
