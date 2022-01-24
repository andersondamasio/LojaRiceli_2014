using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Painel.Pedidox
{
    public class PedidoConsulta
    {

        public void AtualizarPedidoStatus(int ped_id, int stat_id)
        {
            new PedidoDao().AtualizarPedidoStatus(ped_id, stat_id);
        }
    }
}