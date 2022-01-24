using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Painel.ProdutoInfoItemX;

namespace _2_Library.Dao.Painel.ProdutoInfoX
{
    public class ProdutoInfoDto
    {
        public Int32 proInfo_id { get; set; }
        public String proInfo_nome { get; set; }
        public Boolean proInfo_bloquear { get; set; }
        public int? proInfo_excluidoX { get; set; }
        public DateTime? proInfo_dataHoraAtualizacao { get; set; }
        public DateTime proInfo_dataHora { get; set; }
        public Int32 pro_id { get; set; }
        public Int32 loj_id { get; set; }
        public List<ProdutoInfoItemDto> ProdutoInfoItem { get; set; }
    }
}
