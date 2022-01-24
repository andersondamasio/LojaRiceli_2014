<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeusPedidos.aspx.cs" Inherits="_1_WebForm.MeusPedidos" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
         <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.6.js"></script>
    </asp:PlaceHolder>
</head>
<body>

    <form id="form2" runat="server">
        <div>

            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />
                
                <div id="corpo">
                    <div id="conteudo" style="width: 990px; margin-top: 5px;">
                        <h1>Minha Conta</h1>
                        <div id="Pedidos">
                            <div style="border-bottom: 1px solid #8A8A7B; height: 30px;">
                                <ul class="abas">
                                    <li class="aba2"><a href="MeuCadastro">Meu Cadastro</a></li>
                                    <li class="aba2" runat="server"><a href="MeusEnderecos">Meus Endereços</a></li>
                                    <li>Meus Pedidos</li>
                                    <li class="aba2"><a href="MeusAmigos">Meus Amigos</a></li>
                                    <li class="aba2"><a href="MeusPontos">Meus Pontos</a></li>
                                </ul>
                            </div>
                            <div class="pedido">
                                 <asp:Panel runat="server" ID="PanelPedidosMensagem">
                                    Nenhum Pedido encontrado, Faça seu um Pedido, garantimos lhe proporcionar o melhor atendimento possível.
                                </asp:Panel>

                                <asp:Repeater ID="RepeaterPedidos" runat="server">
                                    <ItemTemplate>
                                        <div class="colunaPrincipal">
                                            <p>
                                                Nº do Pedido<br>
                                                <strong><%# Eval("ped_id") %></strong>
                                            </p>
                                            <p>&nbsp;</p>
                                            <p>
                                                <a href="#" onclick="alert('O status atual do seu Pedido é: <%# Eval("statusDto.stat_nome") %>')">Verificar Status</a><br>
                                                <br />
                                                <strong>
                                                    <img src="imagens/objetos/seta_mostrar.png" alt="Mostrar Produto" style="vertical-align: middle;">
                                                   <a onclick="ObjetoVisible('ProdutosPedido<%# Eval("ped_id") %>');">Exibir Produtos</a> </strong>
                                                &nbsp;
                                            </p>
                                        </div>

                                        <div class="coluna2">
                                            <strong>Realizado em</strong><br>
                                            <%# Eval("ped_dataHora") %>
                                        </div>
                                        <div class="coluna3">
                                            <strong>Forma de Pagamento</strong><br>
                                            <%# Eval("ped_forPag_nome") %>
                                        </div>
                                        <div class="coluna4">
                                            <strong>Prazo de Entrega</strong><br>
                                            <%# Eval("ped_ent_prazo") %> dias úteis após a confirmação de pagamento.
                                        </div>
                                        <div class="coluna5">
                                            <strong>Endereço</strong><br>
                                            <%# Eval("ped_cliEnd_nome") %> <%# Eval("ped_cliEnd_sobrenome") %><br />
                                            <%# Eval("ped_cliEnd_endereco") %>, <%# Eval("ped_cliEnd_numero") %>, <%# Eval("ped_cliEnd_referencia") %><br />
                                            <%# Eval("ped_cliEnd_cep") %>, <%# Eval("ped_cliEnd_bairro") %> <%# Eval("ped_cliEnd_cidade") %> - <%# Eval("ped_cliEnd_estado") %><br />
                                            (<%# Eval("ped_cliEnd_ddd1") %>) <%# Eval("ped_cliEnd_fone1") %>
                                        </div>
                                        <div class="coluna6">
                                            <strong>Itens</strong><br>
                                            <%# Eval("PedidoProdutoDto.Count") %>
                                        </div>
                                        <div class="coluna7">
                                            <strong>Condição</strong><br>
                                            <%# Eval("ped_forPagPar_condicao") %>
                                        </div>

                                        <!-- Produtos -->
                                        <div id="ProdutosPedido<%# Eval("ped_id") %>" class="barra_produtos" style="background-color: #F0F0F0; display:none">
                                            <span style="width: 400px;">Descrição do Produto</span><span> SKU</span><span> Qtd </span><span>Preço Unitário </span><span>Preço Total</span>
                                            <div id="Produtos" style="font-weight: normal; position: relative">
                                                <asp:Repeater ID="RepeaterPedidoProduto" runat="server" DataSource='<%# Eval("PedidoProdutoDto") %>'>
                                                    <ItemTemplate>
                                                        <span style="width: 400px;">
                                                            <%# Eval("pedPro_pro_nome") %>
                                                        </span>
                                                        <span>
                                                            <%# Eval("pedPro_proSku_id") %>
                                                        </span>
                                                        <span>
                                                            <%# Eval("pedPro_car_quantidade") %>
                                                        </span>
                                                        <span>
                                                            <%# Eval("pedPro_proSku_precoVenda","{0:N}") %>
                                                        </span>
                                                        <span>
                                                            <%# Eval("pedPro_proSku_precoVendaTotal","{0:N}") %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>

                                            <div>
                                            </div>
                                        </div>
                                        <br />
                                         <table style="border: 2px solid black; width: 100%">
                                            <tr class="barra_produtos">
                                                <td>Status</td>
                                                <td>Descrição</td>
                                                <td>Data e Hora</td>
                                            </tr>
                                            <asp:Repeater ID="RepeaterPedidoStatus" runat="server" DataSource='<%# Eval("PedidoStatusDto") %>'>

                                                <ItemTemplate>
                                                    <tr> 
                                                        <td><%# Eval("statusDto.stat_nome") %> <asp:LinkButton ID="LinkButtonEfetuarPagamento" runat="server" CommandArgument='<%# Eval("statusDto.ped_id") %>' Visible='<%# Container.ItemIndex == 0 %>' OnClick="LinkButtonEfetuarPagamento_Click" OnClientClick="this.innerHTML='Processando Pedido...';this.disabled = true;document.getElementById(this.id).disabled = true;__doPostBack(this.id,'');" >Efetuar Pagamento</asp:LinkButton> </td>
                                                        <td><%# Eval("statusDto.stat_descricao") %></td>
                                                        <td><%# Eval("pedStat_dataHora") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                              <%= RepeaterPedidos.FindControl("RepeaterPedidoStatus") %>

                                        </table>
                                         <div style="clear: both; border: 5px solid #CCC; margin: 30px;"></div>
                                        <!-- Outro Pedido -->
                                    </ItemTemplate>
                                </asp:Repeater>

                                <!-- Fim segundo pedido -->
                            </div>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                    </div>
                    <!-- Fecha div Conteudo -->
                </div>

                <uc1:Rodape runat="server" ID="Rodape" />
            </div>
           
    </form>
</body>
</html>
