<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="potitica-de-privacidade.aspx.cs" Inherits="_1_WebForm.institucional.potitica_de_privacidade" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        div.minha-div {
    width: 779px;
    height: 300px;
    outline: 1px solid #000;
    position: relative;
    left: 50%;
    margin-left: -390px;
    background-color:white;
    margin-bottom: 20px;
    font: 400 14px 'Lato','Open Sans',sans-serif;
color: #888;
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
          <div class="persist-area">
        <uc1:Cabecalho runat="server" ID="Cabecalho" />
     <div id="corpo">
                <div id="conteudo" style="width: 990px; margin-top: 5px;">
        <table >
            <tr>
                <td width="115">Dúvidas Frequentes</td>
                   <td rowspan="10" style="vertical-align:top">

                     <h2>Política de Privacidade</h2>
<p>Navegue com segurança pelo ambiente de nossa loja. Faça seu cadastro, escolha seus produtos e boas compras!</p>
<p>Garantimos o sigilo de suas informações pessoais. Estas serão somente utilizadas para alimentar nosso banco de dados visando melhor praticidade e agilidade em suas compras futuras.</p>
<p>Seus dados pessoais não serão repassados a terceiros em hipótese alguma, fique tranquilo!</p>
<p>Os números de seu cartão de crédito ou outros dados bancários fiquem armazenados exclusivamente nos servidores das administradoras de cartão, desta forma a xxxxx não tem nenhum acesso a essas informações.</p>
<p>O ambiente de pagamento é criptografado e certificado, garantindo ainda mais a segurança de seus dados.</p>
<p>Lembre-se de verificar seu e-mail, bem como sua ferramenta Anti-Spam após se cadastrar em nosso site. &nbsp;E-mails como newsletters, promoções, pesquisas de satisfação ou até mesmo de atualização de seu pedido podem ser encaminhados diretamente ao lixo eletrônico. Para facilitar, cadastre o domínio <strong>xxxxxx.com.br</strong> como seguro ou ainda cadastre nosso e-mail em sua lista de endereços.</p>
<p>O conteúdo presente neste site é de propriedade única e exclusiva da Pijamas for You. Sendo assim, as informações não poderão ser copiadas, extraídas, adulteradas ou de qualquer outra forma utilizadas sem a devida autorização prévia de uso expedida e firmada pelos diretores da empresa.</p>
<p>É a Pijamas for You cuidando de você e pra você nosso amigo e cliente! ;)</p>

                 </td>
            </tr>
            <tr>
                <td>Política de Trocas e Devoluções</td>
              
            </tr>
            <tr>
                <td>Prazo de Entrega</td>
            </tr>
            <tr>
                <td>Como Comprar</td>
            </tr>
            <tr>
                <td><a href="potitica-de-privacidade.aspx" title="Política de Privacidade">Política de Privacidade</a></td>
            </tr>
            <tr>
                <td>Política de Frete</td>
            </tr>
            <tr>
                <td>Parceria</td>
            </tr>
            <tr>
                <td>Trabalhe Conosco</td>
            </tr>
             <tr>
                <td>Sobre Nós</td>
            </tr>
        </table>
        </div>
         </div>
        <uc1:Rodape runat="server" ID="Rodape" />

          </div>
    </form>
</body>
</html>
