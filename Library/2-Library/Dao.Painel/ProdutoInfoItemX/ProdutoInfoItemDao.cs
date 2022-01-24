using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;
using System.Data.Entity;

namespace _2_Library.Dao.Painel.ProdutoInfoItemX
{
    internal class ProdutoInfoItemDao : Repositorio<ProdutoInfoItem>
    {
      /*  private ProdutoInfoItem Duplicar(Int32 proInfoItem_id)
        {
            ProdutoInfoItem produtoInfoItem = Select(s => s.proInfoItem_id == proInfoItem_id).AsNoTracking().FirstOrDefault();
            produtoInfoItem.proInfoItem_dataHora = DateTime.Now;
            produtoInfoItem.proInfoItem_dataHoraAtualizacao = null;
            return produtoInfoItem;
        }*/
    }
}
