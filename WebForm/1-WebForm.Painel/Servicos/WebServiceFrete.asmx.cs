using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Loja.correios.ServiceReferenceCorreios;
using Loja.Correiox;
using Loja.Modelo.Carrinhox;
using Loja.Modelo.Correiox;

namespace Loja.Servicos
{
    /// <summary>
    /// Summary description for WebServiceFrete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebServiceFrete : System.Web.Services.WebService
    {
       [WebMethod(EnableSession = true)]
        public CarrinhoEntrega CalculaPrazo(int cepDestino)
        {
            return new CorreioConsulta().CalculaPrazo(cepDestino);
        }

        [WebMethod]
        public CorreioBean SelecionarEndereco(string cepDestino)
        {
            return new CorreioConsulta().SelecionarEndereco(cepDestino);

        }
    }
}
