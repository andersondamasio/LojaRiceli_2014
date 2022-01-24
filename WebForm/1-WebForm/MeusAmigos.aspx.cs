using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.SocialConfigX;
using _2_Library.Dao.Site.SocialFacebookX;
using _2_Library.Dao.Site.SocialPerfilX;

namespace _1_WebForm
{
    public partial class MeusAmigos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
            {
                if (!IsPostBack)
                    SelectAmigos();
            }
        }
    
        
        private void SelectAmigos()
        {
            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

            if (customPrincipal != null && customPrincipal.CliId != 0)
            {
                if (customPrincipal.CliSpId.HasValue)
                {
                    SocialPerfilDto socialPerfilDto = new SocialPerfilTd().SelectSocialPerfil(null, customPrincipal.CliId);
                    var amigos = new JavaScriptSerializer().Deserialize<List<FriendDto>>(socialPerfilDto.sp_amigos);
                    RepeaterAmigosDesconectados.DataSource = amigos.Where(s=>!s.is_app_user);
                    RepeaterAmigosConectados.DataSource = amigos.Where(s => s.is_app_user);
                    PanelAmigosNaoEncontrado.Visible = amigos.Count() == 0;
                    RepeaterAmigosDesconectados.DataBind();
                    RepeaterAmigosConectados.DataBind();

                   

                    _2_Library.Utils.Validacao.SetScriptHeader(new SocialFacebookTd().GetScriptInitFB(new SocialConfigTd().SelectSocialConfig(null).sc_idApp));
                }
                else
                    PanelAmigos.Visible = true;
            }
        }

    }
}