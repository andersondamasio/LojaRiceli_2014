using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_WebForm
{
    public partial class TesteX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccessToken"] != null)
            {

               /* var accessToken = Session["AccessToken"].ToString();
                var client = new FacebookClient(accessToken);
                dynamic result = client.Get("me", new { fields = "name,id" });
                string name = result.name;
                string id = result.id;*/
            }

        }
    }
}