using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.SocialConfigX
{
    public class SocialConfigTd
    {
        public SocialConfigDto SelectSocialConfig(string loj_dominio)
        {
            SocialConfigDto socialConfigDto = null;
            if (System.Web.HttpContext.Current.Request.Url.Authority != "localhost")
            {
                int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
                using (SocialConfigDao socialConfigDao = new SocialConfigDao())
                {

                    socialConfigDto = socialConfigDao.SelectSocialPerfil(loj_id);
                }
            }
            else {
                socialConfigDto = new SocialConfigDto();
                socialConfigDto.sc_nome = "Localhost appTeste";
                socialConfigDto.sc_idApp = "605370606241659";
                socialConfigDto.sc_secretApp = "79b1f56738c234109891baa17d21528c";
            }
            return socialConfigDto;
        }

    }
}
