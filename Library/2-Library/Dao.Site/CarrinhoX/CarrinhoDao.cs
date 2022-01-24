using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using _2_Library.Config;
using _2_Library.Dao.Site.CorreioX;
using _2_Library.Dao.Site.EntregaX;
using _2_Library.Dao.Site.Produto_GrupoX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Modelo;


namespace _2_Library.Dao.Site.CarrinhoX
{
    internal class CarrinhoDao : Repositorio<Carrinho>
    {

        public CarrinhoDao() { }
        //public CarrinhoDao(LojaEntities lojaEntities) : base(lojaEntities) { }

        /// <summary>
        /// Insere o item ao carrinho
        /// </summary>
        /// <param name="carrinho"></param>
        public void InsertCarrinho(Carrinho carrinho)
        {

            if (Select().Where(s => s.car_sessionId == carrinho.car_sessionId && s.proSku_id == carrinho.proSku_id && s.loj_id == carrinho.loj_id).Count() == 0)
                Add(carrinho);
 
        }

        /// <summary>
        /// Seleciona um item do carrinho
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="car_sessionId"></param>
        /// <returns></returns>
        public List<CarrinhoDto> SelectCarrinho(int loj_id, string car_sessionId)
        {
            List<CarrinhoDto> carrinho = (from car in Select()
                                          where car.loj_id == loj_id &&
                                                car.car_sessionId == car_sessionId
                                          select new CarrinhoDto
                                          {
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
                                              produtoSkuFotoDto = car.ProdutoSku.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao }).FirstOrDefault(),
                                              entregaDto = new EntregaDto
                                              {
                                                  ent_cepInicial = car.ProdutoSku.Entrega.ent_cepInicial,
                                                  ent_cepFinal = car.ProdutoSku.Entrega.ent_cepFinal,
                                                  ent_dataHoraInicial = car.ProdutoSku.Entrega.ent_dataHoraInicial,
                                                  ent_dataHoraFinal = car.ProdutoSku.Entrega.ent_dataHoraFinal,
                                                  ent_valor = car.ProdutoSku.Entrega.ent_valor,
                                                  ent_prazo = car.ProdutoSku.Entrega.ent_prazo
                                              },
                                              parcelamentoDto = new ParcelamentoDto
                                                            {
                                                                parcelamentoParcelaDto = car.ProdutoSku.Parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaDto { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                parc_valorMinimo = car.ProdutoSku.Parcelamento.parc_valorMinimo,
                                                                parc_ativarJuro = car.ProdutoSku.Parcelamento.parc_ativarJuro,
                                                                parc_periodoDe = car.ProdutoSku.Parcelamento.parc_periodoDe,
                                                                parc_periodoAte = car.ProdutoSku.Parcelamento.parc_periodoAte,
                                                                parc_bloquear = car.ProdutoSku.Parcelamento.parc_bloquear,
                                                            }
                                          }).ToList();
            return carrinho;
        }
    }
}
