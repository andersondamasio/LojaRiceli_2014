using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.Site.Produto_GrupoX
{
    public class Produto_GrupoTd
    {
        /// <summary>
        /// Seleciona os produtos da pagina inicial
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoInicial(string loj_dominio, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            List<Produto_GrupoDto> produto_grupo = new Produto_GrupoDao().SelectProduto_GrupoInicial(loj_id, startRowIndex, maximumRows, orderBy);

            List<Produto_GrupoDto> produto_grupoTd = (from proGru in produto_grupo
                                   select new Produto_GrupoDto
                                   {
                                       pro_id = proGru.pro_id,
                                       pro_nome = proGru.pro_nome,
                                       proSku_precoVenda = proGru.proSku_precoVenda,
                                       produtoSkuDto = proGru.produtoSkuDto.Select(s =>
                                       new ProdutoSkuDto
                                       {
                                           proSku_id = s.proSku_id,
                                           pro_id = s.pro_id,
                                           pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(proGru.pro_nome + "-" + s.proSkuCor_nome + "-" + s.proSkuTam_nome) + "-" + s.proSku_id,
                                           proSku_nome = proGru.pro_nome,
                                           proSku_precoAnterior = s.proSku_precoAnterior,
                                           proSku_precoVenda = s.proSku_precoVenda,
                                           proSku_percDesconto = s.proSku_percDesconto,
                                           proSkuCor_nome = s.proSkuCor_nome,
                                           proSkuTam_nome = s.proSkuTam_nome,
                                           proSkuCor_imagem = s.proSkuCor_imagem,
                                           produtoSkuFotoDto = s.produtoSkuFotoDto,
                                           proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                           proSku_disponivel = s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) != 0,
                                           entregaDto = Produto_GrupoUtils.VerificaEntrega(s.entregaDto, 0),
                                           parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(s.parcelamentoDto, s.proSku_precoVenda),
                                           loj_id = loj_id
                                       }).OrderBy(s2 => s2.produtoSkuFotoDto == null).GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).Take(5)
                                   }).ToList();
                
            return produto_grupoTd;
        }

        /// <summary>
        /// Conta os produtos da pagina inicial
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoInicialCount(string loj_dominio, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = orderBy == string.Empty ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            return new Produto_GrupoDao().SelectProduto_GrupoInicialCount(loj_id, startRowIndex, maximumRows, orderBy);
        }


        /// <summary>
        /// Seleciona os produtos de um Grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>

        public List<Produto_GrupoDto> SelectProduto_GrupoProdutoGrupo(string loj_dominio, string gru_nomeAmigavel, string proSku_cores,string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {

            if(string.IsNullOrEmpty(gru_nomeAmigavel))
               gru_nomeAmigavel = Tratamento.GetUrlAmigavelAtual();

            orderBy = string.IsNullOrEmpty(orderBy) ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            List<Produto_GrupoDto> produto_grupo = new Produto_GrupoDao().SelectProduto_GrupoProdutoGrupo(loj_id, gru_nomeAmigavel, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy);

            orderBy = orderBy.Replace("Maior", string.Empty).Replace("Menor", string.Empty);

            List<Produto_GrupoDto> produto_grupoTd = (from proGru in produto_grupo
                                                      select new Produto_GrupoDto
                                                      {
                                                          pro_id = proGru.pro_id,
                                                          pro_nome = proGru.pro_nome,
                                                          proSku_precoVenda = proGru.proSku_precoVenda,
                                                          produtoSkuDto = proGru.produtoSkuDto.Select(s =>
                                                          new ProdutoSkuDto
                                                          {
                                                              proSku_id = s.proSku_id,
                                                              pro_id = s.pro_id,
                                                              pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(proGru.pro_nome + "-" + s.proSkuCor_nome + "-" + s.proSkuTam_nome) + "-" + s.proSku_id,
                                                              proSku_nome = proGru.pro_nome,
                                                              proSku_precoAnterior = s.proSku_precoAnterior,
                                                              proSku_precoVenda = s.proSku_precoVenda,
                                                              proSku_percDesconto = s.proSku_percDesconto,
                                                              proSkuCor_nome = s.proSkuCor_nome,
                                                              proSkuTam_nome = s.proSkuTam_nome,
                                                              proSkuCor_imagem = s.proSkuCor_imagem,
                                                              produtoSkuFotoDto = s.produtoSkuFotoDto,
                                                              proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                                              proSku_disponivel = s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) != 0,
                                                              entregaDto = Produto_GrupoUtils.VerificaEntrega(s.entregaDto, 0),
                                                              parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(s.parcelamentoDto, s.proSku_precoVenda),
                                                              loj_id = loj_id
                                                          }).OrderBy(s2 => s2.produtoSkuFotoDto == null).GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).OrderBy(orderBy).Take(5)
                                                      }).ToList();

            return produto_grupoTd;
        }

        /// <summary>
        /// Conta os produtos de um grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoProdutoGrupoCount(string loj_dominio, string gru_nomeAmigavel, string proSku_cores, string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            if (string.IsNullOrEmpty(gru_nomeAmigavel))
                gru_nomeAmigavel = Tratamento.GetUrlAmigavelAtual();

            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;


            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            return new Produto_GrupoDao().SelectProduto_GrupoProdutoGrupoCount(loj_id, gru_nomeAmigavel, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy);
        }

        /// <summary>
        /// Seleciona os produtos de uma Busca
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_nomeChave"></param>
        /// <param name="proSku_cores"></param>
        /// <param name="proSku_tamanhos"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Produto_GrupoDto> SelectProduto_GrupoProdutoBusca(string loj_dominio, string pro_nomeChave, string proSku_cores, string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? "pro_id" : orderBy;
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            List<Produto_GrupoDto> produto_grupo = new Produto_GrupoDao().SelectProduto_GrupoProdutoBusca(loj_id, pro_nomeChave, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy);

            orderBy = orderBy.Replace("Maior", string.Empty).Replace("Menor", string.Empty);

            List<Produto_GrupoDto> produto_grupoTd = (from proGru in produto_grupo
                                                      select new Produto_GrupoDto
                                                      {
                                                          pro_id = proGru.pro_id,
                                                          pro_nome = proGru.pro_nome,
                                                          proSku_precoVenda = proGru.proSku_precoVenda,
                                                          produtoSkuDto = proGru.produtoSkuDto.Select(s =>
                                                          new ProdutoSkuDto
                                                          {
                                                              proSku_id = s.proSku_id,
                                                              pro_id = s.pro_id,
                                                              pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(proGru.pro_nome + "-" + s.proSkuCor_nome + "-" + s.proSkuTam_nome) + "-" + s.proSku_id,
                                                              proSku_nome = proGru.pro_nome,
                                                              proSku_precoAnterior = s.proSku_precoAnterior,
                                                              proSku_precoVenda = s.proSku_precoVenda,
                                                              proSku_percDesconto = s.proSku_percDesconto,
                                                              proSkuCor_nome = s.proSkuCor_nome,
                                                              proSkuCor_imagem = s.proSkuCor_imagem,
                                                              produtoSkuFotoDto = s.produtoSkuFotoDto,
                                                              proSku_quantidadeDisponivel = s.proSku_quantidadeDisponivel,
                                                              proSku_disponivel = s.proSku_disponivel && (s.proSku_quantidadeDisponivel ?? 1) != 0,
                                                              entregaDto = Produto_GrupoUtils.VerificaEntrega(s.entregaDto, 0),
                                                              parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(s.parcelamentoDto, s.proSku_precoVenda),
                                                              loj_id = loj_id
                                                          }).OrderBy(s2 => s2.produtoSkuFotoDto == null).GroupBy(s => s.proSkuCor_nome).Select(s => s.OrderBy(o => o.proSku_precoVenda).FirstOrDefault()).OrderBy(o => o.proSku_precoVenda).OrderByDescending(o => proSku_coresList.Contains(o.proSkuCor_nome != null ? o.proSkuCor_nome.ToLower() : null)).OrderBy(orderBy).Take(5)
                                                      }).ToList();
            return produto_grupoTd;

        }

        /// <summary>
        /// Conta os produtos de uma busca
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="pro_nomeChave"></param>
        /// <param name="proSku_cores"></param>
        /// <param name="proSku_tamanhos"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public int SelectProduto_GrupoProdutoBuscaCount(string loj_dominio, string pro_nomeChave, string proSku_cores, string proSku_tamanhos, int startRowIndex, int maximumRows, string orderBy)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            string[] proSku_coresList = proSku_cores == null ? new String[] { } : proSku_cores.Split('-');
            string[] proSku_tamanhosList = proSku_tamanhos == null ? new String[] { } : proSku_tamanhos.Split('-');

            return new Produto_GrupoDao().SelectProduto_GrupoProdutoBuscaCount(loj_id, pro_nomeChave, proSku_coresList, proSku_tamanhosList, startRowIndex, maximumRows, orderBy);
        }

       /* public Produto_GrupoDto SelectByProduto_GrupoDetalhe(string loj_dominio, int proSku_id)
        {

            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            Produto_GrupoDto proGru = new Produto_GrupoDao().SelectByProduto_GrupoDetalhe(loj_id, proSku_id);

            Produto_GrupoDto produto_grupoTd =
                                  new Produto_GrupoDto
                                   {
                                       pro_id = proGru.pro_id,
                                       pro_nome = proGru.pro_nome,
                                       proSku_precoVenda = proGru.proSku_precoVenda,

                                       produtoSkuDto = proGru.produtoSkuDto.Where(s => s.proSku_id == proSku_id).Select(proSkuTd =>
                                       new ProdutoSkuDto
                                       {
                                           proSku_id = proSkuTd.proSku_id,
                                           pro_id = proSkuTd.pro_id,
                                           proSku_precoAnterior = proSkuTd.proSku_precoAnterior,
                                           proSku_precoVenda = proSkuTd.proSku_precoVenda,
                                           proSku_percDesconto = proSkuTd.proSku_percDesconto,
                                           proSkuCor_imagem = proSkuTd.proSkuCor_imagem,
                                           proSkuTam_nome = proSkuTd.proSkuTam_nome,

                                           produtoSkuCoresDto = proGru.produtoSkuDto.Select(sC => new ProdutoSkuCoresDto
                                           {
                                               proSkuCor_imagem = sC.proSkuCor_imagem,
                                               produtoSkuFotoUDto = sC.produtoSkuFotoUDto,
                                               proSku_id = sC.proSku_id,
                                               proSkuCor_id = sC.proSkuCor_id
                                           }).GroupBy(g => new { g.proSkuCor_id }).Select(s1 => s1.FirstOrDefault()),

                                           produtoSkuTamanhosDto = proGru.produtoSkuDto.Select(sT =>
                                          new
                                          {
                                              sT.proSku_id,
                                              sT.proSkuCor_imagem,
                                              sT.proSkuTam_nome
                                          }).Where(s1 => s1.proSkuCor_imagem == proSkuTd.proSkuCor_imagem).GroupBy(g => new { g.proSkuTam_nome }).Select(s2 => s2.FirstOrDefault()).Select(s3 => new ProdutoSkuTamanhosDto {proSkuTam_nome = s3.proSkuTam_nome,proSku_id = s3.proSku_id }),

                                           produtoSkuFotoUDto = proSkuTd.produtoSkuFotoUDto,//s.produtoSkuFotoDto.FirstOrDefault().proSkuFot_nome,
                                           produtoSkuFotoDto = proSkuTd.produtoSkuFotoDto,//.GroupBy(g => new { g.proSkuFot_nome }).Select(g2 => g2.OrderBy(o => o.proSku_precoVenda).Select(s2 => new { s2.proSku_id, loj_id = loj_id, s.pro_id, s.pro_nome, s.gru_nome, s2.proSkuCor_nome, s2.proSkuTam_nome, s2.proSkuCor_id, s2.ProdutoSkuFoto, s2.proSku_disponivel, ordem = (s2.proSkuTam_nome == s.proSkuTam_nome) }).OrderBy(ss => !ss.ordem).FirstOrDefault()).OrderBy(o => o.proSkuCor_id),
                                           entregaDto = Produto_GrupoUtils.VerificaEntrega(proSkuTd.entregaDto, 0),
                                           parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(proSkuTd.parcelamentoDto, proSkuTd.proSku_precoVenda),
                                           loj_id = loj_id
                                       })
                                   };

            return produto_grupoTd;






        }*/
       
    }
}
