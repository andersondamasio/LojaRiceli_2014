using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Loja.Modelo.Configuracaox;
using _2_Library.Modelo;

namespace Loja.Views.Pagamento
{
    public partial class GerarBoleto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cli_id = 0;
            int ped_id = 0;
          
            if (!string.IsNullOrEmpty(Request.QueryString["cli_id"]))
                cli_id = Convert.ToInt32(Request.QueryString["cli_id"]);
            
            if (!string.IsNullOrEmpty(Request.QueryString["ped_id"]) && Loja.Utils.Validacao.ValidaNumero(Request.QueryString["ped_id"])){
                ped_id = Convert.ToInt32(Request.QueryString["ped_id"]);
                Response.Write(GerarBoletoHtml(ped_id, cli_id));
            }
            else Loja.Utils.Validacao.Alert(Page, "Boleto não encontrado. Por favor entre em 'Minha Conta' para gerar seu boleto. Obrigado.");
        }

        //Gera boleto
        private string GerarBoletoHtml(int ped_id, int cli_id)
        {
            Pedido pedido = new Loja.Modelo.Pedidox.PedidoConsulta().SelecionarPedido(ped_id);
            string boletoHTML = string.Empty;

               if (Session["usuario"] != null && cli_id != 0)
                   cli_id = pedido.cli_id;
               else
                   if (Session["cli_id"] != null)
                       cli_id = Convert.ToInt32(Session["cli_id"]);

            if (pedido != null && cli_id == pedido.cli_id)
            {
                ConfiguracaoBoleto configuracaoBoleto = new ConfiguracaoConsulta().SelecionarConfiguracaoBoleto();
                String nossoNumero = configuracaoBoleto.conBol_nossoNumero;

                Cedente c = new Cedente(configuracaoBoleto.conBol_cendenteCpfCnpj, configuracaoBoleto.conBol_cendenteNome, configuracaoBoleto.conBol_cendenteAgencia, configuracaoBoleto.conBol_cendenteConta);
                c.Convenio = configuracaoBoleto.conBol_cendenteConvenio;

                Decimal valor = pedido.ped_total;

                c.ContaBancaria.DigitoAgencia = configuracaoBoleto.conBol_cendenteAgenciaDigito;//"X;
                c.ContaBancaria.DigitoConta = configuracaoBoleto.conBol_cendenteContaDigito;//"X";
                c.ContaBancaria.OperacaConta = configuracaoBoleto.conBol_cendenteContaOperacao;
                Boleto b = new Boleto(DateTime.Now.AddDays(configuracaoBoleto.conBol_prazoPagamento), valor, configuracaoBoleto.conBol_carteira, nossoNumero, c, new EspecieDocumento(configuracaoBoleto.conBol_codigoBanco));
                b.NumeroDocumento = nossoNumero;

                b.Sacado = new Sacado(pedido.ped_cli_cpfCnpj, pedido.ped_cli_nome + " " + pedido.ped_cli_sobrenome);
                b.Sacado.Endereco.End = pedido.ped_cli_endereco + ", " + pedido.ped_cli_numero;
                b.Sacado.Endereco.Bairro = pedido.ped_cli_bairro;
                b.Sacado.Endereco.Cidade = pedido.ped_cli_cidade;
                b.Sacado.Endereco.CEP = pedido.ped_cli_cep;
                b.Sacado.Endereco.UF = pedido.ped_cli_estado;

                b.Sacado.InformacoesSacado.Add(new InfoSacado("Inscrição: " + nossoNumero));

                b.LocalPagamento = configuracaoBoleto.conBol_localPagamento;

                Instrucao Instrucao1 = new Instrucao(configuracaoBoleto.conBol_codigoBanco);
                Instrucao1.Descricao = configuracaoBoleto.conBol_instrucao1Descricao;
                b.Instrucoes.Add(Instrucao1);

                var bb = new BoletoNet.BoletoBancario();

                bb.CodigoBanco = (short)configuracaoBoleto.conBol_codigoBanco;
                bb.Boleto = b;

                bb.Boleto.Valida();

                boletoHTML = bb.MontaHtml();
                string headHTML = boletoHTML.Substring(0, boletoHTML.IndexOf("<body>") + 6);
                headHTML = headHTML.Replace("Boleto.Net", configuracaoBoleto.conBol_nome);

                StringBuilder sb = new StringBuilder();
                System.IO.StringWriter tw = new System.IO.StringWriter(sb);
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                bb.Page = new System.Web.UI.Page();
                bb.RenderControl(hw);

                string tableHTML = sb.ToString().Replace("src=\"ImagemCodigoBarra.ashx", "src=\"" + Page.GetRouteUrl("PaginaInicial", null) + "Views/Pagamento/ImagemCodigoBarra.ashx");

                boletoHTML = headHTML + tableHTML + "</body></html>";
            }
            else
            {
                Loja.Utils.Validacao.Alert(Page, "Sua sessão expirou, por favor entre em 'Minha Conta' para gerar seu boleto. Obrigado.");
                Loja.Utils.Validacao.Redirecionar(Page, Page.GetRouteUrl("PaginaInicial", null));
            }
                return boletoHTML;
        }
    }
}