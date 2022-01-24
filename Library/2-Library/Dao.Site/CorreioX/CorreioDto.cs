using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.CorreioX
{
    public class CorreioDto
    {
        public int co_codigo { get; set; }
        public string co_valor { get; set; }
        public string co_prazoEntrega { get; set; }
        public string co_valorMaoPropria { get; set; }
        public string co_valorAvisoRecebimento { get; set; }
        public string co_valorValorDeclarado { get; set; }
        public string co_entregaDomiciliar { get; set; }
        public string co_entregaSabado { get; set; }

        public string co_cidade { get; set; }
        public string co_estado { get; set; }
        public string co_endereco { get; set; }
        public string co_complemento { get; set; }
        public string co_bairro { get; set; }

        public string co_erro { get; set; }
        public string co_msgErro { get; set; }
    }
   
}
