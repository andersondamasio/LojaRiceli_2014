using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.PedidoX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Utils;

namespace _2_Library.Dao.Site.PedidoFeedX
{
    public class PedidoFeedTd
    {
        public List<PedidoFeedDto> SelectPedidoFeed(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<PedidoFeedDto> pedidoFeedDto = new PedidoFeedDao().SelectPedidoFeed(loj_id);

            pedidoFeedDto = (from pf in pedidoFeedDto 
                                select  new PedidoFeedDto
                             {
                                 pf_id = pf.pf_id,
                                 pedidoDto = new PedidoDto
                                 {
                                     ped_cliEnd_nome = pf.pedidoDto.ped_cliEnd_nome,
                                     pedidoProdutoDto = pf.pedidoDto.pedidoProdutoDto.Select(s => 
                                         new PedidoProdutoDto { 
                                             pedPro_proSku_id = s.pedPro_proSku_id, 
                                             pedPro_pro_nome = s.pedPro_pro_nome,
                                             pedPro_pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(s.pedPro_pro_nome + "-" + s.pedPro_proSkuCor_nome + "-" + s.pedPro_proSkuTam_nome) + "-" + s.pedPro_proSku_id,
                                             produtoSkuFotoDto = s.produtoSkuFotoDto.Select(f =>
                                                                              new ProdutoSkuFotoDto
                                                                              {
                                                                                  proSkuFot_nome = f.proSkuFot_nome,
                                                                                  proSkuFot_extensao = f.proSkuFot_extensao,
                                                                                  pro_id = f.pro_id,
                                                                                  loj_id = loj_id
                                                                              }).Take(1)
                                         })
                                 }
                             }).ToList();

            return pedidoFeedDto;
        }
    }
}
