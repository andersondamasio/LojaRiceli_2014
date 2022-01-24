using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.SocialConfigX;
using _2_Library.Dao.Site.SocialPerfilX;
using _2_Library.Utils;

namespace _1_WebForm.Social
{
    public partial class LoginSocial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idApp = string.Empty;
            string redirect_uri = (Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.AbsolutePath.Replace("LoginSocial.aspx", "LoginSocialRetorno.aspx"));
            
            SocialConfigDto socialConfigDto = new SocialConfigTd().SelectSocialConfig(null);
            if (socialConfigDto != null && !string.IsNullOrEmpty(socialConfigDto.sc_idApp) && !string.IsNullOrEmpty(socialConfigDto.sc_secretApp))
                idApp = socialConfigDto.sc_idApp;
            else
            {
                Validacao.Alert("O aplicativo não foi configurado.");
                Validacao.SetScript("closeWindow();");
                return;
            }


            Validacao.SetScript("redirect(\"" + idApp + "\", \"" + redirect_uri + "\");");
        }
    }
}