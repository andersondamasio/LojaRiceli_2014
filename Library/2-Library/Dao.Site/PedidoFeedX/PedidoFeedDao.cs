using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Site.PedidoX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.PedidoFeedX
{
    internal class PedidoFeedDao : Repositorio<PedidoFeed>
    {
        public List<PedidoFeedDto> SelectPedidoFeed(int loj_id) {

            List<PedidoFeedDto> pedidoFeedDto = (from pf in Select()
                                           where pf.loj_id == loj_id
                                           select new PedidoFeedDto
                                            {
                                                pf_id = pf.pf_id,
                                                pedidoDto = new PedidoDto
                                                           {
                                                               ped_cliEnd_nome = pf.Pedido.ped_cliEnd_nome,                                                            
                                                               pedidoProdutoDto = pf.Pedido.PedidoProduto.Select(s =>
                                                                   new PedidoProdutoDto
                                                                   {
                                                                       pedPro_proSku_id = s.pedPro_proSku_id,
                                                                       pedPro_pro_nome = s.pedPro_pro_nome,
                                                                       pedPro_proSkuCor_nome = s.pedPro_proSkuCor_nome,
                                                                       pedPro_proSkuTam_nome = s.pedPro_proSkuTam_nome,
                                                                       produtoSkuFotoDto = s.ProdutoSku.ProdutoSkuFoto.Select(f =>
                                                                           new ProdutoSkuFotoDto
                                                                           {
                                                                               proSkuFot_nome = f.proSkuFot_nome,
                                                                               proSkuFot_extensao = f.proSkuFot_extensao,
                                                                               pro_id = f.ProdutoSku.pro_id
                                                                           })
                                                                   })
                                                           }
                                            }).ToList();
            return pedidoFeedDto;
        }
    }
}
