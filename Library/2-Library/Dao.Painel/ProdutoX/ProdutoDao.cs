using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Painel.GrupoX;
using _2_Library.Dao.Painel.MarcaX;
using _2_Library.Modelo;
using System.Data.Entity;
using _2_Library.Dao.Painel.ProdutoSkuX;
using _2_Library.Dao.Painel.ProdutoInfoX;

namespace _2_Library.Dao.Painel.ProdutoX
{
    internal class ProdutoDao : Repositorio<Produto>
    {
        public ProdutoDto SelectProduto(int loj_id, int pro_id)
        {
            ProdutoDto produtoDto = (from pro in Select()
                                     where pro.pro_id == pro_id
                                     select new ProdutoDto
                                           {
                                               pro_id = pro.pro_id,
                                               pro_nome = pro.pro_nome,
                                               pro_descricao = pro.pro_descricao,
                                               pro_paginaInicialDe = pro.pro_paginaInicialDe,
                                               pro_paginaInicialAte = pro.pro_paginaInicialAte,
                                               pro_posicao = pro.pro_posicao,
                                               pro_bloquear = pro.pro_bloquear,
                                               marcaDto = new MarcaDto
                                               {
                                                   mar_id = pro.Marca.mar_id,
                                                   mar_nome = pro.Marca.mar_nome
                                               },
                                               grupoDto = pro.Produto_Grupo.Select(s => new GrupoDto { gru_id = s.Grupo.gru_id, gru_nome = s.Grupo.gru_nome })

                                           }).FirstOrDefault();
            return produtoDto;
        }


        public int DuplicarProduto(int pro_id)
        {
            Produto produto = Clonar(s => s.pro_id == pro_id).FirstOrDefault();
            foreach (ProdutoSku proSku in produto.ProdutoSku)
            {
                proSku.proSku_dataHoraAtualizacao = null;
                proSku.proSku_dataHora = DateTime.Now;

                foreach (ProdutoSkuFoto proSkuFoto in proSku.ProdutoSkuFoto)
                {
                    proSkuFoto.proSkuFot_dataHoraAtualizacao = null;
                    proSkuFoto.proSkuFot_dataHora = DateTime.Now;
                }
            }

            foreach (ProdutoInfo proInfo in produto.ProdutoInfo)
            {
                proInfo.proInfo_dataHoraAtualizacao = null;
                proInfo.proInfo_dataHora = DateTime.Now;

                foreach (ProdutoInfoItem proInfoItem in proInfo.ProdutoInfoItem)
                {
                    proInfoItem.proInfoItem_dataHoraAtualizacao = null;
                    proInfoItem.proInfoItem_dataHora = DateTime.Now;
                }
            }

            foreach(Produto_Grupo proGrupo in produto.Produto_Grupo){
                proGrupo.proGru_dataHora = DateTime.Now;
            }

            Add(produto);
            return produto.pro_id;
        }
    }
}
