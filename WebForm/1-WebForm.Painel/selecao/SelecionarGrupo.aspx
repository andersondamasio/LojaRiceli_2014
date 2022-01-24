<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelecionarGrupo.aspx.cs" Inherits="Loja.Painel.selecao.SelecionarGrupo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<asp:TreeView ID="TreeViewGrupo" runat="server" DataSourceID="XmlDataSourceGrupos"
                    ImageSet="Arrows" MaxDataBindDepth="50" 
        NodeIndent="20" ShowLines="True"
                    ExpandDepth="1" EnableClientScript="false" 
        PopulateNodesFromClient="true" 
        onselectednodechanged="TreeViewGrupo_SelectedNodeChanged">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="MenuItem" TextField="gru_nome" ValueField="gru_id"
                            PopulateOnDemand="false" SelectAction="SelectExpand" FormatString="{0}" />
                    </DataBindings>
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </asp:TreeView>
                <asp:XmlDataSource ID="XmlDataSourceGrupos" runat="server" EnableCaching="False"
                    XPath="MenuItems/MenuItem" CacheDuration="10" DataFile="~/Cache/grupoPainel.xml"
                    TransformFile="~/Cache/TransformXSLTPainel.xsl" />
    </form>
</body>
</html>
