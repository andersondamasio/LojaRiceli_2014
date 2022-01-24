using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ClienteSocialX
{
    internal class SocialPerfilDao : Repositorio<SocialPerfil>
    {


        public SocialPerfilDto SelectSocialPerfil(int loj_id, string sp_idPerfil)
        {
            SocialPerfilDto socialPerfilDto = (from sp in Select()
                                               where
                                               sp.sp_idPerfil == sp_idPerfil &&
                                               sp.loj_id == loj_id
                                               select new SocialPerfilDto
                                               {
                                                   sp_id = sp.sp_id,
                                                   sp_idPerfil = sp.sp_idPerfil,
                                                   sp_nome = sp.sp_nome,
                                                   sp_sobrenome = sp.sp_sobrenome,
                                                   sp_sexo = sp.sp_sexo,
                                                   sp_dataNascimento = sp.sp_dataNascimento,
                                                   sp_email = sp.sp_email,
                                                   sp_cidade = sp.sp_cidade,
                                                   sp_estado = sp.sp_estado,
                                                   sp_idioma = sp.sp_idioma,
                                                   sp_amigos = sp.sp_amigos,
                                                   cli_id = sp.cli_id
                                               }).FirstOrDefault();
            return socialPerfilDto;
        }

        public SocialPerfilDto SelectSocialPerfil(int loj_id, int cli_id)
        {
            SocialPerfilDto socialPerfilDto = (from sp in Select()
                                               where
                                               sp.cli_id == cli_id &&
                                               sp.loj_id == loj_id
                                               select new SocialPerfilDto
                                               {
                                                   sp_id = sp.sp_id,
                                                   sp_idPerfil = sp.sp_idPerfil,
                                                   sp_nome = sp.sp_nome,
                                                   sp_sobrenome = sp.sp_sobrenome,
                                                   sp_sexo = sp.sp_sexo,
                                                   sp_dataNascimento = sp.sp_dataNascimento,
                                                   sp_email = sp.sp_email,
                                                   sp_cidade = sp.sp_cidade,
                                                   sp_estado = sp.sp_estado,
                                                   sp_idioma = sp.sp_idioma,
                                                   sp_amigos = sp.sp_amigos,
                                                   sp_numeroConexoes = sp.sp_numeroConexoes,
                                                   cli_id = sp.cli_id
                                               }).FirstOrDefault();
            return socialPerfilDto;
        }

    }
}

