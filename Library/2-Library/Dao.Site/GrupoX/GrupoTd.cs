using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.Site.GrupoX
{
    public class GrupoTd
    {

        /// <summary>
        /// Seleciona os grupos
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <returns></returns>
        public List<Grupo> SelectGrupoInicial(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            var produto_GrupoTd = new GrupoDao().SelectGrupoInicial(loj_id);

            return produto_GrupoTd.ToList();
        }

        /// <summary>
        /// Seleciona os grupos da página inicial em forma de xml
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <returns></returns>
        public string SelectGrupoCabecalho(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            DataSet ds = new DataSet("GRUPOS");

            var produto_GrupoTd = new GrupoDao().SelectGrupoInicial(loj_id);

            var result = (from grupo in produto_GrupoTd
                          select new
                          {
                              grupo.gru_id,
                              grupo.gru_nome,
                              grupo.gru_nomeAmigavel,
                              grupo.gru_pai,
                              grupo.gru_posicao,
                          }).OrderBy(x => x.gru_id).OrderBy(s => s.gru_posicao).ToList();

            //Obtem um DataTable preenchido com a tabela que quero que seja exibida (Exemplo: Menu)
            var dataTable = result.ToDataTable();
            dataTable.TableName = "Grupo";

            //Cria um DataSet adicionando a tabela e definindo o auto-relacionamento;

            ds.Tables.Add(dataTable);
            DataRelation relation = new DataRelation("ParentChild",
               ds.Tables["Grupo"].Columns["gru_id"],
               ds.Tables["Grupo"].Columns["gru_pai"], true);

            //Faz com que o xml seja gerado considerando a estrutura do auto-relacionamento

            //Exemplo MenuFilho dentro do Menu Pai

            relation.Nested = true;
            ds.Relations.Add(relation);

            return ds.GetXml();
        }

        /// <summary>
        /// Seleciona o caminho completo até um determinado grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="gru_nomeAmigavel"></param>
        /// <returns></returns>
        public List<GrupoDto> SelectGrupoCaminho(string loj_dominio, string gru_nomeAmigavel)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            var grupoTd = new GrupoDao().SelectGrupoCaminho(loj_id, gru_nomeAmigavel);

            List<GrupoDto> caminhoGrus = ExtrairCaminhoGrupo(grupoTd);
            caminhoGrus.Reverse();
            return caminhoGrus;
        }

        /// <summary>
        /// Seleciona o caminho completo até um determinado grupo
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="proSku_id"></param>
        /// <returns></returns>
        public List<GrupoDto> SelectGrupoCaminho(string loj_dominio, int proSku_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            var grupoTd = new GrupoDao().SelectGrupoCaminho(loj_id, proSku_id);

            List<GrupoDto> caminhoGrus = ExtrairCaminhoGrupo(grupoTd);
            caminhoGrus.Reverse();
            return caminhoGrus;
        }


        List<GrupoDto> caminhoGrupo = new List<GrupoDto>();
        private List<GrupoDto> ExtrairCaminhoGrupo(Grupo grupo)
        {
            if (grupo.gru_nome != null && grupo.gru_nome != "GRUPOS")
            {
                caminhoGrupo.Add(new GrupoDto { gru_id = grupo.gru_id, gru_nome = grupo.gru_nome, gru_nomeAmigavel = grupo.gru_nomeAmigavel });
                ExtrairCaminhoGrupo(grupo.Grupo2);
            }
            return caminhoGrupo;
        }
        
    }
}
