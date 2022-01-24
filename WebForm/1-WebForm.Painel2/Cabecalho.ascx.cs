using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Painel.UsuarioX;
using Loja.Modelo.Usuariox;
using Loja.Utils;

namespace Loja.Painel
{
    public partial class Cabecalho : System.Web.UI.UserControl
    {
        private List<String> menuItemsRemove = new List<String>();

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaPermissao();
           //_2_Library.Modelo.Usuario usuPer = (_2_Library.Modelo.Usuario)Session["usuario"];
            //Session["usuario"] = new UsuarioConsulta().SelecionarUsuario(usuPer.usu_nome, usuPer.usu_senha);
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


        private MenuItem GetMenu(MenuItemCollection itens) {

            MenuItem mItem = null;

                string atualPhysicalPath = "~/" + System.IO.Path.GetFileName(Request.PhysicalPath);
                if (atualPhysicalPath != "~/")
                {
                    foreach (System.Web.UI.WebControls.MenuItem menuItem in itens)
                    {
                        if (menuItem.NavigateUrl == atualPhysicalPath)
                        {
                            mItem = menuItem;
                            break;
                        }
                        if (itens != null && itens.Count > 0)
                            mItem = GetMenu(menuItem.ChildItems);
                        if (mItem != null)
                            break;
                    }
                }

            return mItem;
        }

        private void VerificaPermissao()
        {
             string atualPhysicalPath = "~/" + System.IO.Path.GetFileName(Request.PhysicalPath);

             MenuItem linkMenu = GetMenu(MenuCabecalho.Items);
             UsuarioDto usuarioDto = Aut.AutenticacaoDadosUsuario();

            if (linkMenu != null)
            {
                PropertyInfo[] propertyInfos = typeof(UsuarioDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfos.Where(p => p.Name == linkMenu.Value).Count() > 0)
                {

                   
                    Type tipo = usuarioDto.GetType();
                    var permissao = (bool)tipo.InvokeMember(linkMenu.Value,
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.GetProperty, null, usuarioDto, null);
                    if (!(permissao == true))
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
            SetPermissoes(usuarioDto);
        }

        private void SetPermissoes(UsuarioDto usuarioDto)
        {
            PropertyInfo[] propertyInfos = typeof(UsuarioDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Type tipo = usuarioDto.GetType();

            foreach (System.Web.UI.WebControls.MenuItem menuItem in MenuCabecalho.Items)
            {
                if (propertyInfos.Where(p => p.Name == menuItem.Value).Count() > 0)
                {
                    var permissao = (bool)tipo.InvokeMember(menuItem.Value,
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.GetProperty, null, usuarioDto, null);

                    if (permissao == false)
                        menuItem.Enabled = false;
                }
                if (menuItem.ChildItems.Count > 0)
                    SetPermissoes(usuarioDto, menuItem.ChildItems);

            }
        }

        private void SetPermissoes(UsuarioDto usuarioDto, MenuItemCollection menuItemCollection)
        {
            PropertyInfo[] propertyInfos = typeof(UsuarioDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Type tipo = usuarioDto.GetType();

            foreach (MenuItem menuItem in menuItemCollection)
            {
                if (propertyInfos.Where(p => p.Name == menuItem.Value).Count() > 0)
                {
                    var permissao = (bool)tipo.InvokeMember(menuItem.Value,
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.GetProperty, null, usuarioDto, null);

                    if (permissao == false)
                        menuItem.Enabled = false;
                    
                }
                if (menuItem.ChildItems.Count > 0)
                    SetPermissoes(usuarioDto, menuItem.ChildItems);
            }
        }


      /*  private void DesativaMenu(MenuItem menuItem)
        {

            if (!Page.User.IsInRole("Admin"))
            {
                if (menuItem.NavigateUrl.Equals("~/ConfiguracaoLoja.aspx"))
                {
                    if (menuItem.Parent != null)
                    {
                        MenuItem menu = menuItem.Parent;

                        menu.ChildItems.Remove(menuItem);
                    }
                    else
                    {
                        Menu menu = MenuCabecalho;

                        menu.Items.Remove(menuItem);
                    }
                }
            }
        
        
        }
*/
/*
        private void VerificaPermissao(UsuarioDto usuarioDto)
        {
            Boolean permissao = false;
            if (usuarioDto != null)
            {
                MenuItem item = MenuCabecalho. ("Users/Manage Accounts");
                //var linkMenu = menuItem.NavigateUrl == System.IO.Path.GetFileName(Request.PhysicalPath) ? menuItem : null;
                if (linkMenu != null)
                {
                    PropertyInfo[] propertyInfos = typeof(Usuario).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfos.Where(p => p.Name == linkMenu.Value).Count() > 0)
                    {
                        Type tipo = usuarioDto.GetType();
                        permissao = (Boolean)tipo.InvokeMember(linkMenu.Value,
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.GetProperty, null, usuarioDto, null);

                    }
                }
                if (!permissao)
                {
                    Response.Redirect("Index.aspx");
                }
            }
        }
        */

        protected void LinkButtonsair_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Aut.Logout();
        }

    }
}