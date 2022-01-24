<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProdutoDetalhe.aspx.cs" Inherits="_1_WebForm.ProdutoDetalhe" %>

<%@ Import Namespace="_2_Library.Dao.Site.ProdutoSkuX" %>
<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<%@ Register Src="~/UserControls/SocialLinksCompartilhar.ascx" TagPrefix="uc1" TagName="SocialLinksCompartilhar" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template Riceli</title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.6.js"></script>
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery.jqzoom-core.js"></script>
        <link rel="stylesheet" href="<%= Page.GetRouteUrl("PaginaInicial", null) %>css/jquery.jqzoom.css" type="text/css" />
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/ProdutoDetalhe.js"></script>
    </asp:PlaceHolder>
    <script type="text/javascript">
        
    </script>
    <style>
        .zoomThumbActive
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManagerRiceli" runat="server">
                <Scripts>
                    <asp:ScriptReference Path="~/Service/ServiceRiceli.js" />
                </Scripts>
                <Services>
                    <asp:ServiceReference Path="Service/ServiceRiceli.svc" />
                </Services>
            </asp:ScriptManager>

            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />

                <div id="corpo">
                    <div id="conteudo" style="width: 990px; margin-top: 5px;">
                        <ul id="breadcrumb">
                            <li><a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>" title="Página inicial">Início</a></li>
                            <asp:Repeater ID="RepeaterGrupoCaminho" runat="server" DataSourceID="ObjectDataSourceGrupoCaminho">
                                <ItemTemplate>
                                    <li><a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("gru_nomeAmigavel") %>"><%# Eval("gru_nome") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:ObjectDataSource ID="ObjectDataSourceGrupoCaminho" runat="server"
                            SelectMethod="SelectGrupoCaminho" TypeName="_2_Library.Dao.Site.GrupoX.GrupoTd" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="loj_dominio" Type="String" />
                                <asp:RouteParameter Name="proSku_id" RouteKey="proSku_id" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <hr />
                        <asp:Repeater ID="RepeaterProdutoSkuGrupoDetalhe" runat="server">
                            <ItemTemplate>
                                <div id="ColunaFotos">
                                    <div style="float: left">
                                        <asp:Repeater ID="RepeaterProdutoSkuFoto" runat="server" DataSource='<%# Eval("produtoSkuFotoDto")%>'>
                                            <ItemTemplate>
                                                <div>
                                                    <a id="mini<%# Container.ItemIndex %>" onmouseover="this.click();" href='javascript:void(0);' style="cursor: pointer;"
                                                        rel="{gallery: 'gal1', 
                                                        smallimage: './imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("proSkuFot_nome") %>-d<%# Eval("proSkuFot_extensao") %>',
                                                        largeimage: './imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("proSkuFot_nome") %>-a<%# Eval("proSkuFot_extensao") %>'}">
                                                        <img style="border: 1px solid #666; margin-bottom: 10px" onerror="SelecionaProSkuOnError(this);" src='imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("proSkuFot_nome") %>-m<%# Eval("proSkuFot_extensao") %>' />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <span id="SpanProdutoSkuSemFotoRepeater" runat="server" visible='<%# ((List<ProdutoSkuFotoDto>)Eval("produtoSkuFotoDto")).Count == 0 %>'>
                                            <a id="mini<%# Container.ItemIndex %>" onmouseover="this.click();" href='javascript:void(0);' style="cursor: pointer;"
                                                rel="{gallery: 'gal1', 
                                                        smallimage: './imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg',
                                                        largeimage: './imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg'}">
                                                <img style="border: 1px solid #666; margin-bottom: 10px" onerror="SelecionaProSkuOnError(this);" src='imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg' width="37" height="54" />
                                            </a>
                                        </span>
                                    </div>
                                    <script>
                                        <%# ((List<ProdutoSkuFotoDto>)Eval("produtoSkuFotoDto")).Count == 1  ? 
                                         "$('#mini0').attr({ rel: '' });" : string.Empty%>
                                    </script>

                                    <span id="SpanProdutoSkuComFoto" runat="server" visible='<%# (Eval("produtoSkuFotoUDto") != null) %>'>
                                        <a href="imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoUDto.proSkuFot_nome") %>-a<%# Eval("produtoSkuFotoUDto.proSkuFot_extensao") %>" class="jqzoom" rel='gal1' title="<%# Eval("proSku_nome") %>">
                                            <img style="float: left;" alt="<%# Eval("proSku_nome") %>" src="imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoUDto.proSkuFot_nome") %>-d<%# Eval("produtoSkuFotoUDto.proSkuFot_extensao") %>" title="<%# Eval("proSku_nome") %>" />
                                        </a>
                                    </span>

                                    <span id="SpanProdutoSkuSemFoto" runat="server" visible='<%# !(Eval("produtoSkuFotoUDto") != null) %>'>
                                        <a href="imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg" class="jqzoom" rel='gal1' title="<%# Eval("proSku_nome") %>">
                                            <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg" width="276" height="400" />
                                        </a>
                                    </span>

                                </div>
                                <div id="ColunaDados">
                                    <h1 id="h1NomeProduto"><%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %></h1>
                                    <p><a href="#InfoProduto">Veja mais informações sobre o produto</a></p>

                                    <asp:Panel ID="PanelProdutoSkuEstoque" runat="server" CssClass="estoque" Visible='<%# (Boolean)Eval("proSku_disponivel") && ((int?)Eval("proSku_quantidadeDisponivel")).HasValue %>'>
                                        Há apenas <strong><%# Eval("proSku_quantidadeDisponivel") %></strong> em estoque
                                    </asp:Panel>
                                    <h2>Selecione o tamanho </h2>
                                    <p>
                                        <div style="padding: 5px; height: 30px;" id="tamanhos">
                                            <p>
                                                <asp:Repeater ID="RepeaterProdutoSkuTamanho" runat="server" DataSource='<%# Eval("produtoSkuCoresTamanhos2Dto")%>'>
                                                    <ItemTemplate>
                                                        <span onclick="SelecionarProSkuTamanho('<%# Eval("produtoSkuDto.proSku_id") %>')" id="proSkuTamanho<%# Eval("produtoSkuDto.proSku_id") %>" <%# ((bool)Eval("produtoSkuDto.proSku_disponivel")) ? string.Empty : "disponivel=\"false\" style=\"background:url(imagens/objetos/tam_ind.png)\"" %> title="Selecionar tamanho <%# Eval("produtoSkuDto.proSkuTam_nome") %>"><%# Eval("produtoSkuDto.proSkuTam_nome") %></span></p>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                        </div>
                                        <h2>Selecione a cor </h2>
                                        <div id="cores">
                                            <asp:Repeater ID="RepeaterProdutoSkuCor" runat="server" DataSource='<%# Eval("produtoSkuCoresTamanhos1Dto")%>'>
                                                <ItemTemplate>
                                                    <span id="SpanProdutoSkuComFoto" runat="server" visible='<%# (Eval("produtoSkuDto.produtoSkuFotoUDto") != null) %>'>
                                                        <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+ _2_Library.Utils.Tratamento.GerarNomeAmigavel((string)Eval("produtoSkuDto.proSku_nome") +"-"+ Eval("produtoSkuDto.proSkuCor_nome") +"-"+ Eval("produtoSkuDto.proSkuTam_nome"))+"-"+ Eval("produtoSkuDto.proSku_id") %>">
                                                            <img <%# RouteData.Values["proSku_id"].Equals(Eval("produtoSkuDto.proSku_id")) ? "style=\"border: 1px solid #666;\"" : string.Empty %> title="Selecionar cor <%# Eval("produtoSkuDto.proSkuCor_nome") %>" alt="cor <%# Eval("produtoSkuDto.proSkuCor_nome") %>" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("produtoSkuDto.produtoSkuFotoUDto.loj_id") %>/<%# Eval("produtoSkuDto.produtoSkuFotoUDto.pro_id") %>/<%# Eval("produtoSkuDto.produtoSkuFotoUDto.proSkuFot_nome") %>-m<%# Eval("produtoSkuDto.produtoSkuFotoUDto.proSkuFot_extensao") %>" />
                                                        </a>
                                                    </span>
                                                    <span id="SpanProdutoSkuSemFoto" runat="server" visible='<%# !(Eval("produtoSkuDto.produtoSkuFotoUDto") != null) %>'>
                                                        <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+ _2_Library.Utils.Tratamento.GerarNomeAmigavel((string)Eval("produtoSkuDto.proSku_nome") +"-"+ Eval("produtoSkuDto.proSkuCor_nome") +"-"+ Eval("produtoSkuDto.proSkuTam_nome"))+"-"+ Eval("produtoSkuDto.proSku_id") %>">
                                                            <img <%# RouteData.Values["proSku_id"].Equals(Eval("produtoSkuDto.proSku_id")) ? "style=\"border: 1px solid #666;\"" : string.Empty %> title="Selecionar cor <%# Eval("produtoSkuDto.proSkuCor_nome") %>" alt="cor <%# Eval("produtoSkuDto.proSkuCor_nome") %>" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("produtoSkuDto.loj_id") %>/sem-foto/sem-foto.jpg" width="39" height="56" />
                                                        </a>
                                                    </span>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <h2>Compartilhe</h2>  
                                        <uc1:SocialLinksCompartilhar runat="server" id="SocialLinksCompartilhar" />
                                        <div>
                                            <a id="spanConsultaPrazo" onclick="ObjetoVisible('PanelCalcPrazo');" style="cursor:pointer">Consulte o prazo de entrega</a>
                                             <asp:Panel runat="server" ID="PanelCalcPrazo" ClientIDMode="Static" DefaultButton="ButtonCalcPrazo" Style="display:none;">
                                                 Cep <asp:TextBox ID="TextBoxCep" ValidationGroup="groupCalcPrazo" ClientIDMode="Static" runat="server" MaxLength="8" Columns="8"></asp:TextBox>
                                                 <asp:Button ID="ButtonCalcPrazo" ValidationGroup="groupCalcPrazo" ClientIDMode="Static" runat="server" Text="Calcular" OnClientClick="return SelectCorreioCalcPrazo();" />
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="TextBoxCep" ErrorMessage="Insira o cep" ValidationGroup="groupCalcPrazo"></asp:RequiredFieldValidator>
                                                 <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="TextBoxCep"></asp:FilteredTextBoxExtender>
                                                  <span id="spanResultadoCalcPrazo"></span>
                                             </asp:Panel>
                                        </div>
                                </div>
                                <div id="ColunaPreco">

                                    <asp:Repeater ID="RepeaterProdutoSkuBotaoComprar" runat="server" DataSource='<%# ((IEnumerable<_2_Library.Dao.Site.ProdutoSkuX.ProdutoSkuCoresTamanhosDto>)Eval("produtoSkuCoresTamanhos2Dto")).OrderBy(s=>s.produtoSkuDto.proSku_precoVenda)%>'>
                                        <ItemTemplate>
                                            <span id="SpanPanelProdutoSkuPreco<%# Eval("produtoSkuDto.proSku_id") %>" style="<%# Container.ItemIndex == 0 ? string.Empty: "display:none" %>">
                                                <asp:Panel ID="PanelProdutoSkuPreco" runat="server" Visible='<%# (Boolean)Eval("produtoSkuDto.proSku_disponivel") %>'>

                                                    <asp:Panel runat="server" ID="PanelProdutoSkuPrecoAnterior" Visible='<%# ((decimal)Eval("produtoSkuDto.proSku_percDesconto") > 0) %>'>De <%# Eval("produtoSkuDto.proSku_precoAnterior") %></asp:Panel>
                                                    <div class="preco">Por <%# Eval("produtoSkuDto.proSku_precoVenda") %></div>
                                                    <asp:Panel runat="server" ID="PanelParcelamentoSku" CssClass="parcelamento" Visible='<%#  !(bool)Eval("produtoSkuDto.parcelamentoDto.parc_bloquear") %>'>

                                                        <p style="text-align: right;">
                                                            <asp:Repeater ID="RepeaterProdutoSkuParcelamentoParcela" runat="server" DataSource='<%# ((List<_2_Library.Dao.Site.Produto_GrupoX.ParcelamentoParcelaDto>)Eval("produtoSkuDto.parcelamentoDto.parcelamentoParcelaDto")).OrderByDescending(s=>s.parcPar_quantidade).GroupBy(s => s.parcPar_percentualJuro).Select(s => s.FirstOrDefault()).OrderBy(s=>s.parcPar_percentualJuro)  %>'>
                                                                <ItemTemplate>
                               
                                                                    <asp:Panel runat="server" ID="PanelSemJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") == 0 %>' Wrap="false">
                                                                          em até <%# Eval("parcPar_quantidade") %>x 
                                                                         <span class="parcela">R$ <%# Eval("parcPar_valor","{0:N}") %></span><br /><b>sem juros</b><br />  
                                                                    </asp:Panel>
                                                                     <asp:Panel runat="server" ID="PanelComJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") != 0 %>' Wrap="false">
                                                                          ou em até <%# Eval("parcPar_quantidade") %>x 
                                                                         <span class="parcela">R$ <%# Eval("parcPar_valor","{0:N}") %></span><br />
                                                                    </asp:Panel>

                                                              
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </p>

                                                        <div class="simular_parcelas"><a href="#" onclick="ObjetoVisible('divSimulacaoParcelas<%# Eval("produtoSkuDto.proSku_id") %>');">Simular Parcelamento</a></div>
                                                        <div id="divSimulacaoParcelas<%# Eval("produtoSkuDto.proSku_id") %>" style="display: none">

                                                            <asp:Repeater ID="RepeaterSimulacaoParcelas" runat="server" DataSource='<%# Eval("produtoSkuDto.parcelamentoDto.parcelamentoParcelaDto")%>'>
                                                                <ItemTemplate>
                                                                   <asp:Panel runat="server" ID="PanelSemJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") == 0 %>' Wrap="false">
                                                                         <%# Eval("parcPar_quantidade") %>x 
                                                                         <span class="parcela">R$ <%# Eval("parcPar_valor","{0:N}") %></span><br/><b>sem juros</b><br /></asp:Panel>
                                                                     <asp:Panel runat="server" ID="PanelComJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") != 0 %>' Wrap="false">
                                                                          <%# Eval("parcPar_quantidade") %>x 
                                                                          <span class="parcela">R$ <%# Eval("parcPar_valor","{0:N}") %></span><br />
                                                                    </asp:Panel>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>

                                                    </asp:Panel>
                                                    <asp:Button ID="ButtonProdutoSkuComprar" CssClass="btn_comprar" runat="server" Text="Comprar" ValidationGroup="groupEscolha" OnClick="ButtonProdutoSkuComprar_Click" CommandArgument='<%# Eval("produtoSkuDto.proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Adicionando...', 'groupEscolha')" Style="cursor: pointer" BorderWidth="0px" Width="170px" Height="45px" />
                                                    <asp:Panel ID="PanelProdutoSkuFreteGratis" runat="server" Style="display:block" Visible='<%# Eval("produtoSkuDto.entregaDto.ent_gratis") %>'>
                                                        <b><%# Eval("produtoSkuDto.entregaDto.ent_descricao") %></b>
                                                     </asp:Panel>
                                                
                                                </asp:Panel>
                                                <asp:Panel ID="PanelProdutoSkuBotaoSemEstoque" runat="server" Visible='<%# !(bool)Eval("produtoSkuDto.proSku_disponivel") %>'>
                                                    <p>
                                                        <div class="btn_semEstoque">Sem Estoque</div>
                                                        <a style="cursor: pointer; text-decoration: underline" onclick="$('#DivProdutoSkuAviso').css('display','')">* Avise-me quando chegar </a>
                                                    </p>
                                                </asp:Panel>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>


                        <asp:Panel runat="server" ID="DivProdutoSkuAviso" DefaultButton="ButtonCadastrarAviso" CssClass="aviseme" Style="display: none">
                            <div>
                                <label for="TextBoxProSkuAvi_nome" style="float: left">Nome:</label>
                                <asp:TextBox ID="TextBoxProSkuAvi_nome" runat="server" MaxLength="64" ValidationGroup="groupCadastraAviso"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="TextBoxProSkuAvi_nome" ErrorMessage="Insira seu Nome" ValidationGroup="groupCadastraAviso"></asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <label for="TextBoxProSkuAvi_email" style="float: left">E-mail:</label>
                                <asp:TextBox ID="TextBoxProSkuAvi_email" runat="server" MaxLength="128" ValidationGroup="groupCadastraAviso"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProSkuAvi_email" Display="Dynamic" runat="server" ControlToValidate="TextBoxProSkuAvi_email" ErrorMessage="Insira seu email" ValidationGroup="groupCadastraAviso"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorProSkuAvi_email" runat="server"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxProSkuAvi_email"
                                    ErrorMessage="Email inválido" ValidationGroup="groupCadastraAviso" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                            </div>
                            <asp:Button ID="ButtonCadastrarAviso" runat="server" Text="Cadastrar Aviso" OnClientClick="InsertProdutoSkuAviso(); return false;" ValidationGroup="groupCadastraAviso" />
                        </asp:Panel>

                        <asp:TextBox ID="HiddenFieldProSku_id" runat="server" ClientIDMode="Static" ValidationGroup="groupEscolha" Style="display: none" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorProSku_id" runat="server" ValidationGroup="groupEscolha" ControlToValidate="HiddenFieldProSku_id" Display="Dynamic" ForeColor="Red" ErrorMessage="Escolha o tamanho"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummaryProSku_id" runat="server" DisplayMode="SingleParagraph" ShowMessageBox="True" ValidationGroup="groupEscolha" ShowSummary="False" />
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div id="InfoProduto">
                    <asp:Repeater ID="RepeaterProduto" runat="server" DataSourceID="ObjectDataSourceProduto">
                        <HeaderTemplate>
                            <h3>Informações do Produto:</h3>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("pro_descricao") %>
                            <asp:Repeater ID="RepeaterProdutoInfo" runat="server" DataSource='<%# Eval("produtoInfoDto") %>'>
                                <ItemTemplate>
                                    <h3><%# Eval("proInfo_nome") %></h3>
                                    <asp:Repeater ID="RepeaterInfo" runat="server" DataSource='<%# Eval("produtoInfoItemDto") %> '>
                                        <ItemTemplate>
                                            <div><%# Eval("proInfoItem_descricao") %>:       <%# Eval("proInfoItem_valor") %></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                        </div>

                </div>
                <!-- Fecha div Conteudo -->

                <asp:ObjectDataSource ID="ObjectDataSourceProduto" runat="server"
                    SelectMethod="SelectByProduto" TypeName="_2_Library.Dao.Site.ProdutoInfoX.ProdutoTd" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Name="loj_dominio" Type="String" />
                        <asp:RouteParameter Name="proSku_id" Type="Int32" DefaultValue="proSku_id" RouteKey="proSku_id" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>

        <uc1:Rodape runat="server" ID="Rodape" />
        </div>
    </form>
</body>
</html>
