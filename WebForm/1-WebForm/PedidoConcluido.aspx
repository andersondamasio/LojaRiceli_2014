<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidoConcluido.aspx.cs" Inherits="_1_WebForm.PedidoConcluido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>SEU PEDIDO FOI REALIZADO COM SUCESSO</h1>
            <br />

            <asp:FormView ID="FormViewPedidoConcluido" runat="server">

                <ItemTemplate>

                    <b>O número do seu pedido é: <%# Eval("ped_id") %></b><br />
                    <br />

                    Resumo do pedido:<br />

                    <asp:Repeater ID="RepeaterPedidoProduto" runat="server" DataSource='<%# Eval("pedidoProdutoDto") %>'>
                        <ItemTemplate>
                            Produto:  <%# Eval("pedPro_pro_nome") %>  <%# Eval("pedPro_proSkuCor_nome") %> <%# Eval("pedPro_proSkuTam_nome") %> (<%# Eval("pedPro_proSku_id") %>)<br />
                            Qtde: <%# Eval("pedPro_car_quantidade") %><br />
                            Valor Unitário: <%# Eval("pedPro_proSku_precoVenda") %><br />
                            Valor Total: <%# Eval("pedPro_proSku_precoVendaTotal") %><br />
                            <hr />
                            <br />
                            <br />


                        </ItemTemplate>
                    </asp:Repeater>

                    Total SubTotal: <%# Eval("ped_subTotal") %><br />
                    Total Descontos: <%# Eval("ped_descontos") %><br />
                    Total Frete: <%# Eval("ped_ent_valor") %><br />
                    Total Pedido: <%# Eval("ped_total") %>
                    <br />
                    <br />
                    <br />


                    Endereço de Entrega:<br />
                    <%# Eval("ped_cliEnd_nome") %><br />
                    <%# Eval("ped_cliEnd_endereco") %> <%# Eval("ped_cliEnd_numero") %> <%# Eval("ped_cliEnd_complemento") %> <%# Eval("ped_cliEnd_referencia") %><br />
                    <%# Eval("ped_cliEnd_bairro") %> - <%# Eval("ped_cliEnd_cidade") %> - <%# Eval("ped_cliEnd_estado") %>
                    <br />
                    CEP:<%# Eval("ped_cliEnd_cep") %><br />

                </ItemTemplate>


            </asp:FormView>
        </div>
        <br />
        Por favor acompanhe mais informações diariamente sobre seu pedido em:  <a href="<%= Page.GetRouteUrl("MeusPedidos", null) %>" title="Meus Pedidos"><%= HttpContext.Current.Request.Url.Host+Page.GetRouteUrl("MeusPedidos", null) %></a>

    </form>
</body>
</html>
