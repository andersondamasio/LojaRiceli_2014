using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.ProdutoSkuAvisoX
{
    internal class ProdutoSkuAvisoDao : Repositorio<ProdutoSkuAviso>
    {
        /// <summary>
        /// Adiciona um aviso de chegada de produto especifico
        /// </summary>
        /// <param name="produtoSkuAvisoDto"></param>
        public void InsertProdutoSkuAviso(ProdutoSkuAviso produtoSkuAviso)
        {
            if (Select().Where(s =>
                s.cli_id.Equals(produtoSkuAviso.cli_id)  &&
                s.proSku_id == produtoSkuAviso.proSku_id &&
                s.proSkuAvi_email == produtoSkuAviso.proSkuAvi_email &&
                s.loj_id == produtoSkuAviso.loj_id).Count() == 0)
                Add(produtoSkuAviso);
        
        }
    }
}
