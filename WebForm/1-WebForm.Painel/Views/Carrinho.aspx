<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="Loja.Views.Carrinho" %>


<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Views/js/WebServiceFrete.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="~/Servicos/WebServiceFrete.asmx" InlineScript="true" />
            </Services>
        </asp:ScriptManager>
        <uc1:Cabecalho ID="Cabecalho1" runat="server" />
     
        <asp:Panel ID="PanelCarrinhoCheio" runat="server">
          <table border="2">
            <asp:Repeater runat="server" ID="RepeaterCarrinho" EnableViewState="false">               
                <ItemTemplate>
                    <tr valign="top">
                        <td rowspan="3">
                            <img id="proSkuFotM<%#Container.ItemIndex%>" onclick="trocaImagemProdutoDetalhe(this,'<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>','<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>');" onerror="semFotoProdutoVitrine(this, '<%# Eval("loj_id") %>')" src="<%= Request.ApplicationPath.TrimEnd('/') %>/imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>-m<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>" style="max-height: 40px" alt="<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>" />
                        </td>
                        <td><%# Eval("pro_nome") %></td>
                        <td><%# Eval("proSkuCor_nome") %></td>
                        <td><%# Eval("proSku_precoVenda","{0:N}") %></td>
                        <td>
                            <asp:Literal ID="car_totalItemLiteral" runat="server" Text='<%# ((Decimal)Eval("proSku_precoVenda") * (int)Eval("car_quantidade")).ToString("c") %>'></asp:Literal></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><%# Eval("proSkuTam_nome") %><asp:HiddenField ID="HiddenFieldProSku_id" runat="server" Value='<%# Eval("proSku_id") %>' />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonProdutoSkuRemover" runat="server" OnClick="LinkButtonProdutoSkuRemover_Click" CommandArgument='<%# Eval("proSku_id") %>'>Remover</asp:LinkButton></td>
                        <td>
                            <asp:DropDownList ID="DropDownListCarrinhoQuantidade" runat="server" SelectedValue='<%# Eval("car_quantidade") %>'
                                AutoPostBack="True" DataSource='<%# Enumerable.Range(1, (int)Eval("proSku_quantidadeDisponivel")) %>'
                                OnSelectedIndexChanged="DropDownListCarrinhoQuantidade_SelectedIndexChanged">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
                    <tr>
                        <td colspan="3">Infome o CEP para consultar o prazo de entrega</td>
                        <td>SubTotal</td>
                        <td><asp:Literal ID="car_subTotalLiteral" runat="server" Text="0,00"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel runat="server" ID="PanelCalculaFrete" DefaultButton="ButtonCalcularFrete">
                                <asp:TextBox ID="car_cepTextBox" runat="server" MaxLength="9" Columns="9" ValidationGroup="groupCalculaCep"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="car_cepTextBox" SetFocusOnError="true" Display="Dynamic" ValidationGroup="groupCalculaCep" ErrorMessage="Coloque seu cep"></asp:RequiredFieldValidator>
                            <asp:Button ID="ButtonCalcularFrete" runat="server" Text="Consultar frete" ValidationGroup="groupCalculaCep" OnClientClick="if(Page_ClientValidate('groupCalculaCep')){ CalculaPrazo();return false;}" />
                                </asp:Panel>
                            <img id="imgFreteCarregando" style="display:none" src="..<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/outros/zoomloader.gif"/>
                        </td>
                        <td>Entrega</td>
                        <td ><span id="car_freteSpan"><asp:Literal ID="car_freteLiteral" runat="server" Text=""></asp:Literal></span></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td>Total</td>
                        <td >
                            <asp:Literal ID="car_totalLiteral" runat="server" Text="0,00"></asp:Literal><br />
                            <asp:Literal ID="car_parcelamentoLiteral" runat="server" Text="0,00"></asp:Literal>
                        </td>
                    </tr>
                    </table>

            <asp:Button ID="ButtonFinalizarPedido" runat="server" Text="Finalizar Compra" OnClick="ButtonFinalizarPedido_Click" />

            </asp:Panel>
    
        <asp:Panel ID="PanelCarrinhoVazio" runat="server">
            <br/>
            Seu Carrinho está vazio.
            <br/>

        </asp:Panel>


        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
