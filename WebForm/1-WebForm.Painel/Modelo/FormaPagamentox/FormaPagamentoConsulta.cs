using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.FormaPagamentox
{
    public class FormaPagamentoConsulta
    {

        public IQueryable<FormaPagamento> SelecionarFormaPagamento(int parc_id)
        {
            return new FormaPagamentoDao().SelecionarFormaPagamento(parc_id);
        }
    }
}