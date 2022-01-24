using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja;
using Loja.Painel;
using _2_Library.Modelo;

namespace Loja.Modelo
{
    public class Consulta
    {
        EntitiesDao entidadeDao = new EntitiesDao();


        public List<MensagemSistema> SelecionaMensagemSistema()
        {
            return entidadeDao.SelecionaMensagemSistema();
        }

        public Retorno SelecionarConfiguracao(Int32 loj_id)
        {
            return entidadeDao.SelecionarConfiguracao(loj_id);
        }

        public IQueryable<Grupo> SelecionarGrupo()
        {
           return entidadeDao.SelecionarGrupo();
        }

         public Grupo SelecionarGrupo(string gru_nomeAmigavel)
        {
            return entidadeDao.SelecionarGrupo(gru_nomeAmigavel);
        }

        public Retorno AtualizarGrupo(Grupo grupo)
        {
            Extensions.LinqToCacheRemove("Grupo");
           return entidadeDao.AtualizarGrupo(grupo);
        }

        public Retorno InserirGrupo(Grupo grupo) {

            Extensions.LinqToCacheRemove("Grupo");
            return entidadeDao.InserirGrupo(grupo);
        }

        public Retorno ExcluirGrupo(int gru_id)
        {
            Extensions.LinqToCacheRemove("Grupo");
          return entidadeDao.ExcluirGrupo(gru_id);
        }

        public Retorno InserirProduto(Produto produto)
        {
            Extensions.LinqToCacheRemove("Produto");
           return entidadeDao.InserirProduto(produto);
        }

        public IQueryable<Produto> SelecionarProduto(int pro_id)
        {
            return entidadeDao.SelecionarProduto(pro_id);
        }

        public Retorno AtualizarProduto(Produto produto)
        {
            Extensions.LinqToCacheRemove("Produto");
            return entidadeDao.AtualizarProduto(produto);
        }

        public Retorno ExcluirProduto(Int32 pro_id)
        {
            Extensions.LinqToCacheRemove("Produto");
            return entidadeDao.ExcluirProduto(pro_id);
        }

        public Retorno InserirProdutoGrupo(Produto_Grupo produtoGrupo)
        {
            Extensions.LinqToCacheRemove("Produto");
           return entidadeDao.InserirProdutoGrupo(produtoGrupo);
        }

        public IQueryable<Produto_Grupo> SelecionarProdutoGrupo(Int32 pro_id)
        {
            return entidadeDao.SelecionarProdutoGrupo(pro_id);
        }

        public Retorno ExcluirProdutoGrupo(Int32 pro_id, Int32 gru_id)
        {
            Extensions.LinqToCacheRemove("Produto");
            return entidadeDao.ExcluirProdutoGrupo(pro_id, gru_id);
        }

        public Retorno InserirProdutoSku(ProdutoSku produtoSku)
        {
            Extensions.LinqToCacheRemove("Produto");
            return entidadeDao.InserirProdutoSku(produtoSku);
        }

        public ProdutoSku SelecionarProdutoSku(int proSku_id)
        {
            return entidadeDao.SelecionarProdutoSku(proSku_id);
        }

        public void AtualizarProdutoSku(ProdutoSku produtoSku)
        {
            Extensions.LinqToCacheRemove("Produto");
            entidadeDao.AtualizarProdutoSku(produtoSku);
        }

        public Retorno ExcluirProdutoSku(Int32 proSku_id)
        {
            Extensions.LinqToCacheRemove("Produto");
           return entidadeDao.ExcluirProdutoSku(proSku_id);
        }

        public IQueryable<ProdutoSkuFoto> SelecionarProdutoSkuFoto() {

            return new EntitiesDao().SelecionarProdutoSkuFoto();
        }

        public Retorno InserirProdutoSkuFoto(ProdutoSkuFoto produtoSkuFoto)
        {
            Extensions.LinqToCacheRemove("Produto");
          return new EntitiesDao().InserirProdutoSkuFoto(produtoSkuFoto);
        }

        public void AtualizarProdutoSkuFoto(ProdutoSkuFoto produtoSkuFoto)
        {
            Extensions.LinqToCacheRemove("Produto");
            new EntitiesDao().AtualizarProdutoSkuFoto(produtoSkuFoto);
        }

        public Retorno ExcluirProdutoSkuFoto(Int32 proSkuFot_id)
        {
            Extensions.LinqToCacheRemove("Produto");
            return new EntitiesDao().ExcluirProdutoSkuFoto(proSkuFot_id);
        }

        public IQueryable<ProdutoSkuCor> SelecionarProdutoSkuCor()
        {
            return entidadeDao.SelecionarProdutoSkuCor();
        }

        public IQueryable<ProdutoSkuTamanho> SelecionarProdutoSkuTamanho()
         {
             return entidadeDao.SelecionarProdutoSkuTamanho();
         }

        public void InserirProdutoInfo(ProdutoInfo produtoInfo)
         {
             entidadeDao.InserirProdutoInfo(produtoInfo);
         }

        public ProdutoInfo SelecionarProdutoInfo(Int32 proInfo_id) {

           return entidadeDao.SelecionarProdutoInfo(proInfo_id);
        }

        public void AtualizarProdutoInfo(ProdutoInfo produtoInfo)
        {
            entidadeDao.AtualizarProdutoInfo(produtoInfo);
        }

        public Retorno ExcluirProdutoInfo(Int32 proInfo_id)
        {
           return entidadeDao.ExcluirProdutoInfo(proInfo_id);
        }

        public void InserirProdutoInfoItem(ProdutoInfoItem produtoInfoItem)
        {
            entidadeDao.InserirProdutoInfoItem(produtoInfoItem);
        }

        public void AtualizarProdutoInfoItem(ProdutoInfoItem produtoInfoItem)
        {
            entidadeDao.AtualizarProdutoInfoItem(produtoInfoItem);
        }
        public ProdutoInfoItem SelecionarProdutoInfoItem(Int32 proInfoItem_id)
        {
          return  entidadeDao.SelecionarProdutoInfoItem(proInfoItem_id);
        }

        public Retorno ExcluirProdutoInfoItem(Int32 proInfoItem_id)
        {
            return entidadeDao.ExcluirProdutoInfoItem(proInfoItem_id);
        }


        public void InserirMarca(Marca marca)
        {
            entidadeDao.InserirMarca(marca);
        }

        public IQueryable SelecionaMarca()
        {
            return entidadeDao.SelecionaMarca();
        }

        public Marca SelecionaMarca(Int32 mar_id)
        {
           return entidadeDao.SelecionaMarca(mar_id);
        }

        public void AtualizarMarca(Marca marca)
        {
            entidadeDao.AtualizarMarca(marca);
        }
    }
}