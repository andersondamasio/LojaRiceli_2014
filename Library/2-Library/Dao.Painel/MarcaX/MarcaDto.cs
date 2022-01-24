using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Painel.MarcaX
{
    public class MarcaDto
    {
        public Int32 mar_id { get; set; }
        public String mar_nome { get; set; }
        public String mar_descricao { get; set; }
        public Boolean mar_bloquear { get; set; }
        public int? mar_posicao { get; set; }
        public int? mar_excluidoX { get; set; }
        public DateTime? mar_dataHoraAtualizacao { get; set; }
        public DateTime mar_dataHora { get; set; }
        public Int32 loj_id { get; set; }
    }
}
