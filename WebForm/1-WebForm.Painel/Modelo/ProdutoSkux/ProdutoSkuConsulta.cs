using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Utils;
using _2_Library.Modelo;

namespace Loja.Modelo.ProdutoSkux
{
    public class ProdutoSkuConsulta
    {
        public ProdutoSkuBean SelecionarProdutoSkuBean(int proSku_id)
        {
           ProdutoSkuBean produtoSkuBean = new ProdutoSkuDao().SelecionarProdutoSkuBean(proSku_id);
           produtoSkuBean.ParcelamentoBean = Recursos.CalculaParcelamento(produtoSkuBean.ParcelamentoBean, produtoSkuBean.proSku_precoVenda);
           produtoSkuBean.ProdutoSkuFotoBean = produtoSkuBean.ProdutoSkuFotoBean ?? new ProdutoSkuFotoBean();
           return produtoSkuBean;
        }
        public dynamic SelecionarProdutoSku(int proSku_id)
        {
            return new ProdutoSkuDao().SelecionarProdutoSku(proSku_id);
        }
/*
        public IEnumerable<ProdutoSkuCorBean> SelecionarProdutoSkuCor(Grupo grupo,string gru_nomeAmigavel)
        {
            return new ProdutoSkuDao().SelecionarProdutoSkuCor(grupo,gru_nomeAmigavel);
        }

        public IEnumerable<ProdutoSkuTamanhoBean> SelecionarProdutoSkuTamanho(Grupo grupo, string gru_nomeAmigavel)
        {
            return new ProdutoSkuDao().SelecionarProdutoSkuTamanho(grupo, gru_nomeAmigavel);
        }
        */
        public Boolean ProdutoSkuDisponivel(int proSku_id)
        {
            return new ProdutoSkuDao().ProdutoSkuDisponivel(proSku_id);
        }
    }
}