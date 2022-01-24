using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using _2_Library.Dao.Site.EntregaX;
using _2_Library.Dao.Site.ProdutoSkuX;

namespace _2_Library.Dao.Site.Produto_GrupoX
{
    public class Produto_GrupoDto
    {
        public Int32 pro_id { get; set; }
        public Int32 gru_id { get; set; }
        public string pro_nome { get; set; }
        public string pro_nomeAmigavel { get; set; }
        public string mar_nome { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public decimal proSku_precoVendaMaior { get; set; }
        public decimal proSku_precoVendaMenor { get; set; }
        public decimal proSku_percDesconto { get; set; }
        public IEnumerable<ProdutoSkuDto> produtoSkuDto { get; set; }
        
    }

    public class ProdutoSkuDto
    {
        public int proSku_id { get; set; }
        public int pro_id { get; set; }
        public string gru_nome { get; set; }
        public string gru_nomeAmigavel { get; set; }
        public string pro_nomeAmigavel { get; set; }
        public string proSku_nome { get; set; }
        public decimal proSku_precoAnterior { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public decimal proSku_percDesconto { get; set; }
        public int proSkuCor_id { get; set; }
        public string proSkuCor_nome { get; set; }
        public string proSkuCor_imagem { get; set; }
        public string proSkuTam_nome { get; set; }
        public int? proSku_quantidadeDisponivel { get; set; }
        public bool proSku_disponivel { get; set; }
        public int loj_id { get; set; }
        public ProdutoSkuFotoDto produtoSkuFotoDto { get; set; }
        public ParcelamentoDto parcelamentoDto { get; set; }
        public EntregaDto entregaDto { get; set; }
    }

    public class ParcelamentoDto
    {
        public int parc_quantidade { get; set; }
        //public decimal parc_valor { get; set; }
        public decimal? parc_valorMinimo { get; set; }
        public DateTime? parc_periodoDe { get; set; }
        public DateTime? parc_periodoAte { get; set; }
        public Boolean parc_ativarJuro { get; set; }
        public Boolean parc_bloquear { get; set; }
        public IEnumerable<ParcelamentoParcelaDto> parcelamentoParcelaDto { get; set; }
    }

    public class ParcelamentoParcelaDto
    {
        public ParcelamentoParcelaDto()
        {
            parcPar_bloquear = true;
        }
        public decimal parcPar_valor { get; set; }
        public int parcPar_quantidade { get; set; }
        public decimal? parcPar_percentualJuro { get ; set; }
        public Boolean parcPar_bloquear { get; set; }
    }
}
