using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;
using EntityFramework.Extensions;

namespace _2_Library.Dao.LojaX
{
    internal class LojaDao : Repositorio<LojaCon>
    {
        public List<LojaDto> SelectLoja()
        {
            List<LojaDto> loja = (from loj in Select()
                            select new LojaDto
                            {
                                loj_id = loj.loj_id,
                                loj_cep = loj.loj_cep,
                                loj_email = loj.loj_email,
                                loj_dominio = loj.loj_dominio,
                                loj_subdominio = loj.loj_subdominio,
                                loj_bloquear = loj.loj_bloquear
                            }).ToList();

            return loja;
        }

        public LojaDto SelectLoja(string loj_dominio)
        {
            LojaDto loja = (from loj in Select()
                        where loj.loj_dominio == loj_dominio
                        select new LojaDto { 
                        loj_id = loj.loj_id,
                        loj_cep = loj.loj_cep,
                        loj_email = loj.loj_email,
                        loj_dominio = loj.loj_dominio,
                        loj_subdominio = loj.loj_subdominio,
                        loj_bloquear = loj.loj_bloquear
                        }).FirstOrDefault();

            return loja;
        }

        public LojaDto SelectLoja(int loj_id)
        {
            LojaDto loja = (from loj in Select()
                            where loj.loj_id == loj_id
                            select new LojaDto
                            {
                                loj_id = loj.loj_id,
                                loj_cep = loj.loj_cep,
                                loj_email = loj.loj_email,
                                loj_dominio = loj.loj_dominio,
                                loj_subdominio = loj.loj_subdominio,
                                loj_bloquear = loj.loj_bloquear
                            }).FirstOrDefault();

            return loja;
        }
    }
}
