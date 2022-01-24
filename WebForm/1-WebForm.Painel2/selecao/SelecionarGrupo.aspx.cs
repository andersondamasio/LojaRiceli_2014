using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Loja.Modelo;
using Loja.Utils;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace Loja.Painel.selecao
{
    public partial class SelecionarGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void AlimentaGrupo()
        {
            LojaEntities lojaEntities = new LojaEntities();
            Int16 loja_id = Convert.ToInt16(Session["loj_id"]);

        /*    var teste = lojaEntities.Produto.Where(s => s.Grupo.loj_id == 2);

            System.Data.Objects.ObjectQuery oQuery = (System.Data.Objects.ObjectQuery)teste;
            string cmdSQL = oQuery.ToTraceString();
            */

            var result = (from grupo in lojaEntities.Grupo
                          where grupo.loj_id == loja_id || grupo.gru_id == 0
                          orderby grupo.gru_id
                          select new
                          {
                              grupo.gru_id,
                              gru_nome = grupo.gru_nome,
                              gru_pai = ((int?)grupo.gru_id == 0 ? null : (int?)grupo.gru_pai),
                              grupo.gru_posicao
                          });

            //Obtem um DataTable preenchido com a tabela que quero que seja exibida (Exemplo: Menu)

            var dataTable = result.ToDataTable();
            dataTable.TableName = "Grupo";

            //Cria um DataSet adicionando a tabela e definindo o auto-relacionamento;

            DataSet ds = new DataSet("GRUPOS");
            ds.Tables.Add(dataTable);
            DataRelation relation = new DataRelation("ParentChild",
               ds.Tables["Grupo"].Columns["gru_id"],
               ds.Tables["Grupo"].Columns["gru_pai"], true);

            //Faz com que o xml seja gerado considerando a estrutura do auto-relacionamento

            //Exemplo MenuFilho dentro do Menu Pai

            relation.Nested = true;
            ds.Relations.Add(relation);

            ds.WriteXml(Request.PhysicalApplicationPath + @"\Cache\grupoPainel.xml");
        }

        protected void TreeViewGrupo_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeViewGrupo.SelectedNode.ChildNodes.Count == 0)
            {

                var valorText = TreeViewGrupo.SelectedNode.Text;
                var valorHidden =  TreeViewGrupo.SelectedValue;
                string Script =
                string.Format(
                "parent.SetaValorRegistro('{0}','{1}','{2}','{3}');",
                this.Request.QueryString["controleDestinoText"],
                this.Request.QueryString["controleDestinoHidden"],
                valorText,
                valorHidden
                );
                this.ClientScript.RegisterStartupScript(typeof(SelecionarGrupo), "fechar", "<script>" + Script + "</script>"); }
            else
                Validacao.Alert(Loja.Modelo.Static.MensagemSistema(7).menSis_mensagem);

        }
        /*
        protected override void SavePageStateToPersistenceMedium(object state)
        {
            Session[Request.RawUrl] = state;
        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            return Session[Request.RawUrl];
        }
*/
    }
}