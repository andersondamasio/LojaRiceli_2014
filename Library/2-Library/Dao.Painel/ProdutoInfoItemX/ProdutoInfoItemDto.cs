using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Painel.ProdutoInfoX;

namespace _2_Library.Dao.Painel.ProdutoInfoItemX
{
    public class ProdutoInfoItemDto
    {
        public Int32 proInfoItem_id { get; set; }
        public String proInfoItem_descricao { get; set; }
        public String proInfoItem_valor { get; set; }
        public Nullable<Int32> proInfoItem_posicao { get; set; }
        public Boolean proInfoItem_bloquear { get; set; }
        public Nullable<Int32> proInfoItem_excluidoX { get; set; }
        public Nullable<DateTime> proInfoItem_dataHoraAtualizacao { get; set; }
        public DateTime proInfoItem_dataHora { get; set; }
        public Int32 proInfo_id { get; set; }
        public Int32 loj_id { get; set; }
        public ProdutoInfoDto ProdutoInfo { get; set; }
    }
}
