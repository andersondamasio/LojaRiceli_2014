using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Loja.Modelo.FormaPagamentox;
using Loja.Modelo.Parcelamentox;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace Loja.Painel
{
    public partial class CadastroParcelamento : System.Web.UI.Page
    {
        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
       
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private bool ListarParcelamento()
        {
            List<FormaPagamento> formaPagamento = new FormaPagamentoDao().SelecionarFormaPagamento();

            if (formaPagamento.Count > 0)
            {
                CheckBoxListFormaPagamento.DataSource = formaPagamento;
                CheckBoxListFormaPagamento.DataBind();
                RepeaterParcela.DataBind();
                return true;
            }
            else {
                Validacao.Alert( "Não é possível incluir o parcelamento, primeiro inclua pelo menos um cartão.");
                return false;
            }
        }

        private void SelecionarParcelamento(Int32 parc_id)
        {
            List<FormaPagamento> formaPagamento = new FormaPagamentoDao().SelecionarFormaPagamento();

            ListarParcelamento();

            Parcelamento parcelamento = new ParcelamentoDao().SelecionarParcelamento().Where(s => s.parc_id == parc_id).SingleOrDefault();
            parc_nomeTextBox.Text = parcelamento.parc_nome;
            parc_valorMinimoTextBox.Text = parcelamento.parc_valorMinimo.HasValue ? parcelamento.parc_valorMinimo.Value.ToString() : string.Empty;
            parc_bloquearCheckBox.Checked = parcelamento.parc_bloquear;
            parc_ativarJuroCheckBox.Checked = parcelamento.parc_ativarJuro;

            foreach (ListItem item in CheckBoxListFormaPagamento.Items)
            {
                item.Selected = false;
                Int32 forPag_id = Convert.ToInt32(item.Value);
                if (parcelamento.Parcelamento_FormaPagamento.Where(s => s.forPag_id == forPag_id).Count() > 0)
                {
                    item.Selected = true;
                }
            }

            //IQueryable<ParcelamentoParcela> parcelamentoParcela = new ParcelamentoConsulta().SelecionarParcelamentoParcela(parc_id);

            foreach (RepeaterItem item in RepeaterParcela.Items)
            {
                TextBox par_percJurosTextBox = (TextBox)item.FindControl("par_percJurosTextBox");
                par_percJurosTextBox.Text = string.Empty;
                par_percJurosTextBox.Enabled = parc_ativarJuroCheckBox.Checked;
                CheckBox par_bloquearCheckBox = (CheckBox)item.FindControl("par_bloquearCheckBox");
                par_bloquearCheckBox.Checked = false;
                Label parcPar_quantidadeLabel = (Label)item.FindControl("parcPar_quantidadeLabel");

                Int32 parcPar_quantidade = Convert.ToInt32(parcPar_quantidadeLabel.Text);
                var parcelamento_parcela = new ParcelamentoDao().SelecionarParcelamento_Parcela().Where(s => s.parc_id == parc_id && s.parcPar_quantidade == parcPar_quantidade);

                if (parcelamento_parcela.Count() > 0)
                {
                    par_bloquearCheckBox.Checked = true;
                    par_percJurosTextBox.Text = parcelamento_parcela.SingleOrDefault().parcPar_percentualJuro.ToString();
                }
            }
        }

        protected void ButtonSalvarParcelamento_Click(object sender, EventArgs e)
        {
            SalvarParcelamento(Convert.ToInt32(ListViewParcelamento.SelectedValue));
            ListViewParcelamento.SelectedIndex = -1;
            ListViewParcelamento.DataBind();
            PanelCadastrarParcelamento.Visible = false;
        }

        private void SalvarParcelamento(Int32 parc_id)
        {
            Parcelamento parcelamento = new Parcelamento();
            parcelamento.parc_nome = parc_nomeTextBox.Text.Trim();
            parcelamento.parc_bloquear = parc_bloquearCheckBox.Checked;

            if (!string.IsNullOrEmpty(parc_valorMinimoTextBox.Text.Trim()))
                parcelamento.parc_valorMinimo = Convert.ToDecimal(parc_valorMinimoTextBox.Text.Trim());
            else parcelamento.parc_valorMinimo = null;

            parcelamento.parc_ativarJuro = parc_ativarJuroCheckBox.Checked;

            var checkBoxListFormaPagamento = (from System.Web.UI.WebControls.ListItem listItem in CheckBoxListFormaPagamento.Items
                                              where listItem.Selected == true
                                              select listItem);

            foreach (ListItem item in checkBoxListFormaPagamento)
            {
                Int32 forPag_id = Convert.ToInt32(item.Value);
                Parcelamento_FormaPagamento parcelamento_formaPagamento = new Parcelamento_FormaPagamento();
                parcelamento_formaPagamento.parc_id = parc_id;
                parcelamento_formaPagamento.forPag_id = forPag_id;

                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    parcelamento_formaPagamento.loj_id = loj_id;
                    parcelamento_formaPagamento.parcForPag_dataHora = DateTime.Now;
                }
                parcelamento.Parcelamento_FormaPagamento.Add(parcelamento_formaPagamento);
            }


            var repeaterParcela = (from System.Web.UI.WebControls.RepeaterItem listItem in RepeaterParcela.Items
                                              where ((CheckBox)listItem.FindControl("par_bloquearCheckBox")).Checked == true
                                              select listItem);

            foreach (RepeaterItem item in repeaterParcela)
            {
                TextBox par_percJurosTextBox = (TextBox)item.FindControl("par_percJurosTextBox");
                CheckBox par_bloquearCheckBox = (CheckBox)item.FindControl("par_bloquearCheckBox");
                Label parcPar_quantidadeLabel = (Label)item.FindControl("parcPar_quantidadeLabel");

                Int32 parcPar_quantidade = Convert.ToInt32(parcPar_quantidadeLabel.Text);
                Decimal par_percJuros = 0;
               
                ParcelamentoParcela parcelamentoParcela = new ParcelamentoParcela();
                if (par_percJurosTextBox.Text.Trim() != string.Empty)
                {
                    par_percJuros = Convert.ToDecimal(par_percJurosTextBox.Text);
                    parcelamentoParcela.parcPar_percentualJuro = par_percJuros;
                }

                parcelamentoParcela.parcPar_quantidade = parcPar_quantidade;
                parcelamentoParcela.parc_id = parc_id;
                if (HiddenFieldTipoCadastro.Value == "0")
                {
                    parcelamentoParcela.loj_id = loj_id;
                    parcelamentoParcela.parcPar_dataHora = DateTime.Now;
                }
                parcelamento.ParcelamentoParcela.Add(parcelamentoParcela);
            }



            if (checkBoxListFormaPagamento.Count() == 0 || repeaterParcela.Count() == 0)
            {
                Validacao.Alert( "Pelo menos um Cartão e um Parcelamento deve ser escolhido.");
            }
            else
            {
                if (HiddenFieldTipoCadastro.Value == "0")
                    new ParcelamentoConsulta().InserirParcelamento(parcelamento);
                else
                {
                    parcelamento.parc_id = parc_id;
                    new ParcelamentoConsulta().AtualizarParcelamento(parcelamento);
                }
                if (parcelamento.parc_ativar)
                    new ParcelamentoConsulta().AtualizarParcelamentoAtivar(parcelamento.parc_id);
            }
        }

        protected void ButtonIncluirParcelamento_Click(object sender, EventArgs e)
        {
            if (ListarParcelamento())
            {
                HiddenFieldTipoCadastro.Value = "0";
                parc_nomeTextBox.Text = string.Empty;
                parc_valorMinimoTextBox.Text = string.Empty;
                parc_ativarJuroCheckBox.Checked = false;
                parc_bloquearCheckBox.Checked = false;
                foreach (ListItem item in CheckBoxListFormaPagamento.Items)
                    item.Selected = false;

                foreach (RepeaterItem item in RepeaterParcela.Items)
                {
                    TextBox par_percJurosTextBox = (TextBox)item.FindControl("par_percJurosTextBox");
                    CheckBox par_bloquearCheckBox = (CheckBox)item.FindControl("par_bloquearCheckBox");
                    par_percJurosTextBox.Text = string.Empty;
                    par_bloquearCheckBox.Checked = false;
                    par_percJurosTextBox.Enabled = parc_ativarJuroCheckBox.Checked;
                }
                PanelCadastrarParcelamento.Visible = true;
            }
        }

        protected void ListViewParcelamento_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
             Button button = ((Button)e.CommandSource);

            if (button.ID == "ButtonAlterarParcelamento")
            {
                HiddenFieldTipoCadastro.Value = "1";
                PanelCadastrarParcelamento.Visible = true;
                Int32 parc_id = Convert.ToInt32(button.CommandArgument);
                SelecionarParcelamento(parc_id);
            }
        }

        protected void parc_ativarJuroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterParcela.Items)
            {
                TextBox par_percJurosTextBox = (TextBox)item.FindControl("par_percJurosTextBox");
                par_percJurosTextBox.Enabled = parc_ativarJuroCheckBox.Checked;
            }
        }

        protected void ListViewParcelamento_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            if (((RadioButton)ListViewParcelamento.Items[e.ItemIndex].FindControl("parc_ativarRadioButton")).Checked)
            {
                e.Cancel = true;
                Validacao.Alert( "Não é possível excluir um parcelamento Padrão.");
            }
        }
    }
}