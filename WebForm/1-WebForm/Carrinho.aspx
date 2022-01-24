<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="_1_WebForm.Carrinho" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Validacao.js"></script>
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.6.js"></script>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="persist-area">
            <uc1:Cabecalho runat="server" ID="Cabecalho" />
             <asp:ScriptManager ID="ScriptManagerRiceli" runat="server">
                <Scripts>
                    <asp:ScriptReference Path="~/Service/ServiceRiceli.js" />
                </Scripts>
                <Services>
                    <asp:ServiceReference Path="Service/ServiceRiceli.svc" />
                </Services>
            </asp:ScriptManager>
            <div id="corpo">
                <div id="conteudo" style="width: 990px; margin-top: 5px;">
                    <div style="margin: auto; height: 40px; width: 620px; margin-top: 5px;">
                        <span class="BC_marcado">Carrinho</span>
                        <span class="BC_desmarcado">Identificação</span>
                        <span class="BC_desmarcado">Pagamento</span>
                        <span class="BC_desmarcado">Confirmação</span>
                        <div class="BC_seta" style="margin-left: 115px;">
                            25%
                            <img src="imagens/objetos/seta.png">
                        </div>
                    </div>
                    <asp:Panel ID="PanelCarrinhoCheio" runat="server">
                        <div class="btn_finalizar"><asp:LinkButton ID="LinkButtonPagamento" runat="server" PostBackUrl="~/CadastroPagamento.aspx">Finalizar Compra</asp:LinkButton></div>
                        <div style="clear: both; height: 10px;"></div>
                        <ul id="linha_carrinho">
                            <li style="width: 250px;">Produto</li>
                            <li style="width: 180px;">Quantidade</li>
                            <li style="width: 180px;">Valor Unitário</li>
                            <li style="width: 180px;">Valor Total</li>
                        </ul>
                        <hr />
                        <!-- Inicio linha produto -->
                        <div style="clear: both; height: 10px;"></div>
                        <asp:Repeater ID="RepeaterCarrinho" runat="server" EnableViewState="false">
                            <ItemTemplate>
                               <%----%> <div class="col1">
                                    <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("pro_nomeAmigavel") %>" style="vertical-align: top">
                                        <span id="PanelProdutoSkuComFoto" runat="server" visible='<%# (Eval("produtoSkuFotoDto") != null) %>'>
                                            <img title="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoDto.proSkuFot_nome") %>-v<%# Eval("produtoSkuFotoDto.proSkuFot_extensao") %>" style="float:left;width: 85px" />
                                        </span>
                                        <span id="PanelProdutoSkuSemFoto" runat="server" visible='<%# !(Eval("produtoSkuFotoDto") != null) %>'>
                                            <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg"  style="float:left;width: 85px" />
                                        </span></a> 
                                        <%# Eval("proSku_nome") %>
                                      <br />    
                                    <br />    
                                    <strong>Cor:</strong> <%# Eval("proSkuCor_nome") %><br>
                                    <strong>Tamanho:</strong> <%# Eval("proSkuTam_nome") %>
                                </div>
                                <div class="col2">
                                    <p>
                                        <asp:DropDownList ID="DropDownListCarrinhoQuantidade" runat="server" CssClass="form_list" EnableViewState="false" SelectedValue='<%# Eval("car_quantidade") %>'
                                            AutoPostBack="True" DataSource='<%# Enumerable.Range(1, (int)Eval("proSku_quantidadeDisponivel")) %>'
                                            OnSelectedIndexChanged="DropDownListCarrinhoQuantidade_SelectedIndexChanged">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="LinkButtonRemover" runat="server" CommandArgument='<%# Eval("proSku_id") %>' OnClick="LinkButtonRemover_Click">Remover</asp:LinkButton>
                                        <asp:HiddenField ID="HiddenFieldProSku_id" runat="server" Value='<%# Eval("proSku_id") %>' />

                                    </p>
                                    <p>&nbsp;</p>
                                    <p>
                                        Restam apenas<br>
                                        <asp:Panel ID="PanelProdutoSkuEstoque" runat="server" Visible='<%# (Boolean)Eval("proSku_disponivel") %>'>
                                            <%# Eval("proSku_quantidadeDisponivel") %> unidade(s) desse produto
                                        </asp:Panel>
                                        <asp:Panel ID="PanelProdutoSkuSemEstoque" runat="server" Visible='<%# !(Boolean)Eval("proSku_disponivel")%>'>
                                            Desculpe-nos mais este produto se tornou indisponível no momento, e não poderá ser incluído em sua compra.
                                        </asp:Panel>

                                    </p>
                                </div>
                                <div class="col3">R$ <%# Eval("proSku_precoVenda") %></div>
                                <div class="col4"><span class="preco">R$ <%# Eval("car_itemSubTotal") %></span></div>
                                <div class="col5">
                                    <asp:Panel ID="PanelProdutoSkuDesconto" runat="server" Visible='<%# ((decimal)Eval("proSku_percDesconto") > 0) %>' CssClass="produto_desconto">
                                        -<%# Eval("proSku_percDesconto","{0:P0}") %></asp:Panel>
                                </div>
                                <hr />

                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- Fim linha produto -->
                        <div style="clear: both; height: 10px;"></div>
                        Informe o CEP para consultar o prazo de entrega:

                         <asp:Panel runat="server" ID="PanelCalcPrazo" ClientIDMode="Static" DefaultButton="ButtonCalcPrazo" style="display:inline;">
                                                 Cep <asp:TextBox ID="TextBoxCep" ValidationGroup="groupCalcPrazo" ClientIDMode="Static" runat="server" MaxLength="8" Columns="8"></asp:TextBox>
                                                 <asp:Button ID="ButtonCalcPrazo" ValidationGroup="groupCalcPrazo" ClientIDMode="Static" runat="server" Text="Calcular" OnClientClick="return SelectCorreioCalcPrazo();" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="TextBoxCep" ErrorMessage="Insira o cep" ValidationGroup="groupCalcPrazo"></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="revCEP" ForeColor="red" runat="server" Text="Insira um CEP válido. Exemplo: 31030863 - não utilize traço"
                                                 ControlToValidate="TextBoxCep" ValidationGroup="groupCalcPrazo" ValidationExpression="\d{5}(\d{3})?" CssClass="MsgError" Display="Dynamic"></asp:RegularExpressionValidator>    
                                          
                                                 <span id="spanResultadoCalcPrazo"></span>
                                             </asp:Panel>

  
  
    <div style="clear: both; height: 10px;"></div>
                        <div id="Fecha_Carrinho">
                            <div style="width: 180px; text-align: justify; float: right; line-height: 25px;">
                                <strong>Subtotal:</strong>
                                <asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                               
                                <asp:Panel runat="server" ID="PanelEntregaGratis" CssClass="produto_frete">
                                                       <strong>Entrega:</strong> <span class="produto_frete">
                                    Grátis</span>
                                </asp:Panel>

                                 <strong>Total:</strong> <span class="preco">
                                    <asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></span><br />
                                <asp:Literal ID="LiteralCondicao" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="btn_finalizar"> <asp:LinkButton ID="LinkButtonPagamento2" runat="server" PostBackUrl="~/CadastroPagamento.aspx">Finalizar Compra</asp:LinkButton></div>
                        <div style="clear: both; height: 10px;"></div>

                    </asp:Panel>
                    <asp:Panel ID="PanelCarrinhoVazio" runat="server">
                        Nehum item foi adicionado ao seu carrinho.
                    </asp:Panel>


                </div>
            </div>
            <!-- Fecha div Conteudo -->

            <uc1:Rodape runat="server" ID="Rodape" />
        </div>
    </form>
</body>
</html>
