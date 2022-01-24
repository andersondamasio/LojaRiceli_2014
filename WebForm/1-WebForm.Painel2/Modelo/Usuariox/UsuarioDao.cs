using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace Loja.Modelo.Usuariox
{
    public class UsuarioDao
    {
        private Int32 loj_id = new LojaTd().SelectLoja(null).loj_id;

        public Boolean SelecionarUsuarioExiste(string usu_nome, int excetoUsu_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Boolean existe = (from usu in lojaEntities.Usuario
                              where
                              (usu.usu_nome == usu_nome &&
                              usu.loj_id == loj_id) &&
                              usu.usu_id != excetoUsu_id && !usu.usu_excluido.HasValue
                              select usu).Count() > 0;

            return existe;         
        }

        public Usuario SelecionarUsuario(string usu_nome, string usu_senha)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Usuario usuario = (from usu in lojaEntities.Usuario
                              where
                              usu.usu_nome == usu_nome && usu.usu_senha == usu_senha &&
                              usu.loj_id == loj_id &&
                              !usu.usu_excluido.HasValue
                              select usu).FirstOrDefault();

            return usuario;
        }

    }
}