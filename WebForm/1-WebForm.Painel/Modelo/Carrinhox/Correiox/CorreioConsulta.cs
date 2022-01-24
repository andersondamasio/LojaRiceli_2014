using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.correios.ServiceReferenceCorreios;
using Loja.Correiox;
using Loja.Modelo.Carrinhox;

namespace Loja.Modelo.Correiox
{
    public class CorreioConsulta
    {
        public CorreioBean SelecionarEndereco(string cepDestino)
        {
            return new CorreioDao().SelecionarEndereco(cepDestino);
        }

        public CarrinhoEntrega CalculaPrazo(int cepDestino)
        {

            return new CorreioDao().CalculaPrazo(cepDestino);
        }

        public CarrinhoEntrega CalculaPrecoPrazo(int cepDestino)
        {
            return new CorreioDao().CalculaPrecoPrazo(cepDestino);
        }
    }
}