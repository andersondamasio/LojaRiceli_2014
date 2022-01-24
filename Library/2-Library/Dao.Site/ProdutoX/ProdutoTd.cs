using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.ProdutoX;

namespace _2_Library.Dao.Site.ProdutoInfoX
{
    public class ProdutoTd
    {
        public List<ProdutoDto> SelectByProduto(string loj_dominio, int proSku_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List <ProdutoDto> produtoInfoTd =  new ProdutoDao().SelectByProduto(loj_id, proSku_id);

            return produtoInfoTd;
        }

    }
}
