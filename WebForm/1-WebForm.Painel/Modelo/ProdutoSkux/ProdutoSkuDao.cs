using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.Produtox;
using Loja.Utils;
using _2_Library.Modelo;

namespace Loja.Modelo.ProdutoSkux
{
    public class ProdutoSkuDao
    {
        private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
        public dynamic SelecionarProdutoSku(int proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var resultado = (from proSku in lojaEntities.ProdutoSku
                             where
                              proSku.loj_id == loj_id &&
                              proSku.proSku_bloquear == false &&
                              proSku.proSku_id == proSku_id
                             select new
                             {
                                 proSku.proSku_id,
                                 proSku.Produto.pro_nome,
                                 proSku.Produto.Marca.mar_nome,
                                 gru_nome = proSku.Produto.Produto_Grupo.Select(s => new { s.Grupo.gru_nome }).FirstOrDefault().gru_nome,
                                 proSku.pro_id,
                                 proSku.parc_id,
                                 proSku.proSku_disponivel,
                                 proSku.proSku_precoAnterior,
                                 proSku.proSku_precoVenda,
                                 proSku.ProdutoSkuCor.proSkuCor_nome,
                                 proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                 ProSkuFotos = proSku.ProdutoSkuFoto.Select(s =>
                                     new
                                     {
                                         s.proSkuFot_nome,
                                         s.proSkuFot_extensao
                                     }),
                                 ProSkuFotosTamanhosCores = proSku.Produto.ProdutoSku.Where(
                                               proSkuSub => proSkuSub.proSku_bloquear == false).Select(s =>
                                                   new
                                                   {
                                                       s.proSku_id,
                                                       s.proSku_precoAnterior,
                                                       proSku_precoVenda = s.proSku_disponivel ? s.proSku_precoVenda : 9999999999,
                                                       s.ProdutoSkuTamanho.proSkuTam_nome,
                                                       s.ProdutoSkuCor.proSkuCor_nome,
                                                       s.proSkuTam_id,
                                                       s.proSkuCor_id,
                                                       ParcelamentoBean = new ParcelamentoBean
                                                                       {
                                                                           Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                        parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                                        parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                                                        parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                                                        parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                                        parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                                                        },
                                                       ProdutoSkuFoto = s.ProdutoSkuFoto./*Where(s1=>s1.ProdutoSku.proSkuTam_id == proSku.proSkuTam_id).*/Select(s2 => new { s2.proSkuFot_nome, s2.proSkuFot_extensao }).FirstOrDefault(),
                                                       proSku_disponivel = (s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) != 0)
                                                   }),
                                 ProInfos = proSku.Produto.ProdutoInfo.Where(
                                            ProInfoSub => ProInfoSub.proInfo_bloquear == false).Select(s =>
                                                new
                                                {
                                                    s.proInfo_nome,
                                                    ProdutoInfoItems = s.ProdutoInfoItem.Where(ProInfoItemSub => ProInfoItemSub.proInfoItem_bloquear == false).Select(s2 => new { s2.proInfoItem_descricao, s2.proInfoItem_valor })
                                                }),
                                 ParcelamentoBean = new ParcelamentoBean
                                 {
                                     Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                     parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                     parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                     parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                     parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                     parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                 }
                             }).ToList().Select(s => new
                          {
                              loj_id = loj_id,
                              s.proSku_id,
                              s.pro_id,
                              s.pro_nome,
                              s.mar_nome,
                              s.gru_nome,
                              s.proSku_precoAnterior,
                              s.proSku_precoVenda,
                              s.proSku_disponivel,
                              s.proSkuCor_nome,
                              s.proSkuTam_nome,
                              ProSkuFoto = s.ProSkuFotos.Select(s2 => new { s2.proSkuFot_nome, s2.proSkuFot_extensao, loja_id = loj_id }).FirstOrDefault(),
                              ProSkuFotos = s.ProSkuFotos.Select(s2 => new { pro_id = s.pro_id, loj_id = loj_id, s2.proSkuFot_nome, s2.proSkuFot_extensao }),
                              ProSkuCores = s.ProSkuFotosTamanhosCores.GroupBy(g => new { g.proSkuCor_id }).Select(g2 => g2.OrderBy(o => o.proSku_precoVenda).Select(s2 => new { s2.proSku_id, loj_id = loj_id, s.pro_id, s.pro_nome, s.gru_nome, s2.proSkuCor_nome, s2.proSkuTam_nome, s2.proSkuCor_id, s2.ProdutoSkuFoto, s2.proSku_disponivel, ordem = (s2.proSkuTam_nome == s.proSkuTam_nome) }).OrderBy(ss => !ss.ordem).FirstOrDefault()).OrderBy(o => o.proSkuCor_id),
                              ProSkuTamanhos = s.ProSkuFotosTamanhosCores.Where(s1 => s1.proSkuCor_nome == s.proSkuCor_nome).GroupBy(g => new { g.proSkuTam_id }).Select(g2 => g2.OrderBy(o => o.proSku_precoVenda).Select(s2 => new { s2.proSku_id, s2.proSkuTam_id, s2.proSkuTam_nome, s2.proSku_precoAnterior, s2.proSku_precoVenda, s2.ParcelamentoBean, proSku_disponivel = (s.ProSkuFotosTamanhosCores.Where(sSub => sSub.proSkuCor_nome == s.proSkuCor_nome && sSub.proSkuTam_nome == s2.proSkuTam_nome && sSub.proSku_disponivel == true).Count() > 0)}).FirstOrDefault()).Select(s3 => new { s3.proSku_id, s3.proSkuTam_id, s3.proSkuTam_nome, s3.proSku_precoAnterior, s3.proSku_precoVenda, s3.proSku_disponivel, Parcelamento = (s3.proSku_disponivel ? Recursos.CalculaParcelamento(s3.ParcelamentoBean, s3.proSku_precoVenda) : new ParcelamentoBean() { parc_bloquear = true }) }).OrderBy(o => o.proSkuTam_id),
                              s.ProInfos,
                              //Parcelamento = Recursos.CalculaParcelamento2(lojaEntities.Parcelamento.Where(s2 => s2.parc_id == s.parc_id).FirstOrDefault(), s.proSku_precoVenda)
                              Parcelamento = Recursos.CalculaParcelamento(s.ParcelamentoBean, s.proSku_precoVenda)
                          }).FirstOrDefault();
            return resultado;

        }

