using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Cupom
{
    public class CupomConsulta
    {
        public Retorno SelecionarCupom(string cep_chave, int? cli_id)
        {
            return new CupomDao().SelecionarCupom(cep_chave, cli_id);
        }


        public void ExcluirCupom(int cup_id)
        {
            new CupomDao().ExcluirCupom(cup_id);
        }
    }
}