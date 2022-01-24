using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.GatPagSeguroX
{
    public class GatPagSeguroTd
    {
        public GatPagSeguroDto SelectGatPagSeguro(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            GatPagSeguroDto gatPagSeguroDto = new GatPagSeguroDao().SelectGatPagSeguro(loj_id);

            return gatPagSeguroDto;
        }
    }
}
