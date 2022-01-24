using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace _2_Library.Utils
{
    public class Scripts
    {
        public static void AddPathScriptHeader()
        {
            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = "mylibrary.js";
                    
            System.Web.UI.Page page = new System.Web.UI.Page();
            var http = System.Web.HttpContext.Current;
            if ((http != null))
            {
                page = http.CurrentHandler as System.Web.UI.Page;
            }
            
            page.Header.Controls.Add(js);
        }

        public static void AddPathStyleHeader()
        {
            HtmlLink css = new HtmlLink();
            css.Href = "mystyle.css";
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Attributes["media"] = "all";
            
            System.Web.UI.Page page = new System.Web.UI.Page();
            var http = System.Web.HttpContext.Current;
            if ((http != null))
            {
                page = http.CurrentHandler as System.Web.UI.Page;
            }
            page.Header.Controls.Add(css);
        } 

        public static void AddFacebookScriptHeader(string appId)
        {    
            string script = @"window.fbAsyncInit=function(){FB.init({appId:'275042665941123',status:true,cookie:true,xfbml:true});FB.Event.subscribe('auth.authResponseChange',function(e){if(e.status==='connected'){var t=e.authResponse.userID;var n=e.authResponse.accessToken; "+
                            "$.post('social/FacebookLogin.ashx', function (data) {$('.result').html(data);}); "+
                            "}else if(e.status==='not_authorized'){}else{}})};(function(e){var t,n='facebook-jssdk',r=e.getElementsByTagName('script')[0];if(e.getElementById(n)){return}t=e.createElement('script');t.id=n;t.async=true;t.src='//connect.facebook.net/pt_BR/all.js';r.parentNode.insertBefore(t,r)})(document)";

            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.InnerHtml = script;

            System.Web.UI.Page page = new System.Web.UI.Page();
            var http = System.Web.HttpContext.Current;
            if ((http != null))
            {
                page = http.CurrentHandler as System.Web.UI.Page;
            }

            page.Header.Controls.Add(js);

        }



    }
}