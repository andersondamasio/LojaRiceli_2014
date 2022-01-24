using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Painel.GrupoX
{
    public class GrupoTd
    {
        public List<GrupoDto> SelectGrupo(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            var grupoDto = new GrupoDao().SelectGrupo(loj_id);

            return grupoDto;
        }
    }
}
