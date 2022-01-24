using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.GrupoX
{
    public class GrupoDto
    {
        public int gru_id { get; set; }
        public int gru_pai { get; set; }
        public string gru_nome { get; set; }
        public string gru_nomeAmigavel { get; set; }
        public string gru_descricao { get; set; }
        public int? gru_posicao { get; set; }
    }

}
