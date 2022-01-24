using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Correiox;
using Loja.Utils;
using _2_Library.Modelo;

namespace Loja.Modelo.Carrinhox
{
    public class CarrinhoDao
    {
        private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;

        public Retorno InserirItemCarrinho(Carrinho carrinho)
        {
            Retorno retorno = new Retorno();
            string sessionId = HttpContext.Current.Session.SessionID;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                if (lojaEntities.ProdutoSku.Where(s =>
                    s.proSku_id == carrinho.proSku_id &&
                    s.loj_id == loj_id &&
                    s.Produto.pro_bloquear == false &&
                    s.proSku_disponivel == true &&
                    (s.proSku_quantidadeDisponivel ?? 1) != 0 &&
                    s.Produto.Produto_Grupo.Where(g => g.Grupo.gru_bloquear == false).Count() > 0).Count() > 0)
                {

                    if (lojaEntities.Carrinho.Where(s => s.car_sessionId == sessionId && s.proSku_id == carrinho.proSku_id).Count() == 0)
                    {
                        carrinho.car_sessionId = sessionId;
                        carrinho.loj_id = loj_id;
                        carrinho.car_dataHora = DateTime.Now;
                        lojaEntities.Carrinho.Add(carrinho);
                        lojaEntities.SaveChanges();
                    }
                }
                else
                {
                    retorno = Static.MensagemSistema(17);
                    return retorno;
                }
            }
            return retorno;
        }

