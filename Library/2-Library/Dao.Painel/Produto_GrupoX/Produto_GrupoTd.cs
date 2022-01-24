using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Painel.Produto_GrupoX
{
    public class Produto_GrupoTd
    {
        /// <summary>
        /// Retorna todos os Produtos de um determinado Grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoListar(string loj_dominio, int gru_id, bool paginaInicial, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<Produto_GrupoDto> produto_grupo = new Produto_GrupoDao().SelectProduto_GrupoListar(loj_id, gru_id,paginaInicial, startRowIndex, maximumRows, orderBy);
            return produto_grupo;
        }

        /// <summary>
        /// Conta todos os Produtos de um determinado Grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoListarCount(string loj_dominio, int gru_id, bool paginaInicial, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            int produto_grupo = new Produto_GrupoDao().SelectProduto_GrupoListarCount(loj_id,gru_id,paginaInicial, startRowIndex, maximumRows, orderBy);
            return produto_grupo;
        }
    }
}
