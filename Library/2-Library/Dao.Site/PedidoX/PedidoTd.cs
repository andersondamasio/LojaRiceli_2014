using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.ClienteEnderecoAdicionalX;
using _2_Library.Dao.Site.Clientex;
using _2_Library.Dao.Site.GatPagSeguroX;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.StatusX;
using _2_Library.Modelo;
using _2_Library.Utils;
using _2_Library.Dao.Site.PedidoFeedX;

namespace _2_Library.Dao.Site.PedidoX
{
    public class PedidoTd
    {
        /// <summary>
        /// Seleciona todos os pedidos de um determinado cliente, deve ser passado o cli_id OU cli_email
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="cli_id"></param>
        /// <param name="cli_email"></param>
        /// <returns></returns>
        public List<PedidoDto> SelectPedido(string loj_dominio, int cli_id, string cli_email)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            return new PedidoDao().SelectPedido(loj_id, cli_id, cli_email);

        }

        /// <summary>
        /// Retorna um pedido específico
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="ped_id"></param>
        /// <returns></returns>
        public PedidoDto SelectPedido(string loj_dominio, int ped_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            return new PedidoDao().SelectPedido(loj_id, ped_id);

        }

        public Pedido InsertPedido(string loj_dominio, Pedido pedido)
        {
            if(pedido.Cliente != null)
                pedido.Cliente.cli_senha = Recursos.Hash(pedido.Cliente.cli_senha);

            pedido.PedidoFeed = new List<PedidoFeed>() { new PedidoFeed { ped_id = pedido.ped_id, loj_id = pedido.loj_id,pf_dataHora = DateTime.Now,pf_numeroDenuncias = 0 } };

            using (PedidoDao pedidoDao = new PedidoDao())
            {
              pedidoDao.Add(pedido);
              pedido.ped_id = pedido.ped_id;
              pedido.cli_id = pedido.cli_id;
            }
            return pedido;
        }
     
        public void UpdatePedido(PedidoDto pedidoDto)
        {
            using (PedidoDao pedidoDao = new PedidoDao())
            {
                Pedido pedido = (from ped in pedidoDao.Select()
                                 where ped.ped_id == pedidoDto.ped_id
                                 select ped).FirstOrDefault();
                if (pedidoDto.ped_mensagemErro == null)
                {
                    if (pedidoDto.ped_forPag_gateway != null)
                        pedido.ped_forPag_gateway = pedidoDto.ped_forPag_gateway;
                    if (pedidoDto.ped_forPag_nome != null)
                        pedido.ped_forPag_nome = pedidoDto.ped_forPag_nome;
                    if (pedidoDto.ped_forPag_situacao != null)
                        pedido.ped_forPag_situacao = pedidoDto.ped_forPag_situacao;
                    if (pedidoDto.ped_forPagPar_quantidade > 0)
                        pedido.ped_forPagPar_quantidade = pedidoDto.ped_forPagPar_quantidade;
                    if (pedidoDto.ped_forPagPar_valor != 0)
                        pedido.ped_forPagPar_valor = pedidoDto.ped_forPagPar_valor;
                    if (pedidoDto.ped_forPagPar_condicao != null)
                        pedido.ped_forPagPar_condicao = pedidoDto.ped_forPagPar_condicao;
                   
                    if (pedido.ped_forPag_gateway == "pagseguro" && pedido.ped_forPagPar_valor == 0)
                    {
                        GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroTd().SelectGatPagSeguro(null);
                        if (pedido.ped_forPagPar_quantidade > gatPagSeguroDto.gatps_parcelasSemJuros)
                            pedido.ped_forPagPar_percentualJuro = gatPagSeguroDto.gatps_percentualJuro;

                        pedido.ped_forPagPar_valor = _2_Library.Dao.Site.Produto_GrupoX.Produto_GrupoUtils.CalculaParcelamento(pedido.ped_forPagPar_quantidade, pedido.ped_forPagPar_percentualJuro, pedido.ped_total);
                        pedido.ped_forPagPar_condicao = pedido.ped_forPagPar_quantidade + " x de " + pedido.ped_forPagPar_valor.ToString("n");
                    }
                }else
                    pedido.ped_mensagemErro = pedidoDto.ped_mensagemErro.Length < 400 ? pedidoDto.ped_mensagemErro : pedidoDto.ped_mensagemErro.Substring(0, 399);

                pedidoDao.Update(pedido);
            }
        }

    /*    public void UpdateForPagSituacao(int ped_id, string ped_forPag_situacao)
        {
            using (PedidoDao pedidoDao = new PedidoDao())
            {
                Pedido pedido = (from ped in pedidoDao.Select()
                                 where ped.ped_id == ped_id
                                 select ped).FirstOrDefault();
                pedido.ped_forPag_situacao = ped_forPag_situacao;

                pedidoDao.Update(pedido);
            }
        }*/

