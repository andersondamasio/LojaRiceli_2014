using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.StatusX
{
    internal class StatusDao : Repositorio<Status>
    {

        public Status SelectFirstStatus(int loj_id)
        {
            return Select().FirstOrDefault(s => s.stat_ativar && s.loj_id == loj_id);
        }
    }
}
