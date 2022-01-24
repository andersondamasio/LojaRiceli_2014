using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Site.EntregaX
{
    public class EntregaTd
    {
        public List<EntregaDto> SelectEntregaGlobal(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            List<EntregaDto> entregaDto = null;

            using (EntregaDao entregaDao = new EntregaDao())
            {
                entregaDto = entregaDao.SelectEntregaGlobal(loj_id);
            }
            return entregaDto;
        }

    }
}