     /*   public void InsertPedido2(string loj_dominio, int cli_id, int? cliEnd_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            Cliente cliente = null;

            using (ClienteDao clienteDao = new ClienteDao())
            {
                cliente = clienteDao.Select().Where(s => s.cli_id == cli_id).First();
            }

             Pedido pedido = new Pedido();
             pedido.cli_id = cliente.cli_id;
             pedido.ped_enderecoIp = Recursos.SelecionarIp();
             pedido.ped_cli_cpfCnpj = cliente.cli_cpfCnpj;
             pedido.ped_cli_nome = cliente.cli_nome;
             pedido.ped_cli_sobrenome = cliente.cli_sobrenome;
             pedido.ped_cli_email = cliente.cli_email;
             pedido.ped_cli_cep = cliente.cli_cep;
             pedido.ped_cli_endereco = cliente.cli_endereco;
             pedido.ped_cli_numero = cliente.cli_numero;
             pedido.ped_cli_complemento = cliente.cli_complemento;
             pedido.ped_cli_referencia = cliente.cli_referencia;
             pedido.ped_cli_bairro = cliente.cli_bairro;
             pedido.ped_cli_cidade = cliente.cli_cidade;
             pedido.ped_cli_estado = cliente.cli_estado;
             pedido.ped_cli_ddd1 = cliente.cli_ddd1;
             pedido.ped_cli_fone1 = cliente.cli_fone1;
             pedido.ped_cli_ddd2 = cliente.cli_ddd2;
             pedido.ped_cli_fone2 = cliente.cli_fone2;
             pedido.ped_cli_ddd3 = cliente.cli_ddd3;
             pedido.ped_cli_fone3 = cliente.cli_fone3;
             pedido.ped_cli_rg = cliente.cli_rg;
             pedido.ped_cli_dataNascimento = cliente.cli_dataNascimento;
             pedido.ped_cli_sexo = cliente.cli_sexo;
             pedido.ped_cli_razaoSocial = cliente.cli_razaoSocial;
             pedido.ped_cli_inscricaoEstadual = cliente.cli_inscricaoEstadual;
             pedido.ped_cli_inscricaoEstadualIsento = cliente.cli_inscricaoEstadualIsento;
             pedido.ped_cli_recebeInformativo = cliente.cli_recebeInformativo;

            if(cliEnd_id.HasValue){

                ClienteEnderecoAdicional clienteEnderecoAdicional = new ClienteEnderecoAdicional();

                 using (ClienteEnderecoAdicionalDao clienteEnderecoAdicionalDao = new ClienteEnderecoAdicionalDao())
                {
                clienteEnderecoAdicional = clienteEnderecoAdicionalDao.Select().Where(s => s.cliEnd_id  == cliEnd_id.Value).First();
                 }

                pedido.ped_cliEnd_nome = clienteEnderecoAdicional.cliEnd_nome;
                pedido.ped_cliEnd_sobrenome = clienteEnderecoAdicional.cliEnd_sobrenome;
                pedido.ped_cliEnd_email = clienteEnderecoAdicional.cliEnd_email;
                pedido.ped_cliEnd_cep = clienteEnderecoAdicional.cliEnd_cep;
                pedido.ped_cliEnd_endereco = clienteEnderecoAdicional.cliEnd_endereco;
                pedido.ped_cliEnd_numero = clienteEnderecoAdicional.cliEnd_numero;
                pedido.ped_cliEnd_complemento = clienteEnderecoAdicional.cliEnd_complemento;
                pedido.ped_cliEnd_referencia = clienteEnderecoAdicional.cliEnd_referencia;
                pedido.ped_cliEnd_bairro = clienteEnderecoAdicional.cliEnd_bairro;
                pedido.ped_cliEnd_cidade = clienteEnderecoAdicional.cliEnd_cidade;
                pedido.ped_cliEnd_estado = clienteEnderecoAdicional.cliEnd_estado;
                pedido.ped_cliEnd_ddd1 = clienteEnderecoAdicional.cliEnd_ddd1;
                pedido.ped_cliEnd_fone1 = clienteEnderecoAdicional.cliEnd_fone1;
                pedido.ped_cliEnd_ddd2 = clienteEnderecoAdicional.cliEnd_ddd2;
                pedido.ped_cliEnd_fone2 = clienteEnderecoAdicional.cliEnd_fone2;
                pedido.ped_cliEnd_ddd3 = clienteEnderecoAdicional.cliEnd_ddd3;
                pedido.ped_cliEnd_fone3 = clienteEnderecoAdicional.cliEnd_fone3;
            }
            else
            {
                pedido.ped_cliEnd_nome = cliente.cli_nome;
                pedido.ped_cliEnd_sobrenome = cliente.cli_sobrenome;
                pedido.ped_cliEnd_email = cliente.cli_email;
                pedido.ped_cliEnd_cep = cliente.cli_cep;
                pedido.ped_cliEnd_endereco = cliente.cli_endereco;
                pedido.ped_cliEnd_numero = cliente.cli_numero;
                pedido.ped_cliEnd_complemento = cliente.cli_complemento;
                pedido.ped_cliEnd_referencia = cliente.cli_referencia;
                pedido.ped_cliEnd_bairro = cliente.cli_bairro;
                pedido.ped_cliEnd_cidade = cliente.cli_cidade;
                pedido.ped_cliEnd_estado = cliente.cli_estado;
                pedido.ped_cliEnd_ddd1 = cliente.cli_ddd1;
                pedido.ped_cliEnd_fone1 = cliente.cli_fone1;
                pedido.ped_cliEnd_ddd2 = cliente.cli_ddd2;
                pedido.ped_cliEnd_fone2 = cliente.cli_fone2;
                pedido.ped_cliEnd_ddd3 = cliente.cli_ddd3;
                pedido.ped_cliEnd_fone3 = cliente.cli_fone3;
            }

           

            CarrinhoTotaisDto carrinhoTotaisDto = (CarrinhoTotaisDto)System.Web.HttpContext.Current.Session["carrinhoTotaisDto"];

            

            pedido.ped_ent_meio = "correios";
            pedido.ped_ent_localizacao = carrinhoTotaisDto.correioDto.co_cidade + "-" + carrinhoTotaisDto.correioDto.co_estado;
            pedido.ped_ent_prazo = carrinhoTotaisDto.cart_entregaPrazoTotal.Value;
            pedido.ped_ent_valor = carrinhoTotaisDto.cart_entregaTotal.Value;

            if (!string.IsNullOrEmpty(carrinhoTotaisDto.cupomDto.cup_chave))
            {
                pedido.cup_id = carrinhoTotaisDto.cupomDto.cup_id;
            }
            
            pedido.ped_forPag_nome = "PagSeguro";
            pedido.ped_forPag_valorDesconto = 0;
            pedido.ped_forPag_percentualDesconto = 0;
            pedido.ped_forPagPar_condicao = carrinhoTotaisDto.cart_condicao;
            pedido.ped_forPagPar_quantidade = 0;//Convert.ToInt32(ped_forPagNumParcelaHiddenField.Value);
            pedido.ped_forPagPar_valor = 0;//Convert.ToDecimal(ped_forPagValParcelaHiddenField.Value);
            pedido.ped_forPagPar_percentualJuro = 0;//Convert.ToDecimal(parcPar_perJuroHiddenField.Value);
            pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(2);
            pedido.ped_forPag_situacao = "Pendente";

            using (StatusDao statusDao = new StatusDao())
            {
                pedido.Status = statusDao.Select().FirstOrDefault(s => s.stat_ativar && s.loj_id == cliente.loj_id);
                PedidoStatus pedidoStatus = new PedidoStatus();
                pedidoStatus.loj_id = pedido.loj_id;
                pedidoStatus.ped_id = pedido.ped_id;
                pedidoStatus.stat_id = pedido.Status.stat_id;
                pedidoStatus.pedStat_dataHora = DateTime.Now;
                pedido.PedidoStatus.Add(pedidoStatus);

                pedido.stat_id = pedido.Status.stat_id;
            }
            
          


            string car_sessionId = System.Web.HttpContext.Current.Session.SessionID;

                foreach (var car in carrinhoTotaisDto.carrinhoDto)
                {
                    PedidoProduto pedidoProduto = new PedidoProduto();
                    pedidoProduto.ped_id = pedido.ped_id;
                    pedidoProduto.pedPro_car_quantidade = car.car_quantidade;
                    pedidoProduto.pedPro_gru_nome = null;
                    pedidoProduto.pedPro_pro_nome = car.proSku_nome;
                    pedidoProduto.pedPro_proSku_id = car.proSku_id;
                    pedidoProduto.pedPro_proSku_altura = car.proSku_altura;
                    pedidoProduto.pedPro_proSku_largura = car.proSku_largura;
                    pedidoProduto.pedPro_proSku_comprimento = car.proSku_comprimento;
                    pedidoProduto.pedPro_proSku_peso = car.proSku_peso;
                    pedidoProduto.pedPro_proSkuCor_nome = car.proSkuCor_nome;
                    pedidoProduto.pedPro_proSkuTam_nome = car.proSkuTam_nome;
                    pedidoProduto.pedPro_proSku_prazoEntregaAdicional = car.proSku_prazoEntregaAdicional;
                    pedidoProduto.pedPro_proSku_precoCusto = car.proSku_precoCusto;
                    pedidoProduto.pedPro_proSku_precoAnterior = car.proSku_precoAnterior;
                    pedidoProduto.pedPro_proSku_precoVenda = car.proSku_precoVenda;
                    pedidoProduto.pedPro_proSku_precoVendaTotal = car.proSku_precoVenda * car.car_quantidade;
                    pedidoProduto.pedPro_dataHora = DateTime.Now;
                    pedido.PedidoProduto.Add(pedidoProduto);
                }

                pedido.ped_subTotal = carrinhoTotaisDto.cart_subTotal;
                pedido.ped_descontos = (carrinhoTotaisDto.cart_cupomTotal) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoTotaisDto.cart_subTotal);
                pedido.ped_total = (pedido.ped_subTotal - pedido.ped_descontos) + pedido.ped_ent_valor;
                pedido.ped_dataHora = DateTime.Now;

            using (PedidoDao pedidoDao = new PedidoDao())
            {
                pedidoDao.Add(pedido);
            }

        }*/
    }
}
