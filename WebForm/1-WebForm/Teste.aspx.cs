using System;
using System.Linq;
using _2_Library.Dao.Site.CorreioX;
using _2_Library.Utils;

namespace _1_WebForm
{
    public partial class Teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var cookie = Recursos.RecuperaCookie("TesteC");
                if (cookie != null)
                    Validacao.Alert(cookie.Value);
            }
        }

        private void testePagSeguro() {
            //string transactionCode = "5778F74B-358A-45AC-BD25-68C513E2A12E";
            //Transaction transaction = TransactionSearchService.SearchByCode(credentials,transactionCode);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            testePagSeguro();
        }

        protected void ButtonAtualizaFrete_Click(object sender, EventArgs e)
        {

            /* # Código dos Serviços dos Correios  #
#    FRETE PAC = 41106       #
#    FRETE SEDEX = 40010       #
#    FRETE SEDEX 10 = 40215       #
#    FRETE SEDEX HOJE = 40290    #
#    FRETE E-SEDEX = 81019       #
#    FRETE MALOTE = 44105       #
#    FRETE NORMAL = 41017       #
#   SEDEX A COBRAR = 40045       #*/

            new FreteTd().UpdateAllFrete("40010");

            _2_Library.Utils.Validacao.Alert("Acabou");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {




          /*  var accessToken = "CAACG1uYgelYBAM1GZCoIZB05sjmpvfjTQIwyTYe7U4GQmr5hPZCpaYVNwcxuSXeImJu7gSu390hxMj772zXgQDNF1EMkK8pw5ZBFKGt623EZCbX7ztS8qLSGcO86JE8ROZBvQYoCIRxun7xyJZAbkSZAZBDMldbd6b7ZAaKCNk9LsdzcFOm2ZBtvhxzOVQ8xSgL4KhYI6m2ZByf8e2a5PKnx96AfdMriYABBagEZD";
            var client = new FacebookClient(accessToken);
            dynamic me = client.Get("me");
            string aboutMe = me.about;
            */

          

           // string client = "FacebookClient";
            //AuthorizationRoot authorizationRoot = new OAuth2.AuthorizationRoot();
           // var uri = authorizationRoot.Clients.Where(s => s.Configuration.ClientTypeName == client).FirstOrDefault().GetLoginLinkUri();

           // Validacao.SetScript("AbrirJanela('" + uri + "', '400', '580');");

            
            //AdicionaCookie("TesteC", info.FirstName, 1);

        }
    }
}