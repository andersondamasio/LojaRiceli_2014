<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="_1_WebForm.Index" EnableViewState="False" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Template Riceli</title>
      <asp:PlaceHolder ID="PlaceHolder1" runat="server" >
        <link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>css/jquery-ui.min.css" rel="stylesheet" />
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Produto.js"></script>
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.10.2.min.js"></script>
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-ui.min.js"></script>    
      </asp:PlaceHolder>


</head>
<body>
    <form id="form1" runat="server">
               <asp:ScriptManager ID="ScriptManagerRiceli" runat="server">
                 <Scripts>
                    <asp:ScriptReference Path="~/Service/ServiceRiceli.js" />
                </Scripts>
                <Services>
                    <asp:ServiceReference Path="Service/ServiceRiceli.svc"/>
                </Services>
            </asp:ScriptManager>
        <div>
            <%--
            <div id="dialog" title="Associe sua Conta do Facebook a Riceli">
  <asp:Panel ID="PanelLogin" runat="server" DefaultButton="ButtonEntrar">
                                <p>
                                    <label for="email">Email:</label>
                                    <asp:TextBox ID="TextBoxCli_email" runat="server" CssClass="form" ValidationGroup="groupLogin" Width="250"></asp:TextBox>
                                </p>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="groupLogin"
                                    ControlToValidate="TextBoxCli_email" CssClass="MsgError" SetFocusOnError="true" ErrorMessage="* Insira seu email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server" CssClass="MsgError"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxCli_email"
                                    ErrorMessage="* Email inválido" ValidationGroup="groupLogin" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                <p>
                                    <label for="email2">Senha:</label>
                                    <asp:TextBox ID="TextBoxCli_senha" runat="server" CssClass="form" ValidationGroup="groupLogin" TextMode="Password" Width="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="groupLogin" ControlToValidate="TextBoxCli_senha" CssClass="MsgError" SetFocusOnError="true" ErrorMessage="*Insira sua senha"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Button ID="ButtonEntrar" runat="server" CssClass="btn_form" Text="Entrar" ValidationGroup="groupLogin" />

                                </p>
         </asp:Panel>
