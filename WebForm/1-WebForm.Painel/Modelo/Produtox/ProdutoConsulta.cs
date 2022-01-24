using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.ProdutoSkux;
using Loja.Utils;

namespace Loja.Modelo.Produtox
{
    public class ProdutoConsulta
    {

        private Int32 loja_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;

        public dynamic SelecionarProdutoInicial(int startRowIndex, int maximumRows, string orderBy)
        {
            List<ProdutoBean> consultaDao = new ProdutoDao().SelecionarProdutoInicial(startRowIndex, maximumRows, orderBy).ToList();
                var produtos = (from pro in consultaDao
                                select new
                                {
                                    pro.pro_id,
                                    pro.pro_nome,
                                    pro.GrupoBean,
                                    pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(pro.pro_nome + "-" + pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5).FirstOrDefault().proSkuCor_nome + "-" + pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5).FirstOrDefault().proSkuTam_nome) + "-" + pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5).FirstOrDefault().proSku_id,
                                    pro.mar_nome,
                                    pro.proSku_precoVenda,
                                    ProdutoSku = pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5).FirstOrDefault(),
                                    ProdutoSkuFoto = pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5).FirstOrDefault().ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean(),
                                    ProdutoSkuBean = (from proSku in pro.ProdutoSkuBean
                                                      select new
                                                      {  
                                                          proSku.pro_id,
                                                          proSku.proSku_id,
                                                          proSku.proSku_disponivel,
                                                          proSku.proSku_posicao,
                                                          proSku.proSku_precoAnterior,
                                                          proSku.proSku_precoVenda,
                                                          ProdutoSkuFotoBean = (proSku.ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean()),
                                                          proSku.proSkuCor_nome,
                                                          proSku.proSkuCor_imagem,
                                                          proSku.proSkuTam_nome,
                                                          ParcelamentoBean = Recursos.CalculaParcelamento(proSku.ParcelamentoBean, proSku.proSku_precoVenda)
                                                      }
                                        ).GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5)
                                });
                return produtos;
}

        public int? SelecionarProdutoInicialCont(int startRowIndex, int maximumRows, string orderBy)
        {
            return new ProdutoDao().SelecionarProdutoInicialCont(startRowIndex, maximumRows, orderBy);
        }

        public dynamic SelecionarProdutoGrupo(string gru_nomeAmigavel, string proSku_cores, string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            List<ProdutoBean> consultaDao = new List<ProdutoBean>();

            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');//Tratamento.FiltroCor(gru_nomeAmigavel);
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            IEnumerable<dynamic> produtos = null;

            //consultaDao = new ProdutoDao().SelecionarProdutoGrupo(gru_nomeAmigavel, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy).ToList();
             
              produtos = (from pro in consultaDao
                             select new
                             {
                                 pro.pro_id,
                                 pro.pro_nome,
                                 pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(pro.pro_nome + "-" +

                                 pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5).FirstOrDefault().proSkuCor_nome + "-" +
                                 pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5).FirstOrDefault().proSkuTam_nome) + "-" +
                                 pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5).FirstOrDefault().proSku_id,
                                 
                                 pro.mar_nome,
                                 pro.proSku_precoVenda,
                                 ProdutoSku = pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5).FirstOrDefault(),
                                 ProdutoSkuFoto = pro.ProdutoSkuBean.GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5).FirstOrDefault().ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean(),
                                 ProdutoSkuBean = (from proSku in pro.ProdutoSkuBean
                                                   select new
                                                   {
                                                       proSku.pro_id,
                                                       proSku.proSku_id,
                                                       proSku.proSku_disponivel,
                                                       proSku.proSku_posicao,
                                                       proSku.proSku_precoAnterior,
                                                       proSku.proSku_precoVenda,
                                                       ProdutoSkuFotoBean = (proSku.ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean()),
                                                       proSku.proSkuCor_nome,
                                                       proSku.proSkuCor_imagem,
                                                       proSku.proSkuTam_nome,
                                                       ParcelamentoBean = Recursos.CalculaParcelamento(proSku.ParcelamentoBean, proSku.proSku_precoVenda)
                                                   }
                                     ).GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).Take(5)
                             });

             return produtos;

        }

        public Int32 SelecionarProdutoGrupoCont(string gru_nomeAmigavel, string proSku_cores, string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            int produtos = 0;

            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');//Tratamento.FiltroCor(gru_nomeAmigavel);
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            
            //produtos = new ProdutoDao().SelecionarProdutoGrupoCont(gru_nomeAmigavel, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy);


            return produtos;
        }

        public dynamic SelecionarProduto(int proSku_id)
        {
            dynamic produtoBean = new ProdutoDao().SelecionarProduto(proSku_id);
           
            return produtoBean;
        }

        public ProdutoBean SelecionarProduto2(int proSku_id)
        {
            ProdutoBean produtoBean = new ProdutoDao().SelecionarProduto2(proSku_id).FirstOrDefault();
            foreach (ProdutoSkuBean produtoSkuBean in produtoBean.ProdutoSkuBean)
            {
                produtoSkuBean.ParcelamentoBean = Recursos.CalculaParcelamento(produtoSkuBean.ParcelamentoBean, produtoSkuBean.proSku_precoVenda);
                produtoSkuBean.ProdutoSkuFotoBean = produtoSkuBean.ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean() { proSkuFot_nome = string.Empty, proSkuFot_extensao = string.Empty };
            }
            return produtoBean;
        }

        /* public List<T> AbilitaCache<T>(string nome, List<T> dados, string arquivoDependencia)
        {
            List<T> list = new List<T>();
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            if (cache.Get(nome) == null)
            { 
               System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(HttpContext.Current.Request.PhysicalApplicationPath + arquivoDependencia);
               cache.Insert(nome, dados, cacheDependency);
            }
            return (List<T>)cache.Get(nome);
        }*/
    }
}