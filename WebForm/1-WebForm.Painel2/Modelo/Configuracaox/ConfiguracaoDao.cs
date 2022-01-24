using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Lojax;
using _2_Library.Modelo;

namespace Loja.Modelo.Configuracaox
{
    public class ConfiguracaoDao
    {
        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

        public ConfiguracaoBean SelecionarConfiguracao()
        {
            ConfiguracaoBean configuracaoBean = new ConfiguracaoBean();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                configuracaoBean = (from con in lojaEntities.Configuracao
                                    where con.loj_id == loj_id
                                    select new ConfiguracaoBean
                                    { 
                                    con_emailRecuperarSenha = con.con_emailRecuperarSenha,
                                    con_emailPedidoRecebido = con.con_emailPedidoRecebido,
                                    con_emailPedidoStatus = con.con_emailPedidoStatus
                                    }).SingleOrDefault();
            }
            return configuracaoBean;
        }

        public ConfiguracaoBoleto SelecionarConfiguracaoBoleto()
        {
            ConfiguracaoBoleto configuracaoBoleto = new ConfiguracaoBoleto();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                configuracaoBoleto = (from conBol in lojaEntities.ConfiguracaoBoleto
                                    where conBol.loj_id == loj_id && conBol.conBol_ativar
                                    select conBol).SingleOrDefault();
            }
            return configuracaoBoleto;
        }

        public void AtualizarConfiguracaoBoletoAtivar(int conBol_id)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                var configuracao = (from conBol in lojaEntities.ConfiguracaoBoleto
                                    where conBol.loj_id == loj_id && conBol.conBol_id != conBol_id
                                    select conBol);

                foreach (ConfiguracaoBoleto con in configuracao)
                    con.conBol_ativar = false;

                lojaEntities.SaveChanges();
            }
        }
    }
}