using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ProdutoX
{
    internal class ProdutoDao : Repositorio<Produto>
    {

        public List<ProdutoDto> SelectByProduto(int loj_id, int proSku_id)
        {
            List<ProdutoDto> produtoInfo = (from pro in Select()
                                              where
                                              pro.ProdutoSku.Select(s => s.proSku_id).Contains(proSku_id) &&
                                              pro.loj_id == loj_id
                                                select new ProdutoDto{
                                                    pro_descricao = pro.pro_descricao,
                                                    produtoInfoDto = pro.ProdutoInfo.Where(pi=>!pi.proInfo_bloquear).Select(pi => new ProdutoInfoDto { proInfo_nome = pi.proInfo_nome, produtoInfoItemDto = pi.ProdutoInfoItem.Select(pii => new ProdutoInfoItemDto { proInfoItem_descricao = pii.proInfoItem_descricao, proInfoItem_valor = pii.proInfoItem_valor }) }),      
                                              }).ToList();

            return produtoInfo;

        }

    }
}
