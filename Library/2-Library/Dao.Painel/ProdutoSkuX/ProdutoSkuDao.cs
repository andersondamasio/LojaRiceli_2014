using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;
using System.Data.Entity;

namespace _2_Library.Dao.Painel.ProdutoSkuX
{
    internal class ProdutoSkuDao : Repositorio<ProdutoSku>
    {
        public List<ProdutoSkuDto> SelectProdutoSkuListar(int loj_id, int pro_id, int startRowIndex, int maximumRows, string orderBy)
        {
            List<ProdutoSkuDto> produto_grupo = (from proSku in Select()
                                                 where proSku.pro_id == pro_id
                                                 select new ProdutoSkuDto
                                                 {
                                                     proSku_id = proSku.proSku_id,
                                                     proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                     proSku_precoVenda = proSku.proSku_precoVenda,
                                                     proSku_precoCusto = proSku.proSku_precoCusto,
                                                     proSku_disponivel = proSku.proSku_disponivel,
                                                     proSku_quantidadeDisponivel = proSku.proSku_quantidadeDisponivel,
                                                     proSku_posicao = proSku.proSku_posicao,
                                                     proSku_bloquear = proSku.proSku_bloquear,
                                                     produtoSkuCorDto = new ProdutoSkuCorDto
                                                     {
                                                         proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                         proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem
                                                     },
                                                     produtoSkuTamanhoDto = new ProdutoSkuTamanhoDto
                                                     {
                                                         proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome
                                                     }
                                                 }).ToList();
            return produto_grupo;
        }

        public int SelectProdutoSkuListarCount(int loj_id, int pro_id, int startRowIndex, int maximumRows, string orderBy)
        {
            int produto_grupo = (from proSku in Select()
                                                 where proSku.pro_id == pro_id
                                                 select proSku).Count();
            return produto_grupo;
        }


        public int DuplicarProdutoSku(int proSku_id)
        {
            ProdutoSku produtoSku = Clonar(s => s.proSku_id == proSku_id).FirstOrDefault();
            produtoSku.proSku_dataHoraAtualizacao = null;
            produtoSku.proSku_dataHora = DateTime.Now;

            foreach (ProdutoSkuFoto proSkuFoto in produtoSku.ProdutoSkuFoto)
            {
                proSkuFoto.proSkuFot_dataHoraAtualizacao = null;
                proSkuFoto.proSkuFot_dataHora = DateTime.Now;
            }
            Add(produtoSku);
            return produtoSku.proSku_id;
        }

      /*  private ProdutoSku Duplicar(Int32 proSku_id)
        {
            ProdutoSku produtoSku = Select(s => s.proSku_id == proSku_id).Include("ProdutoSkuFoto").AsNoTracking().FirstOrDefault();
            produtoSku.proSku_dataHoraAtualizacao = null;
            produtoSku.proSku_dataHora = DateTime.Now;

            foreach (ProdutoSkuFoto produtoSkuFoto in produtoSku.ProdutoSkuFoto)
            {
                    produtoSkuFoto.proSkuFot_dataHoraAtualizacao = null;
                    produtoSkuFoto.proSkuFot_dataHora = DateTime.Now;
            }
            return produtoSku;
        }
    */
    }
}
