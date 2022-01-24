using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.ProdutoX
{
    public class ProdutoDto 
    {
        public string pro_descricao { get; set; }
        public IEnumerable<ProdutoInfoDto> produtoInfoDto { get; set; }
    }

    public class ProdutoInfoDto {
        public string proInfo_nome { get; set; }
        public IEnumerable<ProdutoInfoItemDto> produtoInfoItemDto { get; set; }
    }

    public class ProdutoInfoItemDto
    {
        public string proInfoItem_descricao { get; set; }
        public string proInfoItem_valor { get; set; }
    }

}
