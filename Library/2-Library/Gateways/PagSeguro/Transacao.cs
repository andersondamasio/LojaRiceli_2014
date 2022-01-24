using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Site.GatPagSeguroX;
using _2_Library.Dao.Site.PedidoX;
using _2_Library.Modelo;
using _2_Library.Utils;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Service;

namespace _2_Library.Gateways.PagSeguro
{
    public class Transacao
    {
        public string ProcessarPedido(int ped_id)
        {
           return ProcessarPedido(new Pedido() { ped_id = ped_id });
        }

        public string ProcessarPedido(Pedido pedido) {
            try
            {
                if (pedido.ped_cli_nome == null)
                    pedido = new PedidoDao().Select(s => s.ped_id == pedido.ped_id).FirstOrDefault();

                Address address = new Address();
                address.Country = "BRA";
                address.State = pedido.ped_cliEnd_estado;
                address.City = pedido.ped_cliEnd_cidade;
                address.District = pedido.ped_cliEnd_bairro;
                address.PostalCode = pedido.ped_cliEnd_cep;
                address.Street = pedido.ped_cliEnd_endereco;
                address.Number = pedido.ped_cliEnd_numero;
                address.Complement = pedido.ped_cliEnd_complemento;

                // Sender representa quem enviará dinheiro na transação, normalmente o comprador  
                Sender sender = new Sender(pedido.ped_cli_nome + " " + pedido.ped_cli_sobrenome, pedido.ped_cli_email, new Phone() { AreaCode = pedido.ped_cli_ddd1, Number = pedido.ped_cli_fone1 });

                PaymentRequest paymentRequest = new PaymentRequest();
                paymentRequest.Reference = pedido.ped_id.ToString();
                paymentRequest.Currency = Currency.Brl;
                paymentRequest.Sender = sender;

                foreach (PedidoProduto pedidoProduto in pedido.PedidoProduto)
                {
                    Item item = new Item(
                        pedidoProduto.pedPro_proSku_id.ToString(),
                        pedidoProduto.pedPro_pro_nome + " " + pedidoProduto.pedPro_proSkuCor_nome + " " + pedidoProduto.pedPro_proSkuTam_nome,
                        pedidoProduto.pedPro_car_quantidade,
                        pedidoProduto.pedPro_proSku_precoVenda,
                        Convert.ToInt64(pedidoProduto.pedPro_proSku_peso));
                    paymentRequest.Items.Add(item);
                }

                decimal totalPedidoProdutos = pedido.PedidoProduto.Sum(s => s.pedPro_proSku_precoVendaTotal);
                decimal totalDescontos = ((pedido.ped_descontos > totalPedidoProdutos) ? totalPedidoProdutos : pedido.ped_descontos);

                paymentRequest.Shipping = new Shipping();
                paymentRequest.Shipping.ShippingType = ShippingType.NotSpecified;
                paymentRequest.Shipping.Cost = pedido.ped_ent_valor;
                paymentRequest.ExtraAmount = -(totalPedidoProdutos == totalDescontos ? totalPedidoProdutos - Convert.ToDecimal(0.01) : totalDescontos);
                paymentRequest.Shipping.Address = address;
                paymentRequest.MaxAge = 172800; // 2 dias 
                paymentRequest.RedirectUri = new Uri("http://" + _2_Library.Utils.Recursos.SelecionaUrlDominio() + Recursos.GetRouteUrl("PaginaInicial", null) + "PedidoConcluido.aspx");
                paymentRequest.NotificationURL = "http://" + _2_Library.Utils.Recursos.SelecionaUrlDominio() + Recursos.GetRouteUrl("PaginaInicial", null) + "Gateway/RetornoAutPagSeguro.aspx";

                GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroTd().SelectGatPagSeguro(null);

                if (gatPagSeguroDto == null)
                    return "Email e Token do PagSeguro não configurado.";
                else{
                    AccountCredentials credentials = new AccountCredentials(gatPagSeguroDto.gatps_email, gatPagSeguroDto.gatps_token);

                    Uri paymentRedirectUri = paymentRequest.Register(credentials);

                    if (paymentRedirectUri == null)
                        return "Url do PagSeguro está indisponível, Por favor tente novamente.";
                    else
                        return paymentRedirectUri.AbsoluteUri;
                }
            }
            catch (Uol.PagSeguro.Exception.PagSeguroServiceException ex)
            {
                //se a mensagem for 'response answered with null value' pode ser que o servidor não está conseguindo acessar a url do pagSeguro ou sem internet

                string mensagemErro = string.IsNullOrEmpty(ex.Message) ? String.Join("\\n", ex.Errors) : ex.Message + "\\n";

                return "Problemas ao acessar o PagSeguro, tente novamente mais tarde através do link Meus Pedidos.\\n" + mensagemErro;
            }
        }

        public Transaction BuscarTransacao(string transactionCode)
        {        
            GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroTd().SelectGatPagSeguro(null);
            AccountCredentials credentials = new AccountCredentials(gatPagSeguroDto.gatps_email, gatPagSeguroDto.gatps_token);

            Transaction transaction = TransactionSearchService.SearchByCode(credentials, transactionCode);
            return transaction;
        }

