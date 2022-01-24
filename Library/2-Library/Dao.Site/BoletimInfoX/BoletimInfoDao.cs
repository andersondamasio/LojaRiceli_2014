using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.BoletimInfoX
{
    internal class BoletimInfoDao : Repositorio<BoletimInfo>
    {

        public void InsertBoletimInfo(BoletimInfo boletimInfo)
        {
            if (Select().Where(s => s.bo_email == boletimInfo.bo_email && s.loj_id == boletimInfo.loj_id).Count() == 0)
                Add(boletimInfo);
        }

    }
}
