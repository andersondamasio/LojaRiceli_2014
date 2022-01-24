using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace Loja.Modelo.Statusx
{
    public class StatusDao
    {
        private Int32 loj_id = new LojaTd().SelectLoja(null).loj_id;
        public Boolean SelecionarStatusExistePedido(int stat_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Boolean existe = (from stat in lojaEntities.Status
                              where
                              (stat.stat_id == stat_id &&
                              stat.Pedido.Count() > 0) &&
                              stat.loj_id == loj_id
                              select stat).Count() > 0;
            return existe;
        }

        public void AtualizarConfiguracaoStatuAtivar(int stat_id)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                var status = (from stat in lojaEntities.Status
                            where stat.stat_id != stat_id && stat.loj_id == loj_id
                            select stat);

                foreach (Status stat in status)
                    stat.stat_ativar = false;

                lojaEntities.SaveChanges();
            }
        }
    }
}