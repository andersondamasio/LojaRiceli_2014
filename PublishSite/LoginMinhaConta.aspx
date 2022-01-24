<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMinhaConta.aspx.cs" Inherits="_1_WebForm.LoginMinhaConta" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
</head>
<body>

    <form id="form2" runat="server">
        <div>

            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />

                <div id="corpo">
                    <div id="conteudo" style="width: 990px; margin-top: 5px;">
                        <div id="col1ID">
                            <span class="tituloID">Já sou cliente</span>
                            <p>
                                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/bulett.png" width="10" height="10">
                            <asp:Panel runat="server" ID="PanelTextoContaLogin">
                                 Se você já possui uma conta, informe os dados de acesso: </p>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="PanelTextoContaAssociar" Visible="false">
                                Associe sua conta com a do Facebook:
                                 </p>
                            </asp:Panel>
                            <asp:Panel ID="PanelLogin" runat="server" DefaultButton="ButtonEntrar">
                                <p>
                                    <label for="email">Email:</label>
                                    <asp:TextBox ID="TextBoxCli_email" runat="server" CssClass="form" ValidationGroup="groupLogin"></asp:TextBox>
                                </p>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="groupLogin"
                                    ControlToValidate="TextBoxCli_email" CssClass="MsgError" SetFocusOnError="true" ErrorMessage="* Insira seu email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server" CssClass="MsgError"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxCli_email"
                                    ErrorMessage="* Email inválido" ValidationGroup="groupLogin" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                <p>
                                    <label for="email2">Senha:</label>
                                    <asp:TextBox ID="TextBoxCli_senha" runat="server" CssClass="form" ValidationGroup="groupLogin" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="groupLogin" ControlToValidate="TextBoxCli_senha" CssClass="MsgError" SetFocusOnError="true" ErrorMessage="*Insira sua senha"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Button ID="ButtonEntrar" runat="server" CssClass="btn_form" Text="Entrar" ValidationGroup="groupLogin" OnClick="ButtonEntrar_Click" />

                                </p>
                                <p><a href="#" onclick="document.getElementById('divRecuperarSenha').style.display = (document.getElementById('divRecuperarSenha').style.display == 'none') ? '' : 'none'">Esqueceu a senha?</a>
                    <div id="divRecuperarSenha" style="display: none">
                        Coloque seu email para recuperar sua senha<br/>
                        <asp:TextBox ID="TextBoxEmailRecuperarSenha" runat="server" MaxLength="128" ValidationGroup="groupRecuperarSenha"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxEmailRecuperarSenha" ValidationGroup="groupRecuperarSenha" Display="Dynamic" SetFocusOnError="true"  ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxEmailRecuperarSenha"
                    ErrorMessage="Email inválido" ValidationGroup="TextBoxEmailRecuperarSenha" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
              
                        <asp:Button ID="ButtonRecuperarSenha" runat="server" OnClick="ButtonRecuperarSenha_Click" Text="Recuperar Senha" ValidationGroup="groupRecuperarSenha" />
                    </div></p>
                            </asp:Panel>
                            <p>&nbsp;</p>
                        </div>
                        <div id="col2ID">
                            <span class="tituloID">Ainda não sou cliente</span>
                            <p>
                                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/bulett.png" width="10" height="10">
                                Em apenas um passo, crie uma conta e finalize sua compra. É rapido e seguro.
                            </p>
                            <asp:Panel ID="PanelCadastro" runat="server" DefaultButton="ButtonCadastrar">
                                <p>
                                    <label for="email3">Email:</label>
                                    <asp:TextBox ID="TextBoxCli_emailCadastro" runat="server" CssClass="form" ValidationGroup="groupCadastro"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="groupCadastro" ControlToValidate="TextBoxCli_emailCadastro" CssClass="MsgError" SetFocusOnError="true" ErrorMessage="* Insira seu email"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxCli_emailCadastro"
                                        ErrorMessage="* Email inválido" ValidationGroup="groupCadastro" Display="Dynamic" CssClass="MsgError" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </p>
                                <p>
                                    <asp:Button ID="ButtonCadastrar" runat="server" CssClass="btn_form" Text="Me Cadastrar" ValidationGroup="groupCadastro" OnClick="ButtonCadastrar_Click" />
                                </p>
                            </asp:Panel>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                   
                    <div style="clear: both; height: 10px;"></div>
                </div>
                <!-- Fecha div Conteudo -->
                <uc1:Rodape runat="server" ID="Rodape" />
            </div>
    </form>
</body>
</html>