</div>
                 --%>
            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />
                <div id="banner">
                    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/banner-modelo.jpg" width="800" height="237">
                </div>
                <div id="corpo">
                    <nav id="menuLateral">
                        <asp:Literal ID="LiteralMenu" runat="server"></asp:Literal>

                       <h1>Descontos e Ofertas</h1>
                        <div style="line-height: 25px;">
                             <p>Receba promoções em primeira mão.</p>
                            <p>
                                <input type="radio" name="radioRecebaPromocao" id="radio1" value="M" />
                                Sou homem
                                <input type="radio" name="radioRecebaPromocao" id="radio2" value="F" />
                                Sou Mulher
                            </p>
                            E-mail:<br/><input type="text" name="textfield" id="Text1" maxlength="128" style="height: 25px;">
                            <input name="imageField2" type="image" id="image1" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/bt_ok.png" align="middle" onclick="InsertBoletimInfo(); return false;">
                         </div>
                    </nav>

                    <div id="conteudo">
                        <h2>Os mais vendidos</h2>
                        <asp:ListView ID="ListViewProduto_Grupo" runat="server" DataSourceID="ObjectDataSourceProduto_Grupo">
                            <EmptyDataTemplate>
                                <span>Nenhum dado foi retornado.</span>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div class="produto">
                                    <asp:Repeater ID="RepeaterProdutoGrupoConteudo" runat="server" DataSource='<%# Eval("produtoSkuDto")%>'>
                                        <ItemTemplate>
                                            <div id="divProSkuConteudo<%# Eval("pro_id") %><%# Eval("proSku_id") %>" class="produtoInterno" style="display: none;">
                                                <asp:Panel ID="PanelProdutoSkuDesconto" runat="server" Visible='<%# ((decimal)Eval("proSku_percDesconto") > 0) %>' CssClass="produto_desconto">
                                                    -<%# Eval("proSku_percDesconto","{0:P0}") %></asp:Panel>
                                                <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("pro_nomeAmigavel") %>" title="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>">
                                                    <asp:Panel ID="PanelProdutoSkuComFoto" runat="server" Visible='<%# (Eval("produtoSkuFotoDto") != null) %>'>
                                                        <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoDto.proSkuFot_nome") %>-v<%# Eval("produtoSkuFotoDto.proSkuFot_extensao") %>" alt="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>"/>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PanelProdutoSkuSemFoto" runat="server" Visible='<%# !(Eval("produtoSkuFotoDto") != null) %>'>
                                                        <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg" alt="<%# Eval("proSku_nome") %>" height="232" width="160" />
                                                    </asp:Panel>
                                                    <div class="produto_nome">
                                                        <%# Eval("proSku_nome") %>
                                                    </div>
                                                </a>

                                                <asp:Panel ID="PanelProdutoSkuPreco" runat="server" Visible='<%# (Boolean)Eval("proSku_disponivel") %>'>
                                                    <span runat="server" id="PanelProSkuPrecoAnterior" visible='<%# ((decimal)Eval("proSku_percDesconto") > 0) %>'>De <%# Eval("proSku_precoAnterior") %>
                                                    </span>
                                                    <span class="produto_preco">Por <%# Eval("proSku_precoVenda") %></span>

                                                    <asp:Panel runat="server" ID="PanelParcelamentoSku" CssClass="produto_parcelas" Visible='<%#  !(bool)Eval("parcelamentoDto.parc_bloquear") %>'>
                                                        <asp:Repeater ID="RepeaterProdutoSkuParcelamentoParcela" runat="server" DataSource='<%# ((List<_2_Library.Dao.Site.Produto_GrupoX.ParcelamentoParcelaDto>)Eval("parcelamentoDto.parcelamentoParcelaDto")).OrderByDescending(s=>s.parcPar_quantidade).GroupBy(s => s.parcPar_percentualJuro).Select(s => s.FirstOrDefault()).OrderBy(s=>s.parcPar_percentualJuro)  %>'>
                                                                <ItemTemplate>
                                                                      <asp:Panel runat="server" ID="PanelSemJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") == 0 %>' Wrap="false">
                                                                         em até <%# Eval("parcPar_quantidade") %>x 
                                                                         R$ <%# Eval("parcPar_valor","{0:N}") %> 
                                                                    </asp:Panel>
                                                                     <asp:Panel runat="server" ID="PanelComJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") != 0 %>' Wrap="false">
                                                                         ou até <%# Eval("parcPar_quantidade") %>x 
                                                                          R$ <%# Eval("parcPar_valor","{0:N}") %>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                    </asp:Panel>

                                                    <asp:Panel runat="server" ID="PanelEntregaGratis" CssClass="produto_frete" Visible='<%#!(bool)Eval("entregaDto.ent_bloquear") &&  (bool)Eval("entregaDto.ent_gratis") %>'>
                                                        Frete Grátis
                                                    </asp:Panel>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelProdutoSkuIndisponivel" runat="server" Visible='<%# !(Boolean)Eval("proSku_disponivel") %>' Style="height: 51px">
                                                    Indisponível
                                                </asp:Panel>

                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterProdutoGrupoCor" runat="server" DataSource='<%# Eval("produtoSkuDto")%>'>
                                        <ItemTemplate>
                                            <span id='divCor<%# Eval("proSku_id") %>'>
                                              <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("pro_nomeAmigavel") %>" style="text-decoration:none;" >
                                                <img id='proSkuCor' onload="if(<%#Container.ItemIndex%> == 0)SelecionaProSkuCor('<%# Eval("pro_id") %>','<%# Eval("proSku_id") %>')" onerror="SelecionaProSkuCorOnError(this);" onmouseover="SelecionaProSkuCor('<%# Eval("pro_id") %>','<%# Eval("proSku_id") %>')" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" style="margin: 3px; cursor: pointer" />
                                               </a>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>   
                                </div>
                               
                                <%# (Container.DisplayIndex+1) % 3 == 0 ? "<div style=\"clear: both; height: 10px;\"></div>" : string.Empty  %>

                            </ItemTemplate>
                            <LayoutTemplate>
                                <div style="" id="itemPlaceholderContainer" runat="server">
                                    <span runat="server" id="itemPlaceholder" />
                                </div>
                                <div style="display: none">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>

                        </asp:ListView>
                    </div>
                </div>
            </div>

            <uc1:Rodape runat="server" ID="Rodape" />

            <asp:ObjectDataSource ID="ObjectDataSourceProduto_Grupo" runat="server"
                SelectMethod="SelectProduto_GrupoInicial" TypeName="_2_Library.Dao.Site.Produto_GrupoX.Produto_GrupoTd"
                EnablePaging="True" SelectCountMethod="SelectProduto_GrupoInicialCount">
                <SelectParameters>
                    <asp:Parameter Name="loj_dominio" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                    <asp:Parameter Name="orderBy" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>
