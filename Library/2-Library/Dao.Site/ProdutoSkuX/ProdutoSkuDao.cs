using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Site.CorreioX;
using _2_Library.Dao.Site.EntregaX;
using _2_Library.Dao.Site.Produto_GrupoX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ProdutoSkuX
{
    internal class ProdutoSkuDao : Repositorio<ProdutoSku>
    {

        public List<ProdutoSkuDto> SelectByProdutoSkuProdutoDetalhe(int loj_id, int proSku_id)
        {
            List<ProdutoSkuDto> produtoSku = (from proSku in Select()
                                              where
                                              proSku.Produto.ProdutoSku.Select(s => s.proSku_id).Contains(proSku_id) &&
                                              proSku.loj_id == loj_id &&
                                              proSku.proSku_bloquear == false
                                              select new ProdutoSkuDto
                                                {
                                                    proSku_id = proSku.proSku_id,
                                                    pro_id = proSku.pro_id,
                                                    proSku_nome = proSku.Produto.pro_nome,
                                                    proSku_precoAnterior = proSku.proSku_precoAnterior,
                                                    proSku_precoVenda = proSku.proSku_precoVenda,
                                                    proSku_percDesconto = (proSku.proSku_precoAnterior - proSku.proSku_precoVenda) / proSku.proSku_precoAnterior,
                                                    proSkuCor_id = proSku.proSkuCor_id,
                                                    proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                    proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                    proSkuTam_id = proSku.proSkuTam_id,
                                                    proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                                    proSku_quantidadeDisponivel = proSku.proSku_quantidadeDisponivel,
                                                    proSku_disponivel = proSku.proSku_disponivel,
                                                    produtoSkuFotoUDto = proSku.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao, pro_id = proSku.pro_id, loj_id = loj_id }).FirstOrDefault(),
                                                    produtoSkuFotoDto = proSku.ProdutoSkuFoto.Select(s2 => new ProdutoSkuFotoDto { proSkuFot_nome = s2.proSkuFot_nome, proSkuFot_extensao = s2.proSkuFot_extensao, pro_id = proSku.pro_id, loj_id = loj_id }),
                                                    entregaDto = new EntregaDto
                                                    {
                                                        ent_descricao = proSku.Entrega.ent_descricao,
                                                        ent_cepInicial = proSku.Entrega.ent_cepInicial,
                                                        ent_cepFinal = proSku.Entrega.ent_cepFinal,
                                                        ent_dataHoraInicial = proSku.Entrega.ent_dataHoraInicial,
                                                        ent_dataHoraFinal = proSku.Entrega.ent_dataHoraFinal,
                                                        ent_prazo = proSku.Entrega.ent_prazo,
                                                        ent_valor = proSku.Entrega.ent_valor
                                                    },
                                                    parcelamentoDto = new ParcelamentoDto
                                                    {
                                                        parcelamentoParcelaDto = proSku.Parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaDto { parcPar_quantidade = s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                        parc_valorMinimo = proSku.Parcelamento.parc_valorMinimo,
                                                        parc_ativarJuro = proSku.Parcelamento.parc_ativarJuro,
                                                        parc_periodoDe = proSku.Parcelamento.parc_periodoDe,
                                                        parc_periodoAte = proSku.Parcelamento.parc_periodoAte,
                                                        parc_bloquear = proSku.Parcelamento.parc_bloquear,
                                                    }
                                                }).ToList();
            return produtoSku;
        }

        public List<ProdutoSkuCorDto> SelectProdutoSkuCor(int loj_id, string gru_nomeAmigavel)
        {

            List<ProdutoSkuCorDto> produtoSkuCor = new List<ProdutoSkuCorDto>();
            Grupo grupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectByGrupo(loj_id, gru_nomeAmigavel);

            //Verifica se o grupo tem filhos, se sim seleciona todos os grupos finais para listar os produtos
            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectNosFinais(loj_id, gru_nomeAmigavel);

                produtoSkuCor = (from proSku in Select()
                                 where
                                  proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&// Contains(gru_nomeAmigavel) &&//Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) &&
                                  proSku.proSku_bloquear != true &&
                                  proSku.ProdutoSkuCor.proSkuCor_nome != null &&
                                  proSku.loj_id == loj_id
                                 select new ProdutoSkuCorDto
                                 {
                                     proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                     proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                 }).Distinct().ToList();

            }
            else
            {
                produtoSkuCor = (from proSku in Select()
                                 where
                                  proSku.Produto.Produto_Grupo.Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) && /*proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&*/
                                  proSku.proSku_bloquear != true &&
                                  proSku.ProdutoSkuCor.proSkuCor_nome != null &&
                                  proSku.loj_id == loj_id
                                 select new ProdutoSkuCorDto
                                 {
                                     proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                     proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                 }).Distinct().ToList();
            }
            return produtoSkuCor;
        }

        public List<ProdutoSkuTamanhoDto> SelectProdutoSkuTamanho(int loj_id, string gru_nomeAmigavel)
        {

            List<ProdutoSkuTamanhoDto> produtoSkuTamanho = new List<ProdutoSkuTamanhoDto>();
            Grupo grupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectByGrupo(loj_id, gru_nomeAmigavel);

            //Verifica se o grupo tem filhos, se sim seleciona todos os grupos finais para listar os produtos
            if (grupo.Grupo1.Count() > 0)
            {
                List<int?> idsGrupo = new _2_Library.Dao.Site.GrupoX.GrupoDao().SelectNosFinais(loj_id, gru_nomeAmigavel);

                produtoSkuTamanho = (from proSku in Select()
                                     where
                                       proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&
                                      proSku.proSku_bloquear != true &&
                                      proSku.ProdutoSkuTamanho.proSkuTam_nome != null &&
                                      proSku.loj_id == loj_id
                                     select new ProdutoSkuTamanhoDto
                                     {
                                         proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                         proSkuTam_imagem = proSku.ProdutoSkuTamanho.proSkuTam_imagem,
                                     }).Distinct().ToList();
            }
            else
            {
                produtoSkuTamanho = (from proSku in Select()
                                     where
                                      proSku.Produto.Produto_Grupo.Select(s => s.Grupo.gru_nomeAmigavel).Contains(gru_nomeAmigavel) && /*proSku.Produto.Produto_Grupo.Select(s => new { s.gru_id, s.pro_id }).Where(s2 => idsGrupo.Contains(s2.gru_id)).Select(s3 => s3.pro_id).Contains(proSku.pro_id) &&*/
                                      proSku.proSku_bloquear != true &&
                                      proSku.ProdutoSkuTamanho.proSkuTam_nome != null &&
                                      proSku.loj_id == loj_id
                                     select new ProdutoSkuTamanhoDto
                                     {
                                         proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                         proSkuTam_imagem = proSku.ProdutoSkuTamanho.proSkuTam_imagem,
                                     }).Distinct().ToList();
            }
            return produtoSkuTamanho;
        }

        public List<ProdutoSkuCorDto> SelectProdutoSkuBuscaCor(int loj_id, string pro_nomeChave)
        {
            List<ProdutoSkuCorDto> produtoSkuCor = (from proSku in Select()
                                                    where
                                                     proSku.Produto.Produto_Grupo.Select(s => s.Produto.pro_nome).Any(s2 => s2.Contains(pro_nomeChave)) &&
                                                     proSku.proSku_bloquear != true &&
                                                     proSku.ProdutoSkuCor.proSkuCor_nome != null &&
                                                     proSku.loj_id == loj_id
                                                    select new ProdutoSkuCorDto
                                                    {
                                                        proSkuCor_nome = proSku.ProdutoSkuCor.proSkuCor_nome,
                                                        proSkuCor_imagem = proSku.ProdutoSkuCor.proSkuCor_imagem,
                                                    }).Distinct().ToList();
            return produtoSkuCor;
        }

        public List<ProdutoSkuTamanhoDto> SelectProdutoSkuBuscaTamanho(int loj_id, string pro_nomeChave)
        {
            List<ProdutoSkuTamanhoDto> produtoSkuTamanho = (from proSku in Select()
                                                            where
                                                             proSku.Produto.Produto_Grupo.Select(s => s.Produto.pro_nome).Any(s2 => s2.Contains(pro_nomeChave)) &&
                                                             proSku.proSku_bloquear != true &&
                                                             proSku.ProdutoSkuTamanho.proSkuTam_nome != null &&
                                                             proSku.loj_id == loj_id
                                                            select new ProdutoSkuTamanhoDto
                                                            {
                                                                proSkuTam_nome = proSku.ProdutoSkuTamanho.proSkuTam_nome,
                                                                proSkuTam_imagem = proSku.ProdutoSkuTamanho.proSkuTam_imagem,
                                                            }).Distinct().ToList();

            return produtoSkuTamanho;
        }

        

    }
}
