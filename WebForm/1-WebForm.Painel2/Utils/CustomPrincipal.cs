using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using _2_Library.Dao.Painel.UsuarioX;


namespace Loja.Utils
{
    public class CustomPrincipal : UsuarioDto, System.Security.Principal.IPrincipal
    {

        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

     
        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }


    public class Aut {

        public static void Autenticar(UsuarioDto usuarioDto)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var userData = serializer.Serialize(usuarioDto);
            var authTicket = new FormsAuthenticationTicket(1, usuarioDto.usu_nome, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);
            var ticket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        public static CustomPrincipal AutenticacaoDados()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == string.Empty)
                return null;

                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var usuarioDto = serializer.Deserialize<UsuarioDto>(ticket.UserData);
                CustomPrincipal newUser = GetPrincipal(usuarioDto);
             
            return newUser;
        }

        public static UsuarioDto AutenticacaoDadosUsuario()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == string.Empty)
                return null;

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var usuarioDto = serializer.Deserialize<UsuarioDto>(ticket.UserData);

            return usuarioDto;
        }

        public static CustomPrincipal GetPrincipal(UsuarioDto usuarioDto)
        {
            return new CustomPrincipal(usuarioDto.usu_nome)
            {
                usu_id = usuarioDto.usu_id,
                usu_nome = usuarioDto.usu_nome,
                usuPer_usuarioSelecionar = usuarioDto.usuPer_usuarioSelecionar,
                usuPer_pedidoSelecionar = usuarioDto.usuPer_pedidoSelecionar,
                usuPer_lojaSelecionar = usuarioDto.usuPer_lojaSelecionar,
                usuPer_lojaInserir = usuarioDto.usuPer_lojaInserir,
                usuPer_lojaBloquear = usuarioDto.usuPer_lojaBloquear,
                loj_id = usuarioDto.loj_id
            };
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }


        internal static void Autenticar(CliAut cliAut)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var userData = serializer.Serialize(cliAut);
            var authTicket = new FormsAuthenticationTicket(1, cliAut.CliEmail, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);
            var ticket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }


        internal class CliAut
        {
            public int CliId { get; set; }
            public string CliNome { get; set; }
            public string CliEmail { get; set; }
        }

    }

}