using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.UsuarioX
{
    public class UsuarioDto
    {
        public int usu_id { get; set; }
        public string usu_nome { get; set; }
        public string usu_senha { get; set; }
        public bool usuPer_usuarioSelecionar { get; set; }
        public bool usuPer_pedidoSelecionar { get; set; }
        public bool usuPer_lojaSelecionar { get; set; }
        public bool usuPer_lojaInserir { get; set; }
        public bool usuPer_lojaBloquear { get; set; }
        public string loj_dominio { get; set; }
        public DateTime usu_dataHora { get; set; }
        public int loj_id { get; set; }
    }
}
