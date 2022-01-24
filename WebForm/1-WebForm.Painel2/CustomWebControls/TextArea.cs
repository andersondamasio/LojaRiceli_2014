using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CustomWebControls
{
    /// <summary>
    /// Extended TextBox control that applies
    /// javascript validation to Multiline TextBox
    /// </summary>
    public class TextArea : TextBox
    {
        /// <summary>
        /// Override PreRender to include custom javascript
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (MaxLength > 0 && TextMode == TextBoxMode.MultiLine)
            {
                // Add javascript handlers for paste and keypress
                Attributes.Add("onkeypress", "doKeypress(this);");
                Attributes.Add("onbeforepaste", "doBeforePaste(this);");
                Attributes.Add("onpaste", "doPaste(this);");
                // Add attribute for access of maxlength property on client-side
                Attributes.Add("maxLength", this.MaxLength.ToString());
                // Register client side include - only once per page
                if (!Page.ClientScript.IsClientScriptIncludeRegistered("TextArea"))
                {
                    Page.ClientScript.RegisterClientScriptInclude("TextArea",
                        ResolveClientUrl("~/js/textArea.js"));
                }
            }
            base.OnPreRender(e);
        }
    }
}