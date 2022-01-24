using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Produto_GrupoX;

namespace _2_Library.Dao.ProdutoSkuX
{
    public class ProdutoSkuTd
    {

        /// <summary>
        /// Seleciona os skus pertencentes a um determinado produto
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="proSku_id"></param>
        /// <returns></returns>
        public ProdutoSkuDto SelectByProdutoSkuProdutoDetalhe(string loj_dominio, int proSku_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuDto> proSku = new ProdutoSkuDao().SelectByProdutoSkuProdutoDetalhe(loj_id, proSku_id);

            ProdutoSkuDto produtoSkuTd = (from proSkuTd in proSku
                                          where proSkuTd.proSku_id == proSku_id
                                          select new ProdutoSkuDto
                                          {
                                              proSku_id = proSkuTd.proSku_id,
                                              pro_id = proSkuTd.pro_id,
                                              proSku_nome = proSkuTd.proSku_nome,
                                              proSkuCor_nome = proSkuTd.proSkuCor_nome,

                                              produtoSkuCoresTamanhos1Dto = proSku.Select(sC => new ProdutoSkuCoresTamanhosDto
                                              {
                                                  produtoSkuDto = new ProdutoSkuDto()
                                                  {
                                                      proSkuCor_nome = sC.proSkuCor_nome,
                                                      proSkuCor_imagem = sC.proSkuCor_imagem,
                                                      produtoSkuFotoUDto = sC.produtoSkuFotoUDto,
                                                      proSkuCor_id = sC.proSkuCor_id,
                                                      proSku_id = sC.proSku_id,
                                                      proSku_nome = sC.proSku_nome,
                                                      proSku_disponivel = proSku.Where(s => s.pro_id == proSkuTd.pro_id && s.proSkuCor_id == sC.proSkuCor_id).Select(s2 => new { proSku_disponivel = (s2.proSku_disponivel && (s2.proSku_quantidadeDisponivel ?? 1) != 0) }).FirstOrDefault().proSku_disponivel,
                                                      loj_id = loj_id
                                                  }
                                              }).OrderBy(s2 => s2.produtoSkuDto.produtoSkuFotoUDto == null).GroupBy(g => new { g.produtoSkuDto.proSkuCor_id }).Select(s1 => s1.FirstOrDefault()),

                                              produtoSkuCoresTamanhos2Dto = proSku.Select(sT =>
                                             new
                                             {
                                                 sT.proSku_id,
                                                 sT.proSkuCor_imagem,
                                                 sT.proSkuTam_id,
                                                 sT.proSkuTam_nome,
                                                 sT.proSkuCor_nome,
                                                 sT.proSkuCor_id,
                                                 sT.proSku_precoAnterior,
                                                 proSku_precoVenda = (sT.proSku_disponivel && (sT.proSku_quantidadeDisponivel ?? 1) >= 1) ? sT.proSku_precoVenda : 9999999999,
                                                 sT.proSku_percDesconto,
                                                 sT.entregaDto,
                                                 sT.parcelamentoDto
                                             }).Where(s1 => s1.proSkuCor_imagem == proSkuTd.proSkuCor_imagem).GroupBy(g => new { g.proSkuTam_nome }).Select(s2 => s2.FirstOrDefault()).Select(s3 =>
                                                 new ProdutoSkuCoresTamanhosDto
                                                 {
                                                     produtoSkuDto = new ProdutoSkuDto()
                                                     {
                                                         proSkuTam_id = s3.proSkuTam_id,
                                                         proSkuTam_nome = s3.proSkuTam_nome,
                                                         proSku_id = s3.proSku_id,
                                                         proSkuCor_id = s3.proSkuCor_id,
                                                         proSku_precoAnterior = s3.proSku_precoAnterior,
                                                         proSku_precoVenda = s3.proSku_precoVenda,
                                                         proSku_percDesconto = s3.proSku_percDesconto,
                                                         proSkuCor_imagem = s3.proSkuCor_imagem,
                                                         proSku_quantidadeDisponivel = proSkuTd.proSku_quantidadeDisponivel,
                                                         entregaDto = Produto_GrupoUtils.VerificaEntrega(s3.entregaDto, 0),
                                                         parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(s3.parcelamentoDto, s3.proSku_precoVenda),
                                                         proSku_disponivel = proSku.Where(sSub =>
                                                             sSub.proSkuCor_id == s3.proSkuCor_id &&
                                                             sSub.proSkuTam_nome == proSkuTd.proSkuTam_nome &&
                                                             (proSku.Where(s => s.pro_id == proSkuTd.pro_id && s.proSkuCor_id == s3.proSkuCor_id && s.proSkuTam_nome == s3.proSkuTam_nome).Select(s2 => new { proSku_disponivel = (s2.proSku_disponivel && (s2.proSku_quantidadeDisponivel ?? 1) != 0) }).FirstOrDefault().proSku_disponivel)).Count() > 0,
                                                         loj_id = loj_id
                                                     }
                                                 }).OrderBy(ta => ta.produtoSkuDto.proSkuTam_id),

                                              produtoSkuFotoUDto = proSkuTd.produtoSkuFotoUDto,//s.produtoSkuFotoDto.FirstOrDefault().proSkuFot_nome,
                                              produtoSkuFotoDto = proSkuTd.produtoSkuFotoDto,//.OrderBy(o => o.).Select(s2 => new { s2.proSku_id, loj_id = loj_id, s.pro_id, s.pro_nome, s.gru_nome, s2.proSkuCor_nome, s2.proSkuTam_nome, s2.proSkuCor_id, s2.ProdutoSkuFoto, s2.proSku_disponivel, ordem = (s2.proSkuTam_nome == s.proSkuTam_nome) }).OrderBy(ss => !ss.ordem).FirstOrDefault()).OrderBy(o => o.proSkuCor_id),
                                              loj_id = loj_id
                                          }).FirstOrDefault();


            return produtoSkuTd;
        }