        public Transaction ChecarTransacao(string notificationType, string notificationCode)
        {
            GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroTd().SelectGatPagSeguro(null);
            AccountCredentials credentials = new AccountCredentials(gatPagSeguroDto.gatps_email, gatPagSeguroDto.gatps_token);
            Transaction transaction = null;

            if (notificationType == "transaction")
            {
                // obtendo o objeto transaction a partir do código de notificação  
                transaction = NotificationService.CheckTransaction(
                    credentials,
                    notificationCode
                );
            }
            return transaction;
        }

        public int UpdatePedido(string notificationType, string notificationCode)
        {
            Transaction transaction = ChecarTransacao(notificationType, notificationCode);
            int ped_id = Convert.ToInt32(transaction.Reference);
            string ped_forPag_situacao = GetTransactionStatus(transaction.TransactionStatus);

            GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroTd().SelectGatPagSeguro(null);

            PedidoDto pedidoDto = new PedidoDto();
            pedidoDto.ped_id = ped_id;
            pedidoDto.ped_forPag_nome = GetPaymentMethod(transaction.PaymentMethod.PaymentMethodType);
            pedidoDto.ped_forPag_situacao = ped_forPag_situacao;
            pedidoDto.ped_forPagPar_quantidade = transaction.InstallmentCount;

            new PedidoTd().UpdatePedido(pedidoDto);

            return ped_id;
        }

        public void AlterarSituacaoPedido(string notificationType, string notificationCode)
        {
            Transaction transaction = ChecarTransacao(notificationType, notificationCode);
            int ped_id = Convert.ToInt32(transaction.Reference);
            string ped_forPag_situacao = GetTransactionStatus(transaction.TransactionStatus);
            new PedidoTd().UpdatePedido(new PedidoDto() { ped_id = ped_id, ped_forPag_situacao = ped_forPag_situacao });
        }

        private String GetTransactionStatus(int cod) {

            string transactionStatus = null;
            switch (cod)
            {
                case 1:
                   transactionStatus = "Aguardando pagamento";
                    break;
                case 2:
                    transactionStatus = "Em análise";
                    break;
                case 3:
                    transactionStatus = "Paga";
                    break;
                case 4:
                    transactionStatus = "Disponível";
                    break;
                case 5:
                    transactionStatus = "Em disputa";
                    break;
                case 6:
                    transactionStatus = "Devolvida";
                    break;
                case 7:
                    transactionStatus = "Cancelada";
                    break;
                default:
                    transactionStatus = "Aguardando";
                    break;
            }


            return transactionStatus;
            /*
                1 Aguardando pagamento: o comprador iniciou a transação, mas até o momento o PagSeguro não recebeu nenhuma informação sobre o pagamento.
                2 Em análise: o comprador optou por pagar com um cartão de crédito e o PagSeguro está analisando o risco da transação.
                3 Paga: a transação foi paga pelo comprador e o PagSeguro já recebeu uma confirmação da instituição financeira responsável pelo processamento.
                4 Disponível: a transação foi paga e chegou ao final de seu prazo de liberação sem ter sido retornada e sem que haja nenhuma disputa aberta.
                5 Em disputa: o comprador, dentro do prazo de liberação da transação, abriu uma disputa.	
                6 Devolvida: o valor da transação foi devolvido para o comprador.
                7 Cancelada: a transação foi cancelada sem ter sido finalizada.
               */
        
        }

        private String GetPaymentMethod(int cod)
        {
            string paymentMethod = null;
            switch (cod)
            {
                case 1:
                    paymentMethod = "Cartão de crédito";
                    break;
                case 2:
                    paymentMethod = "Boleto";
                    break;
                case 3:
                    paymentMethod = "Débito online (TEF)";
                    break;
                case 4:
                    paymentMethod = "Saldo PagSeguro";
                    break;
                case 5:
                    paymentMethod = "Oi Paggo";
                    break;
                case 7:
                    paymentMethod = "Depósito em conta";
                    break;
                default:
                    paymentMethod = "Aguardando";
                    break;
            }


            return paymentMethod;

            /*
             1 Cartão de crédito: o comprador escolheu pagar a transação com cartão de crédito.
             2 Boleto: o comprador optou por pagar com um boleto bancário.
             3 Débito online (TEF): o comprador optou por pagar a transação com débito online de algum dos bancos conveniados.
             4 Saldo PagSeguro: o comprador optou por pagar a transação utilizando o saldo de sua conta PagSeguro.
             5 Oi Paggo *: o comprador escolheu pagar sua transação através de seu celular Oi.
             7 Depósito em conta: o comprador optou por fazer um depósito na conta corrente do PagSeguro. Ele precisará ir até uma agência bancária, fazer o depósito, guardar o comprovante e retornar ao PagSeguro para informar os dados do pagamento. A transação será confirmada somente após a finalização deste processo, que pode levar de 2 a 13 dias úteis. 
             */

        }

    }
}
