using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Entregax
{
    public class EntregaConsulta
    {
        public Boolean SelecionarEntregaExiste(int cepInicial, int cepFinal, int excetoEnt_id)
        {
            return new EntregaDao().SelecionarEntregaExiste(cepInicial, cepFinal, excetoEnt_id);
        }

        public _2_Library.Modelo.Entrega SelecionarEntrega(int cep_inicial)
        {
            return new EntregaDao().SelecionarEntrega(cep_inicial);
        }
    }
}