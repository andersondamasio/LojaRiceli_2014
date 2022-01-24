using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Painel.Pedidox
{
    public class PedidoDao
    {

        public void AtualizarPedidoStatus(int ped_id, int stat_id)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Pedido pedido = (from ped in lojaEntities.Pedido
                                 where ped.ped_id == ped_id
                                 select ped).FirstOrDefault();

                pedido.stat_id = stat_id;
                lojaEntities.SaveChanges();
            }

        }
    }
}