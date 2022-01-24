using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Painel.UsuarioX;

namespace _2_Library.Dao.LojaX
{
    public class LojaDto
    {
        public int loj_id { get; set; }
        public string loj_nome { get; set; }
        public string loj_dominio { get; set; }
        public bool loj_subdominio { get; set; }
        public string loj_email { get; set; }
        public string loj_cep { get; set; }
        public string loj_mensagemErro { get; set; }
        public DateTime loj_dataHoraAtualizacao { get; set; }
        public DateTime loj_dataHora { get; set; }
        public bool loj_bloquear { get; set; }
        public List<UsuarioDto> usuarioDto { get; set; } 
    }
}
