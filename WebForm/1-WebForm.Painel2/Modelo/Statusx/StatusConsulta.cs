using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Statusx
{
    public class StatusConsulta
    {
        public Boolean SelecionarStatusExistePedido(int stat_id)
        {
            return new StatusDao().SelecionarStatusExistePedido(stat_id);
        }

        public void AtualizarConfiguracaoStatuAtivar(int stat_id)
        {
            new StatusDao().AtualizarConfiguracaoStatuAtivar(stat_id);
        }
    }
}