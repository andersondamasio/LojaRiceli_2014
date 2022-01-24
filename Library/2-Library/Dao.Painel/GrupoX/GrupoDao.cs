using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.GrupoX
{
    internal class GrupoDao : Repositorio<Grupo>
    {
        public List<GrupoDto> SelectGrupo(int loj_id)
        {
            List<GrupoDto> grupoDto = (from gru in Select()
                                       where
                                       gru.loj_id == loj_id
                                       select new GrupoDto
                                        {
                                            gru_id = gru.gru_id,
                                            gru_pai = gru.gru_pai,
                                            gru_nome = gru.gru_nome,
                                            //gru_nomeAmigavel = gru.gru_nomeAmigavel,
                                            gru_descricao = gru.gru_descricao,
                                            /*gru_posicao = gru.gru_posicao,
                                            gru_bloquear = gru.gru_bloquear,
                                            gru_subBloquear = gru.gru_subBloquear,
                                            gru_dataHoraAtualizacao = gru.gru_dataHoraAtualizacao,
                                            gru_dataHora = gru.gru_dataHora,
                                            loj_id = gru.loj_id,*/
                                           /* grupo1Dto = gru.Grupo1.Select(s => new GrupoDto
                                            {
                                                gru_id = gru.gru_id,
                                                gru_pai = gru.gru_pai,
                                                gru_nome = gru.gru_nome,
                                            }),
                                            grupo2Dto = new GrupoDto()
                                            {
                                                gru_id = gru.gru_id,
                                                gru_pai = gru.gru_pai,
                                                gru_nome = gru.gru_nome,
                                            }*/
                                        }).ToList();
            return grupoDto;
        }
    }
}
