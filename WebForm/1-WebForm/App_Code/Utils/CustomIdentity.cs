using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _1_WebForm.App_Code.Utils
{
    public class CustomIdentity : System.Security.Principal.IIdentity
    {

        private FormsAuthenticationTicket _ticket;

        public CustomIdentity(FormsAuthenticationTicket ticket)
        {

            _ticket = ticket;

        }

        public string AuthenticationType
        {

            get { return "Custom"; }

        }

        public bool IsAuthenticated
        {

            get { return true; }

        }

        public int? IdCliente
        {
            get
            {
                if (!string.IsNullOrEmpty(_ticket.UserData))
                {
                    string[] dados = _ticket.UserData.Split("|".ToCharArray());
                    return Convert.ToInt32(dados[0]);
                }
                else return null;
            }
        }

        public string NomeCliente
        {
            get
            {
                if (!string.IsNullOrEmpty(_ticket.UserData))
                {
                    string[] dados = _ticket.UserData.Split("|".ToCharArray());
                    return dados[1];
                }
                else return null;
            }
        }

        public FormsAuthenticationTicket Ticket
        {

            get { return _ticket; }

        }

        public string Name
        {

            get { return _ticket.Name; }

        }
    }
}