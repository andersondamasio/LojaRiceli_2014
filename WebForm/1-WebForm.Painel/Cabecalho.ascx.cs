using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Usuariox;

namespace Loja.Painel
{
    public partial class Cabecalho : System.Web.UI.UserControl
    {
        private List<String> menuItemsRemove = new List<String>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _2_Library.Modelo.Usuario usuPer = (_2_Library.Modelo.Usuario)Session["usuario"];
            Session["usuario"] = new UsuarioConsulta().SelecionarUsuario(usuPer.usu_nome, usuPer.usu_senha);
        }

        private void SetPermissoes(_2_Library.Modelo.Usuario usuPer, MenuItem menuItem)
        {
           DesabilitaItemMenu(usuPer, menuItem);
        }

        private void DesabilitaItemMenu(_2_Library.Modelo.Usuario usuPer, System.Web.UI.WebControls.MenuItem menuItem)
        {
         /*   PropertyInfo[] propertyInfos = typeof(_2_Library.Modelo.Usuario).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Type tipo = usuPer.GetType();

            if (propertyInfos.Where(p => p.Name.Replace("Selecionar", string.Empty).Replace("Bloquear", string.Empty) == menuItem.Value).Count() > 0)
            {
                string menuPermissao = menuItem.Value + "Bloquear";

                var perBloquear = tipo.InvokeMember(menuPermissao, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, usuPer, null);

                if ((Boolean)perBloquear)
                {
                    menuItem.Enabled = false;
                    menuItem.Parent.ChildItems.Remove(menuItem);
                }
                else
                {
                    Boolean perSelecionar = (Boolean)tipo.InvokeMember(menuItem.Value + "Selecionar", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, usuPer, null);
                    menuItem.Enabled = perSelecionar;
                }
            }*/
        }

        private void VerificaPermissao(_2_Library.Modelo.Usuario usuPer, MenuItem menuItem)
        {
            Boolean permissao = false;
            if (usuPer != null)
            {

                var linkMenu = menuItem.NavigateUrl == System.IO.Path.GetFileName(Request.PhysicalPath) ? menuItem : null;


                if (linkMenu != null)
                {
                    PropertyInfo[] propertyInfos = typeof(Usuario).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfos.Where(p => p.Name == linkMenu.Value).Count() > 0)
                    {
                        Type tipo = usuPer.GetType();
                        permissao = (Boolean)tipo.InvokeMember(linkMenu.Value,
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.GetProperty, null, usuPer, null);

                    }
                }
                if (!permissao)
                {
                    //Response.Redirect("Index.aspx");
                }
            }
        }

        protected void MenuCabecalho_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _2_Library.Modelo.Usuario usuPer = (_2_Library.Modelo.Usuario)Session["usuario"];

                VerificaPermissao(usuPer, e.Item);
                SetPermissoes(usuPer, e.Item);
            }
        }
    }
}