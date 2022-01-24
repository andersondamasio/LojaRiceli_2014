using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.Clientex;
using _2_Library.Dao.Site.SocialPerfilX;

namespace _1_WebForm.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceSocial
    {
      /*  [OperationContract]
        public SocialPerfilDto FacebookLogin(string accessToken)
        {
            System.Web.HttpContext.Current.Session.Add("AccessToken", accessToken);

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me", new { fields = "id,name,email,first_name,last_name,gender,birthday,email,location,locale" });
            dynamic friends = client.Get("/me/friends");
            JsonArray amigos = (JsonArray)friends.data;
            
            SocialPerfilDto socialPerfilDto = new SocialPerfilDto();
            socialPerfilDto.sp_idPerfil = result.id;
            socialPerfilDto.sp_nome = result.first_name;
            socialPerfilDto.sp_sobrenome = result.last_name;
            socialPerfilDto.sp_sexo = result.gender != null ? (result.gender.ToString().ToLower() == "male" ? "M" : "F") : null; ;
            socialPerfilDto.sp_dataNascimento = result.birthday != null ? DateTime.Parse(result.birthday.ToString(), new System.Globalization.CultureInfo("en-US", false)) : null;
            socialPerfilDto.sp_email = result.email;
            socialPerfilDto.sp_cidade = result.location != null ? result.location.name.Split(',')[0] : null;
            socialPerfilDto.sp_estado = result.location != null ? result.location.name.Split(',')[1] : null;
            socialPerfilDto.sp_idioma = result.locale;
            socialPerfilDto.sp_amigos = amigos != null && amigos.Count > 0 ? amigos.ToString() : null;

           
            socialPerfilDto = new SocialPerfilTd().InsertSocialPerfil(null, socialPerfilDto);

            //verificao e segurança
            if (socialPerfilDto.cli_id.HasValue)
            {
                ClienteDto clienteDto = new ClienteTd().SelectCliente(null, socialPerfilDto.cli_id.Value);
                if (clienteDto.cli_email == socialPerfilDto.sp_email)
                    Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome });
                else {
                    //caso as contas de email não combinem, considera-se cli_id = 0, e a conta não irá ser autenticada.
                    socialPerfilDto.cli_id = 0;
                }
            }

            System.Web.HttpContext.Current.Session.Add("cli_email", socialPerfilDto.sp_email);

            return socialPerfilDto;
        }*/
    }
}
