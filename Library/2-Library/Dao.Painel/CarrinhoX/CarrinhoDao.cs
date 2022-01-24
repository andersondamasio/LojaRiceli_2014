using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Painel.ClienteX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.CarrinhoX
{
    internal class CarrinhoDao : Repositorio<Carrinho>
    {
        public List<CarrinhoDto> SelectCarrinho(int loj_id, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "car_id" : orderBy;

            List<CarrinhoDto> carrinho = (from car in Select()
                                          where car.loj_id == loj_id
                                          select new CarrinhoDto
                                          {
                                              car_id = car.car_id,
                                              car_sessionId = car.car_sessionId,
                                              car_quantidade = car.car_quantidade,
                                              cli_id = car.cli_id,
                                              pro_id = car.ProdutoSku.Produto.pro_id,
                                              proSku_id = car.proSku_id,
                                              proSku_nome = car.ProdutoSku.Produto.pro_nome,
                                              proSku_altura = car.ProdutoSku.proSku_altura,
                                              proSku_largura = car.ProdutoSku.proSku_largura,
                                              proSku_comprimento = car.ProdutoSku.proSku_comprimento,
                                              proSku_peso = car.ProdutoSku.proSku_peso,
                                              proSku_prazoEntregaAdicional = car.ProdutoSku.proSku_prazoEntregaAdicional,
                                              proSku_precoCusto = car.ProdutoSku.proSku_precoCusto,
                                              proSku_precoAnterior = car.ProdutoSku.proSku_precoAnterior,
                                              proSku_precoVenda = car.ProdutoSku.proSku_precoVenda,
                                              proSkuCor_nome = car.ProdutoSku.ProdutoSkuCor.proSkuCor_nome,
                                              proSkuTam_nome = car.ProdutoSku.ProdutoSkuTamanho.proSkuTam_nome,
                                              proSku_quantidadeDisponivel = car.ProdutoSku.proSku_quantidadeDisponivel,
                                              proSku_disponivel = car.ProdutoSku.proSku_disponivel,
                                              cliente = new ClienteDto{cli_nome = car.Cliente.cli_nome},
                                              car_dataHora = car.car_dataHora
                                          }).OrderBy(orderBy).Skip(startRowIndex).Take(maximumRows).ToList();
            return carrinho;
        }

        public int SelectCarrinhoCount(int loj_id, int startRowIndex, int maximumRows, string orderBy)
        {
            var carrinho = (from car in Select()
                                          where car.loj_id == loj_id
                                          select new{car.car_id});

            return carrinho.Count();
        }



    }
}
