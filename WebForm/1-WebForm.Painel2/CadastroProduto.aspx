<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeBehind="CadastroProduto.aspx.cs" Inherits="_1_WebForm.Painel2.CadastroProduto" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="ScriptManager1" runat="server" EnableTheming="True">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <table border="2" style="vertical-align:top">
       <tr>
           <td>MENU</td>
           <td>CADASTRO</td>
       </tr>
       <tr>
           <td style="vertical-align:top">
               <telerik:RadTreeView ID="RadTreeViewGrupo" Runat="server" DataFieldID="gru_id" DataFieldParentID="gru_pai" DataNavigateUrlField="gru_nomeAmigavel" DataSourceID="ObjectDataSourceGrupo" DataTextField="gru_nome" DataValueField="gru_id" OnNodeClick="RadTreeViewGrupo_NodeClick">
                  <ContextMenus>
                <telerik:RadTreeViewContextMenu ID="MainContextMenu" runat="server">
                    <Items>
                        <telerik:RadMenuItem Value="Copy" Text="Copy ...">
                        </telerik:RadMenuItem>
                     </Items>
                    </telerik:RadTreeViewContextMenu>
              </ContextMenus>
                   
                   
                </telerik:RadTreeView>
               
               
               <asp:ObjectDataSource ID="ObjectDataSourceGrupo" runat="server" SelectMethod="SelectGrupo" TypeName="_2_Library.Dao.Painel.GrupoX.GrupoTd">
                   <SelectParameters>
                       <asp:Parameter Name="loj_dominio" Type="String" />
                   </SelectParameters>
               </asp:ObjectDataSource>
           </td>
           <td style="vertical-align:top"><telerik:RadListView ID="RadListViewProdutosGrupo" runat="server" DataSourceID="ObjectDataSourceProdutosGrupo" AllowPaging="True" ClientDataKeyNames="pro_id" DataKeyNames="pro_id">
                <LayoutTemplate>
                    <div class="RadListView RadListView_Default">
                        <table cellspacing="0" style="width: 100%;">
                            <thead>
                                <tr class="rlvHeader">
                                    <th>pro_id</th>
                                    <th>gru_id</th>
                                    <th>loj_id</th>
                                    <th>proGru_dataHora</th>
                                    <th>produtoDto</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                        <div style="display: none">
                            <telerik:RadCalendar ID="rlvSharedCalendar" runat="server" RangeMinDate="<%#new DateTime(1900, 1, 1) %>" Skin="<%#Container.Skin %>" />
                        </div>
                        <div style="display: none">
                            <telerik:RadTimeView ID="rlvSharedTimeView" runat="server" Skin="<%# Container.Skin %>" />
                        </div>
                    </div>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="rlvI">
                    <td>
                        <asp:Label ID="pro_idLabel" runat="server" Text='<%# Eval("pro_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="gru_idLabel" runat="server" Text='<%# Eval("gru_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="loj_idLabel" runat="server" Text='<%# Eval("loj_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="proGru_dataHoraLabel" runat="server" Text='<%# Eval("proGru_dataHora") %>' />
                    </td>
                    <td>
                        <asp:Label ID="produtoDtoLabel" runat="server" Text='<%# Eval("produtoDto") %>' />
                    </td>
                </tr>
            </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="RadListView RadListView_Default">
                        <div class="rlvEmpty">
                            There are no items to be displayed.</div>
                    </div>
                </EmptyDataTemplate>
                <SelectedItemTemplate>
                    <tr class="rlvISel">
                        <td>
                            <asp:Label ID="pro_idLabel" runat="server" Text='<%# Eval("pro_id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="gru_idLabel" runat="server" Text='<%# Eval("gru_id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="loj_idLabel" runat="server" Text='<%# Eval("loj_id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="proGru_dataHoraLabel" runat="server" Text='<%# Eval("proGru_dataHora") %>' />
                        </td>
                        <td>
                            <asp:Label ID="produtoDtoLabel" runat="server" Text='<%# Eval("produtoDto") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
        </telerik:RadListView>
               <asp:ObjectDataSource ID="ObjectDataSourceProdutosGrupo" runat="server" SelectMethod="SelectProduto_GrupoListar" TypeName="_2_Library.Dao.Painel.Produto_GrupoX.Produto_GrupoTd">
                   <SelectParameters>
                       <asp:Parameter Name="loj_dominio" Type="String" />
                       <asp:ControlParameter ControlID="RadTreeViewGrupo" Name="gru_id" PropertyName="SelectedValue" Type="Int32" />
                       <asp:Parameter Name="paginaInicial" Type="Boolean" />
                       <asp:Parameter Name="startRowIndex" Type="Int32" />
                       <asp:Parameter Name="maximumRows" Type="Int32" />
                       <asp:Parameter Name="orderBy" Type="String" />
                   </SelectParameters>
               </asp:ObjectDataSource>
           </td>
       </tr>
   </table>



    </form>
</body>
</html>
