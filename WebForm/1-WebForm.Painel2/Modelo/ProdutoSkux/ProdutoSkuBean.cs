using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Parcelamentox;
using _2_Library.Modelo;

namespace Loja.Modelo.ProdutoSkux
{

    public class ProdutoSkuBean : ProdutoSku
    {
        public ProdutoSkuFotoBean ProdutoSkuFotoBean { get; set; }
        public IEnumerable<ProdutoSkux.ProdutoSkuFotoBean> ProdutoSkuFotoListBean { get; set; }
        public string proSkuCor_nome { get; set; }
        public string proSkuCor_imagem { get; set; }
        public string proSkuTam_nome { get; set; }
        public ParcelamentoBean ParcelamentoBean { get; set; }
        public Produtox.ProdutoBean ProdutoBean { get; set; }

        
    }

    public class ProdutoSkuFotoBean : ProdutoSkuFoto
    {
        public int pro_id { get; set; }
    }

    public class ProdutoSkuCorBean : ProdutoSkuCor
    {
    }
    public class ProdutoSkuTamanhoBean : ProdutoSkuTamanho
    {
    }
}