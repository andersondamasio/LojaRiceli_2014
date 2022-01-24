using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.SocialConfigX
{
    internal class SocialConfigDao : Repositorio<SocialConfig>
    {
        public SocialConfigDto SelectSocialPerfil(int loj_id)
        {
            SocialConfigDto socialConfigDto = (from sc in Select()
                                               where
                                               sc.loj_id == loj_id
                                               select new SocialConfigDto
                                               {
                                                   sc_id = sc.sc_id,
                                                   sc_idApp = sc.sc_idApp,
                                                   sc_secretApp = sc.sc_secretApp,
                                                   sc_nome = sc.sc_nome,
                                                   sc_icone = sc.sc_icone,
                                                   sc_bloquear = sc.sc_bloquear
                                               }).FirstOrDefault();
            return socialConfigDto;
        }
    }
}
