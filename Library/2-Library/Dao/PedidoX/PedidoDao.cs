using _2_Library.Config;
using _2_Library.Modelo;
using System.Linq;
using System.Collections.Generic;

namespace _2_Library.Dao.PedidoX
{
    internal class PedidoDao : Repositorio<Pedido>
    {

        public List<PedidoDto> SelectPedido(int loj_id, int? cli_id, string cli_email)
        {
            IQueryable<Pedido> pedido = null;

            if (cli_id.HasValue)
                pedido = (from ped in Select()
                          where ped.cli_id == cli_id.Value &&
                          ped.loj_id == loj_id &&
                          !ped.ped_excluido.HasValue
                          select ped);
            else
                if (string.IsNullOrEmpty(cli_email))
                    pedido = (from ped in Select()
                              where ped.ped_cli_email == cli_email &&
                              ped.loj_id == loj_id &&
                              !ped.ped_excluido.HasValue
                              select ped);


            List<PedidoDto> pedidoDto = (from ped in pedido
                                         select new PedidoDto
                                         {
                                             ped_id = ped.ped_id,
                                             cli_id = ped.cli_id,
                                             ped_cli_email = ped.ped_cli_email,
                                             ped_cliEnd_nome = ped.ped_cliEnd_nome,
                                             ped_cliEnd_sobrenome = ped.ped_cliEnd_sobrenome,
                                             ped_cliEnd_cep = ped.ped_cliEnd_cep,
                                             ped_cliEnd_endereco = ped.ped_cliEnd_endereco,
                                             ped_cliEnd_numero = ped.ped_cliEnd_numero,
                                             ped_cliEnd_complemento = ped.ped_cliEnd_complemento,
                                             ped_cliEnd_referencia = ped.ped_cliEnd_referencia,
                                             ped_cliEnd_bairro = ped.ped_cliEnd_bairro,
                                             ped_cliEnd_cidade = ped.ped_cliEnd_cidade,
                                             ped_cliEnd_estado = ped.ped_cliEnd_estado,
                                             ped_cliEnd_ddd1 = ped.ped_cliEnd_ddd1,
                                             ped_cliEnd_fone1 = ped.ped_cliEnd_fone1,
                                             ped_cliEnd_ddd2 = ped.ped_cliEnd_ddd2,
                                             ped_cliEnd_fone2 = ped.ped_cliEnd_fone2,
                                             ped_cliEnd_ddd3 = ped.ped_cliEnd_ddd3,
                                             ped_cliEnd_fone3 = ped.ped_cliEnd_fone3,
                                             ped_cliEnd_recebeInformativo = ped.ped_cliEnd_recebeInformativo,
                                             ped_ent_tipo = ped.ped_ent_tipo,
                                             ped_ent_meio = ped.ped_ent_meio,
                                             ped_ent_localizacao = ped.ped_ent_localizacao,
                                             ped_ent_prazo = ped.ped_ent_prazo,
                                             ped_ent_valor = ped.ped_ent_valor,
                                             ped_forPag_nome = ped.ped_forPag_nome,
                                             ped_forPag_prazoPagamento = ped.ped_forPag_prazoPagamento,
                                             ped_forPag_valorDesconto = ped.ped_forPag_valorDesconto,
                                             ped_forPag_percentualDesconto = ped.ped_forPag_percentualDesconto,
                                             ped_forPag_situacao = ped.ped_forPag_situacao,
                                             ped_forPagPar_condicao = ped.ped_forPagPar_condicao,
                                             ped_forPagPar_quantidade = ped.ped_forPagPar_quantidade,
                                             ped_forPagPar_valor = ped.ped_forPagPar_valor,
                                             ped_forPagPar_percentualJuro = ped.ped_forPagPar_percentualJuro,
                                             ped_forPagCar_nomePortador = ped.ped_forPagCar_nomePortador,
                                             ped_forPagCar_nome = ped.ped_forPagCar_nome,
                                             ped_forPagCar_numero = ped.ped_forPagCar_numero,
                                             ped_forPagCar_mesValidade = ped.ped_forPagCar_mesValidade,
                                             ped_forPagCar_anoValidade = ped.ped_forPagCar_anoValidade,
                                             ped_forPagCar_codigoSeguranca = ped.ped_forPagCar_codigoSeguranca,
                                             ped_urlOrigem = ped.ped_urlOrigem,
                                             ped_subTotal = ped.ped_subTotal,
                                             ped_descontos = ped.ped_descontos,
                                             ped_total = ped.ped_total,
                                             ped_dataHora = ped.ped_dataHora,
                                             statusDto = new StatusDto { ped_id = ped.ped_id, stat_nome = ped.Status.stat_nome, stat_descricao = ped.Status.stat_descricao, stat_dataHora = ped.Status.stat_dataHora },
                                             pedidoProdutoDto = from pedPro in ped.PedidoProduto
                                                                select new PedidoProdutoDto
                                                                {
                                                                    pedPro_car_quantidade = pedPro.pedPro_car_quantidade,
                                                                    pedPro_gru_nome = pedPro.pedPro_gru_nome,
                                                                    pedPro_pro_nome = pedPro.pedPro_pro_nome,
                                                                    pedPro_proSku_id = pedPro.pedPro_proSku_id,
                                                                    pedPro_proSku_idReferencia = pedPro.pedPro_proSku_idReferencia,
                                                                    pedPro_proSku_prazoEntregaAdicional = pedPro.pedPro_proSku_prazoEntregaAdicional,
                                                                    pedPro_proSkuCor_nome = pedPro.pedPro_proSkuCor_nome,
                                                                    pedPro_proSkuTam_nome = pedPro.pedPro_proSkuTam_nome,
                                                                    pedPro_proSku_peso = pedPro.pedPro_proSku_peso,
                                                                    pedPro_proSku_altura = pedPro.pedPro_proSku_altura,
                                                                    pedPro_proSku_largura = pedPro.pedPro_proSku_largura,
                                                                    pedPro_proSku_comprimento = pedPro.pedPro_proSku_comprimento,
                                                                    pedPro_proSku_precoCusto = pedPro.pedPro_proSku_precoCusto,
                                                                    pedPro_proSku_precoAnterior = pedPro.pedPro_proSku_precoAnterior,
                                                                    pedPro_proSku_precoVenda = pedPro.pedPro_proSku_precoVenda,
                                                                    pedPro_proSku_precoVendaTotal = pedPro.pedPro_proSku_precoVendaTotal,
                                                                    proSku_disponivel = pedPro.proSku_disponivel,
                                                                    pedPro_dataHora = pedPro.pedPro_dataHora,
                                                                },
                                             pedidoStatusDto = from pedSta in ped.PedidoStatus
                                                               select new PedidoStatusDto
                                                               {
                                                                   pedStat_descricao = pedSta.pedStat_descricao,
                                                                   stat_idRastreio = pedSta.stat_idRastreio,
                                                                   pedStat_dataHora = pedSta.pedStat_dataHora,
                                                                   statusDto = new StatusDto { ped_id = ped.ped_id, stat_nome = pedSta.Status.stat_nome, stat_descricao = pedSta.Status.stat_descricao, stat_dataHora = pedSta.Status.stat_dataHora }
                                                               }
                                         }).OrderByDescending(s => s.ped_id).ToList();

            return pedidoDto;
        }

