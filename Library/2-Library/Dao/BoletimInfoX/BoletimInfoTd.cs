using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace _2_Library.Dao.BoletimInfoX
{
    public class BoletimInfoTd
    {
        public void InsertBoletimInfo(string loj_dominio, BoletimInfoDto boletimInfoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            BoletimInfo boletimInfo = new BoletimInfo();
            boletimInfo.bo_email = boletimInfoDto.bo_email;
            boletimInfo.bo_sexo = boletimInfoDto.bo_sexo;
            boletimInfo.loj_id = loj_id;

            if (boletimInfoDto.cli_id.HasValue)
                boletimInfo.cli_id = boletimInfoDto.cli_id;

            new BoletimInfoDao().InsertBoletimInfo(boletimInfo);

        }
    }
}
