using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.SocialPerfilX
{
    public class SocialPerfilTd
    {
        public SocialPerfilDto InsertSocialPerfil(string loj_dominio, SocialPerfilDto socialPerfilDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            using (SocialPerfilDao socialPerfilDao = new SocialPerfilDao())
            {

                SocialPerfil socialPerfil = socialPerfilDao.Select().Where(s => s.sp_idPerfil == socialPerfilDto.sp_idPerfil && s.loj_id == loj_id).FirstOrDefault();

                if (socialPerfil == null)
                {
                    socialPerfil = ToSocialPerfil(loj_id, new SocialPerfil(), socialPerfilDto);
                    socialPerfil.loj_id = loj_id;

                    socialPerfilDao.Add(socialPerfil);
                    socialPerfilDto.cli_id = socialPerfil.cli_id;
                    socialPerfilDto.sp_id = socialPerfil.sp_id;
                    return socialPerfilDto;
                }
                else
                {
                    if(socialPerfil.cli_id.HasValue)
                       socialPerfilDto.cli_id = socialPerfil.cli_id;
                   
                    socialPerfilDto.sp_id = socialPerfil.sp_id;
                   
                    //atualiza o perfil caso houver mudanças
                    socialPerfil = ToSocialPerfil(loj_id, socialPerfil, socialPerfilDto);
                    socialPerfilDao.Update(socialPerfil);
                    return socialPerfilDto;
                }
            }
        }

        public SocialPerfilDto SelectSocialPerfil(string loj_dominio, string sp_idPerfil)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            SocialPerfilDto socialPerfilDto = new SocialPerfilDao().SelectSocialPerfil(loj_id, sp_idPerfil);

            return socialPerfilDto;
        }

        public SocialPerfilDto SelectSocialPerfil(string loj_dominio, int cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            SocialPerfilDto socialPerfilDto = new SocialPerfilDao().SelectSocialPerfil(loj_id, cli_id);

            return socialPerfilDto;
        }

        public int UpdateSocialPerfil(string loj_dominio, SocialPerfilDto socialPerfilDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            SocialPerfil socialPerfil = new SocialPerfil();

            using (IRepositorio<SocialPerfil> repSP = new SocialPerfilDao())
            {
                socialPerfil = repSP.Select().Where(s => s.sp_idPerfil == socialPerfilDto.sp_idPerfil && s.loj_id == loj_id).FirstOrDefault();

                socialPerfil = ToSocialPerfil(loj_id, socialPerfil, socialPerfilDto);

                repSP.Update(socialPerfil);

            }
            return socialPerfil.sp_id;

        }


        private SocialPerfil ToSocialPerfil(int loj_id, SocialPerfil socialPerfil, SocialPerfilDto socialPerfilDto)
        {

            Type type = typeof(SocialPerfilDto);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                object value = pi.GetValue(socialPerfilDto, null);
                if (value != null && value.GetType().Name == "String")
                {
                    string novoValue = value.ToString().Trim();
                    novoValue = (novoValue == string.Empty) ? null : novoValue;
                    pi.SetValue(socialPerfilDto, novoValue);
                }
            }

            if (socialPerfil.sp_idPerfil != socialPerfilDto.sp_idPerfil)
                socialPerfil.sp_idPerfil = socialPerfilDto.sp_idPerfil;

            if (socialPerfil.sp_nome != socialPerfilDto.sp_nome)
                socialPerfil.sp_nome = socialPerfilDto.sp_nome;

            if (socialPerfil.sp_sobrenome != socialPerfilDto.sp_sobrenome)
                socialPerfil.sp_sobrenome = socialPerfilDto.sp_sobrenome;

            if (socialPerfil.sp_sexo != socialPerfilDto.sp_sexo)
                socialPerfil.sp_sexo = socialPerfilDto.sp_sexo;

            if (socialPerfil.sp_dataNascimento != socialPerfilDto.sp_dataNascimento)
                socialPerfil.sp_dataNascimento = socialPerfilDto.sp_dataNascimento;

            if (socialPerfil.sp_email != socialPerfilDto.sp_email)
                socialPerfil.sp_email = socialPerfilDto.sp_email;

            if (socialPerfil.sp_cidade != socialPerfilDto.sp_cidade)
                socialPerfil.sp_cidade = socialPerfilDto.sp_cidade;

            if (socialPerfil.sp_estado != socialPerfilDto.sp_estado)
                socialPerfil.sp_estado = socialPerfilDto.sp_estado;

            if (socialPerfil.sp_idioma != socialPerfilDto.sp_idioma)
                socialPerfil.sp_idioma = socialPerfilDto.sp_idioma;

            if (!string.IsNullOrEmpty(socialPerfilDto.sp_amigos))
                if (socialPerfil.sp_amigos != socialPerfilDto.sp_amigos)
                    socialPerfil.sp_amigos = socialPerfilDto.sp_amigos;

            if (socialPerfil.sp_interesses != socialPerfilDto.sp_interesses)
                socialPerfil.sp_interesses = socialPerfilDto.sp_interesses;

            if (socialPerfil.sp_atividades != socialPerfilDto.sp_atividades)
                socialPerfil.sp_atividades = socialPerfilDto.sp_atividades;

            if (socialPerfil.sp_curtidas != socialPerfilDto.sp_curtidas)
                socialPerfil.sp_curtidas = socialPerfilDto.sp_curtidas;

            if (socialPerfil.sp_sobre != socialPerfilDto.sp_sobre)
                socialPerfil.sp_sobre = socialPerfilDto.sp_sobre;

            if (socialPerfil.sp_site != socialPerfilDto.sp_site)
                socialPerfil.sp_site = socialPerfilDto.sp_site;

            if (socialPerfil.sp_religiao != socialPerfilDto.sp_religiao)
                socialPerfil.sp_religiao = socialPerfilDto.sp_religiao;

            if (socialPerfil.sp_relacionamentoStatus != socialPerfilDto.sp_relacionamentoStatus)
                socialPerfil.sp_relacionamentoStatus = socialPerfilDto.sp_relacionamentoStatus;

            if (socialPerfil.sp_verificado != socialPerfilDto.sp_verificado)
                socialPerfil.sp_verificado = socialPerfilDto.sp_verificado;

            if (socialPerfilDto.sp_numeroConexoes != 0)
                if (socialPerfil.sp_numeroConexoes != socialPerfilDto.sp_numeroConexoes)
                    socialPerfil.sp_numeroConexoes = socialPerfilDto.sp_numeroConexoes;

            if (socialPerfil.cli_id != socialPerfilDto.cli_id)
                socialPerfil.cli_id = socialPerfilDto.cli_id;

            return socialPerfil;
        }

    }
}