using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Carrinhox;
using Loja.Utils;

namespace Loja.Modelo.Parcelamentox
{
    public class ParcelamentoOperacao
    {
        public dynamic SelecionarParcelamentoAbrangente(List<dynamic> carrinho)
        {

          // var parcelamentoBean = Recursos.CalculaParcelamento2(lojaEntities.Parcelamento.Where(s2 => (s2.parc_id == ca2.parc_id || s2.parc_ativar) && (s2.loj_id == loj_id)).FirstOrDefault(), (decimal)ca2.proSku_precoVenda)

             

            var parc_quantidade = carrinho.Max(s => s.Parcelamento.parc_quantidade);
            var carrinhoProdutoSku = carrinho.Where(s1 => s1.Parcelamento.parc_quantidade == parc_quantidade).FirstOrDefault();


            return carrinhoProdutoSku;
        }



   /*     public IQueryable<Parcelamento> SelecionarItensCarrinhoParcelamento()
        {
           IQueryable<Parcelamento> itensCarrinhoParcelamento = new CarrinhoConsulta().SelecionarItensCarrinhoParcelamento();

               var parc_quantidade = itensCarrinhoParcelamento.Max(s => s.Parcelamento_Parcela.Count()dade);
            var carrinhoProdutoSku = carrinho.Where(s1 => s1.Parcelamento.parc_quantidade == parc_quantidade).FirstOrDefault();
            


            return car;
        }*/


          /*  var car = (from ca in lojaEntities.Carrinho
                     //  where ca.car_sessionId == sessionId && ca.loj_id == loj_id
                       select new
                       {
                           ca.ProdutoSku.parc_id,
                           ca.ProdutoSku.proSku_precoVenda
                       }).ToList().Select(ca2 => new
                       {
                           Parcelamento = Recursos.CalculaParcelamento2(lojaEntities.Parcelamento.Where(s2 => (s2.parc_id == ca2.parc_id || s2.parc_ativar) && (s2.loj_id == loj_id)).FirstOrDefault(), (decimal)ca2.proSku_precoVenda)
                       }.Parcelamento).ToList();
            return car;
        }*/


    }
}