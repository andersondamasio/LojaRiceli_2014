using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Parcelamentox
{
    public class ParcelamentoConsulta
    {

        public IQueryable<Parcelamento> SelecionarParcelamento()
        {
            return new ParcelamentoDao().SelecionarParcelamento();
        }
        public Parcelamento SelecionarParcelamentoPadrao()
        {
            return new ParcelamentoDao().SelecionarParcelamentoPadrao();
        }

        public IQueryable<ParcelamentoParcela> SelecionarParcelamentoParcela(int parc_id)
        {
            return new ParcelamentoDao().SelecionarParcelamentoParcela(parc_id);
        }

        public void InserirParcelamento(Parcelamento parcelamento)
        {
            Extensions.LinqToCacheRemove("Produto");
            new ParcelamentoDao().InserirParcelamento(parcelamento);
        }
        
        public void AtualizarParcelamento(Parcelamento parcelamento)
        {
            Extensions.LinqToCacheRemove("Produto");
            new ParcelamentoDao().AtualizarParcelamento(parcelamento);
        }

        public void AtualizarParcelamentoAtivar(int parc_id)
        {
            new ParcelamentoDao().AtualizarParcelamentoAtivar(parc_id);
        }
    }
}