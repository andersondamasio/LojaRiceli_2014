using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Pedidox
{
    public class PedidoConsulta
    {
        public Retorno InserirClientePedido(Cliente cliente)
        {
            return new PedidoDao().InserirClientePedido(cliente);
        }
        public Retorno InserirPedido(Pedido pedido)
        {
            return new PedidoDao().InserirPedido(pedido);
        }

        public Pedido SelecionarPedido(int ped_id)
        {
            return new PedidoDao().SelecionarPedido(ped_id);
        }
    }
}