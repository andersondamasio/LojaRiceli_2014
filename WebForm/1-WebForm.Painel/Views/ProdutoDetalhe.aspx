<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProdutoDetalhe.aspx.cs" Inherits="Loja.Views.ProdutoDetalhe" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/css/jquery.jqzoom.css" rel="stylesheet" />
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/js/jquery-1.6.js"></script>
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/js/jquery.jqzoom-core-pack.js"></script>
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/js/jquery.jqzoom-core.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Cabecalho ID="Cabecalho1" runat="server" />

        <asp:FormView ID="FormViewProdutoDetalhe" runat="server">
            <ItemTemplate>
                <table border="2" style="width: 778px">
                    <tr valign="top">
                        <td>
                            <asp:Repeater ID="RepeaterFotoM" runat="server" DataSource='<%# Eval("ProSkuFotos") %>'>
                                <HeaderTemplate>
                                    FotosMini:<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img id="proSkuFotM<%#Container.ItemIndex%>" onclick="trocaImagemProdutoDetalhe(this,'<%# Eval("proSkuFot_nome") %>','<%# Eval("proSkuFot_extensao") %>');" onerror="semFotoProdutoVitrine(this, '<%# Eval("loj_id") %>')" src="<%=Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("proSkuFot_nome") %>-m<%# Eval("proSkuFot_extensao") %>" style="max-height: 40px" alt="<%# Eval("proSkuFot_nome") %>" /><br />
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>FotoMaior<br />
                           
                             <a id="proSkuFotLinkD" class="fotoPrincipalDetalhe" href="<%# Eval("ProSkuFoto") != null ? (Page.GetRouteUrl("PaginaInicial", null)+"imagens/produtos/fotos/"+ Eval("loj_id") +"/"+ Eval("pro_id") +"/"+ Eval("ProSkuFoto.proSkuFot_nome") +"-a"+ Eval("ProSkuFoto.proSkuFot_extensao")) :  Page.GetRouteUrl("PaginaInicial", null) +"imagens/produtos/fotos/" + Eval("loj_id") + "/semFoto/v.jpg" %>" >  
                                 <img id="proSkuFotImageD" onerror="semFotoProdutoVitrine(this, '<%# Eval("loj_id") %>')" src="<%# Eval("ProSkuFoto") != null ? (Page.GetRouteUrl("PaginaInicial", null)+"imagens/produtos/fotos/"+ Eval("loj_id") +"/"+ Eval("pro_id") +"/"+ Eval("ProSkuFoto.proSkuFot_nome") +"-d"+ Eval("ProSkuFoto.proSkuFot_extensao")) :  Page.GetRouteUrl("PaginaInicial", null) +"imagens/produtos/fotos/" + Eval("loj_id") + "/semFoto/v.jpg" %>"  />
                             </a> 
                        </td>

                        <td>Descricoes:<br />
                            <%# Eval("mar_nome") %><br />
                            <%# Eval("pro_nome") %> <%# Eval("proSkuCor_nome") %>

                            <span id="spanTamanhos">
                                <asp:Repeater ID="RepeaterTamanho" runat="server" DataSource='<%# Eval("ProSkuTamanhos") %>'>
                                    <HeaderTemplate>
                                        <br />
                                        Tamanho:
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span id="spanTamanho<%# Eval("proSku_id") %>" style="" onclick="<%# !(bool)Eval("proSku_disponivel") ? "alert('indisponível');return false;" : "" %>produtoDetalheSelecionarProdutoSku(<%# Eval("proSku_id") %>)">
                                           (<%# Eval("proSkuTam_nome") %>) 
                                        </span>

                                        <div id="divParcelamentoPrecoSku" style="display: none">
                                            <div runat="server" id="divPrecoSkuTamanho" visible='<%# (bool)Eval("proSku_disponivel") %>'>
                                                <div id="divPrecoSkuTamanho<%# Eval("proSku_id") %>">
                                                    Preco:<br />
                                                    <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "De: R$ " + Eval("proSku_precoAnterior","{0:N}") : string.Empty%>
                                                    <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "Por: R$ " + Eval("proSku_precoVenda","{0:N}") : "R$ "+Eval("proSku_precoVenda","{0:N}") %>
                                                    <br />
                                                </div>
                                            </div>
                                            <div runat="server" id="divParcelamentoSkuTamanho" visible='<%# !(bool)Eval("Parcelamento.parc_bloquear") && (bool)Eval("proSku_disponivel") %>'>
                                                <div id="divParcelamentoSkuTamanho<%# Eval("proSku_id") %>">
                                                    Parcelamento<br />
                                                    <%# Eval("Parcelamento.parc_quantidade") %> x R$
                                                    <%# Eval("Parcelamento.parc_valor","{0:N}") %>
                                                    <%# (Boolean)Eval("Parcelamento.parc_ativarJuro") ? string.Empty : "sem juros" %>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </span>
                            <asp:Repeater ID="RepeaterCor" runat="server" DataSource='<%# Eval("ProSkuCores") %> '>
                                <HeaderTemplate>
                                    <br />
                                    Cor:
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id='linkProduto1<%# Eval("pro_id") %>' href="<%# Page.GetRouteUrl("PaginaInicial", null)+ Loja.Utils.Tratamento.GerarNomeAmigavel((string)Eval("pro_nome") +"-"+ Eval("proSkuCor_nome") +"-"+ Eval("proSkuTam_nome"))+"-"+ Eval("proSku_id") %>">
                                        <img style="<%# Eval("proSku_id").Equals(RouteData.Values["proSku_id"]) ? "border:2px solid #003399" : string.Empty %>;max-height: 40px" alt="<%# Eval("proSkuCor_nome") %>" id="proSkuFot01<%#Container.ItemIndex%>" onclick="trocaImagemProdutoDetalhe(this,'<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>','<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>');" onerror="semFotoProdutoVitrine(this, '<%# Eval("loj_id") %>')" src="<%=Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>-m<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>"  alt="<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>" />
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <span id="precoSku">Preco:<br />
                                <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "De: R$ " + Eval("proSku_precoAnterior","{0:N}") : string.Empty%>
                                <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "Por: R$ " + Eval("proSku_precoVenda","{0:N}") : "R$ "+Eval("proSku_precoVenda","{0:N}") %>
                                <br />
                            </span>
                            <span id="parcelamentoSku">
                                <asp:Panel runat="server" ID="PanelParcelamentoSku" Visible='<%#  !(bool)Eval("Parcelamento.parc_bloquear") %>'>
                                    Parcelamento<br />
                                    <%# Eval("Parcelamento.parc_quantidade") %> x R$
                                  <%# Eval("Parcelamento.parc_valor","{0:N}") %>
                                    <%# (Boolean)Eval("Parcelamento.parc_ativarJuro") ? string.Empty : "sem juros" %>
                                </asp:Panel>
                            </span>

                            <span runat="server" id="PanelProdutoSkuIndisponivel" visible='<%# !(Boolean)Eval("proSku_disponivel") %>'>
                                <span id='precoSku<%# Eval("proSku_id") %>' style="display: none">indisponível
                                </span>
                            </span>

                            <asp:Button ID="ButtonProdutoSkuComprar" runat="server" Text="Comprar" ValidationGroup="groupEscolha" OnClick="ButtonProdutoSkuComprar_Click" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="groupEscolha" ControlToValidate="HiddenFieldProdutoSkuComprar" ErrorMessage="Escolha o tamanho"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="HiddenFieldProdutoSkuComprar" runat="server" ValidationGroup="groupEscolha"  Text='<%# ((Repeater)FormViewProdutoDetalhe.FindControl("RepeaterTamanho")).Items.Count ==  1 ? RouteData.Values["proSku_id"] : string.Empty %>' Style="display:none"/>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Repeater ID="RepeaterInfo" runat="server" DataSource='<%# Eval("ProInfos") %> '>
                                <HeaderTemplate>
                                    Informações do Produto:<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("proInfo_nome") %><br />
                                    <asp:Repeater ID="RepeaterInfo" runat="server" DataSource='<%# Eval("ProdutoInfoItems") %> '>
                                        <ItemTemplate>
                                            <%# Eval("proInfoItem_descricao") %>:       <%# Eval("proInfoItem_valor") %><br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        
        
       
        <div>
        </div>
        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
