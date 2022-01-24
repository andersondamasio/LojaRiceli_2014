using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Grupox
{
    public class GrupoDao
    {
        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

        #region Lista os grupos da página inicial em forma de xml
        public string SelecionarProdutoVitrineInicial()
        {
            DataSet ds = new DataSet("GRUPOS");

            LojaEntities lojaEntities = new LojaEntities();

            var result = (from grupo in lojaEntities.Grupo
                          where
                          (grupo.loj_id == loj_id && grupo.gru_bloquear == false &&
                          grupo.gru_subBloquear == false) ||
                          (grupo.loj_id == loj_id &&  !grupo.gru_pai.HasValue)
                          select new
                          {
                              grupo.gru_id,
                              grupo.gru_nome,
                              grupo.gru_nomeAmigavel,
                              grupo.gru_pai,
                              gruProQuantidade = grupo.Produto_Grupo.Where(s => s.Produto.pro_bloquear == false && (s.Produto.ProdutoSku.Where(s2 => s2.pro_id == s.Produto.pro_id && s2.proSku_bloquear != true && (s2.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0)).Count() 
                           }).OrderBy(x => x.gru_id).ToList();

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
        #endregion

        public string SelecionarProdutoVitrineGrupo(int gru_id)
        {
            DataSet ds = new DataSet("GRUPOS");

            LojaEntities lojaEntities = new LojaEntities();

            var result = (from grupo in lojaEntities.Grupo
                          where 
                          (grupo.loj_id == loj_id && grupo.gru_bloquear == false && grupo.gru_subBloquear == false) ||
                          (grupo.loj_id == loj_id &&  !grupo.gru_pai.HasValue)
                          select new
                          {
                              grupo.gru_id,
                              grupo.gru_nome,
                              grupo.gru_nomeAmigavel,
                              grupo.gru_pai,
                              gruProQuantidade = grupo.Produto_Grupo.Where(s => s.Produto.pro_bloquear == false && (s.Produto.ProdutoSku.Where(s2 => s2.pro_id == s.Produto.pro_id && s2.proSku_bloquear == false && (s2.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0)).Count() 
                          }).OrderBy(x => x.gru_id).ToList();

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

        public Grupo SelecionarGrupo()
        {
            LojaEntities lojaEntities = new LojaEntities();

            Grupo grupo = (from gru in lojaEntities.Grupo
                                       where gru.loj_id == loj_id
                                       select gru).FirstOrDefault();
            return grupo;
        }
    }
}