        /// <summary>
        /// Seleciona as cores existentes em um determinado grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public List<ProdutoSkuCorDto> SelectProdutoSkuCor(string loj_dominio, string gru_nomeAmigavel)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuCorDto> produtoSkuCorTd = new ProdutoSkuDao().SelectProdutoSkuCor(loj_id, gru_nomeAmigavel);

           return produtoSkuCorTd;

        }

        /// <summary>
        /// Seleciona os tamanhos existentes em um determinado grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public List<ProdutoSkuTamanhoDto> SelectProdutoSkuTamanho(string loj_dominio, string gru_nomeAmigavel)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuTamanhoDto> produtoSkuTamanhoTd = new ProdutoSkuDao().SelectProdutoSkuTamanho(loj_id, gru_nomeAmigavel);

            return produtoSkuTamanhoTd;
        }


        /// <summary>
        /// Seleciona as cores existentes em um determinada busca
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_nomeChave"></param>
        /// <returns></returns>
        public List<ProdutoSkuCorDto> SelectProdutoSkuBuscaCor(string loj_dominio, string pro_nomeChave)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuCorDto> produtoSkuCorTd = new ProdutoSkuDao().SelectProdutoSkuBuscaCor(loj_id, pro_nomeChave);

            return produtoSkuCorTd;

        }

        /// <summary>
        /// Seleciona os tamanhos existentes em um determinada busca
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_nomeChave"></param>
        /// <returns></returns>
        public List<ProdutoSkuTamanhoDto> SelectProdutoSkuBuscaTamanho(string loj_dominio, string pro_nomeChave)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<ProdutoSkuTamanhoDto> produtoSkuTamanhoTd = new ProdutoSkuDao().SelectProdutoSkuBuscaTamanho(loj_id, pro_nomeChave);

            return produtoSkuTamanhoTd;
        }

    }
}
