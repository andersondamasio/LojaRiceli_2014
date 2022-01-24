<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MinhaConta.aspx.cs" Inherits="Loja.Views.MinhaConta" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Cabecalho ID="Cabecalho1" runat="server" />

        <asp:ListView ID="ListViewPedidos" runat="server" DataSourceID="EntityDataSourcePedidos" DataKeyNames="ped_id">
            <EmptyDataTemplate>
                <table runat="server" style="" border="2">
                    <tr>
                        <td>Nenhum dado foi retornado.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="ped_idLabel" runat="server" Text='<%# Eval("ped_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_dataHoraLabel" runat="server" Text='<%# Eval("ped_dataHora") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_conforPag_nomeLabel" runat="server" Text='<%# Eval("ped_forPag_nome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_entregaPrazoLabel" runat="server" Text='<%# Eval("ped_ent_prazo") %>'></asp:Label>
                    </td>
                    <td>
                        <%# Eval("ped_cliEnd_nome") %> <%# Eval("ped_cliEnd_sobrenome") %><br />
                        <%# Eval("ped_cliEnd_endereco") %>, <%# Eval("ped_cliEnd_numero") %>, <%# Eval("ped_cliEnd_referencia") %><br />
                        <%# Eval("ped_cliEnd_cep") %>, <%# Eval("ped_cliEnd_bairro") %> <%# Eval("ped_cliEnd_cidade") %> - <%# Eval("ped_cliEnd_estado") %><br />
                        (<%# Eval("ped_cliEnd_ddd1") %>) <%# Eval("ped_cliEnd_fone1") %>
                    </td>
                         <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("PedidoProduto.Count") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_forpagCondicaoLabel" runat="server" Text='<%# Eval("ped_forPagPar_condicao") %>' />
                        </td>
                    <td>   <asp:LinkButton ID="LinkButtonSelecionar" runat="server" Text="Selecionar" CommandName="Select">Selecionar</asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                 <tr style="background-color:blue">
                    <td>
                        <asp:Label ID="ped_idLabel" runat="server" Text='<%# Eval("ped_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_dataHoraLabel" runat="server" Text='<%# Eval("ped_dataHora") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_conforPag_nomeLabel" runat="server" Text='<%# Eval("ped_forPag_nome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ped_entregaPrazoLabel" runat="server" Text='<%# Eval("ped_ent_prazo") %>'></asp:Label>
                    </td>
                    <td>
                        <%# Eval("ped_cliEnd_nome") %> <%# Eval("ped_cliEnd_sobrenome") %><br />
                        <%# Eval("ped_cliEnd_endereco") %>, <%# Eval("ped_cliEnd_numero") %>, <%# Eval("ped_cliEnd_referencia") %><br />
                        <%# Eval("ped_cliEnd_cep") %>, <%# Eval("ped_cliEnd_bairro") %> <%# Eval("ped_cliEnd_cidade") %> - <%# Eval("ped_cliEnd_estado") %><br />
                        (<%# Eval("ped_cliEnd_ddd1") %>) <%# Eval("ped_cliEnd_fone1") %>
                      </td>
                          <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("PedidoProduto.Count") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_forpagCondicaoLabel" runat="server" Text='<%# Eval("ped_forPagPar_condicao") %>' />
                        </td>
                     <td>   <asp:LinkButton ID="LinkButtonSelecionar" runat="server" Text="Selecionar" CommandName="Select">Selecionar</asp:LinkButton></td>
                        <tr>
                            <td colspan="7">
                                <table border="1" width="100%">
                                    <tr>
                                        <td colspan="7">Produtos</td>
                                    </tr>

                                    <asp:Repeater ID="RepeaterPedidoProduto" runat="server" DataSource='<%# Eval("PedidoProduto") %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("pedPro_pro_nome") %></td>
                                                <td>
                                                    <%# Eval("pedPro_proSku_id") %>
                                                    <br />
                                                    <%# Eval("pedPro_proSkuCor_nome") %>
                                                    <br />
                                                    <%# Eval("pedPro_proSkuTam_nome") %>
                                                </td>
                                                <td><%# Eval("pedPro_car_quantidade") %></td>
                                                <td><%# Eval("pedPro_proSku_precoVenda") %></td>
                                                <td><%# Eval("pedPro_proSku_precoVendaTotal") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>SubTotal:</td>
                                        <td colspan="2"><%# Eval("ped_subTotal") %></td>
                                    </tr>
                                     <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Descontos:</td>
                                        <td colspan="2"><%# Eval("ped_descontos") %></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Frete:</td>
                                        <td colspan="2"><%# Eval("ped_ent_valor") %></td>
                                    </tr>
                                     <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Total:</td>
                                        <td colspan="2"><%# Eval("ped_total") %></td>
                                    </tr>
                                </table>

                                <br />
                                <div><b>Status</b></div>
                                    <asp:Repeater ID="RepeaterStatus" runat="server" DataSourceID="EntityDataSourcePedidoStatus">
                                    <ItemTemplate>
                                       <%# Eval("Status.stat_nome") %><%# Eval("pedStat_descricao") %><%# Eval("pedStat_dataHora") %><br/>  
                                    </ItemTemplate>
                                </asp:Repeater>
                    
        <asp:EntityDataSource ID="EntityDataSourcePedidoStatus" runat="server" ConnectionString="name=LojaEntities"
            DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="PedidoStatus"
            Select="it.[pedStat_id], it.[pedStat_descricao], it.[stat_idRastreio], it.[pedStat_dataHora],it.[Status]"
            OrderBy="it.[pedStat_id] DESC" Where="it.[ped_id] = @ped_id">
            <WhereParameters>
                 
                <asp:ControlParameter ControlID="ListViewPedidos" Name="ped_id" Type="Int32" PropertyName="SelectedValue" />
            </WhereParameters>
        </asp:EntityDataSource>
                            </td>
                        </tr>
                </tr>


            </SelectedItemTemplate>


            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="1" style="">
                                <tr runat="server" style="">
                                    <th runat="server">Pedido nº</th>
                                    <th runat="server">Realizado em</th>
                                    <th runat="server">Forma de Pagamento</th>
                                    <th runat="server">Prazo para Entrega</th>
                                    <th runat="server">Endereço de Entrega</th>
                                    <th runat="server">Quant</th>
                                    <th runat="server">Condição</th>
                                    <th runat="server"></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    

        <asp:EntityDataSource ID="EntityDataSourcePedidos" runat="server" ConnectionString="name=LojaEntities"
            DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Pedido"
            Select="it.[ped_id], it.[ped_cli_nome], it.[ped_cli_sobrenome], it.[ped_cliEnd_nome], it.[ped_cliEnd_sobrenome],
             it.[ped_cliEnd_cep], it.[ped_cliEnd_endereco], it.[ped_cliEnd_numero], it.[ped_cliEnd_complemento], it.[ped_cliEnd_referencia],
             it.[ped_cliEnd_bairro], it.[ped_cliEnd_cidade], it.[ped_cliEnd_estado], it.[ped_cliEnd_ddd1], it.[ped_cliEnd_fone1],
             it.[ped_ent_meio], it.[ped_ent_prazo], it.[ped_ent_valor], it.[ped_forPag_nome], it.[ped_forPagPar_condicao], 
             it.[ped_subTotal], it.[ped_descontos], it.[ped_total], it.[ped_dataHora],it.[PedidoProduto]"            
            OrderBy="it.[ped_id] DESC" Where="it.[cli_id] = @cli_id" EntityTypeFilter="">
            <WhereParameters>
                <asp:SessionParameter DbType="Int32" Name="cli_id" SessionField="cli_id" />
            </WhereParameters>
        </asp:EntityDataSource>


        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
