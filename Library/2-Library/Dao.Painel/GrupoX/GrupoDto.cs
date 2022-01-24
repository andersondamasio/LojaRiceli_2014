using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Painel.GrupoX
{
    public class GrupoDto
    {
        public Int32 gru_id { get; set; }
        public int? gru_pai { get; set; }
        public String gru_nome { get; set; }
        public String gru_nomeAmigavel { get; set; }
        public String gru_descricao { get; set; }
        public int? gru_posicao { get; set; }
        public Boolean gru_bloquear { get; set; }
        public Boolean gru_subBloquear { get; set; }
        public DateTime? gru_dataHoraAtualizacao { get; set; }
        public int? gru_excluidoX { get; set; }
        public DateTime gru_dataHora { get; set; }
        public Int32 loj_id { get; set; }
        public IEnumerable<GrupoDto> grupo1Dto { get; set; }
        public GrupoDto grupo2Dto { get; set; }
    }
}
