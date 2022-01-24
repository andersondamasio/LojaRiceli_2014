using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.PedidoX;
using _2_Library.Gateways.PagSeguro;

namespace _1_WebForm.Gateway
{
    public partial class RetornoAutPagSeguro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string notificationType = string.Empty;
            string notificationCode = string.Empty;
            int ped_id = 0;
            string caminho = Request.PhysicalApplicationPath + @"Logs\" + string.Format(@"Log-{0}.txt", DateTime.Now.Date.ToString("dd-MM-yyyy"));
            try
            {
                notificationType = Request.Form["notificationType"];
                notificationCode = Request.Form["notificationCode"];

                if (notificationCode != null || notificationType != null)
                {
                    ped_id = new Transacao().UpdatePedido(notificationType, notificationCode);
                    _2_Library.Utils.Recursos.EscreveLog(caminho, DateTime.Now + "| ped_id:" + ped_id + " | notificationType:" + notificationType + " | " + "notificationCode:" + notificationCode);
                }
                else
                    _2_Library.Utils.Recursos.EscreveLog(caminho, DateTime.Now + "| ERRO | ped_id:" + ped_id + " | ERRO:notificationType ou notificationCode está nulo | notificationType:" + notificationType + " | " + "notificationCode:" + notificationCode);
            }
            catch (Exception ex)
            {
                _2_Library.Utils.Recursos.EscreveLog(caminho, DateTime.Now + "| ERRO | ped_id:" + ped_id + " | ERRO:" + ex.Message + " | notificationType:" + notificationType + " | " + "notificationCode:" + notificationCode);
            }
        }
    }
}