using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.Painel.UsuarioX
{
    public class UsuarioTd
    {
        public UsuarioDto SelectUsuario(string loj_dominio, string usu_nome, string usu_senha)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            //usu_senha = Recursos.Hash(usu_senha);
            UsuarioDto usuarioDto = null;

            using (UsuarioDao usuarioDao = new UsuarioDao())
            {

                usuarioDto = usuarioDao.SelectUsuario(loj_id, usu_nome, usu_senha);
            }

            return usuarioDto;
        }

        public UsuarioDto SelectUsuario(string loj_dominio, int cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            UsuarioDto usuarioDto = null;

            using (UsuarioDao usuarioDao = new UsuarioDao())
            {
                usuarioDto = usuarioDao.SelectUsuario(loj_id, cli_id);
            }
            return usuarioDto;
        }
    }
}