        public PedidoDto SelectPedido(int loj_id, int ped_id)
        {
            PedidoDto pedidoDto = (from ped in Select()
                                         where ped.ped_id == ped_id &&
                                         ped.loj_id == loj_id &&
                                         !ped.ped_excluido.HasValue
                                         select new PedidoDto
                                         {
                                             ped_id = ped.ped_id,
                                             cli_id = ped.cli_id,
                                             ped_cli_email = ped.ped_cli_email,
                                             ped_cliEnd_nome = ped.ped_cliEnd_nome,
                                             ped_cliEnd_sobrenome = ped.ped_cliEnd_sobrenome,
                                             ped_cliEnd_cep = ped.ped_cliEnd_cep,
                                             ped_cliEnd_endereco = ped.ped_cliEnd_endereco,
                                             ped_cliEnd_numero = ped.ped_cliEnd_numero,
                                             ped_cliEnd_complemento = ped.ped_cliEnd_complemento,
                                             ped_cliEnd_referencia = ped.ped_cliEnd_referencia,
                                             ped_cliEnd_bairro = ped.ped_cliEnd_bairro,
                                             ped_cliEnd_cidade = ped.ped_cliEnd_cidade,
                                             ped_cliEnd_estado = ped.ped_cliEnd_estado,
                                             ped_cliEnd_ddd1 = ped.ped_cliEnd_ddd1,
                                             ped_cliEnd_fone1 = ped.ped_cliEnd_fone1,
                                             ped_cliEnd_ddd2 = ped.ped_cliEnd_ddd2,
                                             ped_cliEnd_fone2 = ped.ped_cliEnd_fone2,
                                             ped_cliEnd_ddd3 = ped.ped_cliEnd_ddd3,
                                             ped_cliEnd_fone3 = ped.ped_cliEnd_fone3,
                                             ped_cliEnd_recebeInformativo = ped.ped_cliEnd_recebeInformativo,
                                             ped_ent_tipo = ped.ped_ent_tipo,
                                             ped_ent_meio = ped.ped_ent_meio,
                                             ped_ent_localizacao = ped.ped_ent_localizacao,
                                             ped_ent_prazo = ped.ped_ent_prazo,
                                             ped_ent_valor = ped.ped_ent_valor,
                                             ped_forPag_nome = ped.ped_forPag_nome,
                                             ped_forPag_prazoPagamento = ped.ped_forPag_prazoPagamento,
                                             ped_forPag_valorDesconto = ped.ped_forPag_valorDesconto,
                                             ped_forPag_percentualDesconto = ped.ped_forPag_percentualDesconto,
                                             ped_forPag_situacao = ped.ped_forPag_situacao,
                                             ped_forPagPar_condicao = ped.ped_forPagPar_condicao,
                                             ped_forPagPar_quantidade = ped.ped_forPagPar_quantidade,
                                             ped_forPagPar_valor = ped.ped_forPagPar_valor,
                                             ped_forPagPar_percentualJuro = ped.ped_forPagPar_percentualJuro,
                                             ped_forPagCar_nomePortador = ped.ped_forPagCar_nomePortador,
                                             ped_forPagCar_nome = ped.ped_forPagCar_nome,
                                             ped_forPagCar_numero = ped.ped_forPagCar_numero,
                                             ped_forPagCar_mesValidade = ped.ped_forPagCar_mesValidade,
                                             ped_forPagCar_anoValidade = ped.ped_forPagCar_anoValidade,
                                             ped_forPagCar_codigoSeguranca = ped.ped_forPagCar_codigoSeguranca,
                                             ped_urlOrigem = ped.ped_urlOrigem,
                                             ped_subTotal = ped.ped_subTotal,
                                             ped_descontos = ped.ped_descontos,
                                             ped_total = ped.ped_total,
                                             ped_dataHora = ped.ped_dataHora,
                                             statusDto = new StatusDto { stat_nome = ped.Status.stat_nome, stat_descricao = ped.Status.stat_descricao, stat_dataHora = ped.Status.stat_dataHora },
                                             pedidoProdutoDto = from pedPro in ped.PedidoProduto
                                                                select new PedidoProdutoDto
                                                                {
                                                                    pedPro_car_quantidade = pedPro.pedPro_car_quantidade,
                                                                    pedPro_gru_nome = pedPro.pedPro_gru_nome,
                                                                    pedPro_pro_nome = pedPro.pedPro_pro_nome,
                                                                    pedPro_proSku_id = pedPro.pedPro_proSku_id,
                                                                    pedPro_proSku_idReferencia = pedPro.pedPro_proSku_idReferencia,
                                                                    pedPro_proSku_prazoEntregaAdicional = pedPro.pedPro_proSku_prazoEntregaAdicional,
                                                                    pedPro_proSkuCor_nome = pedPro.pedPro_proSkuCor_nome,
                                                                    pedPro_proSkuTam_nome = pedPro.pedPro_proSkuTam_nome,
                                                                    pedPro_proSku_peso = pedPro.pedPro_proSku_peso,
                                                                    pedPro_proSku_altura = pedPro.pedPro_proSku_altura,
                                                                    pedPro_proSku_largura = pedPro.pedPro_proSku_largura,
                                                                    pedPro_proSku_comprimento = pedPro.pedPro_proSku_comprimento,
                                                                    pedPro_proSku_precoCusto = pedPro.pedPro_proSku_precoCusto,
                                                                    pedPro_proSku_precoAnterior = pedPro.pedPro_proSku_precoAnterior,
                                                                    pedPro_proSku_precoVenda = pedPro.pedPro_proSku_precoVenda,
                                                                    pedPro_proSku_precoVendaTotal = pedPro.pedPro_proSku_precoVendaTotal,
                                                                    proSku_disponivel = pedPro.proSku_disponivel,
                                                                    pedPro_dataHora = pedPro.pedPro_dataHora
                                                                },
                                             pedidoStatusDto = from pedSta in ped.PedidoStatus
                                                               select new PedidoStatusDto
                                                               {
                                                                   pedStat_descricao = pedSta.pedStat_descricao,
                                                                   stat_idRastreio = pedSta.stat_idRastreio,
                                                                   pedStat_dataHora = pedSta.pedStat_dataHora,
                                                                   statusDto = new StatusDto { stat_nome = pedSta.Status.stat_nome, stat_descricao = pedSta.Status.stat_descricao, stat_dataHora = pedSta.Status.stat_dataHora }
                                                               }
                                         }).FirstOrDefault();

            return pedidoDto;
        }

    }
}
