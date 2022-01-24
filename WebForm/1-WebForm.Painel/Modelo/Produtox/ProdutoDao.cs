using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.SqlClient;
using Loja.Modelo.Parcelamentox;
using Loja.Utils;
using Loja.Modelo.ProdutoSkux;
using System.Web.Routing;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Linq.Expressions;
using _2_Library.Modelo;

namespace Loja.Modelo.Produtox
{
    public class ProdutoDao
    {
        private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
        #region Lista os produtos da página inicial
        public IEnumerable<ProdutoBean> SelecionarProdutoInicial(int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = orderBy == string.Empty ? "pro_id" : orderBy;

            var ontem = DateTime.Now.AddDays(-1);
            var amanha = DateTime.Now.AddDays(1);
            var dataHora = DateTime.Now;

            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from pro in lojaEntities.Produto
                            where
                            pro.ProdutoSku.Where(s => s.pro_id == pro.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                            (pro.pro_paginaInicialDe != null || pro.pro_paginaInicialAte != null) &&
                               dataHora >= (pro.pro_paginaInicialDe ?? ontem) &&
                               dataHora <= (pro.pro_paginaInicialAte ?? amanha)
                               && pro.loj_id == loj_id
                               && pro.pro_bloquear != true
                               && pro.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false).Count() > 0
                            select new ProdutoBean
                            {
                                pro_id = pro.pro_id,
                                pro_nome = pro.pro_nome,
                                GrupoBean = pro.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false && s.Grupo.gru_subBloquear == false).OrderBy(s => s.proGru_dataHora).Select(s => new GrupoBean { gru_nome = s.Grupo.gru_nome, gru_nomeAmigavel = s.Grupo.gru_nomeAmigavel }).FirstOrDefault(),
                                mar_nome = pro.Marca.mar_nome, 
                                proSku_precoVenda = pro.ProdutoSku.Where(proSku => proSku.proSku_bloquear == false && (proSku.proSku_quantidadeDisponivel ?? 1) != 0).Select(s => new { s.proSku_precoVenda, s.proSku_disponivel }).OrderBy(s1 => s1.proSku_precoVenda).OrderByDescending(s2 => s2.proSku_disponivel).FirstOrDefault().proSku_precoVenda,
                                ProdutoSkuBean = (from proSku in pro.ProdutoSku
                                                  where proSku.proSku_bloquear != true
                                                  select new ProdutoSkuBean
                                                  {
                                                      pro_id = proSku.pro_id,
                                                      proSku_id = proSku.proSku_id,
                                                      proSku_disponivel = proSku.proSku_disponivel && (proSku.proSku_quantidadeDisponivel ?? 1) != 0,
                                                      proSku_posicao = proSku.proSku_posicao,
                                                      proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                      proSku_precoVenda = proSku.proSku_disponivel ? proSku.proSku_precoVenda : 9999999999,
                                                      ProdutoSkuFotoBean = proSku.ProdutoSkuFoto.Select(o => new ProdutoSkuFotoBean { proSkuFot_nome = o.proSkuFot_nome, proSkuFot_extensao = o.proSkuFot_extensao }).FirstOrDefault(),
                                                      proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                      proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                      proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,

                                                      ParcelamentoBean =  new ParcelamentoBean
                                                            {
                                                                Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                                parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                                                parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                                                parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                                parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                                            }
                                                  }
                                    )
                            }).Where(s => s.GrupoBean.gru_nome != string.Empty).OrderBy(orderBy).Skip(startRowIndex).Take(maximumRows);


            string chaveCache = Recursos.GetChaveCache(null, startRowIndex+ maximumRows + orderBy);

