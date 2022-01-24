using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Site.EntregaX;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Modelo;
using EntityFramework.Extensions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using EntityFramework.Caching;


namespace _2_Library.Dao.Site.Produto_GrupoX
{

    internal class Produto_GrupoDao : Repositorio<Produto_Grupo>
    {

        DateTime ontem = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0).AddDays(-1);
        DateTime amanha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0).AddDays(1);
        DateTime dataHora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
        /// <summary>
        ///   /// Seleciona os produtos da pagina inicial
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoInicial(int loj_id, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_GrupoDto> produto_grupo = (from proGru in Select()
                                                    where
                                                                          proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                                                                          (proGru.Produto.pro_paginaInicialDe != null || proGru.Produto.pro_paginaInicialAte != null) &&
                                                                          dataHora >= (proGru.Produto.pro_paginaInicialDe ?? ontem) &&
                                                                          dataHora <= (proGru.Produto.pro_paginaInicialAte ?? amanha) &&
                                                                          proGru.loj_id == loj_id &&
                                                                          proGru.Produto.pro_bloquear != true &&
                                                                          proGru.Grupo.gru_bloquear == false
                                                    select new Produto_GrupoDto
                                                    {
                                                        pro_id = proGru.Produto.pro_id,
                                                        pro_nome = proGru.Produto.pro_nome,
                                                        proSku_precoVenda = proGru.Produto.ProdutoSku.FirstOrDefault().proSku_precoVenda,
                                                        produtoSkuDto = (from s in proGru.Produto.ProdutoSku
                                                                         where !s.proSku_bloquear
                                                                         select new ProdutoSkuDto
                                                                         {
                                                                             proSku_id = s.proSku_id,
                                                                             pro_id = s.pro_id,
                                                                             proSku_precoAnterior = s.proSku_precoAnterior,
                                                                             proSku_precoVenda = (s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) >= 1) ? s.proSku_precoVenda : 9999999999,
                                                                             proSku_percDesconto = (s.proSku_precoAnterior - s.proSku_precoVenda) / s.proSku_precoAnterior,
                                                                             proSkuCor_imagem = s.ProdutoSkuCor.proSkuCor_imagem,
                                                                             proSkuCor_nome = s.ProdutoSkuCor.proSkuCor_nome,
                                                                             proSkuTam_nome = s.ProdutoSkuTamanho.proSkuTam_nome,
                                                                             produtoSkuFotoDto = s.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao }).FirstOrDefault(),
                                                                             proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                                                             proSku_disponivel = s.proSku_disponivel,

                                                                             entregaDto = new EntregaDto
                                                                              {
                                                                                  ent_cepInicial = s.Entrega.ent_cepInicial,
                                                                                  ent_cepFinal = s.Entrega.ent_cepFinal,
                                                                                  ent_dataHoraInicial = s.Entrega.ent_dataHoraInicial,
                                                                                  ent_dataHoraFinal = s.Entrega.ent_dataHoraFinal,
                                                                                  ent_valor = s.Entrega.ent_valor
                                                                              },
                                                                             parcelamentoDto = new ParcelamentoDto
                                                                             {
                                                                                 parcelamentoParcelaDto = s.Parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaDto { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                                 parc_valorMinimo = s.Parcelamento.parc_valorMinimo,
                                                                                 parc_ativarJuro = s.Parcelamento.parc_ativarJuro,
                                                                                 parc_periodoDe = s.Parcelamento.parc_periodoDe,
                                                                                 parc_periodoAte = s.Parcelamento.parc_periodoAte,
                                                                                 parc_bloquear = s.Parcelamento.parc_bloquear,
                                                                             }
                                                                         })

                                                    }).GroupBy(g => g.pro_id).Select(s => s.FirstOrDefault()).OrderBy(s => s.pro_id);

            return produto_grupo.Skip(startRowIndex).Take(maximumRows).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300000))).ToList();
        }

        /// <summary>
        /// Conta os produtos da pagina inicial
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoInicialCount(int loj_id, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_Grupo> produto_grupo = (from proGru in Select()
                                                       where
                                                                             proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                                                                             (proGru.Produto.pro_paginaInicialDe != null || proGru.Produto.pro_paginaInicialAte != null) &&
                                                                             dataHora >= (proGru.Produto.pro_paginaInicialDe ?? ontem) &&
                                                                             dataHora <= (proGru.Produto.pro_paginaInicialAte ?? amanha) &&
                                                                             proGru.loj_id == loj_id &&
                                                                             proGru.Produto.pro_bloquear != true &&
                                                                             proGru.Grupo.gru_bloquear == false
                                                       select proGru);

            return produto_grupo.GroupBy(g => g.pro_id).Select(s => s.FirstOrDefault()).FutureCount().Value;

        }

        /// <summary>
        /// Seleciona os produtos de um Grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoProdutoGrupo(int loj_id, string gru_nomeAmigavel, string[] proSku_cores, string[] proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_Grupo> produto_grupoList = PreparaSelectProduto_GrupoGrupo(loj_id, gru_nomeAmigavel);


            IQueryable<Produto_GrupoDto> produto_grupo = (from proGru in produto_grupoList
                                                          select new Produto_GrupoDto
                                                          {
                                                              pro_id = proGru.Produto.pro_id,
                                                              pro_nome = proGru.Produto.pro_nome,
                                                              proSku_precoVendaMaior = proGru.Produto.ProdutoSku.OrderByDescending(s=>s.proSku_precoVenda).FirstOrDefault().proSku_precoVenda,
                                                              proSku_precoVendaMenor = proGru.Produto.ProdutoSku.OrderBy(s => s.proSku_precoVenda).FirstOrDefault().proSku_precoVenda,
                                                              proSku_percDesconto =  proGru.Produto.ProdutoSku.Select(s=>((s.proSku_precoAnterior - s.proSku_precoVenda) / s.proSku_precoAnterior)).OrderByDescending(s=>s).FirstOrDefault(),
                                                              produtoSkuDto = proGru.Produto.ProdutoSku.Where(pg=>!pg.proSku_bloquear).Select(s =>
                                                              new ProdutoSkuDto
                                                              {
                                                                  proSku_id = s.proSku_id,
                                                                  pro_id = s.pro_id,
                                                                  proSku_precoAnterior = s.proSku_precoAnterior,
                                                                  proSku_precoVenda = (s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) >= 1) ? s.proSku_precoVenda : 9999999999,
                                                                  proSku_percDesconto = (s.proSku_precoAnterior - s.proSku_precoVenda) / s.proSku_precoAnterior,
                                                                  proSkuCor_imagem = s.ProdutoSkuCor.proSkuCor_imagem,
                                                                  proSkuCor_nome = s.ProdutoSkuCor.proSkuCor_nome,
                                                                  proSkuTam_nome = s.ProdutoSkuTamanho.proSkuTam_nome,
                                                                  produtoSkuFotoDto = s.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao }).FirstOrDefault(),
                                                                  proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                                                  proSku_disponivel = s.proSku_disponivel,
                                                                  
                                                                  entregaDto = new EntregaDto
                                                                  {
                                                                      ent_cepInicial = s.Entrega.ent_cepInicial,
                                                                      ent_cepFinal = s.Entrega.ent_cepFinal,
                                                                      ent_dataHoraInicial = s.Entrega.ent_dataHoraInicial,
                                                                      ent_dataHoraFinal = s.Entrega.ent_dataHoraFinal,
                                                                      ent_valor = s.Entrega.ent_valor
                                                                  },
                                                                  parcelamentoDto = new ParcelamentoDto
                                                                  {
                                                                      parcelamentoParcelaDto = s.Parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaDto { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                      parc_valorMinimo = s.Parcelamento.parc_valorMinimo,
                                                                      parc_ativarJuro = s.Parcelamento.parc_ativarJuro,
                                                                      parc_periodoDe = s.Parcelamento.parc_periodoDe,
                                                                      parc_periodoAte = s.Parcelamento.parc_periodoAte,
                                                                      parc_bloquear = s.Parcelamento.parc_bloquear,
                                                                  }
                                                              })

                                                          }).GroupBy(g => g.pro_id).Select(s => s.FirstOrDefault()).OrderBy(orderBy);

            if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
            else
            {
                if (proSku_cores.Count() > 0)
                    produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome)).Count() > 0);
                else
                    if (proSku_tamanhos.Count() > 0)
                        produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
            }

            return produto_grupo.Skip(startRowIndex).Take(maximumRows).Take(maximumRows).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300000))).ToList();
        }

        /// <summary>
        /// Conta os produtos de um grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoProdutoGrupoCount(int loj_id, string gru_nomeAmigavel, string[] proSku_cores, string[] proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {

            IQueryable<Produto_Grupo> produto_grupoList = PreparaSelectProduto_GrupoGrupo(loj_id, gru_nomeAmigavel);

            IQueryable<Produto_Grupo> produto_grupo = (from proGru in produto_grupoList
                                                       select proGru).GroupBy(g => g.pro_id).Select(s => s.FirstOrDefault());

            if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            else
            {
                if (proSku_cores.Count() > 0)
                    produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome)).Count() > 0);
                else
                    if (proSku_tamanhos.Count() > 0)
                        produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            }

            return produto_grupo.FutureCount().Value;//Count();
        }


        /// <summary>
        /// Seleciona os produtos de uma busca
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="pro_nomeChave"></param>
        /// <param name="proSku_cores"></param>
        /// <param name="proSku_tamanhos"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoProdutoBusca(int loj_id, string pro_nomeChave, string[] proSku_cores, string[] proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_Grupo> produto_grupoList = (from proGru in Select()
                                                           where
                                                              proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&  
                                                              (
                                                              proGru.Produto.pro_nome.Contains(pro_nomeChave) 
                                                              ) &&
                                                              proGru.loj_id == loj_id &&
                                                              proGru.Produto.pro_bloquear != true &&
                                                              proGru.Grupo.gru_bloquear == false
                                                           select proGru);


            IQueryable<Produto_GrupoDto> produto_grupo = (from proGru in produto_grupoList
                                                          select new Produto_GrupoDto
                                                          {
                                                              pro_id = proGru.Produto.pro_id,
                                                              pro_nome = proGru.Produto.pro_nome,
                                                              proSku_precoVendaMaior = proGru.Produto.ProdutoSku.OrderByDescending(s => s.proSku_precoVenda).FirstOrDefault().proSku_precoVenda,
                                                              proSku_precoVendaMenor = proGru.Produto.ProdutoSku.OrderBy(s => s.proSku_precoVenda).FirstOrDefault().proSku_precoVenda,
                                                              proSku_percDesconto = proGru.Produto.ProdutoSku.Select(s => ((s.proSku_precoAnterior - s.proSku_precoVenda) / s.proSku_precoAnterior)).OrderByDescending(s => s).FirstOrDefault(),
                                                              produtoSkuDto = proGru.Produto.ProdutoSku.Where(pg => !pg.proSku_bloquear).Select(s =>
                                                              new ProdutoSkuDto
                                                              {
                                                                  proSku_id = s.proSku_id,
                                                                  pro_id = s.pro_id,
                                                                  proSku_precoAnterior = s.proSku_precoAnterior,
                                                                  proSku_precoVenda = (s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) >= 1) ? s.proSku_precoVenda : 9999999999,
                                                                  proSku_percDesconto = (s.proSku_precoAnterior - s.proSku_precoVenda) / s.proSku_precoAnterior,
                                                                  proSkuCor_imagem = s.ProdutoSkuCor.proSkuCor_imagem,
                                                                  proSkuCor_nome = s.ProdutoSkuCor.proSkuCor_nome,
                                                                  proSkuTam_nome = s.ProdutoSkuTamanho.proSkuTam_nome,
                                                                  produtoSkuFotoDto = s.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao }).FirstOrDefault(),
                                                                  proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                                                  proSku_disponivel = s.proSku_disponivel,

                                                                  entregaDto = new EntregaDto
                                                                  {
                                                                      ent_cepInicial = s.Entrega.ent_cepInicial,
                                                                      ent_cepFinal = s.Entrega.ent_cepFinal,
                                                                      ent_dataHoraInicial = s.Entrega.ent_dataHoraInicial,
                                                                      ent_dataHoraFinal = s.Entrega.ent_dataHoraFinal,
                                                                      ent_valor = s.Entrega.ent_valor
                                                                  },
                                                                  parcelamentoDto = new ParcelamentoDto
                                                                  {
                                                                      parcelamentoParcelaDto = s.Parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaDto { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                                      parc_valorMinimo = s.Parcelamento.parc_valorMinimo,
                                                                      parc_ativarJuro = s.Parcelamento.parc_ativarJuro,
                                                                      parc_periodoDe = s.Parcelamento.parc_periodoDe,
                                                                      parc_periodoAte = s.Parcelamento.parc_periodoAte,
                                                                      parc_bloquear = s.Parcelamento.parc_bloquear,
                                                                  }
                                                              })

                                                          }).GroupBy(g => g.pro_id).Select(s=>s.FirstOrDefault()).OrderBy(orderBy);

            if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
            else
            {
                if (proSku_cores.Count() > 0)
                    produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_cores.Contains(proSku.proSkuCor_nome)).Count() > 0);
                else
                    if (proSku_tamanhos.Count() > 0)
                        produto_grupo = produto_grupo.Where(s => s.produtoSkuDto.Where(proSku => proSku_tamanhos.Contains(proSku.proSkuTam_nome)).Count() > 0);
            }

            return produto_grupo.Skip(startRowIndex).Take(maximumRows).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300000))).ToList();
        }

        /// <summary>
        /// Conta os produtos de uma busca
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="pro_nomeChave"></param>
        /// <param name="proSku_cores"></param>
        /// <param name="proSku_tamanhos"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoProdutoBuscaCount(int loj_id, string pro_nomeChave, string[] proSku_cores, string[] proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {

            IQueryable<Produto_Grupo> produto_grupoList = (from proGru in Select()
                                                           where
                                                              proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                                                              //(proGru.Produto.ProdutoSku.Select(s=>s.ProdutoSkuCor.proSkuCor_nome).Any(s2=>s2.Contains(pro_nomeChave)) ||
                                                              (
                                                              proGru.Produto.pro_nome.Contains(pro_nomeChave) 
                                                              ) &&
                                                              proGru.loj_id == loj_id &&
                                                              proGru.Produto.pro_bloquear != true &&
                                                              proGru.Grupo.gru_bloquear == false
                                                           select proGru);

            IQueryable<Produto_Grupo> produto_grupo = (from proGru in produto_grupoList
                                                       select proGru);

            if (proSku_cores.Count() > 0 && proSku_tamanhos.Count() > 0)
                produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome) && proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            else
            {
                if (proSku_cores.Count() > 0)
                    produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_cores.Contains(proSku.ProdutoSkuCor.proSkuCor_nome)).Count() > 0);
                else
                    if (proSku_tamanhos.Count() > 0)
                        produto_grupo = produto_grupo.Where(s => s.Produto.ProdutoSku.Where(proSku => proSku_tamanhos.Contains(proSku.ProdutoSkuTamanho.proSkuTam_nome)).Count() > 0);
            }

            return produto_grupo.GroupBy(g => g.pro_id).Select(s => s.FirstOrDefault()).FutureCount().Value;//Count();
        }


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Prepara o select de Produto_GrupoGrupo para antes da selecao dos dados
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        private IQueryable<Produto_Grupo> PreparaSelectProduto_GrupoGrupo(int loj_id, string gru_nomeAmigavel)
        {
            IQueryable<Produto_Grupo> produto_grupoList = null;
            Grupo grupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectByGrupo(loj_id, gru_nomeAmigavel);

            //Verifica se o grupo tem filhos, se sim seleciona todos os grupos finais para listar os produtos
            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectNosFinais(loj_id, gru_nomeAmigavel);
                produto_grupoList = (from proGru in Select()
                                     where
                                                           proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                                                           idsGrupo.Contains(proGru.gru_id) &&
                                                           proGru.loj_id == loj_id &&
                                                           proGru.Produto.pro_bloquear != true &&
                                                           proGru.Grupo.gru_bloquear == false
                                     select proGru);
            }
            else
            {
                produto_grupoList = (from proGru in Select()
                                     where
                                                           proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                                                                      proGru.Grupo.gru_nomeAmigavel == gru_nomeAmigavel &&
                                                                      proGru.loj_id == loj_id &&
                                                                      proGru.Produto.pro_bloquear != true &&
                                                                      proGru.Grupo.gru_bloquear == false
                                     select proGru);
            }

            return produto_grupoList;
        }

      
    }
}
