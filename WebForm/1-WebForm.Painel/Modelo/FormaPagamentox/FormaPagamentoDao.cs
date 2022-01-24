using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.FormaPagamentox
{
    public class FormaPagamentoDao
    {

        private Int32 loja_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;


        public List<FormaPagamento> SelecionarFormaPagamento()
        {
            List<FormaPagamento> formaPagamento = new List<FormaPagamento>();
            LojaEntities lojaEntities = new LojaEntities();
         
                formaPagamento = (from forPag in lojaEntities.FormaPagamento
                                  where forPag.loj_id == loja_id
                                  select forPag).ToList();
                return formaPagamento; 
        }

        public IQueryable<FormaPagamento> SelecionarFormaPagamento(int parc_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<FormaPagamento> formaPagamento =
                                                                   (from parcForPag in lojaEntities.Parcelamento_FormaPagamento
                                                                    where parcForPag.loj_id == loja_id && parcForPag.parc_id == parc_id
                                                                    select parcForPag.FormaPagamento);
            return formaPagamento;
        }
    }
}