using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.EntregaX
{
    public class EntregaDto
    {

        public string ent_descricao { get; set; }
        public int? ent_cepInicial { get; set; }
        public int? ent_cepFinal { get; set; }
        public DateTime? ent_dataHoraInicial { get; set; }
        public DateTime? ent_dataHoraFinal { get; set; }
        public decimal? ent_valor { get; set; }
        public Boolean ent_bloquear { get; set; }
        public Boolean ent_gratis { get; set; }
        public int? ent_prazo { get; set; }
        public int? ent_prazoAdicional { get; set; }
        public int ent_global { get; set; }
        public bool ent_calculaPrazoExterno { get; set; }
        public bool ent_calculaValorExterno { get; set; }


    }
}
