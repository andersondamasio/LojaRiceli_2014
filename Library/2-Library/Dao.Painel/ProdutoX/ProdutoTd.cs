using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Painel.ProdutoSkuX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.Painel.ProdutoX
{
    public class ProdutoTd
    {
        /// <summary>
        /// Seleciona um determinado Produto
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_id"></param>
        /// <returns></returns>
        public ProdutoDto SelectProduto(string loj_dominio, int pro_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            ProdutoDto produtoDto = new ProdutoDao().SelectProduto(loj_id, pro_id);

            return produtoDto;
        }

        /// <summary>
        /// Duplica um Produto
        /// </summary>
        /// <param name="pro_id"></param>
        /// <returns></returns>
        public int DuplicarProduto(int pro_id)
        {
            return new ProdutoDao().DuplicarProduto(pro_id);
        }

        public int InsertProduto(string loj_dominio, ProdutoDto produtoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

           Produto produto = ToProduto(produtoDto);
           
            using (ProdutoDao rep = new ProdutoDao())
            {
                rep.Add(produto);
            }

            return produto.pro_id;
        }

        private Produto ToProduto(ProdutoDto produtoDto){

            Produto produto = new Produto();
            produto.loj_id = produtoDto.loj_id;
            produto.pro_nome = produtoDto.pro_nome;
            produto.pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(produtoDto.pro_nome);
            produto.mar_id = produtoDto.mar_id;
            produto.pro_descricao = produtoDto.pro_descricao;
            produto.pro_posicao = produtoDto.pro_posicao;
            produto.pro_bloquear = produtoDto.pro_bloquear;
            produto.pro_paginaInicialDe = produtoDto.pro_paginaInicialDe;
            produto.pro_paginaInicialDe = produtoDto.pro_paginaInicialAte;
            Produto_Grupo produto_grupo = (from proGru in produtoDto.produto_GrupoDto
                                           select new Produto_Grupo
                                           {
                                               loj_id = produtoDto.loj_id,
                                               gru_id = proGru.gru_id,
                                               pro_id = proGru.pro_id,
                                               proGru_dataHora = DateTime.Now,
                                           }).FirstOrDefault();

            produto.Produto_Grupo.Add(produto_grupo);

            return produto;
        }

    }
}
