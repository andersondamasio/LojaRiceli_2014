using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.Clientex;
using _2_Library.Dao.Site.SocialConfigX;
using _2_Library.Dao.Site.SocialPerfilX;
using _2_Library.Utils;

namespace _1_WebForm.Social
{
    public partial class LoginSocialRetorno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["code"];
            string error_code = Request.QueryString["error_code"];

            Uri uri = Request.Url;
            if (!string.IsNullOrEmpty(code)) // se autenticado
            {
                string accessToken = new SocialFacebookTd().GetAccessToken(code, uri);

                Session["accessToken"] = accessToken;

                SocialPerfilDto socialPerfilDto = new SocialFacebookTd().SelectSocialPerfil(accessToken);

                
               
                if (!socialPerfilDto.cli_id.HasValue)
                {
                    //se já autenticado, associa a conta com a do facebook
                    _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();
                    if (customPrincipal != null && customPrincipal.CliId != 0)
                    {
                        if (customPrincipal.Identity.Name == socialPerfilDto.sp_email)
                        {

                            if(socialPerfilDto.sp_verificado.HasValue && socialPerfilDto.sp_verificado.Value)
                               socialPerfilDto.cli_id = customPrincipal.CliId;
                            else Validacao.Alert("Sua conta do Facebook precisa estar verifica para associa-la.");
                        }
                        else {

                            Validacao.Alert("Seu email do Facebook é diferente de sua conta, não foi possível associa-la.");
                        
                        }
                    }
                }

                socialPerfilDto = new SocialPerfilTd().InsertSocialPerfil(null, socialPerfilDto);

                //verificao e segurança
                if (socialPerfilDto.cli_id.HasValue)
                {
                    ClienteDto clienteDto = new ClienteTd().SelectCliente(null, socialPerfilDto.cli_id.Value);
                    if (clienteDto.cli_email == socialPerfilDto.sp_email)
                        Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome, CliSpId = clienteDto.sp_id });
                    else
                    {
                        //caso as contas de email não combinem, considera-se cli_id = 0, e a conta não irá ser autenticada.
                        socialPerfilDto.cli_id = 0;
                    }
                }
                else {
                    if (!string.IsNullOrEmpty(socialPerfilDto.sp_email))
                    {
                        bool clienteExiste = new ClienteTd().SelectClienteExiste(null, socialPerfilDto.sp_email);

                        if (clienteExiste == false)
                            socialPerfilDto.cli_id = 0;
                    }
                }

                System.Web.HttpContext.Current.Session.Add("socialPerfilDto", socialPerfilDto);

                //para evitar que os amigos sejão renderizados no html
                socialPerfilDto.sp_amigos = null;

                AdicionaDadosPerfil(new JavaScriptSerializer().Serialize(socialPerfilDto));

            }
            else
            {
                if (!string.IsNullOrEmpty(error_code)) // se autenticado
                {
                  //if(error_code == "200")
                      Validacao.SetScriptHeader("fecharJanela()"); 
                }
            }
        }


        private void AdicionaDadosPerfil(dynamic dados)
        {
            string script = "var dadosPerfil = " + dados;
            Validacao.SetScriptHeader(script);
        }
    

   
    }
}