using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Grupox
{
    public class GrupoConsulta
    {
        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
        #region Lista os grupos da página inicial em forma de xml
        public string SelecionarProdutoVitrineInicial()
        {
            string chaveCache = "SelecionarGrupoVitrineGrupo" + loj_id;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            if (cache.Get(chaveCache) == null)
            {
                string xml = new GrupoDao().SelecionarProdutoVitrineInicial();
                System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(HttpContext.Current.Request.PhysicalApplicationPath + @"\Cache\grupoPainel.xml");
                cache.Insert(chaveCache, xml, cacheDependency);

                return xml;
            }
            return cache.Get(chaveCache).ToString();
        }
        #endregion
        public string SelecionarProdutoVitrineGrupo(string gru_nomeAmigavel)
        {
            string chaveCache = "SelecionarGrupoVitrineGrupo" + loj_id+gru_nomeAmigavel;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;

            if (cache.Get(chaveCache) == null)
            {
                string xml = new GrupoDao().SelecionarProdutoVitrineGrupo(0);
                System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(HttpContext.Current.Request.PhysicalApplicationPath + @"\Cache\grupoPainel.xml");
                cache.Insert(chaveCache, xml, cacheDependency);

                return xml;
            }
            return cache.Get(chaveCache).ToString();
        }

        public Grupo SelecionarGrupo()
        {
            return new GrupoDao().SelecionarGrupo();
        }

      /*  public List<int?> SelecionarNosFinais(int? gru_id)
        {
            List<int?> grupos = new List<int?>();
            string chaveCache = "SelecionarNosFinais"+loj_id+gru_id.Value;

             System.Web.Caching.Cache cache = HttpContext.Current.Cache;
             if (cache.Get(chaveCache) == null)
             {
                 grupos = new GrupoDao().SelecionarNosFinais(gru_id);
                 System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(HttpContext.Current.Request.PhysicalApplicationPath + @"\Cache\grupoPainel.xml");
                 cache.Insert(chaveCache, grupos, cacheDependency);
             }
             return (List<int?>)cache.Get(chaveCache);
        }*/
    }
}