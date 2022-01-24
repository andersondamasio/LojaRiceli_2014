<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeuCadastro.aspx.cs" Inherits="_1_WebForm.MeuCadastro" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Validacao.js"></script>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />

                <div id="corpo">
                    <div id="conteudo" style="width: 990px; margin-top: 5px;">
                        <h1>Minha Conta</h1>
                        <div id="conta">
                            <div style="border-bottom: 1px solid #8A8A7B; height: 30px;">
                                <ul class="abas">
                                    <li>Meu Cadastro</li>
                                    <li id="abaMeusEnderecos" class="aba2" runat="server" visible="false"><a href="MeusEnderecos">Meus Endereços</a></li>
                                    <li id="abaMeusPedidos" class="aba2" runat="server" visible="false"><a href="MeusPedidos">Meus Pedidos</a></li>
                                    <li id="abaMeusAmigos" class="aba2" runat="server" visible="false"><a href="MeusAmigos">Meus Amigos</a></li>
                                    <li id="abaMeusPontos" class="aba2" runat="server" visible="false"><a href="MeusPontos">Meus Pontos</a></li>
                                    <li id="abaMeuFeed" class="aba2" runat="server" visible="false"><a href="MeuFeed">Feed de Compras</a></li>
                                </ul>
                            </div>
                          
                            <asp:Panel ID="PanelTipoCliente" runat="server" Visible="false">
                                <asp:RadioButton ID="RadioButtonClienteFisica" runat="server" Text="Sou Pessoa Física" GroupName="groupTipoCliente" AutoPostBack="True" Checked="True" OnCheckedChanged="RadioButtonClienteFisica_CheckedChanged" />
                                <asp:RadioButton ID="RadioButtonClienteJuridica" runat="server" Text="Sou Pessoa Jurídica" GroupName="groupTipoCliente" AutoPostBack="True" OnCheckedChanged="RadioButtonClienteJuridica_CheckedChanged" />
                            </asp:Panel>

                            <asp:Panel runat="server" ID="PanelCadastroCliente" DefaultButton="ButtonCadastroClienteSalvar">
                                <div id="col1">
                                    <p>
                                        <label for="cli_emailTextBox">Email:</label>
                                        <asp:TextBox ID="cli_emailTextBox" runat="server" CssClass="form_conta" MaxLength="128" Text='<%# Session["cli_email"]+"" %>' Enabled="false"/>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_emailTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server" 
                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailTextBox"
                                            ErrorMessage="Email inválido" ValidationGroup="groupCadastroCliente"  CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                     <asp:LinkButton ID="LinkButtonVoltarAlterarEmail" runat="server" Style="float:right" OnClick="LinkButtonAlterarEmail_Click">voltar e alterar email</asp:LinkButton></p>
                                     <p>

                                  <label for="ButtonAlterarSenha"></label>
                                         <fieldset id="fieldsetAlterarSenha" runat="server">
                                             <asp:Button ID="ButtonAlterarSenha" runat="server" Text="Alterar minha senha"  OnClick="ButtonAlterarSenha_Click" />
                                             <asp:Button ID="ButtonCancelarAlterarSenha" runat="server" Text="Cancelar" OnClick="ButtonCancelarAlterarSenha_Click" />
                                    
                                    <asp:Panel ID="PanelSenha" runat="server">

                                        <p>
                                            <label for="cli_senhaTextBox">Senha</label>
                                            <asp:TextBox ID="cli_senhaTextBox" runat="server" CssClass="form_conta" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_senhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <label for="cli_confirmacaoSenhaTextBox">Confirmar senha</label>
                                            <asp:TextBox ID="cli_confirmacaoSenhaTextBox" runat="server" CssClass="form_conta" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="compareSenha" runat="server" ValidationGroup="groupCadastroCliente" ControlToCompare="cli_senhaTextBox" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Senha e confirmação estão diferentes" Display="Dynamic" />
                                        </p>
                                    </asp:Panel>
                                    </fieldset>
                                          </p>
                                    <asp:Panel runat="server" ID="PanelDadosPessoaFisica1">
                                        <p>
                                            <label for="cli_cpfTextBox">CPF</label>
                                            <asp:TextBox ID="cli_cpfTextBox" runat="server" CssClass="form_conta" MaxLength="14" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_cpfTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="groupCadastroCliente" ErrorMessage="Cpf inválido(use apenas números sem separadores)." ControlToValidate="cli_cpfTextBox" ClientValidationFunction="valida_CPF"  CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>

                                        </p>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="PanelDadosPessoaJuridica1" Visible="false">
                                        <p>
                                            <label for="cli_cnpjTextBox">CNPJ</label>
                                            <asp:TextBox ID="cli_cnpjTextBox" runat="server" CssClass="form_conta" MaxLength="14" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_cnpjTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="groupCadastroCliente" ErrorMessage="Cnpj inválido(use apenas números sem separadores)." ControlToValidate="cli_cnpjTextBox" ClientValidationFunction="valida_CNPJ"  CssClass="MsgError" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>


                                        </p>
                                    </asp:Panel>
                                    <p>
                                        <label for="cli_nomeTextBox">Nome</label>
                                        <asp:TextBox ID="cli_nomeTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_nomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <label for="cli_sobrenomeTextBox">Sobrenome</label>
                                        <asp:TextBox ID="cli_sobrenomeTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_sobrenomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </p>
                                    <asp:Panel runat="server" ID="PanelDadosPessoaFisica2">

                                        <label for="cli_diaNascimentoDropDownList">Data de Nascimento</label>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_diaNascimentoDropDownList" ErrorMessage="Dia é campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_mesNascimentoDropDownList" ErrorMessage="Mês é campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_anoNascimentoDropDownList" ErrorMessage="Ano é campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                        <p>
                                            <label for="cli_sexoDropDownList">Sexo</label>
                                            <asp:DropDownList ID="cli_sexoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# Bind("cli_sexo") %>'>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_sexoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="PanelDadosPessoaJuridica2" Visible="false">
                                        <p>
                                            <label for="cli_inscricaoEstadualTextBox">Inscrição estadual</label>
                                            <asp:TextBox ID="cli_inscricaoEstadualTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_inscricaoEstadualTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                             <label for="cli_inscricaoEstadualIsentoCheckBox"></label>
                                            <asp:CheckBox ID="cli_inscricaoEstadualIsentoCheckBox" runat="server" />
                                            Insento
                                        </p>
                                        <p>
                                            <label for="cli_razaoSocialTextBox">Razão Social</label>
                                            <asp:TextBox ID="cli_razaoSocialTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_razaoSocialTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                            </asp:RequiredFieldValidator>

                                        </p>

                                    </asp:Panel>
                                    <p>&nbsp;</p>


                                </div>
                                <div id="col2">
                                    <p>
                                        <label for="cli_cepTextBox">CEP</label>
                                        <asp:TextBox ID="cli_cepTextBox" runat="server" CssClass="form_conta" MaxLength="8" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_cepTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <label for="cli_enderecoTextBox">Endereço</label>
                                        <asp:TextBox ID="cli_enderecoTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_enderecoTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </p>
                                    <p>

                                        <label for="cli_numeroTextBox">Numero</label>
                                        <asp:TextBox ID="cli_numeroTextBox" runat="server" CssClass="form_conta" MaxLength="16" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_numeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </p>
                                    <p>
                                        <label for="cli_complementoTextBox">Complemento</label>
                                        <asp:TextBox ID="cli_complementoTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                    </p>
                                    <p>
                                        <label for="cli_bairroTextBox">Bairro</label>
                                        <asp:TextBox ID="cli_bairroTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_bairroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                    </p>

                                    <div style="width: 70%; float: left;">
                                        <label for="cli_cidadeTextBox">
                                        Cidade</label>
                                        <asp:TextBox ID="cli_cidadeTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="cli_cidadeTextBox" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                    </div>
                                    <div style="width: 25%; float: right;">
                                        <label for="cli_estadoDropDownList">
                                        Estado</label>
                                        <asp:DropDownList ID="cli_estadoDropDownList" runat="server" CssClass="form_list" SelectedValue='<%# Bind("cli_estado") %>'>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="cli_estadoDropDownList" CssClass="MsgError" Display="Dynamic" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" ValidationGroup="groupCadastroCliente">
                                            </asp:RequiredFieldValidator>
                                    </div>
                                    <p>
                                    </p>
                                    <p>
                                        <label for="cli_referenciaTextBox">
                                        Referência</label>
                                        <asp:TextBox ID="cli_referenciaTextBox" runat="server" CssClass="form_conta" MaxLength="64" />
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>

                                </div>
                                <div id="col3">

                                    <div style="width: 25%; float: left;">
                                        <label for="cli_ddd1TextBox">DDD</label>
                                        <asp:TextBox ID="cli_ddd1TextBox" runat="server" CssClass="form_conta" MaxLength="2" />
                                    </div>
                                    <div style="width: 70%; float: right">
                                        <label for="cli_fone1TextBox">Telefone Residencial</label>
                                        <asp:TextBox ID="cli_fone1TextBox" runat="server" CssClass="form_conta" MaxLength="16" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_ddd1TextBox" ErrorMessage="DDD é Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ValidationGroup="groupCadastroCliente" ControlToValidate="cli_fone1TextBox" ErrorMessage="Telefone é Campo obrigátorio" SetFocusOnError="True"  CssClass="MsgError" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <div style="width: 25%; float: left;">
                                        <label for="cli_ddd2TextBox">DDD</label>
                                        <asp:TextBox ID="cli_ddd2TextBox" runat="server" CssClass="form_conta" MaxLength="2" />
                                    </div>
                                    <div style="width: 70%; float: right">
                                        <label for="cli_fone2TextBox">Telefone Comercial</label>
                                        <asp:TextBox ID="cli_fone2TextBox" runat="server" CssClass="form_conta" MaxLength="16" />

                                    </div>
                                    <div style="width: 25%; float: left;">
                                        <label for="cli_ddd3TextBox">DDD</label>
                                        <asp:TextBox ID="cli_ddd3TextBox" runat="server" CssClass="form_conta" MaxLength="2" />
                                    </div>
                                    <div style="width: 70%; float: right">
                                        <label for="cli_fone3TextBox">Telefone Celular</label>
                                        <asp:TextBox ID="cli_fone3TextBox" runat="server" CssClass="form_conta" MaxLength="16" />

                                    </div>
                                    <div style="clear: both; padding: 5px;">
                                        <p>
                                            <asp:CheckBox ID="cli_recebeInformativoCheckBox" runat="server" />
                                            Aceito receber ofertas e descontos exclusivos no email
                                        </p>
                                        <p>
                                            <asp:Button ID="ButtonCadastroClienteSalvar" runat="server" Text="Salvar Alterações" CssClass="btn_form" OnClick="ButtonCadastroClienteSalvar_Click" ValidationGroup="groupCadastroCliente" />
                                        </p>
                                    </div>

                                </div>

                            </asp:Panel>

                        </div>
                         <asp:HiddenField runat="server" ID="HiddenFieldTc" />
                        <div style="clear: both; height: 10px;"></div>
                    </div>
                    <!-- Fecha div Conteudo -->
                    <uc1:Rodape runat="server" ID="Rodape" />
                </div>
                </div>
    </form>
</body>
</html>
