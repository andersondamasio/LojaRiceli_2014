using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Pedidox
{
    public class PedidoBean :Pedido
    {
    }

    public class FinalizarPedidoBean
    {
        public Pedido Pedido { get; set; }
  
    }
}