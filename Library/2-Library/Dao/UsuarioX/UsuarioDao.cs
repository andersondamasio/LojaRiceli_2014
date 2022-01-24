using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.UsuarioX
{
    internal class UsuarioDao : Repositorio<Usuario>
    {
        public UsuarioDto SelectUsuario(int loj_id, string usu_nome, string usu_senha)
        {
            UsuarioDto usuarioDto = (from usu in Select()
                                     where
                                     usu.usu_nome == usu_nome &&
                                     usu.usu_senha == usu_senha &&
                                     (usu.loj_id == loj_id || usu.usu_admin) &&
                                     !usu.usu_excluido.HasValue
                                     select new UsuarioDto
                                     {
                                         usu_id = usu.usu_id,
                                         usu_nome = usu.usu_nome,
                                         usu_senha = usu.usu_senha,
                                         usuPer_usuarioSelecionar = usu.usuPer_usuarioSelecionar,
                                         usuPer_pedidoSelecionar = usu.usuPer_pedidoSelecionar,
                                         usuPer_lojaSelecionar = usu.usuPer_lojaSelecionar,
                                         usuPer_lojaInserir = usu.usuPer_lojaInserir,
                                         loj_id = loj_id
                                     }).FirstOrDefault();
            return usuarioDto;
        }

    }
}
