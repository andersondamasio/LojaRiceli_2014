using System;
using System.Collections.Generic;
using _2_Library.Dao.Painel.ProdutoSkuX;
using _2_Library.Dao.Painel.ProdutoX;

namespace _2_Library.Dao.Painel.Produto_GrupoX
{
    public class Produto_GrupoDto
    {
        public Int32 pro_id { get; set; }
        public Int32 gru_id { get; set; }
        public Int32 loj_id { get; set; }
        public DateTime proGru_dataHora { get; set; }
        public ProdutoDto produtoDto { get; set; }
    }
}
