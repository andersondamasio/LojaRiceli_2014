using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ProdutoSkuAvisoX
{
    public class ProdutoSkuAvisoTd
    {

        public void InsertProdutoSkuAviso(string loj_dominio, ProdutoSkuAvisoDto produtoSkuAvisoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            ProdutoSkuAviso produtoSkuAviso = new ProdutoSkuAviso();
            produtoSkuAviso.proSkuAvi_email = produtoSkuAvisoDto.proSkuAvi_email;
            produtoSkuAviso.proSkuAvi_nome = produtoSkuAvisoDto.proSkuAvi_nome;
            produtoSkuAviso.proSku_id = produtoSkuAvisoDto.proSku_id;
            produtoSkuAviso.loj_id = loj_id;

            if (produtoSkuAvisoDto.cli_id.HasValue)
                produtoSkuAviso.cli_id = produtoSkuAvisoDto.cli_id;

            new ProdutoSkuAvisoDao().InsertProdutoSkuAviso(produtoSkuAviso);

        }

    }
}
