using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Clientex
{
    public class ClienteBean
    {
        public int cli_id { get; set; }
    }

    public class ClienteLogin {

        public int cli_id { get; set; }
        public string cli_cep { get; set; }
    
    }

    public class ClienteRecuperarSenha
    {

        public string cli_nome { get; set; }
        public string cli_email { get; set; }
        public string cli_chave { get; set; }

    }
}