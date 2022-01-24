using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Painel.UsuarioX;
using Loja.Utils;

namespace _1_WebForm.Painel2
{
    public partial class CadastroLoja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void InsertLoja()
        {
            LojaDto lojaDto = new LojaDto();
            lojaDto.loj_nome = loj_nomeTextBox.Text.Trim();
            lojaDto.loj_dominio = loj_dominioTextBox.Text.Trim();
            lojaDto.loj_email = loj_emailTextBox.Text.Trim();
            lojaDto.loj_cep = loj_cepTextBox.Text.Trim();

            UsuarioDto usuarioDto = new UsuarioDto();
            usuarioDto.usu_nome = usu_nomeTextBox.Text.Trim();
            usuarioDto.usu_senha = usu_senhaTextBox.Text.Trim();
            usuarioDto.loj_dominio = lojaDto.loj_dominio;
            usuarioDto.loj_id = lojaDto.loj_id;
            usuarioDto.usuPer_usuarioSelecionar = true;
            usuarioDto.usuPer_pedidoSelecionar = true;
            usuarioDto.usuPer_lojaSelecionar = true;
            usuarioDto.usuPer_lojaInserir = true;
            usuarioDto.usu_dataHora = DateTime.Now;
            lojaDto.usuarioDto = new List<UsuarioDto>();
            lojaDto.usuarioDto.Add(usuarioDto);

            int loj_id = new LojaTd().InsertLoja(lojaDto);

            if (loj_id == 0)
                _2_Library.Utils.Validacao.Alert("Já existe uma loja com este domínio.");
            else
            {
                Aut.Autenticar(usuarioDto);
                _2_Library.Utils.Validacao.Alert("Loja Salva com Sucesso");
                _2_Library.Utils.Validacao.Redirecionar("/Index.aspx");
            }
        }

        protected void ButtonSalvar_Click(object sender, EventArgs e)
        {
            InsertLoja();
        }
    }
}