using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Loja.Utils;
using Loja.Modelo;
using System.IO;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.Lojax;
using _2_Library.Modelo;


namespace Loja.Painel
{
    public partial class CadastroGrupo : System.Web.UI.Page
    {

        private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;

        protected void Page_Load(object sender, EventArgs e)
        {

           Login.ValidaLogin();

            if (TreeViewGrupo.Nodes.Count == 0)
                AlimentaGrupo();

            if (TreeViewGrupo.SelectedNode != null && TreeViewGrupo.SelectedNode.Depth == 0){
                TreeViewGrupo_SelectedNodeChanged(sender, e);

                InicializaObjetos();
            }    
        }

        #region Controle de Grupos

        protected void ButtonAlterar_Click(object sender, EventArgs e)
        {
            Int32 gru_id = Convert.ToInt32(TreeViewGrupo.SelectedValue);
            Grupo grupo = new Consulta().SelecionarGrupo().Where(s => s.gru_id == gru_id).SingleOrDefault();
            gru_nome1TextBox.Text = grupo.gru_nome;
            gru_descricaoTextBox.Text = grupo.gru_descricao;
            gru_posicaoTextBox.Text = grupo.gru_posicao.ToString();
            gru_bloquearCheckBox.Checked = grupo.gru_bloquear;

            HiddenFieldTipoCadastro.Value = "1";
            ControlaObjetos(PanelCadastrarGrupo);

            ButtonAlterar.Enabled = false;
            ButtonIncluir.Enabled = true;
        }

        protected void ButtonSalvarGrupo_Click(object sender, EventArgs e)
        {
            Retorno retorno = new Retorno();

            int gru_id = Convert.ToInt32(TreeViewGrupo.SelectedValue);
            if (HiddenFieldTipoCadastro.Value == "0")
            {
                Grupo grupo = new Grupo();
                grupo.loj_id = loj_id;
                grupo.gru_pai = gru_id;
                grupo.gru_nome = gru_nome1TextBox.Text.Trim();
                grupo.gru_descricao = gru_descricaoTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(gru_posicaoTextBox.Text.Trim()))
                    grupo.gru_posicao = Convert.ToInt32(gru_posicaoTextBox.Text.Trim());

                grupo.gru_bloquear = gru_bloquearCheckBox.Checked;

                retorno = new Consulta().InserirGrupo(grupo);

                if (retorno.menSis_id == 0)
                    HiddenFieldTipoCadastro.Value = "1";
            }
            else
            {
                Grupo grupo = new Grupo();
                grupo.loj_id = loj_id;
                grupo.gru_id = Convert.ToInt32(TreeViewGrupo.SelectedValue);
                grupo.gru_nome = gru_nome1TextBox.Text.Trim();
                grupo.gru_descricao = gru_descricaoTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(gru_posicaoTextBox.Text.Trim()))
                    grupo.gru_posicao = Convert.ToInt32(gru_posicaoTextBox.Text.Trim());

                grupo.gru_bloquear = gru_bloquearCheckBox.Checked;
                retorno = new Consulta().AtualizarGrupo(grupo);
            }

