using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Parcelamentox
{
    public class ParcelamentoBean : Parcelamento
    {
        public int parc_quantidade { get; set; }
        public decimal parc_valor { get; set; }
        public IEnumerable<ParcelamentoParcelaBean> Parcelamento_ParcelaBean { get; set; }
    }
    public class ParcelamentoParcelaBean : ParcelamentoParcela
    {
        public decimal parcPar_valor { get; set; }
        public string parcPar_condicao { get; set; }
    }

}