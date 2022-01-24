using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Configuracaox
{
    public class ConfiguracaoConsulta
    {
        public ConfiguracaoBean SelecionarConfiguracao()
        {
            return new ConfiguracaoDao().SelecionarConfiguracao();
        }

        public ConfiguracaoBoleto SelecionarConfiguracaoBoleto()
        {
            return new ConfiguracaoDao().SelecionarConfiguracaoBoleto();
        }

        public void AtualizarConfiguracaoBoletoAtivar(int conBol_id)
        {
            new ConfiguracaoDao().SelecionarConfiguracaoBoleto();
        }
    }
}