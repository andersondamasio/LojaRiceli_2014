<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracaoBoleto.aspx.cs" Inherits="Loja.Painel.ConfiguracaoBoleto" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <table width="1024px" align="center">
          <tr>
              <td>
          <uc1:Cabecalho ID="Cabecalho1" runat="server" />
      
            <asp:ListView ID="ListViewBoleto" runat="server" DataKeyNames="conBol_id,loj_id" 
                DataSourceID="EntityDataSourceBoleto" InsertItemPosition="None" OnItemEditing="ListViewBoleto_ItemEditing">
                <EditItemTemplate>
                    <tr style="background-color:blue">       
                        <td>
                            <asp:Label ID="conBol_nomeLabel" runat="server" Text='<%# Eval("conBol_nome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_codigoBancoLabel" runat="server" Text='<%# Eval("conBol_codigoBanco") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_nossoNumeroLabel" runat="server" Text='<%# Eval("conBol_nossoNumero") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_carteiraLabel" runat="server" Text='<%# Eval("conBol_carteira") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteCpfCnpjLabel" runat="server" Text='<%# Eval("conBol_cendenteCpfCnpj") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteNomeLabel" runat="server" Text='<%# Eval("conBol_cendenteNome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaLabel" runat="server" Text='<%# Eval("conBol_cendenteAgencia") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaDigitoLabel" runat="server" Text='<%# Eval("conBol_cendenteAgenciaDigito") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteContaLabel" runat="server" Text='<%# Eval("conBol_cendenteConta") %>' />
                        </td>
                         <td>
                          <asp:RadioButton ID="RadioButton1" runat="server" Checked='<%# Eval("conBol_ativar") %>' Enabled="false"/>
                        </td>
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                        </td>
                    </tr>
                      <td colspan="17">
                        Nome: <asp:TextBox ID="conBol_nomeTextBox" runat="server" Text='<%# Bind("conBol_nome") %>'  MaxLength="30"/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_nomeTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>
                             <br/>
                           Codigo do Banco: <asp:TextBox ID="conBol_codigoBancoTextBox" runat="server" MaxLength="20" Text='<%# Bind("conBol_codigoBanco") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_codigoBancoTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>   
                          <br/>
                           Nosso Número: <asp:TextBox ID="conBol_nossoNumeroTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_nossoNumero") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_nossoNumeroTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>   
                          <br/>
                           Carteira: <asp:TextBox ID="conBol_carteiraTextBox" runat="server" MaxLength="30" Text='<%# Bind("conBol_carteira") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_carteiraTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>   
                          <br/>
                           Cedente Nome: <asp:TextBox ID="conBol_cendenteNomeTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_cendenteNome") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteNomeTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>   
                          <br/>
                           Cedente Agência: <asp:TextBox ID="conBol_cendenteAgenciaTextBox" runat="server" MaxLength="15" Text='<%# Bind("conBol_cendenteAgencia") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteAgenciaTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>   
                           Dígito:<asp:TextBox ID="conBol_cendenteAgenciaDigitoTextBox" runat="server" Columns="3" MaxLength="1" Text='<%# Bind("conBol_cendenteAgenciaDigito") %>' />
                          
                          <br/>
                            Cedente Conta:<asp:TextBox ID="conBol_cendenteContaTextBox" runat="server" MaxLength="15" Text='<%# Bind("conBol_cendenteConta") %>' />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteContaTextBox" ValidationGroup="groupEdit"></asp:RequiredFieldValidator>  
                            Digito:<asp:TextBox ID="conBol_cendenteContaDigitoTextBox" runat="server"  Columns="3" MaxLength="1" Text='<%# Bind("conBol_cendenteContaDigito") %>' />
                          
                          <br/>
                           Cendente Conta Operacao: <asp:TextBox ID="conBol_cendenteContaOperacaoTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_cendenteContaOperacao") %>' />
                             <br/>
                            Cendente Convênio: <asp:TextBox ID="conBol_cendenteConvenioTextBox" runat="server" MaxLength="20" Text='<%# Bind("conBol_cendenteConvenio") %>' />
                             <br/>
                           Local Pagamento: <asp:TextBox ID="conBol_localPagamentoTextBox" runat="server" MaxLength="150" Text='<%# Bind("conBol_localPagamento") %>' />
                             <br/>
                            Instrução1 Descrição: <asp:TextBox ID="conBol_instrucao1DescricaoTextBox" runat="server" TextMode="MultiLine" Text='<%# Bind("conBol_instrucao1Descricao") %>' />
                             <br/>
                            Prazo de Pagamento(dias): <asp:TextBox ID="conBol_prazoPagamentoTextBox" runat="server" MaxLength="3" Text='<%# Bind("conBol_prazoPagamento") %>' />
                             <br/>
                             Ativar: <asp:CheckBox ID="conBol_ativarCheckBox" runat="server" Checked='<%# Bind("conBol_ativar") %>' Text="Sim"/>(ao ativar os demais boletos serão desativados)
                             <br/>
                          <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Salvar"  ValidationGroup="groupEdit"/>
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>Nenhum dado foi retornado.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr>
                        <td colspan="17">
                                Nome: <asp:TextBox ID="conBol_nomeTextBox" runat="server" Text='<%# Bind("conBol_nome") %>'  MaxLength="30"/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_nomeTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>
                             <br/>
                           Codigo do Banco: <asp:TextBox ID="conBol_codigoBancoTextBox" runat="server" MaxLength="20" Text='<%# Bind("conBol_codigoBanco") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_codigoBancoTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                          <br/>
                           Nosso Número: <asp:TextBox ID="conBol_nossoNumeroTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_nossoNumero") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_nossoNumeroTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                          <br/>
                           Carteira: <asp:TextBox ID="conBol_carteiraTextBox" runat="server" MaxLength="30" Text='<%# Bind("conBol_carteira") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_carteiraTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                          <br/>
                           Cedente Nome: <asp:TextBox ID="conBol_cendenteNomeTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_cendenteNome") %>' />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteNomeTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                          <br/>
                           Cedente Agência: <asp:TextBox ID="conBol_cendenteAgenciaTextBox" runat="server" MaxLength="15" Text='<%# Bind("conBol_cendenteAgencia") %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteAgenciaTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                           Dígito:<asp:TextBox ID="conBol_cendenteAgenciaDigitoTextBox" runat="server" Columns="3" MaxLength="1" Text='<%# Bind("conBol_cendenteAgenciaDigito") %>' />
                         
                          <br/>
                            Cedente Conta:<asp:TextBox ID="conBol_cendenteContaTextBox" runat="server" MaxLength="15" Text='<%# Bind("conBol_cendenteConta") %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório" ControlToValidate="conBol_cendenteContaTextBox" ValidationGroup="groupInsert"></asp:RequiredFieldValidator>   
                            Digito:<asp:TextBox ID="conBol_cendenteContaDigitoTextBox" runat="server"  Columns="3" MaxLength="1" Text='<%# Bind("conBol_cendenteContaDigito") %>' />
                         
                          <br/>
                           Cendente Conta Operacao: <asp:TextBox ID="conBol_cendenteContaOperacaoTextBox" runat="server" MaxLength="50" Text='<%# Bind("conBol_cendenteContaOperacao") %>' />
                             <br/>
                            Cendente Convênio: <asp:TextBox ID="conBol_cendenteConvenioTextBox" runat="server" MaxLength="20" Text='<%# Bind("conBol_cendenteConvenio") %>' />
                             <br/>
                           Local Pagamento: <asp:TextBox ID="conBol_localPagamentoTextBox" runat="server" MaxLength="150" Text='<%# Bind("conBol_localPagamento") %>' />
                             <br/>
                            Instrução1 Descrição: <asp:TextBox ID="conBol_instrucao1DescricaoTextBox" runat="server" TextMode="MultiLine" Text='<%# Bind("conBol_instrucao1Descricao") %>' />
                             <br/>
                            Prazo de Pagamento(dias): <asp:TextBox ID="conBol_prazoPagamentoTextBox" runat="server" MaxLength="3" Text='<%# Bind("conBol_prazoPagamento") %>' />
                             <br/>
                             Ativar: <asp:CheckBox ID="conBol_ativarCheckBox" runat="server" Checked='<%# Bind("conBol_ativar") %>' Text="Sim"/>(ao ativar os demais boletos serão desativados)
                             <br/>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert"/>
                            <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">         
                        <td>
                            <asp:Label ID="conBol_nomeLabel" runat="server" Text='<%# Eval("conBol_nome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_codigoBancoLabel" runat="server" Text='<%# Eval("conBol_codigoBanco") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_nossoNumeroLabel" runat="server" Text='<%# Eval("conBol_nossoNumero") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_carteiraLabel" runat="server" Text='<%# Eval("conBol_carteira") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteCpfCnpjLabel" runat="server" Text='<%# Eval("conBol_cendenteCpfCnpj") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteNomeLabel" runat="server" Text='<%# Eval("conBol_cendenteNome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaLabel" runat="server" Text='<%# Eval("conBol_cendenteAgencia") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaDigitoLabel" runat="server" Text='<%# Eval("conBol_cendenteAgenciaDigito") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteContaLabel" runat="server" Text='<%# Eval("conBol_cendenteConta") %>' />
                        </td>
                          <td>
                            <asp:RadioButton ID="conBol_ativarRadioButton" runat="server" Checked='<%# Eval("conBol_ativar") %>' Enabled="false"/>
                        </td>
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>

                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                       <tr style="background-color:blue">
                        
                        <td>
                            <asp:Label ID="conBol_nomeLabel" runat="server" Text='<%# Eval("conBol_nome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_codigoBancoLabel" runat="server" Text='<%# Eval("conBol_codigoBanco") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_nossoNumeroLabel" runat="server" Text='<%# Eval("conBol_nossoNumero") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_carteiraLabel" runat="server" Text='<%# Eval("conBol_carteira") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteCpfCnpjLabel" runat="server" Text='<%# Eval("conBol_cendenteCpfCnpj") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteNomeLabel" runat="server" Text='<%# Eval("conBol_cendenteNome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaLabel" runat="server" Text='<%# Eval("conBol_cendenteAgencia") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteAgenciaDigitoLabel" runat="server" Text='<%# Eval("conBol_cendenteAgenciaDigito") %>' />
                        </td>
                        <td>
                            <asp:Label ID="conBol_cendenteContaLabel" runat="server" Text='<%# Eval("conBol_cendenteConta") %>' />
                        </td>
                         <td>
                            <asp:RadioButton ID="conBol_ativarRadioButton" runat="server" Enabled="false" Checked='<%# Eval("conBol_ativar") %>'/>
                        </td>
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                        </td>
                    </tr>
                </SelectedItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                    <tr runat="server" style="">
                                        <th runat="server">nome</th>
                                        <th runat="server">cod.Banco</th>
                                        <th runat="server">nossoNúmero</th>
                                        <th runat="server">Cart.</th>
                                        <th runat="server">Ced.CpfCnpj</th>
                                        <th runat="server">Ced.Nome</th>
                                        <th runat="server">Ced.Agência</th>
                                        <th runat="server">Ced.AgênciaDigito</th>
                                        <th runat="server">Ced.Conta</th>
                                        <th runat="server">Ativar</th>
                                        <th id="Th1" runat="server"></th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
                  <br/>
                  <asp:DataPager ID="DataPagerBoleto" runat="server" PageSize="5" PagedControlID="ListViewBoleto">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                        ShowPreviousPageButton="true" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:NumericPagerField Visible="false" />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="true"
                                                        ShowPreviousPageButton="false" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:TemplatePagerField>
                                                        <PagerTemplate> 
                                                            <b>registros de
                                                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                                                                a
                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                                                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                                registros) </b>

                                                            <%# (this.DataPagerBoleto.Visible = Container.TotalRowCount >= this.DataPagerBoleto.PageSize).ToString().Replace("True",string.Empty) %>

                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager>
                  <asp:Button ID="ButtonIncluirBoleto" runat="server" Text="Incluir Boleto" OnClick="ButtonIncluirBoleto_Click" />

            <asp:EntityDataSource ID="EntityDataSourceBoleto" runat="server" ConnectionString="name=LojaEntities" 
                DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" 
                EnableUpdate="True" EntitySetName="ConfiguracaoBoleto" EntityTypeFilter="ConfiguracaoBoleto"
                OnInserting="EntityDataSourceBoleto_Inserting" OnInserted="EntityDataSourceBoleto_Inserted"
                OnUpdated="EntityDataSourceBoleto_Updated" OnDeleting="EntityDataSourceBoleto_Deleting"
                Where="it.[loj_id] = @loj_id">
                <WhereParameters>
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                </WhereParameters>
            </asp:EntityDataSource>
                   
    <uc1:Rodape ID="Rodape1" runat="server" />
         </td>
          </tr>
      </table>
    </form>
</body>
</html>
