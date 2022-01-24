using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.PedidoX;
using _2_Library.Modelo;

namespace _2_Library.Dao.StatusX
{
    public class StatusTd
    {
        public void InsertStatus(string loj_dominio, StatusDto statusDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            Status status = new Status();
            status.stat_nome = statusDto.stat_nome;
            status.stat_descricao = statusDto.stat_descricao;
            status.stat_ativar = statusDto.stat_ativar;
            status.stat_bloquear = statusDto.stat_bloquear;

            using (StatusDao statusDao = new StatusDao())
            {
                statusDao.Add(status);
            }
        }

        public Status SelectFirstStatus(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            Status status;

            using (StatusDao statusDao = new StatusDao())
            {
                status = statusDao.SelectFirstStatus(loj_id);
            }

            return status;
        }
    }
}
