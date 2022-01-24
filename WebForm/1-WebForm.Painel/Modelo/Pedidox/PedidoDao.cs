using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Pedidox
{
    public class PedidoDao
    {  
        public Retorno InserirClientePedido(Cliente cliente)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                try
                {
                    if (lojaEntities.Cliente.Where(s => s.loj_id == cliente.loj_id && s.cli_email == cliente.cli_email).Count() > 0)
                    {
                        retorno = Static.MensagemSistema(16);
                        return retorno;
                    }
                    else
                    {

                        Pedido pedido = cliente.Pedido.FirstOrDefault();
                        pedido.Status = lojaEntities.Status.FirstOrDefault(s => s.stat_ativar && s.loj_id == cliente.loj_id);
                        PedidoStatus pedidoStatus = new PedidoStatus();
                        pedidoStatus.loj_id = pedido.loj_id;
                        pedidoStatus.ped_id = pedido.ped_id;
                        pedidoStatus.stat_id = pedido.Status.stat_id;
                        pedidoStatus.pedStat_dataHora = DateTime.Now;
                        pedido.PedidoStatus.Add(pedidoStatus);

                        pedido.stat_id = pedido.Status.stat_id;
                        pedido.ped_dataHora = DateTime.Now;

                        cliente.cli_dataHora = DateTime.Now;
                        lojaEntities.Cliente.Add(cliente);
                        lojaEntities.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    retorno.menSis_id = -1;
                    retorno.menSis_mensagem = "Houve um problema ao processar seu pedido, por favor tente novamente. " + ex.Message;
                }
            }

            return retorno;
        }

        public Retorno InserirPedido(Pedido pedido)
        {
            Retorno retorno = new Retorno();
            try
            {
                using (LojaEntities lojaEntities = new LojaEntities())
                {
                    pedido.Status = lojaEntities.Status.FirstOrDefault(s => s.stat_ativar && s.loj_id == pedido.loj_id);

                    PedidoStatus pedidoStatus = new PedidoStatus();
                    pedidoStatus.loj_id = pedido.loj_id;
                    pedidoStatus.ped_id = pedido.ped_id;
                    pedidoStatus.stat_id = pedido.Status.stat_id;
                    pedidoStatus.pedStat_dataHora = DateTime.Now;
                    pedido.PedidoStatus.Add(pedidoStatus);

                    pedido.stat_id = pedido.Status.stat_id;
                    pedido.ped_dataHora = DateTime.Now;
                    lojaEntities.Pedido.Add(pedido);
                    lojaEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                retorno.menSis_id = -1;
                retorno.menSis_mensagem = "Houve um problema ao processar seu pedido, por favor tente novamente. " + ex.Message;
            }
            return retorno;
        }

        public Pedido SelecionarPedido(int ped_id) {

            Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
            LojaEntities lojaEntities = new LojaEntities();

            Pedido pedido = (from ped in lojaEntities.Pedido
                              where ped.ped_id == ped_id &&  ped.loj_id == loj_id
                              select ped).SingleOrDefault();
            return pedido;
        
        }

    }
}