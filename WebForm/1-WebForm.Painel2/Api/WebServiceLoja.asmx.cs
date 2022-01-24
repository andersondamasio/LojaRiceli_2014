using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using Loja.Modelo;

namespace Loja.Api
{
    /// <summary>
    /// Summary description for WebServiceLoja
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceLoja : System.Web.Services.WebService
    {

        [WebMethod]
        public string Teste()
        {
            return "Hello World";
        }


        [WebMethod]
        public Retorno SelecionarProdutoSkuFoto()
        {

            Retorno retorno = new Retorno();

            new Consulta().SelecionarProdutoSkuFoto().ToDataTable().WriteXml(@"X:\SourceAnderson\Aplicativos\Loja\Web\Loja\Loja\Api\xml\teste.xml");

            retorno.objeto = System.IO.File.ReadAllText(@"X:\SourceAnderson\Aplicativos\Loja\Web\Loja\Loja\Api\xml\teste.xml");

            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(@"X:\SourceAnderson\Aplicativos\Loja\Web\Loja\Loja\Api\xml\teste.xml");
            }
            catch (XmlException xmlException)
            {
                retorno.menSis_id = -1;
                retorno.menSis_mensagem = xmlException.Message;
            }

            return retorno;
        }
    }
}
