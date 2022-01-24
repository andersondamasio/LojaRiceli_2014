using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Usuariox
{
    public class UsuarioConsulta
    {
        public Boolean SelecionarUsuarioExiste(string usu_nome, int excetoUsu_id)
        {
            return new UsuarioDao().SelecionarUsuarioExiste(usu_nome, excetoUsu_id);
        }

        public Usuario SelecionarUsuario(string usu_nome, string usu_senha)
        {
            return new UsuarioDao().SelecionarUsuario(usu_nome, usu_senha);
        }
    }
}