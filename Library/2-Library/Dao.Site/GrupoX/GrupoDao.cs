using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;
using EntityFramework.Extensions;


namespace _2_Library.Dao.Site.GrupoX
{

    internal class GrupoDao : Repositorio<Grupo>
    {
        internal IEnumerable<Grupo> SelectGrupoInicial(int loj_id)
        {
            IEnumerable<Grupo> grupo = (from gru in Select()
                                       where (gru.loj_id == loj_id) &&
                                              (gru.gru_bloquear == false &&
                                              gru.gru_subBloquear == false)
                                              orderby gru.gru_id
                                       select gru); /*new GrupoDto
                                                  {
                                                      gru_id = proGru.gru_id,
                                                      gru_pai = proGru.gru_id,
                                                      gru_nome = proGru.Grupo.gru_nome,
                                                      gru_nomeAmigavel = proGru.Grupo.gru_nomeAmigavel,
                                                      gru_descricao = proGru.Grupo.gru_descricao,
                                                      gru_posicao = proGru.Grupo.gru_posicao
                                                  });*/

            return grupo;
        }

        /// <summary>
        /// Seleciona um grupo especifico
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public Grupo SelectByGrupo(int loj_id, string gru_nomeAmigavel)
        {
            Grupo grupo = (from gru in Select()
                                       where (gru.loj_id == loj_id &&
                                              gru.gru_bloquear == false &&
                                              gru.gru_subBloquear == false &&
                                              gru.gru_nomeAmigavel == gru_nomeAmigavel)
                           select gru).FirstOrDefault();



            return grupo;

        }

        /// <summary>
        /// Seleciona o caminho completo até um determinado grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public Grupo SelectGrupoCaminho(int loj_id, string gru_nomeAmigavel)
        {
            Grupo grupo = (from gru in Select()
                           where
                           (gru.gru_nomeAmigavel == gru_nomeAmigavel &&
                           gru.loj_id == loj_id &&
                           gru.gru_bloquear == false &&
                           gru.gru_subBloquear == false)
                           select gru).FirstOrDefault();

            return grupo;
        }

        /// <summary>
        /// Seleciona o caminho completo até um determinado grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="proSku_id"></param>
        /// <returns></returns>
        public Grupo SelectGrupoCaminho(int loj_id, int proSku_id)
        {
            Grupo grupo = (from gru in Select()
                           where
                           (gru.Produto_Grupo.Where(g => g.Produto.ProdutoSku.Select(s => s.proSku_id).Contains(proSku_id)).FirstOrDefault().Produto.ProdutoSku.Select(s => s.proSku_id).Contains(proSku_id) &&
                           gru.loj_id == loj_id &&
                           gru.gru_bloquear == false &&
                           gru.gru_subBloquear == false)
                           select gru).FirstOrDefault();

            return grupo;
        }

        /// <summary>
        /// Seleciona os nos finais de cada grupo
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public List<int?> SelectNosFinais(int loj_id, string gru_nomeAmigavel)
        {
            LojaEntities lojaEntities = new LojaEntities();

            List<int?> grupos = lojaEntities.gru_SelecionarNosFinais(gru_nomeAmigavel, loj_id).ToList();

            return grupos;
        }

    }
}
