<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroFinalizar.aspx.cs" Inherits="Loja.Views.CadastroFinalizar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<%@ Register Src="CabecalhoFinalizarPedido.ascx" TagName="CabecalhoFinalizarPedido" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finalizando seu Pedido - Riceli.com</title>
    <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/js/Validacao.js"></script>
</head>
<body>

    <form id="form1" runat="server">
        <uc1:CabecalhoFinalizarPedido ID="CabecalhoFinalizarPedido1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Views/js/WebServiceFrete.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="~/Servicos/WebServiceFrete.asmx" InlineScript="false" />
            </Services>
        </asp:ScriptManager>
        <table border="1" style="width: 778px">
            <tr style="vertical-align: top">
                <td>
                    <asp:Panel ID="PanelTipoCliente" runat="server">
                        <asp:RadioButton ID="RadioButtonClienteFisica" runat="server" Text="Sou Pessoa Física" GroupName="groupTipoCliente" AutoPostBack="True" Checked="True" OnCheckedChanged="RadioButtonClienteFisica_CheckedChanged" />
                        <asp:RadioButton ID="RadioButtonClienteJuridica" runat="server" Text="Sou Pessoa Jurídica" GroupName="groupTipoCliente" AutoPostBack="True" OnCheckedChanged="RadioButtonClienteJuridica_CheckedChanged" />
                        <br />
                    </asp:Panel>
                    1 - Suas informações<br />


                    <asp:FormView ID="FormViewCadastroCliente" runat="server" DataKeyNames="cli_id,loj_id"
                        DataSourceID="EntityDataSourceCadastroCliente" OnItemUpdating="FormViewCadastroCliente_ItemUpdating" OnItemCreated="FormViewCadastroCliente_ItemCreated">
                        <EditItemTemplate>
                            <table style="vertical-align: top" border="1">
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <asp:TextBox ID="cli_emailTextBox" Text='<%# Bind("cli_email") %>' runat="server" Columns="25" MaxLength="128" ValidationGroup="validGroupCadastroCliente"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_emailTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server"
                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailTextBox"
                                            ErrorMessage="Email inválido" ValidationGroup="validGroupCadastroCliente" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>

                                    </td>
                                </tr>
                                <asp:Panel ID="PanelInformacaoClientePessoaFisica" runat="server" Visible='<%# Eval("cli_cpfCnpj").ToString().Length == 11 %>'>
                                    <tr>
                                        <td>Cpf:</td>
                                        <td>
                                            <asp:TextBox ID="cli_cpfTextBox" Text='<%# Bind("cli_cpfCnpj") %>' runat="server" Columns="25" MaxLength="11"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_cpfTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="validGroupCadastroCliente" ErrorMessage="Cpf inválido(use apenas números sem separadores)." ControlToValidate="cli_cpfTextBox" ClientValidationFunction="valida_CPF" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Nome:</td>
                                        <td>
                                            <asp:TextBox  ID="cli_nomeTextBox" Text='<%# Bind("cli_nome") %>' runat="server" Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_nomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Sobrenome:</td>
                                        <td>
                                            <asp:TextBox ID="cli_sobrenomeTextBox" Text='<%# Bind("cli_sobrenome") %>' runat="server" Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_sobrenomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Nascimento:</td>
                                        <td>
                                            <asp:DropDownList ID="cli_diaNascimentoDropDownList" runat="server" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Day %>'>
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

                                            <asp:DropDownList ID="cli_mesNascimentoDropDownList" runat="server" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Month %>'>
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

                                            <asp:DropDownList ID="cli_anoNascimentoDropDownList" runat="server" SelectedValue='<%# ((DateTime)Eval("cli_dataNascimento")).Year %>'>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_diaNascimentoDropDownList" ErrorMessage="Dia é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_mesNascimentoDropDownList" ErrorMessage="Mês é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_anoNascimentoDropDownList" ErrorMessage="Ano é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sexo:</td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="cli_sexoDropDownList" runat="server" SelectedValue='<%# Bind("cli_sexo") %>'>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_sexoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="PanelInformacaoClientePessoaJuridica" runat="server" Visible='<%# Eval("cli_cpfCnpj").ToString().Length != 11 %>'>
                                    <tr>
                                        <td>Cnpj:</td>
                                        <td>
                                            <asp:TextBox ID="cli_cnpjTextBox" runat="server" Text='<%# Bind("cli_cpfCnpj") %>' Columns="25" MaxLength="14"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_cnpjTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="validGroupCadastroCliente" ErrorMessage="Cnpj inválido(use apenas números sem separadores)." ControlToValidate="cli_cnpjTextBox" ClientValidationFunction="valida_CNPJ" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Inscrição Estadual:</td>
                                        <td>
                                            <asp:TextBox ID="cli_inscricaoEstadualTextBox" Text='<%# Bind("cli_inscricaoEstadual") %>' runat="server" Columns="25" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_inscricaoEstadualTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <br />
                                            <asp:CheckBox ID="cli_inscricaoEstadualInsentoCheckBox" Checked='<%# Eval("cli_inscricaoEstadualIsento") ?? false %>' runat="server" Text="I.E. isento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Razao Social:</td>
                                        <td>
                                            <asp:TextBox ID="cli_razaoSocialTextBox" runat="server" Text='<%# Bind("cli_razaoSocial") %>' Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_razaoSocialTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                            <asp:Panel runat="server" ID="PanelCadastrarInformacaoEnderecoCobrancaEntrega">
                                2 - ENDEREÇOS DE COBRANÇA E ENTREGA<br />
                                <table border="1">
                                    <tr>
                                        <td>Cep</td>
                                        <td>
                                            <asp:TextBox ID="cli_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" Text='<%# Bind("cli_cep") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_cepTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Endereço</td>
                                        <td>
                                            <asp:TextBox ID="cli_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_endereco") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_enderecoTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Número</td>
                                        <td>
                                            <asp:TextBox ID="cli_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cli_numero") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_numeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Complemento</td>
                                        <td>
                                            <asp:TextBox ID="cli_complementoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_complemento") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Bairro</td>
                                        <td>
                                            <asp:TextBox ID="cli_bairroTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_bairro") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_bairroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Estado</td>
                                        <td>
                                            <asp:DropDownList ID="cli_estadoDropDownList" runat="server" SelectedValue='<%# Bind("cli_estado") %>'>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_estadoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cidade</td>
                                        <td>
                                            <asp:TextBox ID="cli_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_cidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_cidadeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd1") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone1") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_ddd1TextBox" ErrorMessage="DDD é Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_fone1TextBox" ErrorMessage="Telefone é Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone 2</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd2") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone2") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_ddd2TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ValidationGroup="validGroupCadastroCliente" ControlToValidate="cli_fone2TextBox" ErrorMessage="Telefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Celular</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd3TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd3") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone3TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone3") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Referência</td>
                                        <td>
                                            <asp:TextBox ID="cli_referenciaTextBox" runat="server" Columns="25" MaxLength="25" Text='<%# Bind("cli_referencia") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Deseja receber informativos e ofertas?</td>
                                        <td>
                                            <asp:CheckBox ID="cli_recebeInformativoCheckBox" runat="server" Checked="true" Text='Sim' />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <br />
                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Atualizar" ValidationGroup="validGroupCadastroCliente" />
                            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table style="vertical-align: top" border="1">
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <asp:TextBox ID="cli_emailTextBox" Text='<%# Session["cli_emailNovo"] == null ? string.Empty : Session["cli_emailNovo"].ToString() %>' runat="server" Columns="25" MaxLength="128"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_emailTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server"
                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailTextBox"
                                            ErrorMessage="Email inválido" ValidationGroup="validGroupFinalizarPedido" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>

                                    </td>

                                </tr>
                                <tr>
                                    <td>Senha:</td>
                                    <td>
                                        <asp:TextBox ID="cli_senhaTextBox" Text='<%# Bind("cli_senha") %>' runat="server" Columns="25" MaxLength="32" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_senhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Confirmar:</td>
                                    <td>
                                        <asp:TextBox ID="cli_confirmarSenhaTextBox" Text='<%# Bind("cli_confirmarSenha") %>' runat="server" Columns="25" MaxLength="32" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_confirmarSenhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="compareSenha" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToCompare="cli_senhaTextBox" ControlToValidate="cli_confirmarSenhaTextBox" ErrorMessage="Senha e confirmação estão diferentes" Display="Dynamic" />
                                    </td>

                                </tr>

                                <asp:Panel ID="PanelInformacaoClientePessoaFisica" runat="server">
                                    <tr>
                                        <td>Cpf:</td>
                                        <td>
                                            <asp:TextBox ID="cli_cpfTextBox" Text='<%# Bind("cli_cpfCnpj") %>' runat="server" Columns="25" MaxLength="11"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_cpfTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="validGroupFinalizarPedido" ErrorMessage="Cpf inválido.(use apenas números sem separadores)" ControlToValidate="cli_cpfTextBox" ClientValidationFunction="valida_CPF" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Nome:</td>
                                        <td>
                                            <asp:TextBox ID="cli_nomeTextBox" Text='<%# Bind("cli_nome") %>' runat="server" Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_nomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Sobrenome:</td>
                                        <td>
                                            <asp:TextBox ID="cli_sobrenomeTextBox" Text='<%# Bind("cli_sobrenome") %>' runat="server" Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_sobrenomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Nascimento:</td>
                                        <td>
                                            <asp:DropDownList ID="cli_diaNascimentoDropDownList" runat="server" SelectedValue='<%# Bind("cli_diaNascimento") %>'>
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
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

                                            <asp:DropDownList ID="cli_mesNascimentoDropDownList" runat="server" SelectedValue='<%# Bind("cli_mesNascimento") %>'>
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:DropDownList ID="cli_anoNascimentoDropDownList" runat="server" SelectedValue='<%# Bind("cli_anoNascimento") %>'>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_diaNascimentoDropDownList" ErrorMessage="Dia é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_mesNascimentoDropDownList" ErrorMessage="Mês é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_anoNascimentoDropDownList" ErrorMessage="Ano é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sexo:</td>
                                        <td>
                                            <asp:DropDownList ID="cli_sexoDropDownList" runat="server" SelectedValue='<%# Bind("cli_sexo") %>'>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_sexoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="PanelInformacaoClientePessoaJuridica" runat="server" Visible="false">
                                    <tr>
                                        <td>Cnpj:</td>
                                        <td>
                                            <asp:TextBox ID="cli_cnpjTextBox" runat="server" Text='<%# Bind("cli_Cnpj") %>' Columns="25" MaxLength="14"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_cnpjTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator3" runat="server" ValidationGroup="validGroupFinalizarPedido" ErrorMessage="Cnpj inválido(use apenas números sem separadores)." ControlToValidate="cli_cnpjTextBox" ClientValidationFunction="valida_CNPJ" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Inscrição Estadual:</td>
                                        <td>
                                            <asp:TextBox ID="cli_inscricaoEstadualTextBox" Text='<%# Bind("cli_inscricaoEstadual") %>' runat="server" Columns="25" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_inscricaoEstadualTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <br />
                                            <asp:CheckBox ID="cli_inscricaoEstadualInsentoCheckBox" Checked='<%# Bind("cli_inscricaoEstadualIsento") %>' runat="server" Text="I.E. isento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Razao Social:</td>
                                        <td>
                                            <asp:TextBox ID="cli_razaoSocialTextBox" runat="server" Text='<%# Bind("cli_razaoSocial") %>' Columns="25" MaxLength="64"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_razaoSocialTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                            <asp:Panel runat="server" ID="PanelCadastrarInformacaoEnderecoCobrancaEntrega">
                                2 - Seu endereço de cobrança e entrega
                                <table border="1">
                                    <tr>
                                        <td>Cep</td>
                                        <td>
                                            <asp:TextBox ID="cli_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" Text='<%# Bind("cli_cep") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_cepTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Endereço</td>
                                        <td>
                                            <asp:TextBox ID="cli_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_endereco") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_enderecoTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Número</td>
                                        <td>
                                            <asp:TextBox ID="cli_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cli_numero") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_numeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Complemento</td>
                                        <td>
                                            <asp:TextBox ID="cli_complementoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_complemento") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Bairro</td>
                                        <td>
                                            <asp:TextBox ID="cli_bairroTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_bairro") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_bairroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Estado</td>
                                        <td>
                                            <asp:DropDownList ID="cli_estadoDropDownList" runat="server" SelectedValue='<%# Bind("cli_estado") %>'>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_estadoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cidade</td>
                                        <td>
                                            <asp:TextBox ID="cli_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cli_cidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_cidadeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd1") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone1") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_ddd1TextBox" ErrorMessage="DDD é Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_fone1TextBox" ErrorMessage="Telefone é Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone 2</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd2") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone2") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_ddd2TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cli_fone2TextBox" ErrorMessage="Telefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Celular</td>
                                        <td>
                                            <asp:TextBox ID="cli_ddd3TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cli_ddd3") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cli_fone3TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cli_fone3") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Referência</td>
                                        <td>
                                            <asp:TextBox ID="cli_referenciaTextBox" runat="server" Columns="25" MaxLength="25" Text='<%# Bind("cli_referencia") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Deseja receber informativos e ofertas?</td>
                                        <td>
                                            <asp:CheckBox ID="cli_recebeInformativoCheckBox" runat="server" Checked="true" Text='Sim' />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            Endereco de entrega e cobranca<br />
                            <%# Eval("cli_nome")%> <%# Eval("cli_sobrenome") %><br />
                            <%# Eval("cli_endereco")%>,  <%# Eval("cli_numero") %>,  <%# Eval("cli_bairro") %><br />
                            <%# Eval("cli_cidade")%> -  <%# Eval("cli_estado") %><br />
                            <asp:Literal ID="cli_cepLiteral" runat="server" Text='<%# Eval("cli_cep")%>'></asp:Literal>
                            <br />
                            (<%# Eval("cli_ddd1") %>)  <%# Eval("cli_fone1") %>
                            <br />
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar" />
                        </ItemTemplate>
                    </asp:FormView>


                    <asp:EntityDataSource ID="EntityDataSourceCadastroCliente" runat="server" ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities" EnableFlattening="False" EnableUpdate="True"
                        EntitySetName="Cliente" EntityTypeFilter="Cliente" Where="it.[cli_id] = @cli_id">
                        <WhereParameters>
                            <asp:SessionParameter Name="cli_id" SessionField="cli_id" Type="Int32" />
                        </WhereParameters>
                    </asp:EntityDataSource>


                    <asp:CheckBox ID="CheckBoxCadastrarEnderecoAdicional" runat="server" AutoPostBack="True" Text="Endereço de entrega diferente da cobrança" OnCheckedChanged="CheckBoxCadastrarEnderecoAdicional_CheckedChanged" /><br />
                    <span id="spanEnderecoAdicional">


                        <asp:Button ID="ButtonNovoEnderecoEntrega" runat="server" Text="Novo endereco" OnClick="ButtonNovoEnderecoEntrega_Click" Visible="False" /><br />
                        <asp:ListView ID="ListViewCadastroClienteAdicional" runat="server"
                            DataKeyNames="cliEnd_id,loj_id"
                            DataSourceID="EntityDataSourceFormViewCadastroClienteAdicional"
                            Visible="false"
                            InsertItemPosition="FirstItem"
                            OnItemCreated="ListViewCadastroClienteAdicional_ItemCreated"
                            OnSelectedIndexChanged="ListViewCadastroClienteAdicional_SelectedIndexChanged">
                            <EditItemTemplate>
                                <table border="1">
                                    <tr>
                                        <td>Nome</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_nomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_nome") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_nomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sobrenome</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_sobrenomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_sobrenome") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_sobrenomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cep</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" Text='<%# Bind("cliEnd_cep") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_cepTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Email</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_emailTextBox" runat="server" Columns="25" MaxLength="128" Text='<%# Bind("cliEnd_email") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_emailTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Endereço</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_endereco") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_enderecoTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Número</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cliEnd_numero") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_numeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_bairroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_estadoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cidade</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_cidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_cidadeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_sexoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd1") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cliEnd_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone1") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_ddd1TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_fone1TextBox" ErrorMessage="Telfefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone 2</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd2") %>'></asp:TextBox>

                                            -
                    <asp:TextBox ID="cliEnd_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone2") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_ddd2TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="cliEnd_fone2TextBox" ErrorMessage="Telfefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                        <td>Deseja receber informativos e ofertas?</td>
                                        <td>
                                            <asp:CheckBox ID="cliEnd_recebeInformativoCheckBox" runat="server" Checked='<%# Bind("cliEnd_recebeInformativo") %>' Text="Sim" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Atualizar" />
                                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                <br />
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <table border="1">
                                    <tr>
                                        <td>Nome</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_nomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_nome") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_nomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sobrenome</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_sobrenomeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_sobrenome") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_sobrenomeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cep</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_cepTextBox" runat="server" Columns="25" MaxLength="8" autocomplete="off" Text='<%# Bind("cliEnd_cep") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_cepTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCEP" ForeColor="red" runat="server" Text="Insira um CEP válido. Exemplo: 31030863 - não utilize traço"
                                                ControlToValidate="cliEnd_cepTextBox" ValidationGroup="validGroupFinalizarPedido" ValidationExpression="\d{5}(\d{3})?" Display="Dynamic"></asp:RegularExpressionValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Email</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_emailTextBox" runat="server" Columns="25" MaxLength="128" Text='<%# Bind("cliEnd_email") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_emailTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Endereço</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_enderecoTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_endereco") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_enderecoTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Número</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_numeroTextBox" runat="server" Columns="25" MaxLength="16" Text='<%# Bind("cliEnd_numero") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_numeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_bairroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_estadoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cidade</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_cidadeTextBox" runat="server" Columns="25" MaxLength="64" Text='<%# Bind("cliEnd_cidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_cidadeTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_sexoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_ddd1TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd1") %>'></asp:TextBox>
                                            -
                    <asp:TextBox ID="cliEnd_fone1TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone1") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_ddd1TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_fone1TextBox" ErrorMessage="Telfefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DDD+Telefone 2</td>
                                        <td>
                                            <asp:TextBox ID="cliEnd_ddd2TextBox" runat="server" Columns="1" MaxLength="2" Text='<%# Bind("cliEnd_ddd2") %>'></asp:TextBox>

                                            -
                    <asp:TextBox ID="cliEnd_fone2TextBox" runat="server" Columns="15" MaxLength="16" Text='<%# Bind("cliEnd_fone2") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_ddd2TextBox" ErrorMessage="DDD é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ValidationGroup="validGroupCadastroClienteAdicional" ControlToValidate="cliEnd_fone2TextBox" ErrorMessage="Telfefone é campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
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
                                        <td>Deseja receber informativos e ofertas?</td>
                                        <td>
                                            <asp:CheckBox ID="cliEnd_recebeInformativoCheckBox" runat="server" Checked='<%# Bind("cliEnd_recebeInformativo") %>' Text="Sim" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" ValidationGroup="validGroupCadastroClienteAdicional" Text="Inserir" />
                                            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" OnClick="InsertCancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <%# Eval("cliEnd_nome")%> <%# Eval("cliEnd_sobrenome") %><br />
                                <%# Eval("cliEnd_endereco")%>,  <%# Eval("cliEnd_numero") %>,  <%# Eval("cliEnd_bairro") %><br />
                                <%# Eval("cliEnd_cidade")%> -  <%# Eval("cliEnd_estado") %><br />
                                <asp:Literal ID="cliEnd_cepLiteral" runat="server" Text='<%# Eval("cliEnd_cep")%>'></asp:Literal>
                                <br />
                                (<%# Eval("cliEnd_ddd1") %>)  <%# Eval("cliEnd_fone1") %>
                                <br />
                                <asp:Button ID="ButtonSelecionarEnderecoAdicional" runat="server" CommandName="Select" Text="Entregar aqui" OnClientClick="return DesabilitarDuploClick(this,'Calculando entrega...','', false);" />
                                <br />
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar" />
                                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Excluir" /><br />

                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <%# Eval("cliEnd_nome")%> <%# Eval("cliEnd_sobrenome") %><br />
                                <%# Eval("cliEnd_endereco")%>,  <%# Eval("cliEnd_numero") %>,  <%# Eval("cliEnd_bairro") %><br />
                                <%# Eval("cliEnd_cidade")%> -  <%# Eval("cliEnd_estado") %><br />
                                <asp:Literal ID="cliEnd_cepLiteral" runat="server" Text='<%# Eval("cliEnd_cep")%>'></asp:Literal>
                                <br />
                                (<%# Eval("cliEnd_ddd1") %>)  <%# Eval("cliEnd_fone1") %>
                                <br />
                                <asp:Button ID="ButtonSelecionarEnderecoAdicional" runat="server" CommandName="Select" Text="Entregar aqui" BackColor="Blue" OnClientClick="return DesabilitarDuploClick(this,'Calculando entrega...','', false);" />
                                <br />
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar" />
                                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Excluir" /><br />
                            </SelectedItemTemplate>
                        </asp:ListView>
                    </span>

                    <asp:EntityDataSource ID="EntityDataSourceFormViewCadastroClienteAdicional" runat="server"
                        ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities"
                        EnableFlattening="False"
                        EnableDelete="True"
                        EnableInsert="True"
                        EnableUpdate="True"
                        EntitySetName="ClienteEnderecoAdicional"
                        EntityTypeFilter="ClienteEnderecoAdicional"
                        OnInserting="EntityDataSourceFormViewCadastroClienteAdicional_Inserting"
                        Where="it.[cli_id] = @cli_id" OnInserted="EntityDataSourceFormViewCadastroClienteAdicional_Inserted">
                        <WhereParameters>
                            <asp:SessionParameter Name="cli_id" SessionField="cli_id" Type="Int32" />
                        </WhereParameters>
                    </asp:EntityDataSource>

                </td>

                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelEntrega">
                        <ProgressTemplate>
                            <img src="..<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/outros/zoomloader.gif" alt="Carregando" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanelEntrega" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonCalculaEntrega" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="PanelInformacaoEntrega">
                                3 OPÇÕES DE ENTREGA<br />

                                <table border="1" width="100%">
                                    <tr>
                                        <td>Meio</td>
                                        <td>Preço</td>
                                        <td>Prazo de Entrega</td>
                                    </tr>
                                    <asp:Panel runat="server" ID="PanelEntregaDesativada" Visible="false">
                                        <tr>
                                            <td colspan="3">Informe o CEP para ver prazo e preço</td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="PanelEntregaAtivada" Visible="false">
                                        <tr>
                                            <td id="ent_tipo">
                                                <asp:Literal ID="ent_tipoLiteral" runat="server"></asp:Literal></td>
                                            <td id="ent_valor">
                                                <asp:Literal ID="ent_valorLiteral" runat="server"></asp:Literal></td>
                                            <td id="ent_prazo">
                                                <asp:Literal ID="ent_prazoLiteral" runat="server"></asp:Literal></td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <br />
                    <asp:Panel runat="server" ID="PanelInformacaoFormaPagamanento">
                        4 FORMA DE PAGAMENTO<br />
                        <asp:RadioButton ID="RadioButtonBoleto" runat="server" GroupName="groupTipoPagamento" Text="Boleto" AutoPostBack="True" OnCheckedChanged="RadioButtonBoleto_CheckedChanged" ValidationGroup="validGroupFinalizarPedido" />
                        <asp:RadioButton ID="RadioButtonCartao" runat="server" GroupName="groupTipoPagamento" Text="Cartão de Crédito" AutoPostBack="True" OnCheckedChanged="RadioButtonCartao_CheckedChanged" ValidationGroup="validGroupFinalizarPedido" />
                        <asp:Panel runat="server" ID="PanelPagamentoBoleto" Visible="false">
                            <br />

                            Pagamento somente à vista.

O boleto vence em 3 dias úteis.

É necessário imprimir o boleto ou utilizar o código de barras do mesmo para fazer o pagamento.

Imprima o boleto após a finalização da compra.

O boleto não será enviado para o seu endereço físico.
                          <br />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="PanelPagamentoCartao" Visible="false">
                            <table border="1">
                                <tr>
                                    <td>Nome do Cartão</td>
                                    <td>
                                        <asp:DropDownList ID="ped_forPagCarNomeDropDownList" runat="server"
                                            DataValueField="forPag_nome" DataTextField="forPag_nome" AppendDataBoundItems="true">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarNomeDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nº do Cartão</td>
                                    <td>
                                        <asp:TextBox ID="ped_forPagCarNumeroTextBox" runat="server" Columns="25" MaxLength="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarNumeroTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nº de parcelas</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelPed_forPagNumParcela" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ButtonCalculaEntrega" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ped_forPagNumParcelaDropDownList" runat="server"
                                                    DataValueField="parcPar_quantidade" DataTextField="parcPar_condicao" AutoPostBack="true" OnSelectedIndexChanged="ped_forPagNumParcelaDropDownList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagNumParcelaDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanelPed_forPagNumParcela">
                                            <ProgressTemplate>
                                                <img src="..<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/outros/zoomloader.gif" alt="Carregando" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Seu Nome(como no cartão)</td>
                                    <td>
                                        <asp:TextBox ID="ped_forPagCarNomePortadorTextBox" runat="server" Columns="25" MaxLength="64"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarNomePortadorTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vencimento</td>
                                    <td>

                                        <asp:DropDownList ID="ped_forPagCarMesVencimentoDropDownList" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarMesVencimentoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ped_forPagCarAnoVencimentoDropDownList" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarAnoVencimentoDropDownList" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic"> 
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Cód. Segurança</td>
                                    <td>
                                        <asp:TextBox ID="ped_forPagCarCodSegurancaTextBox" runat="server" Columns="3" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ValidationGroup="validGroupFinalizarPedido" ControlToValidate="ped_forPagCarCodSegurancaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </td>
                <td>5 REVISÃO DO PEDIDO<br />
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelRevisaoPedido">
                        <ProgressTemplate>
                            <img src="..<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/outros/zoomloader.gif" alt="Carregando" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanelRevisaoPedido" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonCalculaEntrega" />
                        </Triggers>
                        <ContentTemplate>
                            <table border="1">
                                <tr>
                                    <td>Produto</td>
                                    <td>Qtd</td>
                                    <td>Preço</td>
                                </tr>
                                <asp:Repeater runat="server" ID="RepeaterCarrinhoFinalizarPedido">
                                    <ItemTemplate>
                                        <tr valign="top">
                                            <td>
                                                <img id="proSkuFotM<%#Container.ItemIndex%>" onclick="trocaImagemProdutoDetalhe(this,'<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>','<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>');" onerror="semFotoProdutoVitrine(this, '<%# Eval("loj_id") %>')" src="<%= Request.ApplicationPath.TrimEnd('/') %>/imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>-m<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>" style="max-height: 40px" alt="<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>" />
                                                <%# Eval("pro_nome") %>
                                                <%# Eval("proSkuCor_nome") %>
                                                <%# Eval("proSkuTam_nome") %>
                                                <%# Eval("proSku_id") %>
                                            </td>
                                            <td><%# Eval("car_quantidade") %></td>
                                            <td>
                                                <asp:Literal ID="car_totalItemLiteral" runat="server" Text='<%# ((Decimal)Eval("proSku_precoVenda") * (int)Eval("car_quantidade")).ToString("c") %>'></asp:Literal></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                  <tr>
                                    <td colspan="3">Cupom<asp:TextBox ID="car_cupomTextBox" runat="server" ValidationGroup="groupCupom"></asp:TextBox><asp:Button ID="ButtonAplicarCupom" runat="server" Text="Aplicar" ValidationGroup="groupCupom" OnClick="ButtonAplicarCupom_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ErrorMessage="<br/>Digite ou cole seu cupom na caixa." ControlToValidate="car_cupomTextBox" ValidationGroup="groupCupom" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator><asp:Literal ID="car_cupomMensagemLiteral" runat="server" Text=""></asp:Literal>
                                         <asp:HiddenField ID="ped_cup_idHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">SubTotal</td>
                                    <td><asp:Literal ID="car_subTotalLiteral" runat="server" Text="0,00"></asp:Literal></td>
                                </tr>
                                <asp:Panel runat="server" ID="PanelDesconto" Visible="false">
                                <tr>
                                    <td colspan="2">Desconto</td>
                                    <td>R$ <asp:Literal ID="car_descontoLiteral" runat="server" Text="0,00"></asp:Literal></td>
                                </tr>
                                </asp:Panel>
                                <tr>
                                    <td colspan="2">Entrega</td>
                                    <td><span id="car_freteSpan">
                                        <asp:Label ID="car_totalEntregaLabel" runat="server" Text=""></asp:Label></span></td>
                                </tr>
                                <tr>
                                    <td colspan="2">Total</td>
                                    <td>
                                        <asp:Label ID="car_totalLabel" runat="server" Text="0,00"></asp:Label><br />
                                        <asp:Literal ID="car_parcelamentoLiteral" runat="server" Text="0,00"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="ButtonFinalizarPedido" runat="server" Text="Finalizar Pedido" Style="background-color: #FF6600" OnClick="ButtonFinalizarPedido_Click" ValidationGroup="validGroupFinalizarPedido" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="ped_forPagNumParcelaHiddenField" runat="server" />
        <asp:HiddenField ID="ped_forPagValParcelaHiddenField" runat="server" />
        <asp:HiddenField ID="parcPar_perJuroHiddenField" runat="server" />
        <asp:HiddenField ID="cli_cepHiddenField" runat="server" />
        
        <asp:Button ID="ButtonCalculaEntrega" runat="server" Text="Button" OnClick="ButtonCalculaEntrega_Click" Style="display: none" />


        <uc2:Rodape ID="Rodape1" runat="server" />

    </form>
</body>
</html>
