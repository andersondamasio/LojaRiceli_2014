using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using _2_Library.Dao.Site.Clientex;

namespace _1_WebForm.App_Code.Utils
{
    public class CustomPrincipal : System.Security.Principal.IPrincipal
    {

        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        public int CliId
        {
            get;
            set;
        }

        public string CliNome
        {
            get;
            set;
        }
       public int? CliSpId
        {
            get;
            set;
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

    public class CliAut {

        public int CliId { get; set; }
        public string CliNome { get; set; }
        public string CliEmail { get; set; }
        public int? CliSpId { get; set; }
    
    
    }


    public class Aut {

        public static void Autenticar(CliAut cliAut)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var userData = serializer.Serialize(cliAut);
            var authTicket = new FormsAuthenticationTicket(1, cliAut.CliEmail, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);
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
                var user = serializer.Deserialize<CliAut>(ticket.UserData);
                CustomPrincipal newUser = GetPrincipal(user);
             
            return newUser;
        }



        public static CustomPrincipal GetPrincipal(CliAut cliAut)
        {
            return new CustomPrincipal(cliAut.CliEmail) { CliId = cliAut.CliId, CliNome = cliAut.CliNome,CliSpId = cliAut.CliSpId };
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
        }

        public static void LogoutLogin()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }





    }

}