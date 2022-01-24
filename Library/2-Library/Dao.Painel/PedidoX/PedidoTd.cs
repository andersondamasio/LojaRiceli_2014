using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.PedidoX
{
    public class PedidoTd
    {
        /// <summary>
        /// Atualiza os valores passados do pedido
        /// </summary>
        /// <param name="pedidoDto"></param>
        /// <returns>ped_id</returns>
        public int UpdatePedido(PedidoDto pedidoDto)
        {
            using (PedidoDao pedidoDao = new PedidoDao())
            {
                Pedido pedido = (from ped in pedidoDao.Select()
                                 where ped.ped_id == pedidoDto.ped_id
                                 select ped).FirstOrDefault();
                    if (pedidoDto.ped_forPag_gateway != null)
                        pedido.ped_forPag_gateway = pedidoDto.ped_forPag_gateway;
                    if (pedidoDto.ped_forPag_nome != null)
                        pedido.ped_forPag_nome = pedidoDto.ped_forPag_nome;
                    if (pedidoDto.ped_forPag_situacao != null)
                        pedido.ped_forPag_situacao = pedidoDto.ped_forPag_situacao;
                    if (pedidoDto.ped_forPagPar_quantidade > 0)
                        pedido.ped_forPagPar_quantidade = pedidoDto.ped_forPagPar_quantidade;
                    if (pedidoDto.ped_forPagPar_valor != 0)
                        pedido.ped_forPagPar_valor = pedidoDto.ped_forPagPar_valor;
                    if (pedidoDto.ped_forPagPar_condicao != null)
                        pedido.ped_forPagPar_condicao = pedidoDto.ped_forPagPar_condicao;
                    if (pedidoDto.ped_ent_rastreio != null)
                        pedido.ped_ent_rastreio = pedidoDto.ped_ent_rastreio == string.Empty ? null : pedidoDto.ped_ent_rastreio;

                pedidoDao.Update(pedido);
                return pedido.ped_id;
            }
        }
    }
}
