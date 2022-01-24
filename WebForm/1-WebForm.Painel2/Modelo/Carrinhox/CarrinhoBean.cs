using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Correiox;
using Loja.Modelo.FormaPagamentox;
using Loja.Modelo.Parcelamentox;

namespace Loja.Modelo.Carrinhox
{
    public class CarrinhoBean
    {
        public decimal car_totalProdutos { get; set; }
        public decimal car_cupomTotal { get; set; }
        public decimal car_subTotal { get; set; }
        public decimal car_totalDesconto { get; set; }
        public decimal car_totalEntrega { get; set; }
        public decimal car_total { get; set; }
        public CarrinhoParcelamentoBean car_parcelamento { get; set; }
        public CarrinhoEntrega car_entrega { get; set; }
        public List<dynamic> car_produtos { get; set; }

        public Medidas car_medidas { get; set; }
    }

    public class CarrinhoParcelamentoBean
    {
        public int carPar_quantidadeParcelas { get; set; }
        public decimal carPar_valorParcela { get; set; }
        public decimal carPar_percentualJuro { get; set; }
        public string carPar_condicao { get; set; }
        public List<ParcelamentoParcelaBean> parcelas { get; set; }
        public List<FormaPagamentoBean> formasPagamento { get; set; }
    }

    public class CarrinhoEntrega {
        public string car_localizacao { get; set; }
        public string car_meioEntrega { get; set; }
        public int car_prazoEntrega { get; set; }
        public decimal car_valorEntrega { get; set; }
    }

}