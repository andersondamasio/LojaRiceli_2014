using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Site.ProdutoSkuX;

namespace _2_Library.Dao.Site.PedidoX
{
    public class PedidoDto
    {
        public int ped_id { get; set; }
        public int cli_id { get; set; }
        public string ped_cli_email { get; set; }
        public string ped_cliEnd_nome { get; set; }
        public string ped_cliEnd_sobrenome { get; set; }
        public string ped_cliEnd_cep { get; set; }
        public string ped_cliEnd_endereco { get; set; }
        public string ped_cliEnd_numero { get; set; }
        public string ped_cliEnd_complemento { get; set; }
        public string ped_cliEnd_referencia { get; set; }
        public string ped_cliEnd_bairro { get; set; }
        public string ped_cliEnd_cidade { get; set; }
        public string ped_cliEnd_estado { get; set; }
        public string ped_cliEnd_ddd1 { get; set; }
        public string ped_cliEnd_fone1 { get; set; }
        public string ped_cliEnd_ddd2 { get; set; }
        public string ped_cliEnd_fone2 { get; set; }
        public string ped_cliEnd_ddd3 { get; set; }
        public string ped_cliEnd_fone3 { get; set; }
        public bool ped_cliEnd_recebeInformativo { get; set; }
        public string ped_ent_tipo { get; set; }
        public string ped_ent_meio { get; set; }
        public string ped_ent_localizacao { get; set; }
        public int ped_ent_prazo { get; set; }
        public decimal ped_ent_valor { get; set; }
        public string ped_forPag_gateway { get; set; }
        public string ped_forPag_nome { get; set; }
        public DateTime ped_forPag_prazoPagamento { get; set; }
        public decimal ped_forPag_valorDesconto { get; set; }
        public decimal ped_forPag_percentualDesconto { get; set; }
        public string ped_forPag_situacao { get; set; }
        public string ped_forPagPar_condicao { get; set; }
        public int ped_forPagPar_quantidade { get; set; }
        public decimal ped_forPagPar_valor { get; set; }
        public decimal ped_forPagPar_percentualJuro { get; set; }
        public string ped_forPagCar_nomePortador { get; set; }
        public string ped_forPagCar_nome { get; set; }
        public string ped_forPagCar_numero { get; set; }
        public string ped_forPagCar_mesValidade { get; set; }
        public string ped_forPagCar_anoValidade { get; set; }
        public string ped_forPagCar_codigoSeguranca { get; set; }
        public string ped_excluido { get; set; }
        public string ped_urlOrigem { get; set; }
        public decimal ped_subTotal { get; set; }
        public decimal ped_descontos { get; set; }
        public decimal ped_total { get; set; }
        public string ped_mensagemErro { get; set; }
        public DateTime ped_dataHora { get; set; }
        public int cup_id { get; set; }
        public IEnumerable<PedidoProdutoDto> pedidoProdutoDto { get; set; }
        public IEnumerable<PedidoStatusDto> pedidoStatusDto { get; set; }
        public StatusDto statusDto { get; set; }
    }


    public class PedidoProdutoDto
    {
        public decimal pedPro_car_quantidade { get; set; }
        public string pedPro_gru_nome { get; set; }
        public string pedPro_pro_nome { get; set; }
        public string pedPro_pro_nomeAmigavel { get; set; }
        public int pedPro_proSku_id { get; set; }
        public string pedPro_proSku_idReferencia { get; set; }
        public int? pedPro_proSku_prazoEntregaAdicional { get; set; }
        public string pedPro_proSkuCor_nome { get; set; }
        public string pedPro_proSkuTam_nome { get; set; }
        public decimal pedPro_proSku_peso { get; set; }
        public decimal pedPro_proSku_altura { get; set; }
        public decimal pedPro_proSku_largura { get; set; }
        public decimal pedPro_proSku_comprimento { get; set; }
        public decimal pedPro_proSku_precoCusto { get; set; }
        public decimal pedPro_proSku_precoAnterior { get; set; }
        public decimal pedPro_proSku_precoVenda { get; set; }
        public decimal pedPro_proSku_precoVendaTotal { get; set; }
        public bool proSku_disponivel { get; set; }
        public DateTime pedPro_dataHora { get; set; }
        public IEnumerable<ProdutoSkuFotoDto> produtoSkuFotoDto { get; set; }
        
    }


    public class PedidoStatusDto
    {
        public string pedStat_descricao { get; set; }
        public string stat_idRastreio { get; set; }
        public DateTime pedStat_dataHora { get; set; }
        public StatusDto statusDto { get; set; }
    }


    public class StatusDto {

        public int ped_id { get; set; }
        public string stat_nome { get; set; }
        public string stat_descricao { get; set; }
        public bool stat_ativar { get; set; }
        public bool stat_bloquear { get; set; }
        public DateTime stat_dataHora { get; set; }
        
    }
}