        public IQueryable<dynamic> SelecionarProdutoSkuBeanX(int proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from proSku in lojaEntities.ProdutoSku
                            where
                             proSku.loj_id == loj_id &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                            select new
                            {
                                proSku.pro_id,
                                proSku.proSku_id,
                                proSku.proSku_disponivel,
                                proSku.proSku_posicao,
                                proSku.proSku_precoAnterior,
                                proSku.proSku_precoVenda,
                                ProdutoSkuFotoBean = proSku.ProdutoSkuFoto.Select(o => new { o.proSkuFot_nome, o.proSkuFot_extensao }),
                                proSku.ProdutoSkuCor.proSkuCor_nome,
                                proSku.ProdutoSkuCor.proSkuCor_imagem,
                                proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                proSku.Produto.pro_nome,
                                proSku.Produto.pro_nomeAmigavel,
                                proSku.Produto.Marca.mar_nome,
                                Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new { parcPar_quantidade = s2.parcPar_quantidade, parcPar_perJuro = s2.parcPar_percentualJuro }),
                                proSku.Parcelamento.parc_valorMinimo,
                                parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                proSku.Parcelamento.parc_periodoDe,
                                proSku.Parcelamento.parc_periodoAte,
                                Produto = proSku.Produto,
                                ProdutoSku = proSku.Produto.ProdutoSku
                            });


            return produtos;
        }

        public ProdutoSkuBean SelecionarProdutoSkuBean(int proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from proSku in lojaEntities.ProdutoSku
                            where
                             proSku.loj_id == loj_id &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                            select new ProdutoSkuBean
                                              {
                                                  pro_id = proSku.pro_id,
                                                  proSku_id = proSku.proSku_id,
                                                  proSku_disponivel = proSku.proSku_disponivel,
                                                  proSku_posicao = proSku.proSku_posicao,
                                                  proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                  proSku_precoVenda = proSku.proSku_precoVenda,
                                                  ProdutoSkuFotoListBean = proSku.ProdutoSkuFoto.Select(o => new ProdutoSkuFotoBean { proSkuFot_nome = o.proSkuFot_nome, proSkuFot_extensao = o.proSkuFot_extensao }),
                                                  proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                  proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                  proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                                  ProdutoBean = new ProdutoBean
                                                  {
                                                      pro_id = proSku.Produto.pro_id,
                                                      pro_nome = proSku.Produto.pro_nome,
                                                      pro_nomeAmigavel = proSku.Produto.pro_nomeAmigavel,
                                                      mar_nome = proSku.Produto.Marca.mar_nome
                                                  },
                                                  ParcelamentoBean = new ParcelamentoBean
                                                  {
                                                      Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                      parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                      parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro == null ? false : proSku.Parcelamento.parc_ativarJuro,
                                                      parc_bloquear = proSku.Parcelamento.parc_bloquear == null ? true : proSku.Parcelamento.parc_bloquear,
                                                      parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                      parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                                  }
                                              }).FirstOrDefault();
            return produtos;
        }

  /*      public IEnumerable<ProdutoSkuCorBean> SelecionarProdutoSkuCor(Grupo grupo, string gru_nomeAmigavel)
        {
            LojaEntities lojaEntities = new LojaEntities();
            
            IQueryable<ProdutoSkuCorBean> resultado = null;
   
            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new Loja.Modelo.Grupox.GrupoConsulta().SelecionarNosFinais(grupo.gru_id).ToList();

                resultado = (from proSku in lojaEntities.ProdutoSku
                             where
                             proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&// Contains(gru_nomeAmigavel) &&//Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                             select proSku).Select(s => new ProdutoSkuCorBean
                                 {
                                     proSkuCor_nome = s.ProdutoSkuCor.proSkuCor_nome,
                                     proSkuCor_imagem = s.ProdutoSkuCor.proSkuCor_imagem,
                                 }).Where(s => s.proSkuCor_nome != null);
            }
            else
                resultado = (from proSku in lojaEntities.ProdutoSku
                             where
                             proSku.Produto.Produto_Grupo.Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&//Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                             select proSku).Select(s => new ProdutoSkuCorBean
                                    {
                                        proSkuCor_nome = s.ProdutoSkuCor.proSkuCor_nome,
                                        proSkuCor_imagem = s.ProdutoSkuCor.proSkuCor_imagem,
                                    }).Where(s => s.proSkuCor_nome != null);

           string chaveCache = Recursos.GetChaveCache(new Object[] { grupo }, gru_nomeAmigavel);

           return resultado.Distinct().LinqToCacheAdd("Produto", chaveCache);
        }

        public IEnumerable<ProdutoSkuTamanhoBean> SelecionarProdutoSkuTamanho(Grupo grupo, string gru_nomeAmigavel)
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<ProdutoSkuTamanhoBean> resultado = null;

            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new Loja.Modelo.Grupox.GrupoConsulta().SelecionarNosFinais(grupo.gru_id).ToList();

                resultado = (from proSku in lojaEntities.ProdutoSku
                             where
                             proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&// Contains(gru_nomeAmigavel) &&//Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                             select proSku).Select(s => new ProdutoSkuTamanhoBean
                             {
                                 proSkuTam_nome = s.ProdutoSkuTamanho.proSkuTam_nome
                             }).Where(s => s.proSkuTam_nome != null);
            }
            else
                resultado = (from proSku in lojaEntities.ProdutoSku
                             where
                             proSku.Produto.Produto_Grupo.Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&
                             proSku.proSku_bloquear == false &&
                             (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                             select proSku).Select(s => new ProdutoSkuTamanhoBean
                             {
                                 proSkuTam_nome = s.ProdutoSkuTamanho.proSkuTam_nome
                             }).Where(s => s.proSkuTam_nome != null);

            string chaveCache = Recursos.GetChaveCache(new object[] { grupo }, gru_nomeAmigavel);

            return resultado.Distinct().LinqToCacheAdd("Produto", chaveCache);
        }
        */
        public Boolean ProdutoSkuDisponivel(int proSku_id) {

            using (LojaEntities lojaEntities = new LojaEntities())
            {
                return lojaEntities.ProdutoSku.Where(s =>
                       s.proSku_id == proSku_id &&
                       s.loj_id == loj_id &&            
                       s.Produto.pro_bloquear == false &&
                       s.proSku_disponivel == true &&
                       (s.proSku_quantidadeDisponivel ?? 1) != 0 &&
                       s.Produto.Produto_Grupo.Where(g => g.Grupo.gru_bloquear == false).Count() > 0).Count() > 0;
            }
        }
    }
}