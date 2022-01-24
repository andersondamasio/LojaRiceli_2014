<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidoConcluido.aspx.cs" Inherits="Loja.Views.PedidoConcluido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    PEDIDO CONCLUIDO COM SUCESSO!.
    </div>
        Dados do Pedido e entrega:<br/>
        <asp:FormView ID="FormViewPedidoConcluido" runat="server" DataSourceID="EntityDataSourcePedidoConcluido">
            <ItemTemplate>
               Número do Pedido:
                <asp:Label ID="ped_idLabel" runat="server" Text='<%# Bind("ped_id") %>' />
                <br />
                Nome:
                <asp:Label ID="ped_cli_nomeLabel" runat="server" Text='<%# Bind("ped_cli_nome") %>' />
                <br />
                Nome de quem irá receber:
                <asp:Label ID="ped_cliEnd_nomeLabel" runat="server" Text='<%# Bind("ped_cliEnd_nome") %>' />
                <br />
                Sobrenome
                <asp:Label ID="ped_cliEnd_sobrenomeLabel" runat="server" Text='<%# Bind("ped_cliEnd_sobrenome") %>' />
                <br/>
                Cep:
                <asp:Label ID="ped_cliEnd_cepLabel" runat="server" Text='<%# Bind("ped_cliEnd_cep") %>' />
                <br />
                Endereço:
                <asp:Label ID="ped_cliEnd_enderecoLabel" runat="server" Text='<%# Bind("ped_cliEnd_endereco") %>' />
                <br />
                Número:
                <asp:Label ID="ped_cliEnd_numeroLabel" runat="server" Text='<%# Bind("ped_cliEnd_numero") %>' />
                <br />
                Complemento:
                <asp:Label ID="ped_cliEnd_complementoLabel" runat="server" Text='<%# Bind("ped_cliEnd_complemento") %>' />
                <br />
                Referencia:
                <asp:Label ID="ped_cliEnd_referenciaLabel" runat="server" Text='<%# Bind("ped_cliEnd_referencia") %>' />
                <br />
                Bairro:
                <asp:Label ID="ped_cliEnd_bairroLabel" runat="server" Text='<%# Bind("ped_cliEnd_bairro") %>' />
                <br />
                Cidade:
                <asp:Label ID="ped_cliEnd_cidadeLabel" runat="server" Text='<%# Bind("ped_cliEnd_cidade") %>' />
                <br />
                Estado:
                <asp:Label ID="ped_cliEnd_estadoLabel" runat="server" Text='<%# Bind("ped_cliEnd_estado") %>' />
                <br />
            </ItemTemplate>
        </asp:FormView>
        <asp:EntityDataSource ID="EntityDataSourcePedidoConcluido" runat="server" ConnectionString="name=LojaEntities"
             DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Pedido"
             Select="it.[ped_id], it.[ped_cli_nome], it.[ped_cliEnd_nome], it.[ped_cliEnd_sobrenome], it.[ped_cliEnd_cep], it.[ped_cliEnd_endereco], 
            it.[ped_cliEnd_numero], it.[ped_cliEnd_complemento], it.[ped_cliEnd_referencia], it.[ped_cliEnd_bairro], 
            it.[ped_cliEnd_cidade], it.[ped_cliEnd_estado]" Where="it.[cli_id] = @cli_id and it.[ped_id] = @ped_id" OnSelected="EntityDataSourcePedidoConcluido_Selected">
         <WhereParameters>
             <asp:SessionParameter Name="cli_id" SessionField="cli_id" Type="Int32" />
             <asp:SessionParameter Name="ped_id" SessionField="ped_id" Type="Int32" />
         </WhereParameters>
        </asp:EntityDataSource>
        <asp:Panel ID="PanelPagamentoBoleto" runat="server" Visible="false">
            <a href="<%= Page.GetRouteUrl("GerarBoleto", null) %>?ped_id=<%= Session["ped_id"] %>" target="_blank">Clique aqui para gerar o seu boleto</a>
        </asp:Panel>
    </form>
</body>
</html>
