using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.SocialConfigX
{
    public class SocialConfigDto
    {
        public int sc_id { get; set; }
        public string sc_idApp { get; set; }
        public string sc_secretApp { get; set; }
        public string sc_nome { get; set; }
        public string sc_icone { get; set; }
        public bool sc_bloquear { get; set; }

    }
}
