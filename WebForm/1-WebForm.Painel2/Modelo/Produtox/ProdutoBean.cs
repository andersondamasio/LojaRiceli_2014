using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.ProdutoSkux;
using _2_Library.Modelo;

namespace Loja.Modelo.Produtox
{

    public class Produto_GrupoBean : Produto_Grupo
    {
    }

    public class ProdutoBean : Produto
    {
        public string gru_nome { get; set; }
        public string gru_nomeAmigavel { get; set; }
        public string mar_nome { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public IEnumerable<ProdutoSkuBean> ProdutoSkuBean { get; set; }
        public GrupoBean GrupoBean { get; set; } 
    }

    public class GrupoBean : Grupo
    {
    }



}