using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ConfiguracaoX
{
    internal class ConfiguracaoDao : Repositorio<Configuracao>
    {
        public ConfiguracaoDto SelectConfiguracao(int loj_id)
        {
            ConfiguracaoDto configuracaoDto = (from conf in Select()
                                               where conf.loj_id == loj_id
                                               select new ConfiguracaoDto
                                               {
                                                   con_emailRecuperarSenha = conf.con_emailRecuperarSenha
                                               }).FirstOrDefault();
            return configuracaoDto;
        }
    }
}
