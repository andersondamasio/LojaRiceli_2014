using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Correiox;
using _2_Library.Modelo;

namespace Loja.Modelo.Carrinhox
{
    public class CarrinhoConsulta
    {
        public Retorno InserirItemCarrinho(Carrinho carrinho)
        {
           return new CarrinhoDao().InserirItemCarrinho(carrinho);
        }

      /*  public List<dynamic> SelecionarItensCarrinho(decimal car_totalDesconto, decimal car_totalEntrega)
        {
            return new CarrinhoDao().SelecionarItensCarrinho(car_totalDesconto, car_totalEntrega);
        }*/

        /*public IQueryable<Parcelamento> SelecionarItensCarrinhoParcelamento()
        {
            return new CarrinhoDao().SelecionarItensCarrinhoParcelamento();
        }

        public List<dynamic> SelecionarItensCarrinhoConfere()
        {
            return new CarrinhoDao().SelecionarItensCarrinhoConfere();
        }

        public Medidas SelecionarSomaMedidasItensCarrinho()
        {
            return new CarrinhoDao().SelecionarSomaMedidasItensCarrinho();
        }

        public void AtualizarItemCarrinho(int proSku_id, int car_quantidade)
        {
            new CarrinhoDao().AtualizarItemCarrinho(proSku_id, car_quantidade);
        }

        public void ExcluirItemCarrinho(int proSku_id)
        {
           new CarrinhoDao().ExcluirItemCarrinho(proSku_id);
        }

        public void ExcluirItensCarrinho()
        {
            new CarrinhoDao().ExcluirItensCarrinho();
        }

        public void AtualizarItensCarrinhoCliente(int cli_id)
        {
            new CarrinhoDao().AtualizarItensCarrinhoCliente(cli_id);
        }*/
    }
}