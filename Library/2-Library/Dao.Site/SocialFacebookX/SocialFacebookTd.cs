using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.SocialFacebookX;
using Facebook;

namespace _2_Library.Dao.Site.SocialConfigX
{
    public class SocialFacebookTd
    {
        public string GetAccessToken(string code, Uri uri)
        {
            string idApp = string.Empty;
            string secretApp = string.Empty;

             SocialConfigDto socialConfigDto = new SocialConfigTd().SelectSocialConfig(null);
             idApp = socialConfigDto.sc_idApp; //"275042665941123";
             secretApp = socialConfigDto.sc_secretApp;//"755c52c6f62b99cf201bb85bd8cbe8ca";


            string token = string.Empty;
            string redirect_uri = uri.Scheme + "://" + uri.Authority + uri.AbsolutePath;

            Uri getTokenUri = new Uri(
                string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "https://graph.facebook.com/oauth/access_token?"
                + "client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                idApp,
                Uri.EscapeDataString(redirect_uri),
                secretApp,
                Uri.EscapeDataString(code)
                ));
            using (var wc = new WebClient())
            {
                try
                {
                    token = wc.DownloadString(getTokenUri).Replace("access_token=", string.Empty);
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }

            return token;
        }

        public SocialPerfilDto SelectSocialPerfil(string accessToken)
        {
            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me");
            //dynamic friends = client.Get("/me/friends");
            dynamic interests = client.Get("/me/interests");
            dynamic activities = client.Get("/me/activities");
            dynamic likes = client.Get("/me/likes");
            dynamic friends = client.Get("/fql",
                new
                {
                    q = @"SELECT uid,name,is_app_user FROM user
                                  WHERE uid IN 
                                  (SELECT uid2 FROM friend WHERE uid1 = me()) ORDER BY name"
                });


            JsonArray sp_amigos = (JsonArray)friends.data;
            JsonArray sp_interesses = (JsonArray)interests.data;
            JsonArray sp_atividades = (JsonArray)activities.data;
            JsonArray sp_curtidas = (JsonArray)likes.data;

            SocialPerfilDto socialPerfilDto = new SocialPerfilDto();
            socialPerfilDto.sp_idPerfil = result.id;
            socialPerfilDto.sp_nome = result.first_name;
            socialPerfilDto.sp_sobrenome = result.last_name;
            socialPerfilDto.sp_sexo = result.gender != null ? (result.gender.ToString().ToLower() == "male" ? "M" : "F") : null;
            socialPerfilDto.sp_dataNascimento = result.birthday != null ? DateTime.Parse(result.birthday.ToString(), new System.Globalization.CultureInfo("en-US", false)) : null;
            socialPerfilDto.sp_email = result.email;
            socialPerfilDto.sp_cidade = result.location != null ? result.location.name.Split(',')[0] : null;
            socialPerfilDto.sp_estado = result.location != null ? result.location.name.Split(',')[1] : null;
            socialPerfilDto.sp_idioma = result.locale;
            socialPerfilDto.sp_amigos =     sp_amigos != null && sp_amigos.Count > 0 ? sp_amigos.ToString() : null;
            //socialPerfilDto.sp_amigosApp = (sp_amigos != null && sp_amigos.Count > 0 ? string.Join(",", new JavaScriptSerializer().Deserialize<List<FriendDto>>(sp_amigos.ToString()).Where(s => s.is_app_user).Select(s2=>s2.uid)) : null);
            socialPerfilDto.sp_interesses = (sp_interesses != null) ? new JavaScriptSerializer().Serialize(sp_interesses.Select((Func<dynamic, string>)(x => x.name))) : null;
            socialPerfilDto.sp_atividades = sp_atividades != null && sp_atividades.Count > 0 ? sp_atividades.ToString() : null;
            socialPerfilDto.sp_curtidas = (sp_curtidas != null) ? new JavaScriptSerializer().Serialize(sp_curtidas.Select(s => new { ((dynamic)s).name, ((dynamic)s).id })) : null;

            socialPerfilDto.sp_trabalho = (result.work != null && result.work[0].employer != null) ? (result.work[0].employer[1]) : null;
            socialPerfilDto.sp_sobre = result.bio;
            socialPerfilDto.sp_site = result.website;
            socialPerfilDto.sp_religiao = result.religion;
            socialPerfilDto.sp_relacionamentoStatus = result.relationship_status;
            socialPerfilDto.sp_verificado = result.verified;
            socialPerfilDto.sp_numeroConexoes = (sp_amigos != null && sp_amigos.Count > 0 ? new JavaScriptSerializer().Deserialize<List<FriendDto>>(sp_amigos.ToString()).Where(s => s.is_app_user).Count() : 0);

            return socialPerfilDto;
        }

        /// <summary>
        /// Script de inicializacao do facebook. É nescessário incluir a tag html '&lt;div id="fb-root"&gt;&lt;/div&gt;' antes
        /// </summary>
        /// <param name="idApp"></param>
        /// <returns></returns>
        public string GetScriptInitFB(string idApp)
        {
            return @"window.fbAsyncInit=function(){FB.init({appId:'" + idApp + "\',status:true,cookie:true,xfbml:true,frictionlessRequests:true});FB.Canvas.setSize({height:2e3});FB.Canvas.scrollTo(0,0);FB.Canvas.setAutoGrow()};(function(e){var t,n=\"facebook-jssdk\",r=e.getElementsByTagName(\"script\")[0];if(e.getElementById(n)){return}t=e.createElement(\"script\");t.id=n;t.async=true;t.src=\"//connect.facebook.net/pt_BR/all.js\";r.parentNode.insertBefore(t,r)})(document)";
        }

    }
}