            if (retorno.menSis_id == 0)
            {
                AlimentaGrupo();
                TreeViewGrupo.DataBind();
                Buscar((retorno.id_registro != 0 ? retorno.id_registro : gru_id).ToString(), TreeViewGrupo.Nodes);
            }
            else
            {
                Validacao.Alert(Page, retorno.menSis_mensagem);
            }
            ControlaObjetos(TreeViewGrupo);
        }

        protected void ButtonIncluir_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            gru_nome1TextBox.Text = string.Empty;
            gru_descricaoTextBox.Text = string.Empty;
            gru_posicaoTextBox.Text = string.Empty;
            gru_bloquearCheckBox.Checked = false;

            ControlaObjetos(PanelCadastrarGrupo);

            ButtonAlterar.Enabled = true;
            ButtonIncluir.Enabled = false;
        }

        protected void ButtonExcluir_Click(object sender, EventArgs e)
        {
            Retorno retorno = new Consulta().ExcluirGrupo(Convert.ToInt32(TreeViewGrupo.SelectedNode.Value));

            if (retorno.menSis_id == 0)
            {
                var parentGrupo = TreeViewGrupo.SelectedNode.Parent.Value;

                AlimentaGrupo();
                TreeViewGrupo.DataBind();
                Buscar(parentGrupo, TreeViewGrupo.Nodes);
                ControlaObjetos(TreeViewGrupo);
            }
            else
            {
                Validacao.Alert(Page, retorno.menSis_mensagem);
            }
        }

        protected void TreeViewGrupo_SelectedNodeChanged(object sender, EventArgs e)
        {
            ControlaObjetos(TreeViewGrupo);
        }

        public void AlimentaGrupo()
        {
            LojaEntities lojaEntities = new LojaEntities();
           
            var result = (from grupo in lojaEntities.Grupo
                          where (grupo.loj_id == loj_id) ||
                          (grupo.loj_id == loj_id && !grupo.gru_pai.HasValue)
                          select new
                          {
                              grupo.gru_id,
                              gru_nome = grupo.gru_nome,
                              gru_pai = grupo.gru_pai,
                              grupo.gru_bloquear,
                              grupo.gru_subBloquear,
                              //gru_quantidadeProduto = grupo.Produto.Count,
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

        private void Buscar(string valorProcurado, TreeNodeCollection treeview)
        {
            int i = 0;
            foreach (TreeNode tre in treeview)
            {
                //utilizando a recursividade
                this.Buscar(valorProcurado, treeview[i].ChildNodes);
                //faz a busca por trecho de texto
                if (tre.Value.Equals(valorProcurado))
                {
                    if (tre.Parent == null) //se não tiver parent (caso do 1º nivel)
                        tre.Expand(); //expande o nivel atual 
                    else
                        tre.Parent.Expand(); //expande o nivel anterior 

                    TreeViewGrupo.CollapseAll();
                    ExpandNodes(tre.ValuePath);

                    break;
                }
                i++;
            }
        }

        private void ExpandNodes(string caminho)
        {
            string[] tmp = caminho.Split('/');
            string tmpValuePath = string.Empty;
            for (int i = 0; i < tmp.Length; i++)
            {
                if (i == 0)
                    tmpValuePath = tmp[i];
                else
                    tmpValuePath += "/" + tmp[i];

                TreeNodeEventArgs e = new TreeNodeEventArgs(TreeViewGrupo.FindNode(tmpValuePath));
                TreeViewGrupo.FindNode(tmpValuePath).Expand();
                TreeViewGrupo.FindNode(tmpValuePath).Selected = true;
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            TreeViewGrupo_SelectedNodeChanged(sender, e);
        }
        #endregion

        #region Controle de Produtos
        protected void ButtonIncluirProduto_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            ControlaObjetos(PanelCadastrarProduto);
            PanelListarProduto.Visible = true;
            ListViewProduto.DataBind();

            pro_nomeTextBox.Text = string.Empty;
            mar_idHiddenField.Value = string.Empty;
            mar_nomeTextBox.Text = string.Empty;
            pro_descricaoTextBox.Text = string.Empty;
            pro_posicaoTextBox.Text = string.Empty;
            pro_paginaInicialDataDeTextBox.Text = string.Empty;
            pro_paginaInicialDataAteTextBox.Text = string.Empty;
            pro_paginaInicialHoraDeTextBox.Text = string.Empty;
            pro_paginaInicialHoraAteTextBox.Text = string.Empty;
            pro_bloquearCheckBox.Checked = false;
            ButtonIncluirProduto.Enabled = false;
            gru_idHiddenField.Value = TreeViewGrupo.SelectedValue;
            gru_nomeTextBox.Text = TreeViewGrupo.SelectedNode.Text;
            ButtonAdicionarProdutoGrupo.Visible = false;
            LinkButtonSelecionarGrupo.Visible = false;
            RepeaterProdutoGrupo.Visible = false;

            LinkButtonSelecionarMarca.Attributes["onclick"] =
                   string.Format("SelecionarRegistro('selecao/SelecionarMarca.aspx','{0}','{1}');", mar_nomeTextBox.ClientID, mar_idHiddenField.ClientID);

        }
        #endregion

        protected void ButtonCancelarProduto_Click(object sender, EventArgs e)
        {
            ButtonListarProduto_Click(sender, e);
        }

        private void AbilitaBotoesAcao(Button buttonExcecao)
        {
            ButtonAlterar.Enabled = (buttonExcecao.ID != ButtonAlterar.ID);
            ButtonIncluir.Enabled = (buttonExcecao.ID != ButtonIncluir.ID);
            ButtonExcluir.Enabled = (buttonExcecao.ID != ButtonExcluir.ID);
            ButtonIncluirProduto.Enabled = (buttonExcecao.ID != ButtonIncluirProduto.ID);
            ButtonListarProduto.Enabled = (buttonExcecao.ID != ButtonListarProduto.ID);
        }

        private void ControlaObjetos(Control control)
        {
            PanelCadastrarGrupo.Visible = false;
            PanelListarProdutoSku.Visible = false;
            PanelListarProdutoInfo.Visible = false;
            PanelListarProdutoInfoItem.Visible = false;
            PanelCadastrarProdutoInfoItem.Visible = false;
            PanelCadastrarProdutoSku.Visible = false;
            PanelListarProdutoSkuFoto.Visible = false;
            ListViewProduto.SelectedIndex = -1;
            PanelBotoes.Enabled = true;

            if (TreeViewGrupo.SelectedNode.Depth == 0)
            {
                ButtonAlterar.Enabled = false;
                ButtonExcluir.Enabled = false;
                ButtonIncluir.Enabled = true;
                ButtonIncluirProduto.Enabled = false;
                ButtonListarProduto.Enabled = false;
            }
            else
            {
                ButtonAlterar.Enabled = true;
                ButtonIncluir.Enabled = true;
                ButtonExcluir.Enabled = TreeViewGrupo.SelectedNode.ChildNodes.Count == 0;
                ButtonIncluirProduto.Enabled = TreeViewGrupo.SelectedNode.ChildNodes.Count == 0;
                ButtonListarProduto.Enabled = ButtonIncluirProduto.Enabled;
            }

            PanelCadastrarGrupo.Visible = (control.ID == PanelCadastrarGrupo.ID);
            PanelListarProduto.Visible = (control.ID == PanelListarProduto.ID);
            PanelCadastrarProduto.Visible = (control.ID == PanelCadastrarProduto.ID);
        }

        private void InicializaObjetos()
        {
            if (ButtonAlterar.Enabled)
                Recursos.DesabilitarDuploSemValidarClick(ButtonAlterar, "Carregando...");
            if (ButtonExcluir.Enabled)
                Recursos.DesabilitarDuploSemValidarClick(ButtonExcluir, "Carregando...");
            if (ButtonIncluir.Enabled)
                Recursos.DesabilitarDuploSemValidarClick(ButtonIncluir, "Carregando...");
            if (ButtonIncluirProduto.Enabled)
                Recursos.DesabilitarDuploSemValidarClick(ButtonIncluirProduto, "Carregando...");
            if (ButtonListarProduto.Enabled)
                Recursos.DesabilitarDuploSemValidarClick(ButtonListarProduto, "Carregando...");
        }

        protected void ButtonListarProduto_Click(object sender, EventArgs e)
        {
            ControlaObjetos(PanelListarProduto);
            ButtonListarProduto.Enabled = false;
            ListViewProduto.DataBind();
        }

        protected void ButtonSalvarProduto_Click(object sender, EventArgs e)
        {
            if (PanelCadastrarProduto.Visible)
            {
                if (!string.IsNullOrEmpty(pro_paginaInicialDataDeTextBox.Text))
                {
                    if (!Validacao.ValidaData(pro_paginaInicialDataDeTextBox.Text))
                    {
                        Validacao.Alert(Page, "Data da 'página inicial de' inválida");
                        return;
                    }
                    if (!Validacao.ValidaHora(pro_paginaInicialHoraDeTextBox.Text))
                    {
                        Validacao.Alert(Page, "Hora 'página inicial de' inválida ou não informada");
                        return;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(pro_paginaInicialDataAteTextBox.Text))
                    {
                        if (!Validacao.ValidaData(pro_paginaInicialDataAteTextBox.Text))
                        {
                            Validacao.Alert(Page, "Data da 'página inicial até' inválida");
                            return;
                        }
                        if (!Validacao.ValidaData(pro_paginaInicialHoraAteTextBox.Text))
                        {
                            Validacao.Alert(Page, "Hora 'página inicial até' inválida ou não informada");
                            return;
                        }
                    }

                Int32 gru_id = Convert.ToInt32(TreeViewGrupo.SelectedValue);

                Produto produto = new Produto();
                produto.pro_nome = pro_nomeTextBox.Text.Trim();
                produto.mar_id = Convert.ToInt32(mar_idHiddenField.Value);
                produto.pro_descricao = pro_descricaoTextBox.Text.Trim();
                produto.pro_posicao = Convert.ToInt32(pro_posicaoTextBox.Text);
                produto.pro_bloquear = pro_bloquearCheckBox.Checked;

                if (!string.IsNullOrEmpty(pro_paginaInicialDataDeTextBox.Text))
                {
                    produto.pro_paginaInicialDe = Convert.ToDateTime(pro_paginaInicialDataDeTextBox.Text);
                    if (!string.IsNullOrEmpty(pro_paginaInicialHoraDeTextBox.Text))
                        produto.pro_paginaInicialDe = Convert.ToDateTime(produto.pro_paginaInicialDe.Value.ToShortDateString() + " " + pro_paginaInicialHoraDeTextBox.Text.Trim());
                }
                if (!string.IsNullOrEmpty(pro_paginaInicialDataAteTextBox.Text))
                {
                    produto.pro_paginaInicialAte = Convert.ToDateTime(pro_paginaInicialDataAteTextBox.Text);
                    if (!string.IsNullOrEmpty(pro_paginaInicialHoraAteTextBox.Text))
                        produto.pro_paginaInicialAte = Convert.ToDateTime(produto.pro_paginaInicialAte.Value.ToShortDateString() + " " + pro_paginaInicialHoraAteTextBox.Text.Trim());
                }

                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    produto.loj_id = loj_id;

                    Produto_Grupo produtoGrupo = new Produto_Grupo();
                    produtoGrupo.loj_id = loj_id;
                    produtoGrupo.gru_id = Convert.ToInt32(gru_idHiddenField.Value);
                    produtoGrupo.proGru_dataHora = DateTime.Now;
                    produto.Produto_Grupo.Add(produtoGrupo);

                    new Consulta().InserirProduto(produto);
                    Buscar(gru_id.ToString(), TreeViewGrupo.Nodes);
                    ButtonListarProduto_Click(sender, e);
                }
                else
                {
                    produto.pro_id = Convert.ToInt32(pro_idHiddenField.Value);

                    Retorno retorno = new Consulta().AtualizarProduto(produto);
                    Produto pro = (Produto)retorno.objeto;

                    if (retorno.menSis_id == 0)
                    {
                        if (pro.Produto_Grupo.Any(s => s.gru_id == gru_id))
                            Buscar(gru_id.ToString(), TreeViewGrupo.Nodes);
                        else
                        {
                            Buscar(pro.Produto_Grupo.FirstOrDefault().gru_id.ToString(), TreeViewGrupo.Nodes);
                        }
                        ButtonListarProduto_Click(sender, e);
                    }
                    else
                    {
                        Validacao.Alert(Page, retorno.menSis_mensagem);
                    }
                }
            }
        }

        protected void ListViewProduto_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandSource is Button)
            {
                Button button = ((Button)e.CommandSource);
                if (button.ID == "ButtonAlterarProduto")
                {
                    Int32 pro_id = Convert.ToInt32(button.CommandArgument);
                    HiddenFieldTipoCadastro.Value = "1";
                    Produto produto = new Consulta().SelecionarProduto(pro_id).FirstOrDefault();

                    pro_idHiddenField.Value = pro_id.ToString();
                    pro_nomeTextBox.Text = produto.pro_nome;
                    gru_nomeTextBox.Text = string.Empty;
                    gru_idHiddenField.Value = TreeViewGrupo.SelectedValue;
                    mar_idHiddenField.Value = produto.mar_id.ToString();
                    mar_nomeTextBox.Text = produto.Marca.mar_nome;

                    pro_descricaoTextBox.Text = produto.pro_descricao;
                    pro_posicaoTextBox.Text = produto.pro_posicao.ToString();
                    pro_paginaInicialDataDeTextBox.Text = produto.pro_paginaInicialDe.HasValue ? produto.pro_paginaInicialDe.Value.ToShortDateString() : string.Empty;
                    pro_paginaInicialDataAteTextBox.Text = produto.pro_paginaInicialAte.HasValue ? produto.pro_paginaInicialAte.Value.ToShortDateString() : string.Empty;

                    pro_paginaInicialHoraDeTextBox.Text = produto.pro_paginaInicialDe.HasValue ? produto.pro_paginaInicialDe.Value.ToLongTimeString() : string.Empty;
                    pro_paginaInicialHoraAteTextBox.Text = produto.pro_paginaInicialAte.HasValue ? produto.pro_paginaInicialAte.Value.ToLongTimeString() : string.Empty;
                    pro_bloquearCheckBox.Checked = produto.pro_bloquear;

                    PanelCadastrarProduto.Visible = true;
                    PanelListarProdutoSku.Visible = false;
                    PanelCadastrarProdutoSku.Visible = false;
                    PanelListarProdutoSkuFoto.Visible = false;
                    PanelListarProdutoInfo.Visible = false;
                    PanelListarProdutoInfoItem.Visible = false;
                    PanelCadastrarProdutoInfoItem.Visible = false;
                    LinkButtonSelecionarGrupo.Visible = true;
                    ButtonAdicionarProdutoGrupo.Visible = true;
                    RepeaterProdutoGrupo.Visible = true;

                    RepeaterProdutoGrupo.DataSource = new Consulta().SelecionarProdutoGrupo(pro_id).Select(s => new { s.gru_id, s.Grupo.gru_nome }).ToList();
                    RepeaterProdutoGrupo.DataBind();

                    LinkButtonSelecionarGrupo.Attributes["onclick"] =
                    string.Format("SelecionarRegistro('selecao/SelecionarGrupo.aspx','{0}','{1}');", gru_nomeTextBox.ClientID, gru_idHiddenField.ClientID);

                    LinkButtonSelecionarMarca.Attributes["onclick"] =
                    string.Format("SelecionarRegistro('selecao/SelecionarMarca.aspx','{0}','{1}');", mar_nomeTextBox.ClientID, mar_idHiddenField.ClientID);
                }
                else
                {
                    if (button.ID == "ButtonListarProdutoSku")
                    {
                        PanelCadastrarProduto.Visible = false;
                        PanelListarProdutoSku.Visible = true;
                        PanelCadastrarProdutoSku.Visible = false;
                        PanelListarProdutoSkuFoto.Visible = false;
                        ListViewProdutoSku.SelectedIndex = -1;
                        PanelListarProdutoInfo.Visible = false;
                        PanelListarProdutoInfoItem.Visible = false;
                        PanelCadastrarProdutoInfoItem.Visible = false;
                        ListViewProdutoSku.DataBind();
                    }
                    else
                    {
                        if (button.ID == "ButtonListarProdutoInfo")
                        {
                            PanelCadastrarProduto.Visible = false;
                            PanelListarProdutoSku.Visible = false;
                            PanelListarProdutoInfo.Visible = true;
                            PanelCadastrarProdutoSku.Visible = false;
                            PanelListarProdutoSkuFoto.Visible = false;
                            PanelCadastrarProdutoInfo.Visible = false;
                            PanelListarProdutoInfoItem.Visible = false;
                            PanelCadastrarProdutoInfoItem.Visible = false;
                            ListViewProdutoInfo.SelectedIndex = -1;

                            ButtonExcluirProdutoInfo.Enabled = false;
                            ButtonAlterarProdutoInfo.Enabled = false;
                        }
                        else
                        {
                            if (button.ID == "ButtonDuplicarProduto")
                            {
                              Int32 pro_id = Convert.ToInt32(button.CommandArgument);
                              Produto produto = DuplicarProduto(pro_id);
                              Retorno retorno = new Consulta().InserirProduto(produto);

                              if (retorno.menSis_id == 0)
                              {
                                  String caminhoDiretorioOrigem = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + pro_id + "\\";
                                  String caminhoDiretorioDestino = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + retorno.id_registro + "\\";
                                  Recursos.CopiarPasta(caminhoDiretorioOrigem, caminhoDiretorioDestino);

                                  PanelCadastrarProduto.Visible = false;
                                  PanelListarProdutoSku.Visible = false;
                                  PanelCadastrarProdutoSku.Visible = false;
                                  PanelListarProdutoSkuFoto.Visible = false;
                                  PanelListarProdutoInfo.Visible = false;
                                  PanelListarProdutoInfoItem.Visible = false;
                                  PanelCadastrarProdutoInfoItem.Visible = false;
                                  ListViewProduto.SelectedIndex = -1;
                                  ListViewProduto.DataBind();
                              }else
                              Validacao.Alert(Page, retorno.menSis_mensagem);                           
                            }
                            else
                                if (button.ID == "ButtonExcluirProduto")
                                {
                                    Int32 pro_id = Convert.ToInt32(button.CommandArgument);
                                    Retorno retorno = new Consulta().ExcluirProduto(pro_id);

                                    if (retorno.menSis_id == 0)
                                    {
                                        PanelCadastrarProduto.Visible = false;
                                        PanelListarProdutoSku.Visible = false;
                                        PanelCadastrarProdutoSku.Visible = false;
                                        PanelListarProdutoSkuFoto.Visible = false;
                                        PanelListarProdutoInfo.Visible = false;
                                        PanelListarProdutoInfoItem.Visible = false;
                                        PanelCadastrarProdutoInfoItem.Visible = false;
                                        ListViewProduto.SelectedIndex = -1;
                                        ListViewProduto.DataBind();
                                    }
                                    else
                                    {
                                        Validacao.Alert(Page, retorno.menSis_mensagem);
                                    }
                                }
                        }
                    }
                }
            }
            AbilitaBotoesAcao(ButtonListarProduto);
        }

        protected void ButtonSalvarProdutoSku_Click(object sender, EventArgs e)
        {
            if (PanelCadastrarProdutoSku.Visible)
            {
                ProdutoSku produtoSku = new ProdutoSku();
                produtoSku.pro_id = Convert.ToInt32(ListViewProduto.SelectedValue);

                produtoSku.proSku_nome = proSku_nomeTextBox.Text.Trim();

                produtoSku.proSku_precoAnterior = Convert.ToDecimal(proSku_precoAnteriorTextBox.Text.Trim());
                produtoSku.proSku_precoVenda = Convert.ToDecimal(proSku_precoVendaTextBox.Text.Trim());
                produtoSku.proSku_precoCusto = Convert.ToDecimal(proSku_precoCustoTextBox.Text.Trim());
                produtoSku.proSku_idReferencia = proSku_idReferenciaTextBox.Text.Trim();
                produtoSku.proSku_peso = Convert.ToDecimal(proSku_pesoTextBox.Text.Trim());
                produtoSku.proSku_altura = Convert.ToDecimal(proSku_alturaTextBox.Text.Trim());
                produtoSku.proSku_largura = Convert.ToDecimal(proSku_larguraTextBox.Text.Trim());
                produtoSku.proSku_comprimento = Convert.ToDecimal(proSku_comprimentoTextBox.Text.Trim());

                if (!string.IsNullOrEmpty(proSku_prazoEntregaAdicionalTextBox.Text.Trim()))
                    produtoSku.proSku_prazoEntregaAdicional = Convert.ToInt32(proSku_prazoEntregaAdicionalTextBox.Text.Trim());
                if (!string.IsNullOrEmpty(proSku_quantidadeMaximaTextBox.Text.Trim()))
                    produtoSku.proSku_quantidadeMaxima = Convert.ToInt32(proSku_quantidadeMaximaTextBox.Text.Trim());
                if (!string.IsNullOrEmpty(proSku_quantidadeDisponivelTextBox.Text.Trim()))
                    produtoSku.proSku_quantidadeDisponivel = Convert.ToInt32(proSku_quantidadeDisponivelTextBox.Text.Trim());

                if (!string.IsNullOrEmpty(proSku_parcelamentoDropDownList.SelectedValue))
                    produtoSku.parc_id = Convert.ToInt32(proSku_parcelamentoDropDownList.SelectedValue);
                if (!string.IsNullOrEmpty(proSku_tamanhoDropDownList.SelectedValue))
                    produtoSku.proSkuTam_id = Convert.ToInt32(proSku_tamanhoDropDownList.SelectedValue);
                if (!string.IsNullOrEmpty(proSku_corDropDownList.SelectedValue))
                    produtoSku.proSkuCor_id = Convert.ToInt32(proSku_corDropDownList.SelectedValue);

                produtoSku.proSku_disponivel = proSku_disponivelCheckBox.Checked;
                produtoSku.proSku_bloquear = proSku_bloquearCheckBox.Checked;
                produtoSku.proSku_destaque = proSku_destaqueCheckBox.Checked;
                produtoSku.proSku_posicao = Convert.ToInt32(proSku_posicaoTextBox.Text);

                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    produtoSku.loj_id = loj_id;
                    new Consulta().InserirProdutoSku(produtoSku);
                }
                else
                {
                    produtoSku.proSku_id = Convert.ToInt32(ListViewProdutoSku.SelectedValue);
                    new Consulta().AtualizarProdutoSku(produtoSku);
                }
                ListViewProduto.DataBind();
                ListViewProdutoSku.DataBind();
                ListViewProdutoSku.SelectedIndex = -1;
                PanelCadastrarProdutoSku.Visible = false;
            }
        }

        protected void ButtonIncluirProdutoSku_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            PanelCadastrarProdutoSku.Visible = true;
            PanelListarProdutoSkuFoto.Visible = false;
            ListViewProdutoSku.SelectedIndex = -1;

            proSku_nomeTextBox.Text = String.Empty;
            proSku_precoAnteriorTextBox.Text = String.Empty;
            proSku_precoVendaTextBox.Text = String.Empty;
            proSku_precoCustoTextBox.Text = String.Empty;
            proSku_idReferenciaTextBox.Text = String.Empty;
            proSku_pesoTextBox.Text = String.Empty;
            proSku_alturaTextBox.Text = String.Empty;
            proSku_larguraTextBox.Text = String.Empty;
            proSku_comprimentoTextBox.Text = String.Empty;
            proSku_prazoEntregaAdicionalTextBox.Text = String.Empty;
            proSku_quantidadeMaximaTextBox.Text = String.Empty;
            proSku_quantidadeDisponivelTextBox.Text = String.Empty;
            proSku_tamanhoDropDownList.SelectedValue = string.Empty;
            proSku_corDropDownList.SelectedValue = string.Empty;
            proSku_disponivelCheckBox.Checked = false;
            proSku_bloquearCheckBox.Checked = false;
            proSku_destaqueCheckBox.Checked = false;
            proSku_posicaoTextBox.Text = string.Empty;

            proSku_parcelamentoDropDownList.Items.Clear();
            proSku_parcelamentoDropDownList.Items.Add(new ListItem(string.Empty,string.Empty));
            proSku_parcelamentoDropDownList.DataSource = new ParcelamentoConsulta().SelecionarParcelamento().ToList();

            proSku_tamanhoDropDownList.Items.Clear();
            proSku_tamanhoDropDownList.Items.Add(string.Empty);
            proSku_tamanhoDropDownList.DataSource = new Consulta().SelecionarProdutoSkuTamanho().ToList();

            proSku_corDropDownList.Items.Clear();
            proSku_corDropDownList.Items.Add(string.Empty);
            proSku_corDropDownList.DataSource = new Consulta().SelecionarProdutoSkuCor().ToList();

            proSku_parcelamentoDropDownList.DataBind();
            proSku_tamanhoDropDownList.DataBind();
            proSku_corDropDownList.DataBind();
        }

        protected void ButtonCancelarProdutoSku_Click(object sender, EventArgs e)
        {
            ListViewProdutoSku.SelectedIndex = -1;
            PanelCadastrarProdutoSku.Visible = false;
        }

        protected void ListViewProdutoSku_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Button button = ((Button)e.CommandSource);

            if (button.ID == "ButtonAlterarProdutoSku")
            {
                HiddenFieldTipoCadastro.Value = "1";
                PanelCadastrarProdutoSku.Visible = true;
                PanelListarProdutoSkuFoto.Visible = false;
                ProdutoSku produtoSku = new Consulta().SelecionarProdutoSku(Convert.ToInt32(button.CommandArgument));

                proSku_parcelamentoDropDownList.Items.Clear();
                proSku_parcelamentoDropDownList.Items.Add(new ListItem(string.Empty, string.Empty));
                proSku_parcelamentoDropDownList.DataSource = new ParcelamentoConsulta().SelecionarParcelamento().ToList();

                proSku_tamanhoDropDownList.Items.Clear();
                proSku_tamanhoDropDownList.Items.Add(string.Empty);
                proSku_tamanhoDropDownList.DataSource = new Consulta().SelecionarProdutoSkuTamanho().ToList();

                proSku_corDropDownList.Items.Clear();
                proSku_corDropDownList.Items.Add(string.Empty);
                proSku_corDropDownList.DataSource = new Consulta().SelecionarProdutoSkuCor().ToList();

                proSku_parcelamentoDropDownList.DataBind();
                proSku_tamanhoDropDownList.DataBind();
                proSku_corDropDownList.DataBind();

                proSku_nomeTextBox.Text = produtoSku.proSku_nome;
                proSku_precoAnteriorTextBox.Text = produtoSku.proSku_precoAnterior.ToString();
                proSku_precoVendaTextBox.Text = produtoSku.proSku_precoVenda.ToString();
                proSku_precoCustoTextBox.Text = produtoSku.proSku_precoCusto.ToString();
                proSku_idReferenciaTextBox.Text = produtoSku.proSku_idReferencia;
                proSku_pesoTextBox.Text = produtoSku.proSku_peso.ToString();
                proSku_alturaTextBox.Text = produtoSku.proSku_altura.ToString();
                proSku_larguraTextBox.Text = produtoSku.proSku_largura.ToString();
                proSku_comprimentoTextBox.Text = produtoSku.proSku_comprimento.ToString();
                proSku_prazoEntregaAdicionalTextBox.Text = produtoSku.proSku_prazoEntregaAdicional.HasValue ? produtoSku.proSku_prazoEntregaAdicional.Value.ToString() : string.Empty;
                proSku_quantidadeMaximaTextBox.Text = produtoSku.proSku_quantidadeMaxima.HasValue ? produtoSku.proSku_quantidadeMaxima.Value.ToString() : string.Empty;
                proSku_quantidadeDisponivelTextBox.Text = produtoSku.proSku_quantidadeDisponivel.HasValue ? produtoSku.proSku_quantidadeDisponivel.Value.ToString() : string.Empty;
                proSku_parcelamentoDropDownList.SelectedValue = produtoSku.parc_id.ToString();
                proSku_tamanhoDropDownList.SelectedValue = produtoSku.proSkuTam_id.HasValue ? produtoSku.proSkuTam_id.Value.ToString() : string.Empty;
                proSku_corDropDownList.SelectedValue = produtoSku.proSkuCor_id.HasValue ? produtoSku.proSkuCor_id.Value.ToString() : string.Empty;
                proSku_disponivelCheckBox.Checked = produtoSku.proSku_disponivel;
                proSku_bloquearCheckBox.Checked = produtoSku.proSku_bloquear;
                proSku_destaqueCheckBox.Checked = produtoSku.proSku_destaque.HasValue ? produtoSku.proSku_destaque.Value : false;
                proSku_posicaoTextBox.Text = produtoSku.proSku_posicao.ToString();

            }
            else
                if (button.ID == "ButtonAlterarProdutoSkuFoto")
                {
                    PanelListarProdutoSkuFoto.Visible = true;
                    PanelCadastrarProdutoSku.Visible = false;
                    ListViewProdutoSkuFoto.EditIndex = -1;
                    ListViewProdutoSkuFoto.DataBind();
                }
                else

                    if (button.ID == "ButtonDuplicarProdutoSku")
                    {
                        ProdutoSku proSku = new Consulta().SelecionarProdutoSku(Convert.ToInt32(button.CommandArgument));
                        ProdutoSku produtoSku = new ProdutoSku();
                        produtoSku.loj_id = loj_id;
                        produtoSku.pro_id = proSku.pro_id;
                        produtoSku.proSku_idReferencia = proSku.proSku_idReferencia;
                        produtoSku.proSku_nome = proSku.proSku_nome;
                        produtoSku.proSku_precoAnterior = proSku.proSku_precoAnterior;
                        produtoSku.proSku_precoVenda = proSku.proSku_precoVenda;
                        produtoSku.proSku_precoCusto = proSku.proSku_precoCusto;
                        produtoSku.proSku_peso = proSku.proSku_peso;
                        produtoSku.proSku_altura = proSku.proSku_altura;
                        produtoSku.proSku_largura = proSku.proSku_largura;
                        produtoSku.proSku_comprimento = proSku.proSku_comprimento;
                        produtoSku.proSku_prazoEntregaAdicional = proSku.proSku_prazoEntregaAdicional;
                        produtoSku.proSku_quantidadeMaxima = proSku.proSku_quantidadeMaxima;
                        produtoSku.proSku_quantidadeDisponivel = proSku.proSku_quantidadeDisponivel;
                        produtoSku.proSku_disponivel = proSku.proSku_disponivel;
                        produtoSku.proSku_destaque = proSku.proSku_destaque;
                        produtoSku.parc_id = proSku.parc_id;
                        produtoSku.proSkuTam_id = proSku.proSkuTam_id;
                        produtoSku.proSkuCor_id = proSku.proSkuCor_id;
                        produtoSku.proSku_bloquear = proSku.proSku_bloquear;


                        String caminhoDiretorioFoto = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + proSku.pro_id + "\\";


                        foreach (ProdutoSkuFoto produtoSkuFoto in proSku.ProdutoSkuFoto)
                        {
                            ProdutoSkuFoto proSkuFot = new ProdutoSkuFoto();
                            proSkuFot.proSkuFot_nome = produtoSkuFoto.proSkuFot_nome;
                            proSkuFot.proSkuFot_extensao = produtoSkuFoto.proSkuFot_extensao;
                            proSkuFot.proSkuFot_posicao = produtoSkuFoto.proSkuFot_posicao;
                            proSkuFot.proSkuFot_titulo = produtoSkuFoto.proSkuFot_titulo;
                            proSkuFot.loj_id = produtoSkuFoto.loj_id;
                            proSkuFot.proSkuFot_dataHora = DateTime.Now;
                            produtoSku.ProdutoSkuFoto.Add(proSkuFot);
                        }

                        new Consulta().InserirProdutoSku(produtoSku);
                        ListViewProduto.DataBind();
                        ListViewProdutoSku.DataBind();
                        PanelCadastrarProdutoSku.Visible = false;
                        PanelListarProdutoSkuFoto.Visible = false;

                    }
                    else
                        if (button.ID == "ButtonExcluirProdutoSku")
                        {
                            Retorno retorno = new Consulta().ExcluirProdutoSku(Convert.ToInt32(button.CommandArgument));

                            if (retorno.menSis_id == 0)
                            {
                                ListViewProduto.DataBind();
                                ListViewProdutoSku.DataBind();
                            }
                            else
                            {
                                Validacao.Alert(Page, retorno.menSis_mensagem);
                            }
                            PanelCadastrarProdutoSku.Visible = false;
                            PanelListarProdutoSkuFoto.Visible = false;
                        }


            ListViewProdutoSku.SelectedIndex = -1;

        }

        protected void ButtonIncluirProdutoInfo_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            PanelCadastrarProdutoInfo.Visible = true;

            proInfo_nomeTextBox.Text = string.Empty;
            proInfo_bloquearCheckBox.Checked = false;
        }

        protected void ButtonAlterarProdutoInfo_Click(object sender, EventArgs e)
        {
            if (ListViewProdutoInfo.SelectedValue != null)
            {
                HiddenFieldTipoCadastro.Value = "1";
                PanelCadastrarProdutoInfo.Visible = true;

                Int32 proInfo_id = Convert.ToInt32(ListViewProdutoInfo.SelectedValue);
                HiddenFieldTipoCadastro.Value = "1";
                PanelListarProdutoInfoItem.Visible = true;
                ProdutoInfo produtoInfo = new Consulta().SelecionarProdutoInfo(proInfo_id);

                proInfo_nomeTextBox.Text = produtoInfo.proInfo_nome;
                proInfo_bloquearCheckBox.Checked = produtoInfo.proInfo_bloquear;
                HiddenFieldProInfo_id.Value = produtoInfo.proInfo_id.ToString();
            }
        }

        protected void ButtonSalvarProdutoInfo_Click(object sender, EventArgs e)
        {
            if (PanelListarProdutoInfo.Visible)
            {
                ProdutoInfo produtoInfo = new ProdutoInfo();
                produtoInfo.pro_id = Convert.ToInt32(ListViewProduto.SelectedValue);
                produtoInfo.proInfo_nome = proInfo_nomeTextBox.Text.Trim();
                produtoInfo.proInfo_bloquear = proInfo_bloquearCheckBox.Checked;

                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    new Consulta().InserirProdutoInfo(produtoInfo);
                }
                else
                {
                    produtoInfo.proInfo_id = Convert.ToInt32(HiddenFieldProInfo_id.Value);
                    new Consulta().AtualizarProdutoInfo(produtoInfo);
                }
                PanelCadastrarProdutoInfo.Visible = false;
                PanelCadastrarProdutoInfoItem.Visible = false;
                PanelListarProdutoInfoItem.Visible = false;
                ListViewProdutoInfo.SelectedIndex = -1;
                ListViewProdutoInfo.DataBind();
            }
        }

        protected void ListViewProdutoInfo_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ButtonExcluirProdutoInfo.Enabled = true;
            ButtonAlterarProdutoInfo.Enabled = true;
            PanelListarProdutoInfoItem.Visible = true;
            PanelCadastrarProdutoInfoItem.Visible = false;
            ListViewProdutoInfoItem.SelectedIndex = -1;
        }

        protected void ButtonExcluirProdutoInfo_Click(object sender, EventArgs e)
        {
            Retorno retorno = new Consulta().ExcluirProdutoInfo(Convert.ToInt32(ListViewProdutoInfo.SelectedValue));
            if (retorno.menSis_id == 0)
            {
                ListViewProdutoInfo.SelectedIndex = -1;
                PanelListarProdutoInfoItem.Visible = false;
                PanelCadastrarProdutoInfoItem.Visible = false;
                ListViewProdutoInfo.DataBind();
            }
            else
            {
                Validacao.Alert(Page, retorno.menSis_mensagem);
            }
        }

        protected void ListViewProdutoInfoItem_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Button button = ((Button)e.CommandSource);

            if (button.ID == "ButtonAlterarProdutoInfoItem")
            {
                HiddenFieldTipoCadastro.Value = "1";
                PanelCadastrarProdutoInfoItem.Visible = true;
                ProdutoInfoItem produtoInfoItem = new Consulta().SelecionarProdutoInfoItem(Convert.ToInt32(button.CommandArgument));
                proInfoItem_idHiddenField.Value = produtoInfoItem.proInfoItem_id.ToString();
                proInfoItem_descricaoTextBox.Text = produtoInfoItem.proInfoItem_descricao;
                proInfoItem_valorTextBox.Text = produtoInfoItem.proInfoItem_valor;
                proInfoItem_bloquearCheckBox.Checked = produtoInfoItem.proInfoItem_bloquear;
            }
            else
                if (button.ID == "ButtonExcluirProdutoInfoItem")
                {
                    Retorno retorno = new Consulta().ExcluirProdutoInfoItem(Convert.ToInt32(button.CommandArgument));
                    if (retorno.menSis_id == 0)
                    {
                        PanelCadastrarProdutoInfoItem.Visible = false;
                        ListViewProdutoInfoItem.DataBind();
                    }
                }
        }

        protected void ButtonIncluirProdutoInfoItem_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            PanelCadastrarProdutoInfoItem.Visible = true;
            ListViewProdutoInfoItem.SelectedIndex = -1;
            proInfoItem_descricaoTextBox.Text = string.Empty;
            proInfoItem_valorTextBox.Text = string.Empty;
            proInfoItem_bloquearCheckBox.Checked = false;

        }

        protected void ButtonSalvarProdutoInfoItem_Click(object sender, EventArgs e)
        {
            if (PanelListarProdutoInfoItem.Visible)
            {
                ProdutoInfoItem produtoInfoItem = new ProdutoInfoItem();
                produtoInfoItem.proInfo_id = Convert.ToInt32(ListViewProdutoInfo.SelectedValue);
                produtoInfoItem.proInfoItem_descricao = proInfoItem_descricaoTextBox.Text.Trim();
                produtoInfoItem.proInfoItem_valor = proInfoItem_valorTextBox.Text.Trim();
                produtoInfoItem.proInfoItem_bloquear = proInfoItem_bloquearCheckBox.Checked;

                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    new Consulta().InserirProdutoInfoItem(produtoInfoItem);
                }
                else
                {
                    produtoInfoItem.proInfoItem_id = Convert.ToInt32(proInfoItem_idHiddenField.Value);
                    new Consulta().AtualizarProdutoInfoItem(produtoInfoItem);
                }
                PanelCadastrarProdutoInfoItem.Visible = false;
                ListViewProdutoInfoItem.SelectedIndex = -1;
                ListViewProdutoInfoItem.DataBind();
            }
        }

        protected void ButtonCancelarProdutoInfoItem_Click(object sender, EventArgs e)
        {
            PanelCadastrarProdutoInfoItem.Visible = false;
            ListViewProdutoInfoItem.SelectedIndex = -1;
        }

        protected void ButtonAdicionarProdutoGrupo_Click(object sender, EventArgs e)
        {
            Produto_Grupo produtoGrupo = new Produto_Grupo();
            produtoGrupo.loj_id = loj_id;
            produtoGrupo.gru_id = Convert.ToInt32(gru_idHiddenField.Value);
            produtoGrupo.pro_id = Convert.ToInt32(pro_idHiddenField.Value);

            Retorno retorno = new Consulta().InserirProdutoGrupo(produtoGrupo);

            RepeaterProdutoGrupo.DataSource = new Consulta().SelecionarProdutoGrupo(produtoGrupo.pro_id).Select(s => new { s.gru_id, s.Grupo.gru_nome }).ToList();
            RepeaterProdutoGrupo.DataBind();

            if (retorno.menSis_id != 0)
                Validacao.Alert(Page, retorno.menSis_mensagem);
        }

        protected void LinkButtonRemover_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = ((LinkButton)sender);
            Int32 pro_id = Convert.ToInt32(pro_idHiddenField.Value);
            Int32 gru_id = Convert.ToInt32(linkButton.CommandArgument);
            Int32 gru_idSelecaoAtual = Convert.ToInt32(TreeViewGrupo.SelectedValue);


            Retorno retorno = new Consulta().ExcluirProdutoGrupo(pro_id, gru_id);

            if (retorno.menSis_id == 0)
            {
                var produtoGrupo = new Consulta().SelecionarProdutoGrupo(pro_id).Select(s => new { s.gru_id, s.Grupo.gru_nome });

                RepeaterProdutoGrupo.DataSource = produtoGrupo.ToList();
                RepeaterProdutoGrupo.DataBind();

                if (gru_id == gru_idSelecaoAtual)
                {
                    Buscar(produtoGrupo.FirstOrDefault().gru_id.ToString(), TreeViewGrupo.Nodes);
                }
            }
            else
            {
                Validacao.Alert(Page, retorno.menSis_mensagem);
            }
        }

        protected void ButtonEnviarProdutoSkuFoto_Click(object sender, EventArgs e)
        {
            InserirFoto(proSku_fotoFileUpload);
        }

        protected void ExcluirFoto(Int32 proSkuFot_id)
        {
            Retorno retorno = new Consulta().ExcluirProdutoSkuFoto(proSkuFot_id);
           
            ProdutoSkuFoto produtoSkuFoto = ((ProdutoSkuFoto)retorno.objeto);
            string proSkuFot_nome = produtoSkuFoto.proSkuFot_nome;
            string extensao = produtoSkuFoto.proSkuFot_extensao;
            Int32 proSku_id = produtoSkuFoto.proSku_id;

            if (new Consulta().SelecionarProdutoSkuFoto().Where(s => s.proSku_id == proSku_id && s.proSkuFot_nome == proSkuFot_nome).Count() == 0)
            {
                String caminhoDiretorioFoto = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + ListViewProduto.SelectedValue + "\\";
               
                File.Delete(caminhoDiretorioFoto + proSkuFot_nome+"-m" + extensao);
                File.Delete(caminhoDiretorioFoto + proSkuFot_nome+"-v" + extensao);
                File.Delete(caminhoDiretorioFoto + proSkuFot_nome+"-d" + extensao);
                File.Delete(caminhoDiretorioFoto + proSkuFot_nome+"-a" + extensao);
                File.Delete(caminhoDiretorioFoto + proSkuFot_nome + extensao);

                if (Directory.GetFiles(caminhoDiretorioFoto).Count() == 0)
                    Directory.Delete(caminhoDiretorioFoto);
            }
            ListViewProdutoSkuFoto.DataBind();
        }

        private void InserirFoto(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                Retorno retornoConfiguracao = new Consulta().SelecionarConfiguracao(loj_id);
                Retorno retornoRedimensionaSalvaImagem = new Retorno();

                if (retornoConfiguracao.menSis_id == 0)
                {
                    String caminhoDiretorioFoto = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + ListViewProduto.SelectedValue + "\\";

                    if (!Directory.Exists(caminhoDiretorioFoto))
                        Directory.CreateDirectory(caminhoDiretorioFoto);

                    ProdutoSkuFoto produtoSkuFoto = new ProdutoSkuFoto();
                    produtoSkuFoto.proSku_id = Convert.ToInt32(ListViewProdutoSku.SelectedValue);
                    produtoSkuFoto.proSkuFot_nome = "temp";
                    produtoSkuFoto.proSkuFot_posicao = 1;
                    string extensao = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                    produtoSkuFoto.proSkuFot_extensao = extensao;

                    Retorno retornoProdutoSkuFoto = new Consulta().InserirProdutoSkuFoto(produtoSkuFoto);

                    string pro_nome = new Consulta().SelecionarProdutoSku(produtoSkuFoto.proSku_id).Produto.pro_nome;

                    string nomeFoto = pro_nome + "-" + proSku_corDropDownList.SelectedItem.Text + "-" + proSku_tamanhoDropDownList.SelectedItem.Text + "-" + retornoProdutoSkuFoto.id_registro;
                    produtoSkuFoto.proSkuFot_titulo = Validacao.Truncate(nomeFoto.Replace("--", "-").TrimEnd('-').Replace("-", " - "), 70);
                    nomeFoto = Tratamento.GerarNomeAmigavel(nomeFoto);


                    string fotoOriginal = Path.Combine(caminhoDiretorioFoto, nomeFoto + extensao);

                    fileUpload.SaveAs(fotoOriginal);

                    retornoRedimensionaSalvaImagem = new Tratamento().RedimensionaSalvaImagem((Configuracao)retornoConfiguracao.objeto, fotoOriginal, caminhoDiretorioFoto, nomeFoto, extensao);
                    produtoSkuFoto.proSkuFot_nome = nomeFoto;
                    new Consulta().AtualizarProdutoSkuFoto(produtoSkuFoto);

                    if (retornoProdutoSkuFoto.menSis_id == 0 && retornoRedimensionaSalvaImagem.menSis_id == 0)
                    {
                        ListViewProdutoSkuFoto.DataBind();
                    }
                    else
                    {
                        LinkButton linkButton = new LinkButton() { CommandArgument = retornoProdutoSkuFoto.id_registro.ToString() };
                        ExcluirFoto(retornoProdutoSkuFoto.id_registro);
                        Validacao.Alert(Page, retornoProdutoSkuFoto.menSis_id != 0 ? retornoProdutoSkuFoto.menSis_mensagem : retornoRedimensionaSalvaImagem.menSis_mensagem);
                    }
                }
                else
                {
                    Validacao.Alert(Page, retornoConfiguracao.menSis_mensagem);
                }
            }
        }

        private void AtualizarFoto(FileUpload fileUpload, Int32 proSkuFot_id, Int32 proSkuFot_posicao)
        {
            if (fileUpload.HasFile)
            {
                String caminhoDiretorioFoto = Request.PhysicalApplicationPath.Replace("1-WebForm.Painel", "1-WebForm") + @"imagens\produtos\fotos\" + loj_id + "\\" + ListViewProduto.SelectedValue + "\\";

                ProdutoSkuFoto produtoSkuFoto = new Consulta().SelecionarProdutoSkuFoto().Where(s => s.proSkuFot_id == proSkuFot_id).SingleOrDefault();

                string nomeFoto = produtoSkuFoto.ProdutoSku.Produto.pro_nome + "-" + produtoSkuFoto.ProdutoSku.ProdutoSkuCor.proSkuCor_nome + "-" + produtoSkuFoto.ProdutoSku.ProdutoSkuTamanho.proSkuTam_nome + "-" + produtoSkuFoto.proSkuFot_id;
                produtoSkuFoto.proSkuFot_titulo = Validacao.Truncate(nomeFoto.Replace("--", "-").TrimEnd('-'), 70);
                nomeFoto = Tratamento.GerarNomeAmigavel(nomeFoto);

                string extensao = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                string fotoOriginal = Path.Combine(caminhoDiretorioFoto, nomeFoto + extensao);

                fileUpload.SaveAs(fotoOriginal);

                Retorno retorno = new Consulta().SelecionarConfiguracao(loj_id);
                if (retorno.menSis_id == 0)
                {
                    retorno = new Tratamento().RedimensionaSalvaImagem((Configuracao)retorno.objeto, fotoOriginal, caminhoDiretorioFoto, nomeFoto, extensao);
                }

                if (retorno.menSis_id == 0)
                {
                    produtoSkuFoto.proSkuFot_nome = nomeFoto;
                    produtoSkuFoto.proSkuFot_extensao = extensao;
                    produtoSkuFoto.proSkuFot_posicao = proSkuFot_posicao;
                    new Consulta().AtualizarProdutoSkuFoto(produtoSkuFoto);
                    ListViewProdutoSkuFoto.DataBind();
                }
                else
                {
                    Validacao.Alert(Page, retorno.menSis_mensagem);
                    return;
                }
            }
            else
            {
                ProdutoSkuFoto produtoSkuFoto = new ProdutoSkuFoto();
                produtoSkuFoto.proSkuFot_id = proSkuFot_id;
                produtoSkuFoto.proSkuFot_posicao = proSkuFot_posicao;
                new Consulta().AtualizarProdutoSkuFoto(produtoSkuFoto);
            }
        }

        protected void ListViewProdutoSkuFoto_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Button button = ((Button)e.CommandSource);

            int proSkuFot_id = Convert.ToInt32(button.CommandArgument);

            if (button.ID == "ButtonSalvarProdutoSkuFoto")
            {
                FileUpload proSku_fotoFileUpload = (FileUpload)e.Item.FindControl("proSkuFot_nomeFileUpload");
                Int32 proSkuFot_posicao = Convert.ToInt32(((TextBox)e.Item.FindControl("proSkuFot_posicaoTextBox")).Text);
                AtualizarFoto(proSku_fotoFileUpload, proSkuFot_id, proSkuFot_posicao);

                ListViewProdutoSkuFoto.EditIndex = -1;
            }
            else
            {
                if (button.ID == "ButtonExcluirProdutoSkuFoto")
                {
                    ExcluirFoto(proSkuFot_id);
                }
            }
        }

        private Produto DuplicarProduto(Int32 pro_id) { 
        
        Produto pro = new Consulta().SelecionarProduto(pro_id).SingleOrDefault();
        Produto produto = new Produto();
        produto.pro_nome = pro.pro_nome;
        produto.mar_id = pro.mar_id;
        produto.loj_id = pro.loj_id;
        produto.pro_bloquear = pro.pro_bloquear;
        produto.pro_dataHora = DateTime.Now;
        produto.pro_descricao = pro.pro_descricao;
        produto.pro_nomeAmigavel = pro.pro_nomeAmigavel;
        produto.pro_paginaInicialAte = pro.pro_paginaInicialAte;
        produto.pro_paginaInicialDe = pro.pro_paginaInicialDe;
        produto.pro_posicao = pro.pro_posicao;
        produto.Produto_Grupo = DuplicarProdutoGrupo(pro_id);

       foreach (ProdutoInfo produtoInfo in pro.ProdutoInfo)
		    produto.ProdutoInfo.Add(DuplicarProdutoInfo(produtoInfo.proInfo_id));

        foreach (ProdutoSku produtoSku in pro.ProdutoSku)
		 produto.ProdutoSku.Add(DuplicarProdutoSku(produtoSku.proSku_id));
        
        return produto;
        }

        private System.Data.Objects.DataClasses.EntityCollection<Produto_Grupo> DuplicarProdutoGrupo(Int32 pro_id)
        {
            System.Data.Objects.DataClasses.EntityCollection<Produto_Grupo> proGru = new System.Data.Objects.DataClasses.EntityCollection<Produto_Grupo>();
            IQueryable<Produto_Grupo> produtosGrupo = new Consulta().SelecionarProdutoGrupo(pro_id);
            foreach (Produto_Grupo produtoGrupo in produtosGrupo)
                proGru.Add(new Produto_Grupo()
                {
                    gru_id = produtoGrupo.gru_id, 
                    loj_id = produtoGrupo.loj_id,
                    proGru_dataHora  = DateTime.Now 
                });
    
            return proGru;
        }

        private ProdutoInfo DuplicarProdutoInfo(Int32 proInfo_id)
        {
            ProdutoInfo prodInfo = new Consulta().SelecionarProdutoInfo(proInfo_id);
            ProdutoInfo produtoInfo = new ProdutoInfo();
            produtoInfo.loj_id = prodInfo.loj_id;
            produtoInfo.proInfo_bloquear = prodInfo.proInfo_bloquear;
            produtoInfo.proInfo_nome = prodInfo.proInfo_nome;
            produtoInfo.proInfo_dataHora = DateTime.Now;

           foreach (ProdutoInfoItem produtoInfoItem in prodInfo.ProdutoInfoItem)
                produtoInfo.ProdutoInfoItem.Add(DuplicarProdutoInfoItem(produtoInfoItem.proInfoItem_id));
           
            return produtoInfo;
        }

        private ProdutoInfoItem DuplicarProdutoInfoItem(Int32 proInfoItem_id)
        {
            ProdutoInfoItem proInfItem = new Consulta().SelecionarProdutoInfoItem(proInfoItem_id);
            ProdutoInfoItem produtoInfoItem = new ProdutoInfoItem();
            produtoInfoItem.loj_id = proInfItem.loj_id;
            produtoInfoItem.proInfoItem_bloquear = proInfItem.proInfoItem_bloquear;
            produtoInfoItem.proInfoItem_dataHora = DateTime.Now;
            produtoInfoItem.proInfoItem_descricao = proInfItem.proInfoItem_descricao;
            produtoInfoItem.proInfoItem_posicao = proInfItem.proInfoItem_posicao;
            produtoInfoItem.proInfoItem_valor = proInfItem.proInfoItem_valor;
            return produtoInfoItem;
        }

        private ProdutoSku DuplicarProdutoSku(Int32 proSku_id)
        {
            ProdutoSku proSku = new Consulta().SelecionarProdutoSku(proSku_id);
            ProdutoSku produtoSku = new ProdutoSku();
            produtoSku.loj_id = loj_id;
            produtoSku.proSku_idReferencia = proSku.proSku_idReferencia;
            produtoSku.proSku_nome = proSku.proSku_nome;
            produtoSku.proSku_precoAnterior = proSku.proSku_precoAnterior;
            produtoSku.proSku_precoVenda = proSku.proSku_precoVenda;
            produtoSku.proSku_precoCusto = proSku.proSku_precoCusto;
            produtoSku.proSku_peso = proSku.proSku_peso;
            produtoSku.proSku_altura = proSku.proSku_altura;
            produtoSku.proSku_largura = proSku.proSku_largura;
            produtoSku.proSku_comprimento = proSku.proSku_comprimento;
            produtoSku.proSku_prazoEntregaAdicional = proSku.proSku_prazoEntregaAdicional;
            produtoSku.proSku_quantidadeMaxima = proSku.proSku_quantidadeMaxima;
            produtoSku.proSku_quantidadeDisponivel = proSku.proSku_quantidadeDisponivel;
            produtoSku.proSku_disponivel = proSku.proSku_disponivel;
            produtoSku.proSku_destaque = proSku.proSku_destaque;
            produtoSku.parc_id = proSku.parc_id;
            produtoSku.proSkuTam_id = proSku.proSkuTam_id;
            produtoSku.proSkuCor_id = proSku.proSkuCor_id;
            produtoSku.proSku_bloquear = proSku.proSku_bloquear;
            produtoSku.proSku_dataHora = DateTime.Now;

            foreach (ProdutoSkuFoto produtoSkuFoto in proSku.ProdutoSkuFoto)
            {
                produtoSku.ProdutoSkuFoto.Add(new ProdutoSkuFoto() {
                    proSkuFot_nome = produtoSkuFoto.proSkuFot_nome,
                    proSkuFot_extensao = produtoSkuFoto.proSkuFot_extensao,
                    proSkuFot_posicao = produtoSkuFoto.proSkuFot_posicao,
                    proSkuFot_titulo = produtoSkuFoto.proSkuFot_titulo,
                    loj_id = produtoSkuFoto.loj_id,
                    proSkuFot_dataHora = DateTime.Now 
                });
            }

            return produtoSku;
        }

       /* private ProdutoSkuFoto DuplicarProdutoSkuFoto(Int32 proSkuFot_nome)
        {
            ProdutoSkuFoto proSkuFot = new ProdutoSkuFoto();
            ProdutoSkuFoto produtoSkuFoto = new Consulta().SelecionarProdutoSkuFoto().Where(s => s.proSkuFot_id == proSkuFot_nome).SingleOrDefault();

            proSkuFot.proSkuFot_nome = produtoSkuFoto.proSkuFot_nome;
            proSkuFot.proSkuFot_extensao = produtoSkuFoto.proSkuFot_extensao;
            proSkuFot.proSkuFot_posicao = produtoSkuFoto.proSkuFot_posicao;
            proSkuFot.proSkuFot_titulo = produtoSkuFoto.proSkuFot_titulo;
            proSkuFot.loj_id = produtoSkuFoto.loj_id;
            proSkuFot.proSkuFot_dataHora = DateTime.Now;

            return proSkuFot;
        }*/

        protected void LinkButtonAtualizarParcelamento_Click(object sender, EventArgs e)
        {

            string parcIdSelecionado = proSku_parcelamentoDropDownList.SelectedValue;

            proSku_parcelamentoDropDownList.Items.Clear();
            proSku_parcelamentoDropDownList.Items.Add(new ListItem(string.Empty, string.Empty));
            proSku_parcelamentoDropDownList.DataSource = new ParcelamentoConsulta().SelecionarParcelamento().ToList();
            
            proSku_parcelamentoDropDownList.DataBind();
            proSku_parcelamentoDropDownList.SelectedValue = parcIdSelecionado;
        }

        //private static Int32 testeCont = 0;
        protected void Button1_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            produto.pro_id = 2022;
            for (int i = 0; i < 10000; i++)
            {

                produto = DuplicarProduto(produto.pro_id);
               new Consulta().InserirProduto(produto);
            }
            Validacao.Alert(Page, "Acabou");

        }


    }
}