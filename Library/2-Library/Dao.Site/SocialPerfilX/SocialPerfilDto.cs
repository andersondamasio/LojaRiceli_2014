using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.ClienteSocialX
{
    public class SocialPerfilDto
    {
        public int sp_id { get; set; }
        public string sp_idPerfil { get; set; }
        public string sp_nome { get; set; }
        public string sp_sobrenome { get; set; }
        public string sp_sexo { get; set; }
        public DateTime? sp_dataNascimento { get; set; }
        public string sp_email { get; set; }
        public string sp_cidade { get; set; }
        public string sp_estado { get; set; }
        public string sp_idioma { get; set; }
        public string sp_amigos { get; set; }
        public string sp_amigosApp { get; set; }
        public string sp_interesses { get; set; }
        public string sp_atividades { get; set; }
        public string sp_curtidas { get; set; }
        public string sp_trabalho { get; set; }
        public string sp_sobre { get; set; }
        public string sp_site { get; set; }
        public string sp_religiao { get; set; }
        public string sp_relacionamentoStatus { get; set; }
        public bool? sp_verificado { get; set; }
        public int sp_numeroConexoes { get; set; }
        public int? cli_id { get; set; }
        public DateTime sp_dataHora { get; set; }
        public List<SocialConexaoDto> socialConexaoDto { get; set; }
        
    }
    public class SocialConexaoDto
    {
        public int sp_id_conexao { get; set; }
        public int sp_id_conectado { get; set; }
        public bool sco_ativo { get; set; }
    }
}
