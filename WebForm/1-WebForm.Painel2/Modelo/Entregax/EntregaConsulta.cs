using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Entregax
{
    public class EntregaConsulta
    {
        public Boolean SelecionarEntregaExiste(int cepInicial, int cepFinal, decimal? prazo, decimal? valor, int excetoEnt_id)
        {
            return new EntregaDao().SelecionarEntregaExiste(cepInicial, cepFinal,prazo, valor, excetoEnt_id);
        }

        public _2_Library.Modelo.Entrega SelecionarEntrega(int cep_inicial)
        {
            return new EntregaDao().SelecionarEntrega(cep_inicial);
        }

        public List<_2_Library.Modelo.Entrega> SelecionarEntrega()
        {
            return new EntregaDao().SelecionarEntrega();
        }
    }
}