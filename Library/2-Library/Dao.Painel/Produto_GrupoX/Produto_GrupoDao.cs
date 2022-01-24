using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Painel.ProdutoX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.Produto_GrupoX
{
    internal class Produto_GrupoDao : Repositorio<Produto_Grupo>
    {

        DateTime ontem = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0).AddDays(-1);
        DateTime amanha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0).AddDays(1);
        DateTime dataHora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);


        public List<Produto_GrupoDto> SelectProduto_GrupoListar(int loj_id, int gru_id, bool paginaInicial, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_Grupo> produtoGrupo = PreparaSelectProduto_GrupoListar(loj_id, gru_id, paginaInicial, startRowIndex, maximumRows, orderBy);

            List<Produto_GrupoDto> produto_grupo = (from proGru in produtoGrupo
                                                    select new Produto_GrupoDto
                                                          {
                                                           pro_id = proGru.pro_id,
                                                           produtoDto = (new ProdutoDto
                                                                        {
                                                                         pro_nome = proGru.Produto.pro_nome,
                                                                         pro_bloquear = proGru.Produto.pro_bloquear,
                                                                         produtoSkuDtoCount = proGru.Produto.ProdutoSku.Count
                                                                           })
                                                          }).ToList();
            return produto_grupo;
        }

        public int SelectProduto_GrupoListarCount(int loj_id, int gru_id, bool paginaInicial, int startRowIndex, int maximumRows, string orderBy)
        {
            IQueryable<Produto_Grupo> produtoGrupo = PreparaSelectProduto_GrupoListar(loj_id, gru_id, paginaInicial, startRowIndex, maximumRows, orderBy);

            int produto_grupo = (from proGru in produtoGrupo
                                 select proGru).Count();
            return produto_grupo;
        }

        private IQueryable<Produto_Grupo> PreparaSelectProduto_GrupoListar(int loj_id, int gru_id, bool paginaInicial, int startRowIndex, int maximumRows, string orderBy) {

            IQueryable<Produto_Grupo> produtoGrupo = null;

            if (paginaInicial)
            {
                produtoGrupo = (from proGru in Select()
                                where
                               proGru.Produto.ProdutoSku.Where(s => s.pro_id == proGru.Produto.pro_id && s.proSku_bloquear != true).Count() > 0 &&
                               (proGru.Produto.pro_paginaInicialDe != null || proGru.Produto.pro_paginaInicialAte != null) &&
                               dataHora >= (proGru.Produto.pro_paginaInicialDe ?? ontem) &&
                               dataHora <= (proGru.Produto.pro_paginaInicialAte ?? amanha) &&
                               proGru.loj_id == loj_id &&
                               proGru.Produto.pro_bloquear != true &&
                               proGru.Grupo.gru_bloquear == false
                                select proGru);
            }else{
                        produtoGrupo = (from proGru in Select()
                                        where
                                        proGru.gru_id == gru_id &&
                                        proGru.loj_id == loj_id
                                        select proGru);

                    }

            return produtoGrupo;
        }
    
    }
}
