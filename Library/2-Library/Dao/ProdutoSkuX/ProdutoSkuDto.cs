using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.ProdutoSkuX
{

    public class ProdutoSkuDto
    {
        public int proSku_id { get; set; }
        public int pro_id { get; set; }
        public string proSku_nome { get; set; }
        public decimal proSku_precoAnterior { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public decimal proSku_percDesconto { get; set; }
        public int? proSkuCor_id { get; set; }
        public string proSkuCor_nome { get; set; }
        public string proSkuCor_imagem { get; set; }
        public int? proSkuTam_id { get; set; }
        public string proSkuTam_nome { get; set; }
        public string proSkuFot_nome { get; set; }
        public int? proSku_quantidadeDisponivel { get; set; }
        public bool proSku_disponivel { get; set; }
        public int loj_id { get; set; }
        public ParcelamentoDto parcelamentoDto { get; set; }
        public EntregaDto entregaDto { get; set; }
        public ProdutoSkuFotoDto produtoSkuFotoUDto { get; set; }
        public IEnumerable<ProdutoSkuFotoDto> produtoSkuFotoDto { get; set; }
        public IEnumerable<ProdutoSkuCoresTamanhosDto> produtoSkuCoresTamanhos1Dto { get; set; }
        public IEnumerable<ProdutoSkuCoresTamanhosDto> produtoSkuCoresTamanhos2Dto { get; set; }
    }

    public class ProdutoSkuCorDto
    {
        public string proSkuCor_nome { get; set; }
        public string proSkuCor_imagem { get; set; }
    }

    public class ProdutoSkuTamanhoDto
    {
        public string proSkuTam_nome { get; set; }
        public string proSkuTam_imagem { get; set; }
    }

    public class ProdutoSkuCoresTamanhosDto
    {
        public ProdutoSkuDto produtoSkuDto { get; set; }
    }


    public class ProdutoSkuFotoDto
    {
        public string proSkuFot_nome { get; set; }
        public string proSkuFot_extensao { get; set; }
        public int pro_id { get; set; }
        public int loj_id { get; set; }
    }

}



