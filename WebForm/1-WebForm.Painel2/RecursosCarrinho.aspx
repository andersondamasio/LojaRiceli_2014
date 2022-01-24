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
                        <script language="javascript" type="text/javascript">
                            $(function () {
                                $("#TextBoxDataMaior").datepicker({maxDate: "+12M +10D" });
                            });
                        </script>
                    Data maior que:
                    <asp:TextBox ID="TextBoxDataMaior" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:Button ID="ButtonExcluir" runat="server" OnClick="ButtonExcluir_Click" OnClientClick="return confirm('Pode ter clientes usando esses carrinhos,tem certeza que deseja excluir todos esses registro?');" Text="Excluir" />

                    <asp:ListView ID="ListViewCarrinho" runat="server" DataSourceID="ObjectDataSourceCarrinho">                       
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
                                     <%# Eval("cli_id") %> 
                                </td>
                                <td>
                                   <%# Eval("cliente.cli_nome") %>
                                </td>
                                <td>
                                    <asp:Label ID="proSku_idLabel" runat="server" Text='<%# Eval("proSku_id") %>' />
                                </td>
                                <td>
                                 <%# Eval("proSku_nome") %>
                                </td>
                                  <td>
                                 <%# Eval("proSkuCor_nome") %>
                                      </td>
                                <td>
                                     <%# Eval("proSkuTam_nome") %>
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
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton8" Text="Cód Cliente" CommandName="Sort" CommandArgument="cliente.cli_nome" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton3" Text="Nome Cliente" CommandName="Sort" CommandArgument="cli_id" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton1" Text="Sku" CommandName="Sort" CommandArgument="proSku_id" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton7" Text="Nome" CommandName="Sort" CommandArgument="proSku_nome" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton5" Text="Cor" CommandName="Sort" CommandArgument="proSkuCor_nome" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton6" Text="Tamanho" CommandName="Sort" CommandArgument="proSkuTam_nome" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton2" Text="Quantidade" CommandName="Sort" CommandArgument="car_quantidade" /></th>
                                                <th runat="server"><asp:LinkButton runat="server" ID="LinkButton4" Text="Data e Hora" CommandName="Sort" CommandArgument="car_dataHora" /></th>
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

                    <asp:ObjectDataSource ID="ObjectDataSourceCarrinho" runat="server"
                        SelectMethod="SelectCarrinho"
                        TypeName="_2_Library.Dao.Painel.CarrinhoX.CarrinhoTd"
                        EnablePaging="True" SelectCountMethod="SelectCarrinhoCount" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:Parameter Name="loj_dominio" Type="String" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                            <asp:Parameter Name="orderBy" Type="String" DefaultValue="car_id" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
