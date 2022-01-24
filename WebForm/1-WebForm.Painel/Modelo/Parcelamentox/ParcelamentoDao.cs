using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Lojax;
using _2_Library.Modelo;

namespace Loja.Modelo.Parcelamentox
{
    public class ParcelamentoDao
    {
     private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
   
        public IQueryable<Parcelamento> SelecionarParcelamento()
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<Parcelamento> parcelamento = (from parc in lojaEntities.Parcelamento
                                                     where parc.loj_id == loj_id
                                                     select parc);

            return parcelamento;
        }

        public Parcelamento SelecionarParcelamentoPadrao()
        {
            LojaEntities lojaEntities = new LojaEntities();

            Parcelamento parcelamento = (from parc in lojaEntities.Parcelamento
                                         where parc.loj_id == loj_id && parc.parc_ativar
                                                     select parc).FirstOrDefault();

            return parcelamento;
        }

       /* public IQueryable<ParcelamentoParcela> SelecionarParcelamentoParcela()
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<ParcelamentoParcela> parcelamentoParcela =
                                                    (from parPar in lojaEntities.ParcelamentoParcela
                                                     where parPar.loj_id == loj_id
                                                     select parPar);
            return parcelamentoParcela;
        }*/

        public IQueryable<ParcelamentoParcela> SelecionarParcelamentoParcela(int parc_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<ParcelamentoParcela> parcelamentoParcela =
                                                    (from parPar in lojaEntities.ParcelamentoParcela
                                                     where parPar.loj_id == loj_id && parPar.parc_id == parc_id
                                                     select parPar);
            return parcelamentoParcela;
        }

