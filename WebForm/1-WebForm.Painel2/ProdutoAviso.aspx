<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProdutoAviso.aspx.cs" Inherits="Loja.Painel.ProdutoAviso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>

</head>
<body>
    <form id="form1" runat="server">    
        <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />

                    <asp:ListView ID="ListViewProdutoAviso" runat="server" DataKeyNames="proSkuAvi_id,loj_id" DataSourceID="EntityDataSourceProdutoAviso" InsertItemPosition="None">                        
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>Nenhum dado foi retornado.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="proSkuAvi_emailLabel" runat="server" Text='<%# Eval("proSkuAvi_email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="proSkuAvi_nomeLabel" runat="server" Text='<%# Eval("proSkuAvi_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="proSku_idLabel" runat="server" Text='<%# Eval("proSku_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ClienteLabel" runat="server" Text='<%# Eval("Cliente.cli_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ProdutoSkuLabel" runat="server" Text='<%# Eval("ProdutoSku.proSku_nome") %>' />
                                </td>
                                   
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">                                           
                                                <th runat="server">Email</th>
                                                <th runat="server">Nome</th>
                                                <th runat="server">Cod.Sku</th>
                                                <th runat="server">Cliente</th>
                                                <th runat="server">Nome Sku</th>
                                                
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                     <asp:DataPager ID="DataPagerProdutoAviso" runat="server" PageSize="5" PagedControlID="ListViewProdutoAviso">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                ShowPreviousPageButton="true" FirstPageText="Primeiro" LastPageText="Último"
                                NextPageText="Próximo" PreviousPageText="Anterior" />
                            <asp:NumericPagerField Visible="false" />
                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="true"
                                ShowPreviousPageButton="false" FirstPageText="Primeiro" LastPageText="Último"
                                NextPageText="Próximo" PreviousPageText="Anterior" />
                            <asp:TemplatePagerField>
                                <PagerTemplate>
                                    <b>registros de
                                                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                                        a
                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                                        (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                        registros) </b>

                                    <%# (this.DataPagerProdutoAviso.Visible = Container.TotalRowCount > this.DataPagerProdutoAviso.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                    <asp:EntityDataSource ID="EntityDataSourceProdutoAviso" runat="server" ConnectionString="name=LojaEntities" 
                        DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False"
                         EnableInsert="True" EnableUpdate="True" EntitySetName="ProdutoSkuAviso" Include="ProdutoSku,Cliente" 
                        EntityTypeFilter="ProdutoSkuAviso" Where="it.[loj_id] = @loj_id">
                        <WhereParameters>
                          <CustomControls:CustomParameterLoja Name="loj_id" DbType="Int32"   />
                        </WhereParameters>
                    </asp:EntityDataSource>

                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
