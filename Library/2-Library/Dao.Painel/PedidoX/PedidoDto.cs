using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Painel.PedidoX
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
        public string ped_ent_rastreio { get; set; }
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
    }
}
