using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.SocialPerfilX;
using _2_Library.Modelo;

namespace _1_WebForm
{
    public partial class MeusPontos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
            {
                ListaPontuacao();
            }
        }

        private void ListaPontuacao()
        {
            LiteralPontuacaoNumeroConexoes.Text = SelectPontuacaoNumeroConexoes().ToString();
            LiteralPontuacaoReferencia.Text = "0";
            LiteralPontuacaoTotal.Text = (Convert.ToInt32(LiteralPontuacaoReferencia.Text)+Convert.ToInt32(LiteralPontuacaoNumeroConexoes.Text)).ToString();
        }

        private int SelectPontuacaoNumeroConexoes() { 
        
                 _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();
                 if (customPrincipal != null && customPrincipal.CliId != 0)
                 {
                     if (customPrincipal.CliSpId.HasValue)
                     {
                         SocialPerfilDto socialPerfilDto = new SocialPerfilTd().SelectSocialPerfil(null, customPrincipal.CliId);

                         if (socialPerfilDto != null)
                             return socialPerfilDto.sp_numeroConexoes;
                     }
                 }

            return 0;
        }

       



    }
}