using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.ProdutoSkuX
{
    public class ProdutoSkuTd
    {
        /// <summary>
        /// Retorna todos os Skus de um determinado Produto
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<ProdutoSkuDto> SelectProdutoSkuListar(string loj_dominio, int pro_id, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "proSku_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuDto> produto_grupo = new ProdutoSkuDao().SelectProdutoSkuListar(loj_id, pro_id, startRowIndex, maximumRows, orderBy);

            return produto_grupo;
        }

        /// <summary>
        /// Conta todos os Skus de um determinado Produto
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProdutoSkuListarCount(string loj_dominio, int pro_id, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "proSku_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            int produto_grupo = new ProdutoSkuDao().SelectProdutoSkuListarCount(loj_id, pro_id, startRowIndex, maximumRows, orderBy);
            return produto_grupo;
        }


        /// <summary>
        /// Atualiza a quantidade disponível nos produtos de um determinado pedido
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="ped_id"></param>
        /// <returns></returns>
        public List<ProdutoSkuDto> UpdateProdutoSkuRepoeQuantidadeDisponivel(string loj_dominio, int ped_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            List<ProdutoSku> produtoSku = null;
            using (ProdutoSkuDao rep = new ProdutoSkuDao())
            {
                produtoSku = rep.Select(s => s.PedidoProduto.Where(p => p.ped_id == ped_id).Count() > 0 && s.loj_id == loj_id).ToList();

                foreach (ProdutoSku proSku in produtoSku)
                {
                    if (proSku.proSku_quantidadeDisponivel.HasValue)
                        proSku.proSku_quantidadeDisponivel += 1;
                }
                rep.Update(produtoSku);
            }

            var produtos = (from proSku in produtoSku
                            select new ProdutoSkuDto
                            {
                                pro_id = proSku.pro_id,
                                proSku_nome = proSku.Produto.pro_nome
                            }).ToList();

            return produtos;
        }

        /// <summary>
        /// Duplica um Produto
        /// </summary>
        /// <param name="proSku_id"></param>
        /// <returns></returns>
        public int DuplicarProdutoSku(int proSku_id)
        {
            return new ProdutoSkuDao().DuplicarProdutoSku(proSku_id);
        }
    }
}