            return produtos.LinqToCacheAdd("Produto",chaveCache);
        }

        public Int32 SelecionarProdutoInicialCont(int startRowIndex, Int32 maximumRows, string orderBy)
        {
            var ontem = DateTime.Now.AddDays(-1);
            var amanha = DateTime.Now.AddDays(1);
            var dataHora = DateTime.Now;

            LojaEntities lojaEntities = new LojaEntities();

            var produtos =
                             (from pro in lojaEntities.Produto
                              where
                                pro.ProdutoSku.Where(s => s.pro_id == pro.pro_id && s.proSku_bloquear != true && (s.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0 &&
                               (pro.pro_paginaInicialDe != null || pro.pro_paginaInicialAte != null) &&
                               dataHora >= (pro.pro_paginaInicialDe ?? ontem) &&
                               dataHora <= (pro.pro_paginaInicialAte ?? amanha)
                               && pro.loj_id == loj_id
                               && pro.pro_bloquear == false
                               && pro.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false).Count() > 0
                              select new
                              {
                                  pro.pro_id,
                                  GrupoBean = pro.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false && s.Grupo.gru_subBloquear == false).OrderBy(s => s.proGru_dataHora).Select(s => new { gru_nome = s.Grupo.gru_nome }).FirstOrDefault()
                              }).Where(s => s.GrupoBean.gru_nome != string.Empty);

            string chaveCache = Recursos.GetChaveCache(null, startRowIndex + maximumRows + orderBy);

            return produtos.LinqToCacheAdd("Produto", chaveCache,true);
        }
        #endregion

      /*  #region Lista os produtos do Grupo
        public IEnumerable<ProdutoBean> SelecionarProdutoGrupo(string gru_nomeAmigavel,string[] proSku_cores,string[] proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = orderBy == string.Empty ? "pro_id" : orderBy;
            string chaveCache = Recursos.GetChaveCache(new object[]{proSku_cores,proSku_tamanhos}, gru_nomeAmigavel+startRowIndex + maximumRows + orderBy);

            IQueryable<Produto_Grupo> produtosGrupo = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {

                var grupo = lojaEntities.Grupo.Where(s =>
                      s.gru_nomeAmigavel == gru_nomeAmigavel &&
                      s.loj_id == loj_id &&
                      s.gru_bloquear != true).LinqToCacheAdd("Grupo", chaveCache + "grupoCount").FirstOrDefault();

                if (grupo.Grupo1.Count() > 0)
                {
                    List<int?> idsGrupo = new Loja.Modelo.Grupox.GrupoConsulta().SelecionarNosFinais(grupo.gru_id).ToList();
                    produtosGrupo = lojaEntities.Produto_Grupo.Where
                    (s =>
                      s.loj_id == loj_id &&
                      idsGrupo.Contains(s.Grupo.gru_id)
                    );
                }
                else
                {
                    produtosGrupo = lojaEntities.Produto_Grupo.Where
                        (s =>
                          s.Grupo.gru_nomeAmigavel == gru_nomeAmigavel &&
                          s.loj_id == loj_id &&
                          s.Grupo.gru_bloquear == false &&
                          s.Produto.pro_bloquear != true
                        );
                }
                var produtos = (from proGru in produtosGrupo.Select(s => new { s.loj_id, s.Produto, s.Grupo.gru_nome, s.Grupo.gru_nomeAmigavel })
                                where proGru.loj_id == loj_id &&
                                (proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear == false).Count() > 0)
                                select new ProdutoBean
                                {
                                    pro_id = proGru.Produto.pro_id,
                                    pro_nome = proGru.Produto.pro_nome,
                                    mar_nome = proGru.Produto.Marca.mar_nome,
                                    proSku_precoVenda = proGru.Produto.ProdutoSku.Where(proSku => proSku.proSku_bloquear == false && (proSku.proSku_quantidadeDisponivel ?? 1) != 0).Select(s => new { s.proSku_precoVenda, s.proSku_disponivel }).OrderBy(s1 => s1.proSku_precoVenda).OrderByDescending(s2 => s2.proSku_disponivel).FirstOrDefault().proSku_precoVenda,
                                    ProdutoSkuBean = (from proSku in proGru.Produto.ProdutoSku
                                                      where proSku.proSku_bloquear != true
                                                      select new ProdutoSkuBean
                                                      {
                                                          pro_id = proSku.pro_id,
                                                          proSku_id = proSku.proSku_id,
                                                          proSku_disponivel = proSku.proSku_disponivel && (proSku.proSku_quantidadeDisponivel ?? 1) != 0,
                                                          proSku_posicao = proSku.proSku_posicao,
                                                          proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                          proSku_precoVenda = proSku.proSku_disponivel ? proSku.proSku_precoVenda : 9999999999,
                                                          ProdutoSkuFotoBean = proSku.ProdutoSkuFoto.Select(o => new ProdutoSkuFotoBean { proSkuFot_nome = o.proSkuFot_nome, proSkuFot_extensao = o.proSkuFot_extensao }).FirstOrDefault(),
                                                          proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                          proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                          proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                                          ParcelamentoBean =
                                                                new ParcelamentoBean
                                                                {
                                                                    Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                    parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                                    parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                                                    parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                                                    parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                                    parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                                                }
                                                      }
                                        )
                                }).OrderBy(orderBy);

                if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                    produtos = produtos.Where(s => s.ProdutoSkuBean.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
                else
                {
                    if (proSku_cores.Count() > 0)
                        produtos = produtos.Where(s => s.ProdutoSkuBean.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome)).Count() > 0);
                    else
                        if (proSku_tamanhos.Count() > 0)
                            produtos = produtos.Where(s => s.ProdutoSkuBean.Where(proSku => proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
                }

                return produtos.Skip(startRowIndex).Take(maximumRows).LinqToCacheAdd("Produto", chaveCache);
            }
        }

        public Int32 SelecionarProdutoGrupoCont(string gru_nomeAmigavel, string[] proSku_cores, string[] proSku_tamanhos, int startRowIndex, Int32 maximumRows, string orderBy)
        {
            IQueryable<Produto> produtosGrupo = null;

            LojaEntities lojaEntities = new LojaEntities();
            string chaveCache = Recursos.GetChaveCache(new object[] { proSku_cores, proSku_tamanhos }, gru_nomeAmigavel + startRowIndex + maximumRows + orderBy);

            var grupo = lojaEntities.Grupo.Where(s =>
             s.gru_nomeAmigavel == gru_nomeAmigavel &&
             s.loj_id == loj_id &&
             s.gru_bloquear != true).LinqToCacheAdd("Grupo", chaveCache + "grupoCount").FirstOrDefault();

            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new Loja.Modelo.Grupox.GrupoConsulta().SelecionarNosFinais(grupo.gru_id).ToList();
                produtosGrupo = lojaEntities.Produto_Grupo.Where
               (s =>
                   s.loj_id == loj_id &&
                 idsGrupo.Contains(s.Grupo.gru_id)
               ).Select(s => s.Produto);
            }
            else
            {
                produtosGrupo = lojaEntities.Produto_Grupo.Where
                 (s =>
                   s.Grupo.gru_nomeAmigavel == gru_nomeAmigavel &&
                   s.loj_id == loj_id &&
                   s.Produto.pro_bloquear == false
                 ).Select(s => s.Produto);
            }

            var produtos = (from proGru in produtosGrupo
                            where
                            proGru.ProdutoSku.Where(s => s.proSku_bloquear == false && (s.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0
                            select proGru);

            if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                produtos = produtos.Where(s => s.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            else
            {
                if (proSku_cores.Count() > 0)
                    produtos = produtos.Where(s => s.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome)).Count() > 0);
                else
                    if (proSku_tamanhos.Count() > 0)
                        produtos = produtos.Where(s => s.ProdutoSku.Where(proSku => proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            }
           
            return produtos.GroupBy(s => s.pro_id).Select(s => s.FirstOrDefault()).LinqToCacheAdd("Produto", chaveCache,true);
        }
        #endregion
        */
        public dynamic SelecionarProduto(int proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from pro in lojaEntities.ProdutoSku
                            where pro.proSku_id == proSku_id
                            select new {
                                pro.pro_id,
                                pro.proSku_precoVenda,
                                pro.Produto.pro_nome,
                                pro.parc_id
                            }).ToList().Select(s=>new 
                            {
                                s.pro_id,
                                s.proSku_precoVenda,
                                s.pro_nome,
                                parcelamento = Recursos.CalculaParcelamento2(lojaEntities.Parcelamento.Where(s2=>s2.parc_id == s.parc_id).FirstOrDefault(),s.proSku_precoVenda)
                            });
            return produtos;
        }

        public IQueryable<ProdutoBean> SelecionarProduto2(int proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
           
            var produtos = (from pro in lojaEntities.Produto
                            where
                               pro.loj_id == loj_id &&
                               pro.pro_bloquear != true
                              && pro.ProdutoSku.Where(s => s.proSku_id == proSku_id).Count() > 0
                            select new ProdutoBean
                            {
                                pro_id = pro.pro_id,
                                pro_nome = pro.pro_nome,
                                pro_nomeAmigavel = pro.pro_nomeAmigavel,
                                GrupoBean = pro.Produto_Grupo.Where(s => s.Grupo.gru_bloquear == false && s.Grupo.gru_subBloquear == false).OrderBy(s => s.proGru_dataHora).Select(s => new GrupoBean { gru_nome = s.Grupo.gru_nome, gru_nomeAmigavel = s.Grupo.gru_nomeAmigavel }).FirstOrDefault(),
                                mar_nome = pro.Marca.mar_nome,
                                proSku_precoVenda = pro.ProdutoSku.Where(proSku => proSku.proSku_bloquear == false && (proSku.proSku_quantidadeDisponivel ?? 1) != 0).Select(s => new { s.proSku_precoVenda, s.proSku_disponivel }).OrderBy(s1 => s1.proSku_precoVenda).OrderByDescending(s2 => s2.proSku_disponivel).FirstOrDefault().proSku_precoVenda,
                                ProdutoSkuBean = (from proSku in pro.ProdutoSku
                                                  where proSku.proSku_bloquear != true &&
                                                  (proSku.proSku_quantidadeDisponivel ?? 1) != 0
                                                  select new ProdutoSkuBean
                                                  {
                                                      pro_id = proSku.pro_id,
                                                      proSku_id = proSku.proSku_id,
                                                      proSku_disponivel = proSku.proSku_disponivel,
                                                      proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                      proSku_precoVenda = proSku.proSku_precoVenda,
                                                      ProdutoSkuFotoListBean = proSku.ProdutoSkuFoto.Select(o => new ProdutoSkuFotoBean { pro_id = proSku.pro_id, proSkuFot_nome = o.proSkuFot_nome, proSkuFot_extensao = o.proSkuFot_extensao }),
                                                      proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                      proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                      proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                                      ParcelamentoBean = new ParcelamentoBean
                                                      {
                                                          Parcelamento_ParcelaBean = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new Parcelamentox.ParcelamentoParcelaBean { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                          parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                          parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro == null ? false : proSku.Parcelamento.parc_ativarJuro,
                                                          parc_bloquear = proSku.Parcelamento.parc_bloquear == null ? true : proSku.Parcelamento.parc_bloquear,
                                                          parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                          parc_periodoAte = proSku.Parcelamento.parc_periodoAte
                                                      }
                                                  }
                                    )
                            });

            return produtos;
        }



    }
}