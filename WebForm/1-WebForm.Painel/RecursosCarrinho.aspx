<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecursosCarrinho.aspx.cs" Inherits="Loja.Painel.RecursosCarrinho" %>

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

                    <asp:ListView ID="ListViewCarrinho" runat="server" DataSourceID="EntityDataSourceCarrinho">                       
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
                                    <%# Eval("cli_id") %><%# Eval("Cliente") is DBNull ?  string.Empty : "("+Eval("Cliente.cli_nome")+")" %>
                                </td>
                                <td>
                                    <asp:Label ID="proSku_idLabel" runat="server" Text='<%# Eval("proSku_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="car_quantidadeLabel" runat="server" Text='<%# Eval("car_quantidade") %>' />
                                </td>
                                 <td>
                                    <asp:Label ID="car_dataHoraLabel" runat="server" Text='<%# Eval("car_dataHora") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">
                                                
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton3" Text="Cliente" CommandName="Sort" CommandArgument="cli_id" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton1" Text="Sku" CommandName="Sort" CommandArgument="proSku_id" /></th>
                                                <th id="Th2" runat="server"><asp:LinkButton runat="server" ID="LinkButton2" Text="Quantidade" CommandName="Sort" CommandArgument="car_quantidade" /></th>
                                                <th id="Th1" runat="server"><asp:LinkButton runat="server" ID="LinkButton4" Text="Data e Hora" CommandName="Sort" CommandArgument="car_dataHora" /></th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                                        <asp:DataPager ID="DataPagerCarrinho" runat="server" PageSize="5" PagedControlID="ListViewCarrinho">
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

                                    <%# (this.DataPagerCarrinho.Visible = Container.TotalRowCount >= this.DataPagerCarrinho.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>

                    <asp:EntityDataSource ID="EntityDataSourceCarrinho" runat="server" ConnectionString="name=LojaEntities"
                         DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Carrinho" 
                        Select="it.[car_id], it.[car_quantidade], it.[car_dataHora], it.[cli_id], it.[proSku_id],it.[Cliente],it.[ProdutoSku]"
                        Where="it.[loj_id] = @loj_id" OrderBy="it.[car_id] desc">
                        <WhereParameters>
                            <asp:SessionParameter Type="Int32" Name="loj_id" SessionField="loj_id" />
                        </WhereParameters>
                       
                    </asp:EntityDataSource>


                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
