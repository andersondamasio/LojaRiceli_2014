<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroPagamento.aspx.cs" Inherits="_1_WebForm.CadastroPagamento" %>

<%@ Register Src="CabecalhoPagamento.ascx" TagName="CabecalhoPagamento" TagPrefix="uc1" %>

<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <link href="css/estilos.css" rel="stylesheet" />
        <link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>css/estilos.css" rel="stylesheet" type="text/css" />
        <asp:Literal ID="LiteralCssLoja" runat="server"></asp:Literal>
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.10.2.min.js"></script>
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Validacao.js"></script>


        <asp:ScriptManager ID="ScriptManagerRiceli" runat="server">
            <Scripts>
                <asp:ScriptReference Path="scripts/global.js" />
                <asp:ScriptReference Path="~/Service/ServiceRiceli.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="Service/ServiceRiceli.svc" />
            </Services>
        </asp:ScriptManager>
        <script>var index = "<%= Page.GetRouteUrl("PaginaInicial", null) %>";</script>
        <div id="header">
            <div id="Topo">
                Site 100% Seguro <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>">Continuar comprando</a>
                <div style="margin: auto; height: 40px; width: 620px; margin-top: 50px;">
                    <span class="BC_marcado">Carrinho</span>
                    <span class="BC_marcado">Identificação</span>
                    <span class="BC_marcado">Pagamento</span>
                    <span class="BC_desmarcado">Confirmação</span>
                    <div class="BC_seta" style="margin-left: 415px;">
                        75%
                        <img src="imagens/objetos/seta.png" />
                    </div>
                </div>
            </div>
            <div id="Pagamento">
                <div id="corpo">
                    <div class="PagCol1">
                        <div class="clear">
                            <span class="numeral">01</span> <span class="TituloItem">Informações do Cliente</span>
                        </div>
                        <div class="clear">
                            <p>
                                <br />
                                <asp:Panel ID="PanelEnderecoCobrancaResumo" runat="server" Visible="false">
                                    Endereco de entrega e cobranca<br />
                                    <asp:Literal ID="cli_nomeLiteral" runat="server"></asp:Literal>
                                    <asp:Literal ID="cli_sobrenomeLiteral" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="cli_enderecoLiteral" runat="server"></asp:Literal>, 
                                    <asp:Literal ID="cli_numeroLiteral" runat="server"></asp:Literal>, 
                                    <asp:Literal ID="cli_bairroLiteral" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="cli_cidadeLiteral" runat="server"></asp:Literal>
                                    - 
                                    <asp:Literal ID="cli_estadoLiteral" runat="server"></asp:Literal><br />
                                    <span id="cli_cepEntrega">
                                        <asp:Literal ID="cli_cepLiteral" runat="server"></asp:Literal></span>
                                    <br />
                                    (<asp:Literal ID="cli_ddd1Literal" runat="server"></asp:Literal>) 
                                    <asp:Literal ID="cli_fone1Literal" runat="server"></asp:Literal>
                                    <br />
                                    <asp:LinkButton ID="LinkButtonEditarCadastro" runat="server" OnClick="LinkButtonEditarCadastro_Click">Editar</asp:LinkButton>
                                </asp:Panel>

                                <asp:Panel ID="PanelEnderecoCobrancaEditar" runat="server" Visible="false">

                                    <asp:Panel ID="PanelTipoCliente" runat="server">
                                        <asp:RadioButton ID="RadioButtonClienteFisica" runat="server" Text="Sou Pessoa Física" GroupName="groupTipoCliente" AutoPostBack="True" Checked="True" OnCheckedChanged="RadioButtonClienteFisica_CheckedChanged" />
                                        <asp:RadioButton ID="RadioButtonClienteJuridica" runat="server" Text="Sou Pessoa Jurídica" GroupName="groupTipoCliente" AutoPostBack="True" OnCheckedChanged="RadioButtonClienteJuridica_CheckedChanged" />
                                        <br />
                                    </asp:Panel>
                                    <label for="cli_emailTextBox" class="Rotulo">Email</label>
                                    <asp:TextBox ID="cli_emailTextBox" Text='<%# Bind("cli_email") %>' runat="server" CssClass="CaixaTexto" Columns="25" MaxLength="128" ValidationGroup="groupCadastroCliente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_emailTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailTextBox"
                                        ErrorMessage="Email inválido" ValidationGroup="groupCadastroCliente" CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    <asp:LinkButton ID="LinkButtonVoltarAlterarEmail" runat="server" Style="float: right" OnClick="LinkButtonAlterarEmail_Click">voltar e alterar email</asp:LinkButton>
                                    <p>
                                        <asp:Panel ID="PanelSenha" runat="server">
                                            <label for="cli_senhaTextBox" class="Rotulo">Senha</label>
                                            <asp:TextBox ID="cli_senhaTextBox" runat="server" CssClass="CaixaTexto" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_senhaTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>

                                            <label for="cli_confirmacaoSenhaTextBox" class="Rotulo">Confirmar senha</label>
                                            <asp:TextBox ID="cli_confirmacaoSenhaTextBox" runat="server" CssClass="CaixaTexto" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="compareSenha" runat="server" ValidationGroup="groupCadastroCliente" ControlToCompare="cli_senhaTextBox" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Senha e confirmação estão diferentes" CssClass="MsgError" Display="Dynamic" />
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="PanelDadosPessoaFisica1">
                                            <p>
                                                <label for="cli_cpfTextBox">CPF</label>
                                                <asp:TextBox ID="cli_cpfTextBox" runat="server" CssClass="CaixaTexto" MaxLength="14" ValidationGroup="groupCadastroCliente" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_cpfTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="groupCadastroCliente" ErrorMessage="Cpf inválido(use apenas números sem separadores)." ControlToValidate="cli_cpfTextBox" ClientValidationFunction="valida_CPF" CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>

                                            </p>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="PanelDadosPessoaJuridica1" Visible="false">
                                            <p>
                                                <label for="cli_cnpjTextBox" class="Rotulo">CNPJ</label>
                                                <asp:TextBox ID="cli_cnpjTextBox" runat="server" CssClass="CaixaTexto" MaxLength="14" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_cnpjTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="groupCadastroCliente" ErrorMessage="Cnpj inválido(use apenas números sem separadores)." ControlToValidate="cli_cnpjTextBox" ClientValidationFunction="valida_CNPJ" CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>


                                            </p>
                                        </asp:Panel>
                                        <p>
                                            <label for="cli_nomeTextBox" class="Rotulo">Nome</label>
                                            <asp:TextBox ID="cli_nomeTextBox" runat="server" CssClass="CaixaTexto" MaxLength="64" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_nomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <label for="cli_sobrenomeTextBox" class="Rotulo">Sobrenome</label>
                                            <asp:TextBox ID="cli_sobrenomeTextBox" runat="server" CssClass="CaixaTexto" MaxLength="64" ValidationGroup="groupCadastroCliente" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_sobrenomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>

                                        </p>
                                        <asp:Panel runat="server" ID="PanelDadosPessoaFisica2" Style="display: inline;">

                                            <label for="cli_diaNascimentoDropDownList" class="Rotulo">
                                                Data de Nascimento<br />
                                            </label>
                                            <p>
                                                <asp:DropDownList ID="cli_diaNascimentoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Day %>'>
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">01</asp:ListItem>
                                                    <asp:ListItem Value="2">02</asp:ListItem>
                                                    <asp:ListItem Value="3">03</asp:ListItem>
                                                    <asp:ListItem Value="4">04</asp:ListItem>
                                                    <asp:ListItem Value="5">05</asp:ListItem>
                                                    <asp:ListItem Value="6">06</asp:ListItem>
                                                    <asp:ListItem Value="7">07</asp:ListItem>
                                                    <asp:ListItem Value="8">08</asp:ListItem>
                                                    <asp:ListItem Value="9">09</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem>
                                                    <asp:ListItem Value="19">19</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="21">21</asp:ListItem>
                                                    <asp:ListItem Value="22">22</asp:ListItem>
                                                    <asp:ListItem Value="23">23</asp:ListItem>
                                                    <asp:ListItem Value="24">24</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="26">26</asp:ListItem>
                                                    <asp:ListItem Value="27">27</asp:ListItem>
                                                    <asp:ListItem Value="28">28</asp:ListItem>
                                                    <asp:ListItem Value="29">29</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="31">31</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="cli_mesNascimentoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Month %>'>
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">01</asp:ListItem>
                                                    <asp:ListItem Value="2">02</asp:ListItem>
                                                    <asp:ListItem Value="3">03</asp:ListItem>
                                                    <asp:ListItem Value="4">04</asp:ListItem>
                                                    <asp:ListItem Value="5">05</asp:ListItem>
                                                    <asp:ListItem Value="6">06</asp:ListItem>
                                                    <asp:ListItem Value="7">07</asp:ListItem>
                                                    <asp:ListItem Value="8">08</asp:ListItem>
                                                    <asp:ListItem Value="9">09</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="cli_anoNascimentoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Year %>'>
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                                    <asp:ListItem Value="2006">2006</asp:ListItem>
                                                    <asp:ListItem Value="2005">2005</asp:ListItem>
                                                    <asp:ListItem Value="2004">2004</asp:ListItem>
                                                    <asp:ListItem Value="2003">2003</asp:ListItem>
                                                    <asp:ListItem Value="2002">2002</asp:ListItem>
                                                    <asp:ListItem Value="2001">2001</asp:ListItem>
                                                    <asp:ListItem Value="2000">2000</asp:ListItem>
                                                    <asp:ListItem Value="1999">1999</asp:ListItem>
                                                    <asp:ListItem Value="1998">1998</asp:ListItem>
                                                    <asp:ListItem Value="1997">1997</asp:ListItem>
                                                    <asp:ListItem Value="1996">1996</asp:ListItem>
                                                    <asp:ListItem Value="1995">1995</asp:ListItem>
                                                    <asp:ListItem Value="1994">1994</asp:ListItem>
                                                    <asp:ListItem Value="1993">1993</asp:ListItem>
                                                    <asp:ListItem Value="1992">1992</asp:ListItem>
                                                    <asp:ListItem Value="1991">1991</asp:ListItem>
                                                    <asp:ListItem Value="1990">1990</asp:ListItem>
                                                    <asp:ListItem Value="1989">1989</asp:ListItem>
                                                    <asp:ListItem Value="1988">1988</asp:ListItem>
                                                    <asp:ListItem Value="1987">1987</asp:ListItem>
                                                    <asp:ListItem Value="1986">1986</asp:ListItem>
                                                    <asp:ListItem Value="1985">1985</asp:ListItem>
                                                    <asp:ListItem Value="1984">1984</asp:ListItem>
                                                    <asp:ListItem Value="1983">1983</asp:ListItem>
                                                    <asp:ListItem Value="1982">1982</asp:ListItem>
                                                    <asp:ListItem Value="1981">1981</asp:ListItem>
                                                    <asp:ListItem Value="1980">1980</asp:ListItem>
                                                    <asp:ListItem Value="1979">1979</asp:ListItem>
                                                    <asp:ListItem Value="1978">1978</asp:ListItem>
                                                    <asp:ListItem Value="1977">1977</asp:ListItem>
                                                    <asp:ListItem Value="1976">1976</asp:ListItem>
                                                    <asp:ListItem Value="1975">1975</asp:ListItem>
                                                    <asp:ListItem Value="1974">1974</asp:ListItem>
                                                    <asp:ListItem Value="1973">1973</asp:ListItem>
                                                    <asp:ListItem Value="1972">1972</asp:ListItem>
                                                    <asp:ListItem Value="1971">1971</asp:ListItem>
                                                    <asp:ListItem Value="1970">1970</asp:ListItem>
                                                    <asp:ListItem Value="1969">1969</asp:ListItem>
                                                    <asp:ListItem Value="1968">1968</asp:ListItem>
                                                    <asp:ListItem Value="1967">1967</asp:ListItem>
                                                    <asp:ListItem Value="1966">1966</asp:ListItem>
                                                    <asp:ListItem Value="1965">1965</asp:ListItem>
                                                    <asp:ListItem Value="1964">1964</asp:ListItem>
                                                    <asp:ListItem Value="1963">1963</asp:ListItem>
                                                    <asp:ListItem Value="1962">1962</asp:ListItem>
                                                    <asp:ListItem Value="1961">1961</asp:ListItem>
                                                    <asp:ListItem Value="1960">1960</asp:ListItem>
                                                    <asp:ListItem Value="1959">1959</asp:ListItem>
                                                    <asp:ListItem Value="1958">1958</asp:ListItem>
                                                    <asp:ListItem Value="1957">1957</asp:ListItem>
                                                    <asp:ListItem Value="1956">1956</asp:ListItem>
                                                    <asp:ListItem Value="1955">1955</asp:ListItem>
                                                    <asp:ListItem Value="1954">1954</asp:ListItem>
                                                    <asp:ListItem Value="1953">1953</asp:ListItem>
                                                    <asp:ListItem Value="1952">1952</asp:ListItem>
                                                    <asp:ListItem Value="1951">1951</asp:ListItem>
                                                    <asp:ListItem Value="1950">1950</asp:ListItem>
                                                    <asp:ListItem Value="1949">1949</asp:ListItem>
                                                    <asp:ListItem Value="1948">1948</asp:ListItem>
                                                    <asp:ListItem Value="1947">1947</asp:ListItem>
                                                    <asp:ListItem Value="1946">1946</asp:ListItem>
                                                    <asp:ListItem Value="1945">1945</asp:ListItem>
                                                    <asp:ListItem Value="1944">1944</asp:ListItem>
                                                    <asp:ListItem Value="1943">1943</asp:ListItem>
                                                    <asp:ListItem Value="1942">1942</asp:ListItem>
                                                    <asp:ListItem Value="1941">1941</asp:ListItem>
                                                    <asp:ListItem Value="1940">1940</asp:ListItem>
                                                    <asp:ListItem Value="1939">1939</asp:ListItem>
                                                    <asp:ListItem Value="1938">1938</asp:ListItem>
                                                    <asp:ListItem Value="1937">1937</asp:ListItem>
                                                    <asp:ListItem Value="1936">1936</asp:ListItem>
                                                    <asp:ListItem Value="1935">1935</asp:ListItem>
                                                    <asp:ListItem Value="1934">1934</asp:ListItem>
                                                    <asp:ListItem Value="1933">1933</asp:ListItem>
                                                    <asp:ListItem Value="1932">1932</asp:ListItem>
                                                    <asp:ListItem Value="1931">1931</asp:ListItem>
                                                    <asp:ListItem Value="1930">1930</asp:ListItem>
                                                    <asp:ListItem Value="1929">1929</asp:ListItem>
                                                    <asp:ListItem Value="1928">1928</asp:ListItem>
                                                    <asp:ListItem Value="1927">1927</asp:ListItem>
                                                    <asp:ListItem Value="1926">1926</asp:ListItem>
                                                    <asp:ListItem Value="1925">1925</asp:ListItem>
                                                    <asp:ListItem Value="1924">1924</asp:ListItem>
                                                    <asp:ListItem Value="1923">1923</asp:ListItem>
                                                    <asp:ListItem Value="1922">1922</asp:ListItem>
                                                    <asp:ListItem Value="1921">1921</asp:ListItem>
                                                    <asp:ListItem Value="1920">1920</asp:ListItem>
                                                    <asp:ListItem Value="1919">1919</asp:ListItem>
                                                    <asp:ListItem Value="1918">1918</asp:ListItem>
                                                    <asp:ListItem Value="1917">1917</asp:ListItem>
                                                    <asp:ListItem Value="1916">1916</asp:ListItem>
                                                    <asp:ListItem Value="1915">1915</asp:ListItem>
                                                    <asp:ListItem Value="1914">1914</asp:ListItem>
                                                    <asp:ListItem Value="1913">1913</asp:ListItem>
                                                    <asp:ListItem Value="1912">1912</asp:ListItem>
                                                    <asp:ListItem Value="1911">1911</asp:ListItem>
                                                    <asp:ListItem Value="1910">1910</asp:ListItem>
                                                    <asp:ListItem Value="1909">1909</asp:ListItem>
                                                    <asp:ListItem Value="1908">1908</asp:ListItem>
                                                    <asp:ListItem Value="1907">1907</asp:ListItem>
                                                    <asp:ListItem Value="1906">1906</asp:ListItem>
                                                    <asp:ListItem Value="1905">1905</asp:ListItem>
                                                    <asp:ListItem Value="1904">1904</asp:ListItem>
                                                    <asp:ListItem Value="1903">1903</asp:ListItem>
                                                    <asp:ListItem Value="1902">1902</asp:ListItem>
                                                    <asp:ListItem Value="1901">1901</asp:ListItem>
                                                    <asp:ListItem Value="1900">1900</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_diaNascimentoDropDownList" ErrorMessage="Dia é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_mesNascimentoDropDownList" ErrorMessage="Mês é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_anoNascimentoDropDownList" ErrorMessage="Ano é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <p>
                                                    <label for="cli_sexoDropDownList" class="Rotulo">Sexo</label><p />
                                                    <asp:DropDownList ID="cli_sexoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# Bind("cli_sexo") %>'>
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_sexoDropDownList" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </p>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="PanelDadosPessoaJuridica2" Visible="false">
                                            <p>
                                                <label for="cli_inscricaoEstadualTextBox" class="Rotulo">Inscrição estadual</label>
                                                <asp:TextBox ID="cli_inscricaoEstadualTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_inscricaoEstadualTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </p>
                                            <p>
                                                <asp:CheckBox ID="cli_inscricaoEstadualIsentoCheckBox" runat="server" CssClass="Rotulo" Text="Insento" />
                                            </p>
                                            <p>
                                                <label for="cli_razaoSocialTextBox" class="Rotulo">Razão Social</label>
                                                <asp:TextBox ID="cli_razaoSocialTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_razaoSocialTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                </asp:RequiredFieldValidator>

                                            </p>

                                        </asp:Panel>
                                        <p>
                                        </p>
                                        <div style="margin-bottom: 5px; margin-top: 15px;">
                                            <span class="numeral">02</span> <span class="TituloItem">Endereço de Cobrança e Entrega</span>
                                        </div>
                                        <p>
                                            <label class="Rotulo" for="cli_cepTextBox">
                                                CEP</label>
                                            <asp:TextBox ID="cli_cepTextBox" runat="server" autocomplete="off" Columns="25" CssClass="CaixaTexto" MaxLength="8" onkeyup="SelectCorreioCalcPrecoPrazoLocalidade(this)" Style="width: 120px;" Text='<%# Bind("cli_cep") %>'></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="cli_cepTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" ValidChars="1234567890-" TargetControlID="cli_cepTextBox">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="revCEP" ForeColor="red" runat="server" Text="Insira um CEP válido. Exemplo: 31030863 - não utilize traço"
                                                ControlToValidate="cli_cepTextBox" ValidationGroup="groupCadastroCliente" ValidationExpression="\d{5}(\d{3})?" CssClass="MsgError" Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cli_cepTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                            &nbsp; <a href="#">Esqueceu seu CEP?</a>
                                        </p>
                                        <div style="float: left; width: 150px;">
                                            <label class="Rotulo" for="cli_enderecoTextBox">
                                                Endereço</label>
                                            <asp:TextBox ID="cli_enderecoTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="64" Style="width: 150px;" Text='<%# Bind("cli_endereco") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="cli_enderecoTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float: right; width: 100px;">
                                            <label class="Rotulo" for="cli_numeroTextBox">
                                                Número</label>
                                            <asp:TextBox ID="cli_numeroTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="16" Style="width: 100px;" Text='<%# Bind("cli_numero") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cli_numeroTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div style="padding: 10px;">
                                            &nbsp;
                                        </div>
                                        <label class="Rotulo" for="cli_complementoTextBox">
                                            Complemento</label>
                                        <asp:TextBox ID="cli_complementoTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="64" Text='<%# Bind("cli_complemento") %>'></asp:TextBox>
                                        <label class="Rotulo" for="cli_bairroTextBox">
                                            Bairro</label>
                                        <asp:TextBox ID="cli_bairroTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="64" Text='<%# Bind("cli_bairro") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="cli_bairroTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                        </asp:RequiredFieldValidator>
                                        <div style="float: left; width: 150px;">
                                            <label class="Rotulo" for="Cidade">
                                                Cidade</label>
                                            <asp:TextBox ID="cli_cidadeTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="64" Style="width: 150px;" Text='<%# Bind("cli_cidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="cli_cidadeTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float: right; width: 100px;">
                                            <label class="Rotulo" for="cli_estadoDropDownList">
                                                Estado</label>
                                            <asp:DropDownList ID="cli_estadoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# Bind("cli_estado") %>' Style="width: 105px;">
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Value="AC">Acre</asp:ListItem>
                                                <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                                <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                                <asp:ListItem Value="AP">Amapá</asp:ListItem>
                                                <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                                <asp:ListItem Value="CE">Ceará</asp:ListItem>
                                                <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                                <asp:ListItem Value="ES">Espírito Santo</asp:ListItem>
                                                <asp:ListItem Value="GO">Goiás</asp:ListItem>
                                                <asp:ListItem Value="MA">Maranhão</asp:ListItem>
                                                <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                                <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                                <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                                <asp:ListItem Value="PA">Pará</asp:ListItem>
                                                <asp:ListItem Value="PB">Paraíba</asp:ListItem>
                                                <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                                <asp:ListItem Value="PI">Piauí</asp:ListItem>
                                                <asp:ListItem Value="PR">Paraná</asp:ListItem>
                                                <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                                <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                                <asp:ListItem Value="RO">Rondônia</asp:ListItem>
                                                <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                                <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                                <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                                <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                                <asp:ListItem Value="SP">São Paulo</asp:ListItem>
                                                <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="cli_estadoDropDownList" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear" style="height: 0;">
                                        </div>
                                        <label class="Rotulo" for="cli_referenciaTextBox">
                                            Referência</label>
                                        <asp:TextBox ID="cli_referenciaTextBox" runat="server" Columns="25" CssClass="CaixaTexto" MaxLength="25" Text='<%# Bind("cli_referencia") %>'></asp:TextBox>
                                        <div style="float: left; width: 50px;">
                                            <label class="Rotulo" for="cli_ddd1TextBox">
                                                DDD</label>
                                            <asp:TextBox ID="cli_ddd1TextBox" runat="server" Columns="1" CssClass="CaixaTexto" MaxLength="2" Style="width: 50px;" Text='<%# Bind("cli_ddd1") %>'></asp:TextBox>
                                        </div>
                                        <div style="float: Right; width: 200px;">
                                            <label class="Rotulo" for="cli_fone1TextBox">
                                                Telefone Residencial</label>
                                            <asp:TextBox ID="cli_fone1TextBox" runat="server" Columns="15" CssClass="CaixaTexto" MaxLength="16" Style="width: 200px;" Text='<%# Bind("cli_fone1") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="cli_ddd1TextBox" ID="RegularExpressionValidator2" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="cli_fone1TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="cli_ddd1TextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="DDD é Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="cli_fone1TextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Telefone é Campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float: left; width: 50px;">
                                            <label class="Rotulo" for="cli_ddd2TextBox">
                                                DDD</label>
                                            <asp:TextBox ID="cli_ddd2TextBox" runat="server" Columns="1" CssClass="CaixaTexto" MaxLength="2" Style="width: 50px;" Text='<%# Bind("cli_ddd2") %>'></asp:TextBox>

                                        </div>
                                        <div style="float: Right; width: 200px;">
                                            <label class="Rotulo" for="cli_fone2TextBox">
                                                Telefone Comercial</label>
                                            <asp:TextBox ID="cli_fone2TextBox" runat="server" Columns="15" CssClass="CaixaTexto" MaxLength="16" Style="width: 200px;" Text='<%# Bind("cli_fone2") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="cli_ddd2TextBox" ID="RegularExpressionValidator1" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="cli_fone2TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                        </div>
                                        <div style="float: left; width: 50px;">
                                            <label class="Rotulo" for="cli_ddd3TextBox">
                                                DDD</label>
                                            <asp:TextBox ID="cli_ddd3TextBox" runat="server" Columns="1" CssClass="CaixaTexto" MaxLength="2" Style="width: 50px;" Text='<%# Bind("cli_ddd3") %>'></asp:TextBox>

                                        </div>
                                        <div style="float: Right; width: 200px;">
                                            <label class="Rotulo" for="cli_fone3TextBox">
                                                Telefone Celular</label>
                                            <asp:TextBox ID="cli_fone3TextBox" runat="server" Columns="15" CssClass="CaixaTexto" MaxLength="16" Style="width: 200px;" Text='<%# Bind("cli_fone3") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="cli_ddd3TextBox" ID="RegularExpressionValidator5" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="cli_fone3TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="groupCadastroCliente"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="cli_ddd3TextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="DDD é campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="cli_fone3TextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Telefone é campo obrigatório" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <asp:CheckBox ID="cli_recebeInformativoCheckBox" runat="server" Checked="true" Text="Aceito receber ofertas e descontos exclusivos por email." />
                                        <br>
                                        <asp:LinkButton ID="LinkButtonAtualizarCadastro" runat="server" CssClass="btn_form" OnClick="LinkButtonAtualizarCadastro_Click" ValidationGroup="groupCadastroCliente">Salvar</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButtonCancelarCadastro" runat="server" CssClass="btn_form" OnClick="LinkButtonCancelarCadastro_Click">Cancelar</asp:LinkButton>
                                        <br>
                                        <br>
                                </asp:Panel>

                                <asp:Panel ID="PanelEnderecoAdicionalEntregaPasso2" runat="server">
                                    <hr />
                                    <span class="numeral">02</span> <span class="TituloItem">Endereço de Entrega</span><br />
                                    <br />
                                    <asp:CheckBox ID="CheckBoxEnderecoAdicionalEntrega" runat="server" AutoPostBack="true" Checked="true" Text="Endereço de entrega igual ao endereço de cobrança." OnCheckedChanged="CheckBoxEnderecoAdicionalEntrega_CheckedChanged" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <%--/////////////////////ADD ADICIONAL//////////////////////////////////////////////////////////////////////////////////////// --%>



                                <asp:Panel ID="PanelEnderecoAdicionalEntrega" runat="server">

                                    <asp:LinkButton ID="LinkButtonAdicionarEnderecoAdicionalEntrega" runat="server" CssClass="btn_form" OnClick="LinkButtonAdicionarEnderecoAdicionalEntrega_Click">Adicionar novo endereço</asp:LinkButton>
                                    <br />
                                    <br />
                                    <asp:ListView ID="ListViewEnderecoAdicionalEntrega" runat="server"
                                        DataKeyNames="cliEnd_id,loj_id"
                                        InsertItemPosition="None"
                                        SelectMethod="ListViewEnderecoAdicionalEntrega_GetData"
                                        InsertMethod="ListViewEnderecoAdicionalEntrega_InsertItem"
                                        UpdateMethod="ListViewEnderecoAdicionalEntrega_UpdateItem"
                                        DeleteMethod="ListViewEnderecoAdicionalEntrega_DeleteItem">
                                        <EditItemTemplate>
                                            <table border="1">
                                                <tr>
                                                    <td>Nome</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_nomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_nome") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_nomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sobrenome</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_sobrenomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_sobrenome") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_sobrenomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Cep</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" onkeyup="SelectCorreioLocalidade(this)" Text='<%# Bind("cliEnd_cep") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_cepTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revCEP" ForeColor="red" runat="server" Text="Insira um CEP válido. Exemplo: 31030863 - não utilize traço"
                                                            ControlToValidate="cliEnd_cepTextBox" ValidationGroup="validGroupFinalizarPedido" ValidationExpression="\d{5}(\d{3})?" CssClass="MsgError" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        <asp:FilteredTextBoxExtender ID="cliEnd_cepTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" ValidChars="1234567890-" TargetControlID="cliEnd_cepTextBox">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Endereço</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_endereco") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_enderecoTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Número</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cliEnd_numero") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_numeroTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Complemento</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_complementoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_complemento") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Bairro</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_bairroTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_bairro") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_bairroTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Estado</td>
                                                    <td>
                                                        <asp:DropDownList ID="cliEnd_estadoDropDownList" runat="server" SelectedValue='<%# Bind("cliEnd_estado") %>'>
                                                            <asp:ListItem Value="" Text="Selecione seu estado" />
                                                            <asp:ListItem Value="AC">Acre</asp:ListItem>
                                                            <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                                            <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                                            <asp:ListItem Value="AP">Amap&#225;</asp:ListItem>
                                                            <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                                            <asp:ListItem Value="CE">Cear&#225;</asp:ListItem>
                                                            <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                                            <asp:ListItem Value="ES">Esp&#237;rito Santo</asp:ListItem>
                                                            <asp:ListItem Value="GO">Goi&#225;s</asp:ListItem>
                                                            <asp:ListItem Value="MA">Maranh&#227;o</asp:ListItem>
                                                            <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                                            <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                                            <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                                            <asp:ListItem Value="PA">Par&#225;</asp:ListItem>
                                                            <asp:ListItem Value="PB">Para&#237;ba</asp:ListItem>
                                                            <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                                            <asp:ListItem Value="PI">Piau&#237;</asp:ListItem>
                                                            <asp:ListItem Value="PR">Paran&#225;</asp:ListItem>
                                                            <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                                            <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                                            <asp:ListItem Value="RO">Rond&#244;nia</asp:ListItem>
                                                            <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                                            <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                                            <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                                            <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                                            <asp:ListItem Value="SP">S&#227;o Paulo</asp:ListItem>
                                                            <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_estadoDropDownList" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Cidade</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_cidade") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_cidadeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sexo:</td>
                                                    <td>
                                                        <asp:DropDownList ID="cliEnd_sexoDropDownList" runat="server" SelectedValue='<%# Bind("cliEnd_sexo") %>'>
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_sexoDropDownList" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Telefone</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd1") %>'></asp:TextBox>
                                                        -
                                            <asp:TextBox ID="cliEnd_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone1") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="cliEnd_fone1TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ControlToValidate="cliEnd_ddd1TextBox" ID="RegularExpressionValidator1" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_ddd1TextBox" ErrorMessage="DDD é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_fone1TextBox" ErrorMessage="Telfefone é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Telefone 2</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd2") %>'></asp:TextBox>

                                                        -
                                            <asp:TextBox ID="cliEnd_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone2") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="cliEnd_fone2TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ControlToValidate="cliEnd_ddd2TextBox" ID="RegularExpressionValidator3" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_ddd2TextBox" ErrorMessage="DDD é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_fone2TextBox" ErrorMessage="Telfefone é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Celular</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd3TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd3") %>'></asp:TextBox>
                                                        -
                                            <asp:TextBox ID="cliEnd_fone3TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone3") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ControlToValidate="cliEnd_ddd3TextBox" ID="RegularExpressionValidator4" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Seu DDD está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="cliEnd_fone3TextBox" CssClass="MsgError" Display="Dynamic" ValidationExpression="^[\s\S]{7,}$" ErrorMessage="Seu Telefone está incorreto." SetFocusOnError="True" ValidationGroup="validGroupFinalizarPedido"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Referência</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_referenciaTextBox" runat="server" Columns="25" MaxLength="25" Text='<%# Bind("cliEnd_referencia") %>'></asp:TextBox></td>
                                                </tr>

                                            </table>
                                            <br />
                                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn_form" CausesValidation="True" CommandName="Update" Text="Salvar" ValidationGroup="validGroupFinalizarPedido" />
                                            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                            <br />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <table border="1">
                                                <tr>
                                                    <td>Nome</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_nomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_nome") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_nomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sobrenome</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_sobrenomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_sobrenome") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_sobrenomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Cep</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" onkeyup="SelectCorreioLocalidade(this)" Text='<%# Bind("cliEnd_cep") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_cepTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revCEP" ForeColor="red" runat="server" Text="Insira um CEP válido. Exemplo: 31030863 - não utilize traço"
                                                            ControlToValidate="cliEnd_cepTextBox" ValidationGroup="validGroupCadastroClienteAdicional" ValidationExpression="\d{5}(\d{3})?" CssClass="MsgError" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        <asp:FilteredTextBoxExtender ID="cliEnd_cepTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" ValidChars="1234567890-" TargetControlID="cliEnd_cepTextBox">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Endereço</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_endereco") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_enderecoTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Número</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cliEnd_numero") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_numeroTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Complemento</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_complementoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_complemento") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Bairro</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_bairroTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_bairro") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_bairroTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Estado</td>
                                                    <td>
                                                        <asp:DropDownList ID="cliEnd_estadoDropDownList" runat="server" SelectedValue='<%# Bind("cliEnd_estado") %>'>
                                                            <asp:ListItem Value="" Text="Selecione seu estado" />
                                                            <asp:ListItem Value="AC">Acre</asp:ListItem>
                                                            <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                                            <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                                            <asp:ListItem Value="AP">Amap&#225;</asp:ListItem>
                                                            <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                                            <asp:ListItem Value="CE">Cear&#225;</asp:ListItem>
                                                            <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                                            <asp:ListItem Value="ES">Esp&#237;rito Santo</asp:ListItem>
                                                            <asp:ListItem Value="GO">Goi&#225;s</asp:ListItem>
                                                            <asp:ListItem Value="MA">Maranh&#227;o</asp:ListItem>
                                                            <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                                            <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                                            <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                                            <asp:ListItem Value="PA">Par&#225;</asp:ListItem>
                                                            <asp:ListItem Value="PB">Para&#237;ba</asp:ListItem>
                                                            <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                                            <asp:ListItem Value="PI">Piau&#237;</asp:ListItem>
                                                            <asp:ListItem Value="PR">Paran&#225;</asp:ListItem>
                                                            <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                                            <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                                            <asp:ListItem Value="RO">Rond&#244;nia</asp:ListItem>
                                                            <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                                            <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                                            <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                                            <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                                            <asp:ListItem Value="SP">S&#227;o Paulo</asp:ListItem>
                                                            <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_estadoDropDownList" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Cidade</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_cidade") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_cidadeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sexo:</td>
                                                    <td>
                                                        <asp:DropDownList ID="cliEnd_sexoDropDownList" runat="server" SelectedValue='<%# Bind("cliEnd_sexo") %>'>
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_sexoDropDownList" ErrorMessage="Campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Telefone</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd1") %>'></asp:TextBox>
                                                        -
                    <asp:TextBox ID="cliEnd_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone1") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_ddd1TextBox" ErrorMessage="DDD é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_fone1TextBox" ErrorMessage="Telfefone é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Telefone 2</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd2") %>'></asp:TextBox>

                                                        -
                    <asp:TextBox ID="cliEnd_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone2") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_ddd2TextBox" ErrorMessage="DDD é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_fone2TextBox" ErrorMessage="Telfefone é campo obrigatório" SetFocusOnError="True" CssClass="MsgError" Display="Dynamic">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>DDD+Celular</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_ddd3TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd3") %>'></asp:TextBox>
                                                        -
                    <asp:TextBox ID="cliEnd_fone3TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone3") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Referência</td>
                                                    <td>
                                                        <asp:TextBox ID="cliEnd_referenciaTextBox" runat="server" Columns="25" MaxLength="25" Text='<%# Bind("cliEnd_referencia") %>'></asp:TextBox></td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="InsertButton" runat="server" CssClass="btn_form" CausesValidation="True" CommandName="Insert" ValidationGroup="validGroupCadastroClienteAdicional" Text="Salvar" />
                                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Cancel" Text="Cancelar" OnClick="LinkButtonInsertCancel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <fieldset>
                                                <legend style="margin-left: 41px; padding: 5px">
                                                    <asp:Button ID="ButtonSelecionarEnderecoAdicional" runat="server" CommandName="Select" Text="Entregar aqui" OnClientClick="return DesabilitarDuploClick(this,'Calculando entrega...','', false);" />
                                                    <-- Selecione</legend>
                                                <%# Eval("cliEnd_nome")%> <%# Eval("cliEnd_sobrenome") %><br />
                                                <%# Eval("cliEnd_endereco")%>,  <%# Eval("cliEnd_numero") %>,  <%# Eval("cliEnd_bairro") %><br />
                                                <%# Eval("cliEnd_cidade")%> -  <%# Eval("cliEnd_estado") %><br />
                                                <asp:Literal ID="cliEnd_cepLiteral" runat="server" Text='<%# Eval("cliEnd_cep")%>'></asp:Literal>
                                                <br />
                                                (<%# Eval("cliEnd_ddd1") %>)  <%# Eval("cliEnd_fone1") %>
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="EditButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Edit" Text="Editar" />
                                                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir?');" /><br />
                                                <br />
                                            </fieldset>
                                        </ItemTemplate>
                                        <SelectedItemTemplate>
                                            <fieldset style="border: solid blue 2px;">
                                                <legend style="margin-left: 41px; padding: 5px">
                                                    <asp:Button ID="ButtonSelecionarEnderecoAdicional" runat="server" CommandName="Select" Text="Entregar aqui" BackColor="Blue" OnClientClick="return DesabilitarDuploClick(this,'Calculando entrega...','', false);" />
                                                    <-- Selecione</legend>
                                                <%# Eval("cliEnd_nome")%> <%# Eval("cliEnd_sobrenome") %><br />
                                                <%# Eval("cliEnd_endereco")%>,  <%# Eval("cliEnd_numero") %>,  <%# Eval("cliEnd_bairro") %><br />
                                                <%# Eval("cliEnd_cidade")%> -  <%# Eval("cliEnd_estado") %><br />
                                                <asp:Literal ID="cliEnd_cepLiteral" runat="server" Text='<%# Eval("cliEnd_cep")%>'></asp:Literal>
                                                <br />
                                                (<%# Eval("cliEnd_ddd1") %>)  <%# Eval("cliEnd_fone1") %>
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="EditButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Edit" Text="Editar" />
                                                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn_form" CausesValidation="False" CommandName="Delete" Text="Excluir" /><br />
                                                <br />
                                            </fieldset>
                                        </SelectedItemTemplate>
                                        <ItemSeparatorTemplate>
                                            <hr />
                                        </ItemSeparatorTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                        </div>
                    </div>
                    <div class="PagCol2">
                        <div class="clear">
                            <span class="numeral">03</span> <span class="TituloItem">Opções de Entrega</span>
                            <div style="clear: both; height: 250px;">
                                <!-- Listagem -->
                                <ul>
                                    <li>Método</li>
                                    <li>Preço</li>
                                    <li>Prazo</li>
                                </ul>
                                <ul id="ListaTiposEntrega" class="Lista" style="display: none">
                                    <li>
                                        <asp:RadioButton ID="RadioButtonCorreios" runat="server" Checked="true" />
                                        <label for="RadioButtonCorreios">Correios</label>
                                    </li>
                                    <li>
                                        <asp:Literal ID="LiteralValorEntrega" runat="server" Text="R$ 10,00"></asp:Literal></li>
                                    <li>
                                        <asp:Literal ID="LiteralPrazoEntrega" runat="server" Text="9 a 10 dias"></asp:Literal></li>
                                </ul>
                                <ul id="ListaTiposEntregaCarrega" class="Lista">
                                    Digite seu cep para calcular seu frete
                                </ul>
                            </div>
                        </div>
                        <div class="clear" style="padding-top: 100px;">
                            <span class="numeral">04</span> <span class="TituloItem">Formas de Pagamento</span>
                            <div class="clear"></div>
                            <asp:RadioButton ID="RadioButtonPagamentoPagSeguro" runat="server" Checked="true" />
                            <label for="RadioButtonPagamentoPagSeguro">PagSeguro</label><br />
                            <br />
                            <img src="imagens/objetos/pag_seguro.png">
                            <br />
                            <asp:Panel ID="PanelPagamentoCartao" runat="server" Visible="false">
                                <br />
                                <input type="radio" name="radio" id="Pagamento3" value="Pagamento" />
                                <label for="Pagamento3">Boleto Bancário</label><br>
                                <br>

                                <input type="radio" name="radio" id="Pagamento4" value="Pagamento">
                                <label for="Pagamento4">Cartão de Crédito</label>
                                <br>
                                <br>
                                <p>
                                    <img src="imagens/objetos/cartoes.png" width="94" height="18">
                                </p>
                                * a bandeira de seu cartão será selecionada automaticamente.
       <span class="Rotulo">Número de parcelas<br>
           <br>
       </span>

                                <select name="select5" size="1" class="form_list" id="select5">
                                    <option>01 x R$299,90</option>
                                    <option>02x R$150,00</option>
                                </select>
                                <label for="textfield5" class="Rotulo">Nº do Cartão</label>
                                <input type="text" name="textfield5" id="text7" class="CaixaTexto">
                                <label for="textfield5" class="Rotulo">Nome do Cartão</label>
                                <input type="text" name="textfield5" id="text8" class="CaixaTexto">

                                <label for="cep" class="Rotulo">Vencimento</label>
                                <input type="text" name="cep" id="Text9" class="CaixaTexto" style="width: 120px;">
                                <label for="cep" class="Rotulo">Código de Seguraça</label>
                                <input type="text" name="cep" id="Text10" class="CaixaTexto" style="width: 120px;">
                            </asp:Panel>
                        </div>

                    </div>
                    <div class="PagCol3">
                        <div class="clear">
                            <span class="numeral">05</span> <span class="TituloItem">Revisão do Pedido</span>
                        </div>
                        <%-- <asp:Button ID="ButtonFinalizarPedido2" runat="server" ValidationGroup="groupCadastroCliente" OnClick="ButtonFinalizarPedido_Click" CssClass="btn_finalizar" Text="Finalizar Pedido" />--%>
                        <div class="clear"></div>
                        <ul>
                            <li style="width: 140px">Produto</li>
                            <li>Qtde</li>
                            <li>Preço</li>
                        </ul>
                        <ul id="ListaCarrinho" class="Lista">
                        </ul>
                        <hr />
                        <a onclick="ObjetoVisible('PanelCupomDesconto')" style="cursor: pointer; text-decoration: underline">Aplicar Cupom de Desconto</a>
                        <asp:Panel ID="PanelCupomDesconto" runat="server" Style="display: none" DefaultButton="ButtonAplicarChaveCupom">
                            <asp:TextBox ID="TextBoxChaveCupom" runat="server" CssClass="CaixaTexto" ValidationGroup="groupCupom" Style="width: 190px;"></asp:TextBox>
                            <asp:Button ID="ButtonAplicarChaveCupom" runat="server" Text="Aplicar" CssClass="btn_apply" ValidationGroup="groupCupom" OnClientClick="if(Page_ClientValidate('groupCupom')) return SelectCarrinhoTotais(); else $('#cup_msgErro').text('');" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCupom" runat="server" ErrorMessage="Insira o código do cupom" CssClass="MsgError" Display="Dynamic" SetFocusOnError="true" ValidationGroup="groupCupom" ControlToValidate="TextBoxChaveCupom"></asp:RequiredFieldValidator>
                            <div id="cup_msgErro" class="MsgError"></div>
                        </asp:Panel>
                        <div class="clear" style="border: 1px solid #999; height: 170px;">
                            <div style="float: left; width: 150px; padding: 5px;">
                                Subtotal
                            </div>
                            <div id="cart_subTotal" style="float: right; width: 100px; text-align: right; padding: 5px;">
                            </div>
                            <hr />
                            <span id="ListaDesconto" style="display: none">
                                <div style="float: left; width: 150px; padding: 5px;">
                                    Desconto
                                </div>
                                <div id="cup_valor" style="float: right; width: 100px; text-align: right; padding: 5px;">
                                </div>
                                <hr />
                            </span>
                            <div style="float: left; width: 150px; padding: 5px;">
                                Entrega
                            </div>
                            <div id="cart_entregaTotal" style="float: right; width: 100px; text-align: right; padding: 5px;">
                            </div>
                            <hr />
                            <div style="float: left; width: 150px; padding: 5px;">
                                <span class="preco">Total</span>
                            </div>
                            <div style="float: right; width: 100px; text-align: right; padding: 5px;">
                                <span id="cart_total" class="preco"></span>
                            </div>
                            <hr />
                            <div id="cart_condicao" style="float: right; width: 200px; text-align: right; padding: 5px;">
                            </div>

                        </div>
                        <asp:Button ID="ButtonFinalizarPedido" runat="server" CssClass="btn_finalizar" Text="Finalizar Pedido" ValidationGroup="groupCadastroCliente" OnClick="ButtonFinalizarPedido_Click" />
                    </div>
                </div>
                <div class="clear"></div>
                <asp:HiddenField ID="HiddenFieldCepEntrega" runat="server" />
            </div>

        </div>
        <asp:HiddenField runat="server" ID="HiddenFieldTc" />
        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>





    <%--

<script src="http://www.dreamson.com.br/loja/js/prototype/prototype.js"></script>


    <div id="div-innerhtml-pagseguro">
</div>

<style type='text/css'>
#parcelamento-pag-seguro {
    border: 1px solid #C5E093;
    width: 350px !important;
    border-collapse: collapse;
    padding: 0;
}
#parcelamento-pag-seguro td {
    width: 50%;
    border-bottom: 1px solid #C5E093;
    padding: 2px;
}
.zebra-tabela {
    background: #F0F9E6;
    padding: 0px;
}
.informacao-taxa {
    color: #8BC763;
}
.exibe-informacao-sobre-cartao {
    background: #8BC763;
    color: #FFF; font-weight: bold;
}
#titulo-parcele-no-pagseguro {
    color: #569C39;
}
</style>
<script type="text/javascript">
    function formatJuros(num) {
        num = "" + Math.floor(num * 100.0 + 0.5) / 100.0;

        var i = num.indexOf(".");

        if (i < 0)
            num += ",00";
        else {
            num = num.substring(0, i) + "," + num.substring(i + 1);
            i = (num.length - i) - 1;
            if (i == 0) num += "00";
            else if (i == 1) num += "0";
            else if (i > 2) num = num.substring(0, i + 3);
        }
        return num;
    }

    Parcelamento = Class.create();
    Parcelamento.prototype = {

        initialize: function (preco) {
         
            this.taxaJuros = 2.99;
            this.taxaJurosConvert = (this.taxaJuros / 100);
         
            this.qtdParcelas = 12;
            this.parcelaMin = 5;
            this.tipoParcelamento = 'pagseguro';

            //Config de parcelamento sem juros
            this.semJurosParcelas = 2;
            this.semJurosMinCompra = 0;
            this.semJurosMaxCompra = 0;

            this.innerTable = '<h3 id="titulo-parcele-no-pagseguro">Parcele no Cartão com PagSeguro</h3>';
            this.innerTable = this.innerTable + '<table id="parcelamento-pag-seguro">';

            this.tableParc = $('div-innerhtml-pagseguro');

            this.preco = preco;

        },

        criaParcelamento: function () {

            if (this.preco == undefined) {
                return false;
            }

            if (this.preco <= this.parcelaMin) {
                this.tableParc.update();
                return false;
            } 

            this.count = 1;
            this.countZebra = 2;
            this.msgJuros = false;
           
            for (this.i = 2; this.i <= this.qtdParcelas; this.i++) {
                this.parcela = new Array();

                if (
                    (this.tipoParcelamento == 'semacrescimo'
                     && this.i <= this.semJurosParcelas) &&
                    (
                        (this.preco >= this.semJurosMinCompra
                        && this.semJurosMaxCompra == 0) ||
                        (this.preco >= this.semJurosMinCompra
                        && this.preco <= this.semJurosMaxCompra)
                    )
                ) {

                    this.parcela['juros'] = false;
                    this.parcela['valor'] = this.preco / this.i;

                    if ((this.i + 1) <= this.semJurosParcelas) {
                        this.parcela['valor_prox_parcela'] = this.preco / (this.i + 1);
                    } else {
                        this.parcela['valor_prox_parcela'] = (this.taxaJurosConvert / (1 - Math.pow((1 + this.taxaJurosConvert), ((this.i + 1) * -1))) * this.preco);
                    }

                } else {
                    alert(this.i);
                    alert(this.taxaJurosConvert);
                    var math = (this.taxaJurosConvert / (1 - Math.pow((1 + this.taxaJurosConvert), (this.i * -1))));

                    this.parcela['juros'] = true;
                    this.parcela['valor'] = (this.taxaJurosConvert / (1 - Math.pow((1 + this.taxaJurosConvert), (this.i * -1))) * this.preco);
                    this.parcela['valor_prox_parcela'] = (this.taxaJurosConvert / (1 - Math.pow((1 + this.taxaJurosConvert), ((this.i + 1) * -1))) * this.preco);

                    alert(this.parcela['valor']);

                    this.msgJuros = true;
                }
              
                if (this.parcela['valor'] >= this.parcelaMin) {
                    if (this.count % 2) {
                        if (this.countZebra % 2) {
                            this.trClass = 'class="zebra-tabela"';
                        } else {
                            this.trClass = '';
                        }
                        this.innerTable = this.innerTable + '<tr ' + this.trClass + '>';
                    }
                   
                    colspan = '';
                    if (((this.parcela['valor_prox_parcela'] < this.parcelaMin) || (this.i + 1) > this.qtdParcelas) && (this.i % 2) == 0) {
                        colspan = 'colspan="2"';
                    }
                   
                    this.innerTable = this.innerTable + '<td ' + colspan + '><strong>' + this.i + 'x </strong>' + float2moeda(this.parcela['valor']);
                   
                    if (this.parcela['juros']) {
                        this.innerTable = this.innerTable + ' *';
                    } else {
                        this.innerTable = this.innerTable + ' <i>sem juros</i>';
                    }
                    this.innerTable = this.innerTable + '</td>';

                    if (!(this.count++ % 2)) {
                        this.countZebra += 3;
                    } 
                } else {
                    if (this.i == 2) {
                       
                        this.tableParc.update();
                        return false;
                    }
                }
            }
            
            this.innerTable = this.innerTable + '<tr><td colspan="2" class="exibe-informacao-sobre-cartao">Em todos os cartões de Crédito</td></tr>';
            this.innerTable = this.innerTable + '<tr><td colspan="2"><img src="http://loja.meuadesivo.com.br/skin/frontend/default/generico/bandeiras-cartao.gif" alt="Bandeiras de Cartões de Crédito" /></td></tr>';
           
            if (this.msgJuros) {
                this.innerTable = this.innerTable + '<tr><td colspan="2" class="informacao-taxa">*  Taxa de ' + formatJuros(this.taxaJuros) + '% ao mês (parcela mínima de ' + float2moeda(this.parcelaMin) + ')</td></tr>';
            }
           
            this.innerTable = this.innerTable + '</table>';
          
            this.tableParc.update(this.innerTable); 
        }
    };

    try{

        var parcelamentoPagseguro = new Parcelamento(300.00);
        parcelamentoPagseguro.criaParcelamento();
    } catch (ex) {
        var text = "There was an error on this page.\n\n";
        text += "Error description: " + err.message + "\n\n";
        text += "Click OK to continue.\n\n";
        alert(text);
    }

</script>
    --%>
</body>
</html>
