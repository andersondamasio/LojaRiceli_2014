using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Painel.CarrinhoX;
using _2_Library.Utils;

namespace Loja.Painel
{
    public partial class RecursosCarrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonExcluir_Click(object sender, EventArgs e)
        {
            if (Validacao.ValidaData(TextBoxDataMaior.Text))
            {
                DateTime data_hora = Convert.ToDateTime(TextBoxDataMaior.Text);
                new CarrinhoTd().RemoveCarrinhoDataMaior(data_hora);
                ListViewCarrinho.DataBind();

            }
            else
                Validacao.Alert("Data inválida");
        }
    }
}