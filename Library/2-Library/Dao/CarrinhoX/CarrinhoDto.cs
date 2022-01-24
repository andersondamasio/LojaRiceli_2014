using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.CorreioX;
using _2_Library.Dao.CupomX;
using _2_Library.Dao.ProdutoSkuX;

namespace _2_Library.Dao.CarrinhoX
{
    public class CarrinhoDto
    {
        public string car_sessionId { get; set; }
        public int car_quantidade { get; set; }
        public int? cli_id { get; set; }
        public int proSku_id { get; set; }
        public int pro_id { get; set; }
        public string pro_nome { get; set; }
        public string pro_nomeAmigavel { get; set; }
        public string proSku_nome { get; set; }
        public decimal proSku_altura { get; set; }
        public decimal proSku_largura { get; set; }
        public decimal proSku_comprimento { get; set; }
        public decimal proSku_peso { get; set; }
        public string proSkuCor_nome { get; set; }
        public string proSkuTam_nome { get; set; }
        public int? proSku_prazoEntregaAdicional { get; set; }
        public decimal proSku_precoCusto { get; set; }
        public decimal proSku_precoAnterior { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public decimal car_itemSubTotal { get; set; }
        public decimal proSku_percDesconto { get; set; }
        public int? proSku_quantidadeDisponivel { get; set; }
        public bool proSku_disponivel { get; set; }
        public int loj_id { get; set; }
        public ProdutoSkuFotoDto produtoSkuFotoDto { get; set; }
        public EntregaDto entregaDto { get; set; }
        public ParcelamentoDto parcelamentoDto { get; set; }
    }

    public class CarrinhoTotaisDto
    {

        public decimal cart_subTotal { get; set; }
        public decimal? cart_entregaTotal { get; set; }
        public int? cart_entregaPrazoTotal { get; set; }
        public string cart_condicao { get; set; }
        public decimal cart_cupomTotal { get; set; }
        public decimal cart_total { get; set; }
        //public ParcelamentoDto parcelamentoDto { get; set; }
        public CorreioDto correioDto { get; set; }
        public List<CarrinhoDto> carrinhoDto { get; set; }
        public CupomDto cupomDto { get; set; }
    }

    public class CarrinhoMedidas
    {
        public decimal carm_totalComprimento { get; set; }
        public decimal carm_totalLargura { get; set; }
        public decimal carm_totalAltura { get; set; }
        public decimal carm_totalPeso { get; set; }
        public decimal carm_totalPesoCubico { get; set; }
        public decimal carm_totalDiametro { get; set; }
        public List<string> carm_msgErros { get; set; }
        //public decimal carm_totalEntrega { get; set; }
        //public IEnumerable<EntregaDto> entregaDto { get; set; }
    }

}