        public void InserirParcelamento(Parcelamento parcelamento)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                parcelamento.loj_id = loj_id;
                parcelamento.parc_dataHora = DateTime.Now;
                lojaEntities.Parcelamento.Add(parcelamento);
                lojaEntities.SaveChanges();
            }
        }

        public IQueryable<ParcelamentoParcela> SelecionarParcelamento_Parcela()
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<ParcelamentoParcela> parcelamentoParcela = (from parParc in lojaEntities.ParcelamentoParcela
                                                                    where parParc.loj_id == loj_id
                                                                    select parParc);
            return parcelamentoParcela;
        }

        public IQueryable<Parcelamento_FormaPagamento> SelecionarParcelamento_FormaPagamento()
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<Parcelamento_FormaPagamento> parcelamentoFormaPagamento =
                                                                   (from parcForPag in lojaEntities.Parcelamento_FormaPagamento
                                                                    where parcForPag.loj_id == loj_id
                                                                    select parcForPag);
            return parcelamentoFormaPagamento;
        }
        
        public void AtualizarParcelamento(Parcelamento parcelamento)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Parcelamento parcel = (from parc in lojaEntities.Parcelamento
                                       where parc.loj_id == loj_id && parc.parc_id == parcelamento.parc_id
                                       select parc).SingleOrDefault();

                parcel.parc_nome = parcelamento.parc_nome;
                parcel.parc_valorMinimo = parcelamento.parc_valorMinimo;
                parcel.parc_bloquear = parcelamento.parc_bloquear;
                parcel.parc_ativarJuro = parcelamento.parc_ativarJuro;
                parcel.parc_ativar = parcelamento.parc_ativar;
                parcel.parc_dataHoraAtualizacao = DateTime.Now;

                //Remove as formas de pagamentos demarcados
                var parcelamento_formaPagamentoRemove = parcel.Parcelamento_FormaPagamento.Select(s => new { s.forPag_id, s.parc_id }).Except(parcelamento.Parcelamento_FormaPagamento.Select(s => new { s.forPag_id, s.parc_id }));
                foreach (var parcForPag in parcelamento_formaPagamentoRemove.ToList())
                    parcel.Parcelamento_FormaPagamento.Remove(parcel.Parcelamento_FormaPagamento.Where(s => s.parc_id == parcForPag.parc_id && s.forPag_id == parcForPag.forPag_id).SingleOrDefault());

                //adiciona as formas de pagamentos marcados
                foreach (var parcForPag in parcelamento.Parcelamento_FormaPagamento.ToList())
                {
                    var parcelamento_formaPagamento = parcel.Parcelamento_FormaPagamento.Where(s => s.forPag_id == parcForPag.forPag_id && s.parc_id == parcForPag.parc_id).SingleOrDefault();
                    if (parcelamento_formaPagamento == null)
                    {
                        parcForPag.loj_id = parcel.loj_id = loj_id;
                        parcForPag.parcForPag_dataHora = DateTime.Now;
                        parcel.Parcelamento_FormaPagamento.Add(parcForPag);
                    }
                }

                //Remove as formas de pagamentos desmarcados
                var ParcelamentoParcelaRemove = parcel.ParcelamentoParcela.Select(s => new { s.parcPar_quantidade, s.parc_id }).Except(parcelamento.ParcelamentoParcela.Select(s => new { s.parcPar_quantidade, s.parc_id }));
                foreach (var parcForPag in ParcelamentoParcelaRemove.ToList())
                    parcel.ParcelamentoParcela.Remove(parcel.ParcelamentoParcela.Where(s => s.parc_id == parcForPag.parc_id && s.parcPar_quantidade == parcForPag.parcPar_quantidade).SingleOrDefault());


                //adiciona as parcelas marcadas
                foreach (ParcelamentoParcela parc in parcelamento.ParcelamentoParcela.ToList())
                {
                    var parcelamento_parcela = parcel.ParcelamentoParcela.Where(s => s.parcPar_quantidade == parc.parcPar_quantidade && s.parc_id == parc.parc_id).SingleOrDefault();
                    if (parcelamento_parcela == null)
                    {
                        parc.loj_id = loj_id;
                        parc.parcPar_percentualJuro = parc.parcPar_percentualJuro;
                        parc.parcPar_dataHora = DateTime.Now;
                        parcel.ParcelamentoParcela.Add(parc);
                    }
                    else
                    {
                        parcelamento_parcela.loj_id = loj_id;
                        parcelamento_parcela.parcPar_percentualJuro = parc.parcPar_percentualJuro;
                    }
                }
                lojaEntities.SaveChanges();
            }
        }

        // id da forma de pagamento + valor de qual parcela e o valor a ser divido 
        public IQueryable<ParcelamentoBean> SelecionarParcelamento(Int32 forPag_id, Int32 parc_id, decimal valor)
        {
            var selecionarParcelamentoFormaPagamento = new ParcelamentoDao().SelecionarParcelamento_FormaPagamento().Where(s => s.forPag_id == forPag_id);
            var selecionado = selecionarParcelamentoFormaPagamento.Select(s => new {
                s.forPag_id, s.parc_id, 
                s.Parcelamento, s.FormaPagamento, 
                s.Parcelamento.ParcelamentoParcela 
            });

            if (parc_id == 0)
                return selecionado.Select(s => new ParcelamentoBean { 
                    parc_quantidade = s.ParcelamentoParcela.Count,
                    parc_valor = (valor/s.ParcelamentoParcela.Count()),
                    parc_bloquear = ((s.Parcelamento.parc_valorMinimo ?? 0) < (valor / s.ParcelamentoParcela.Count())) 
                });
            else return selecionado.Where(s => s.parc_id == parc_id).Select(s => 
                new ParcelamentoBean {
                    parc_quantidade = s.ParcelamentoParcela.Count,
                    parc_valor = (valor/s.ParcelamentoParcela.Count()),
                    parc_ativarJuro = s.Parcelamento.parc_ativarJuro 
                });

        }

        public void AtualizarParcelamentoAtivar(int parc_id)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                var parcelamento = (from parc in lojaEntities.Parcelamento
                                    where parc.parc_id != parc_id && parc.loj_id == loj_id
                                    select parc);

                foreach (Parcelamento parc in parcelamento)
                    parc.parc_ativar = false;

                lojaEntities.SaveChanges();
            }
        }
    }
}