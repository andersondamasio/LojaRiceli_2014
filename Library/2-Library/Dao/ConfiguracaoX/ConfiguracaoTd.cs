using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.ConfiguracaoX
{
    public class ConfiguracaoTd
    {
        public ConfiguracaoDto SelectConfiguracao(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

           ConfiguracaoDto configuracaoDto = new ConfiguracaoDao().SelectConfiguracao(loj_id);

           return configuracaoDto;
        }
    }
}