        public void AtualizarItemCarrinho(int proSku_id, int car_quantidade)
        {
            Retorno retorno = new Retorno();
            string sessionId = HttpContext.Current.Session.SessionID;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Carrinho car = (from ca in lojaEntities.Carrinho
                                where ca.car_sessionId == sessionId && ca.proSku_id == proSku_id && ca.loj_id == loj_id
                                select ca).FirstOrDefault();
                car.car_quantidade = car_quantidade;
                lojaEntities.SaveChanges();
            }
        }

        public void ExcluirItemCarrinho(int proSku_id)
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Carrinho car = (from ca in lojaEntities.Carrinho
                                where ca.car_sessionId == sessionId && ca.proSku_id == proSku_id && ca.loj_id == loj_id
                                select ca).FirstOrDefault();

                lojaEntities.Carrinho.Remove(car);
                lojaEntities.SaveChanges();
            }
        }

        public List<dynamic> SelecionarItensCarrinho(decimal car_totalDesconto, decimal car_totalEntrega)
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            LojaEntities lojaEntities = new LojaEntities();

            List<dynamic> car = (from ca in lojaEntities.Carrinho
                                 where ca.car_sessionId == sessionId && ca.loj_id == loj_id
                                 select new
                                 {
                                     ca.loj_id,
                                     ca.cli_id,
                                     ca.proSku_id,
                                     ca.ProdutoSku.pro_id,
                                     ca.ProdutoSku.Produto.pro_nome,
                                     ca.ProdutoSku.parc_id,
                                     ca.ProdutoSku.proSku_quantidadeDisponivel,
                                     ca.ProdutoSku.ProdutoSkuCor.proSkuCor_nome,
                                     ca.ProdutoSku.proSku_precoVenda,
                                     ca.ProdutoSku.proSku_prazoEntregaAdicional,
                                     ca.ProdutoSku.proSku_precoCusto,
                                     ca.ProdutoSku.proSku_precoAnterior,
                                     ca.ProdutoSku.proSku_altura,
                                     ca.ProdutoSku.proSku_largura,
                                     ca.ProdutoSku.proSku_comprimento,
                                     ca.ProdutoSku.proSku_peso,
                                     ca.ProdutoSku.ProdutoSkuTamanho.proSkuTam_nome,
                                     ProdutoSkuFoto = ca.ProdutoSku.ProdutoSkuFoto.Select(s => new { s.proSkuFot_nome, s.proSkuFot_extensao }).FirstOrDefault(),
                                     ca.car_quantidade
                                 }).ToList().Select(ca2 => new
                                 {
                                     ca2.loj_id,
                                     ca2.cli_id,
                                     ca2.proSku_id,
                                     ca2.pro_id,
                                     ca2.pro_nome,
                                     ca2.parc_id,
                                     proSku_quantidadeDisponivel = ca2.proSku_quantidadeDisponivel ?? 2,
                                     ca2.proSkuCor_nome,
                                     ca2.proSku_precoVenda,
                                     ca2.proSku_prazoEntregaAdicional,
                                     ca2.proSku_precoCusto,
                                     ca2.proSku_precoAnterior,
                                     ca2.proSku_altura,
                                     ca2.proSku_largura,
                                     ca2.proSku_comprimento,
                                     ca2.proSku_peso,
                                     ca2.proSkuTam_nome,
                                     ca2.ProdutoSkuFoto,
                                     ca2.car_quantidade,
                                     Parcelamento = Recursos.CalculaParcelamento2(lojaEntities.Parcelamento.Where(s2 => (s2.parc_id == ca2.parc_id) && (s2.loj_id == loj_id)).FirstOrDefault(), (lojaEntities.Carrinho.Where(s => s.car_sessionId == sessionId && s.loj_id == loj_id).Sum(s => s.ProdutoSku.proSku_precoVenda) - car_totalDesconto) + car_totalEntrega)
                                 }).ToList<dynamic>();
            return car;
        }

        /* public IQueryable<Parcelamento> SelecionarItensCarrinhoParcelamento()
         {
             string sessionId = HttpContext.Current.Session.SessionID;
             LojaEntities lojaEntities = new LojaEntities();


             var car = (from ca in lojaEntities.Carrinho
                        where ca.car_sessionId == sessionId && ca.loj_id == loj_id
                        select new
                        {
                            Parcelamento = lojaEntities.Parcelamento.Where(s2 => (s2.parc_id == ca.ProdutoSku.parc_id || s2.parc_ativar) && (s2.loj_id == loj_id)).FirstOrDefault()
                        }.Parcelamento);

             return car;
         }*/

        public Medidas SelecionarSomaMedidasItensCarrinho()
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            LojaEntities lojaEntities = new LojaEntities();

            Medidas car = (from ca in lojaEntities.Carrinho
                           where ca.car_sessionId == sessionId && ca.loj_id == loj_id
                           group ca by ca.car_sessionId into g
                           select new Medidas
                 {
                     totalAltura = g.Sum(s => s.ProdutoSku.proSku_altura*s.car_quantidade),
                     totalLargura = g.Sum(s => s.ProdutoSku.proSku_largura * s.car_quantidade),
                     totalComprimento = g.Sum(s => s.ProdutoSku.proSku_comprimento * s.car_quantidade),
                     totalPeso = g.Sum(s => s.ProdutoSku.proSku_peso * s.car_quantidade)
                 }).SingleOrDefault();
            return car;
        }

        public List<dynamic> SelecionarItensCarrinhoConfere()
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            LojaEntities lojaEntities = new LojaEntities();

            List<dynamic> car = (from ca in lojaEntities.Carrinho
                                 where ca.car_sessionId == sessionId &&
                                 ca.ProdutoSku.proSku_bloquear == false &&
                                 (ca.ProdutoSku.proSku_quantidadeDisponivel ?? 1) != 0 &&
                                 ca.ProdutoSku.proSku_disponivel == true &&
                                 ca.ProdutoSku.Produto.pro_bloquear == false &&
                                 ca.ProdutoSku.Produto.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false).Count() > 0
                                 select new
                                 {
                                     ca.loj_id,
                                     ca.cli_id,
                                     ca.proSku_id,
                                     ca.ProdutoSku.pro_id,
                                     ca.ProdutoSku.Produto.pro_nome,
                                     ca.ProdutoSku.parc_id,
                                     ca.ProdutoSku.proSku_quantidadeDisponivel,
                                     ca.ProdutoSku.ProdutoSkuCor.proSkuCor_nome,
                                     ca.ProdutoSku.proSku_precoVenda,
                                     ca.ProdutoSku.ProdutoSkuTamanho.proSkuTam_nome,
                                     ca.car_quantidade
                                 }).ToList<dynamic>();
            return car;
        }

        public void ExcluirItensCarrinho()
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<_2_Library.Modelo.Carrinho> cars = (from ca in lojaEntities.Carrinho
                                              where
                                              ca.car_sessionId == sessionId &&
                                              ca.loj_id == loj_id
                                              select ca);
            foreach (var car in cars)
            {
                lojaEntities.Carrinho.Remove(car);
            }

            lojaEntities.SaveChanges();
        }


        public void AtualizarItensCarrinhoCliente(int cli_id)
        {
            string sessionId = HttpContext.Current.Session.SessionID;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                IQueryable<Carrinho> cars = (from ca in lojaEntities.Carrinho
                                where ca.car_sessionId == sessionId && ca.loj_id == loj_id
                                select ca);

                foreach (Carrinho car in cars) {
                    car.cli_id = cli_id;
                }
                lojaEntities.SaveChanges();
            }
        }
    }
}