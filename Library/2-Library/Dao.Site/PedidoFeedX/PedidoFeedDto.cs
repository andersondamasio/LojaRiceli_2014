using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Dao.Site.PedidoX;

namespace _2_Library.Dao.Site.PedidoFeedX
{
    public class PedidoFeedDto
    {
        public int pf_id { get; set;}
        public PedidoDto pedidoDto { get; set; }
    }
}
