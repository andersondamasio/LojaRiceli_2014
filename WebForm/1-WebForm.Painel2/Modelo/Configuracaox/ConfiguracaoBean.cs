using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Configuracaox
{
    public class ConfiguracaoBean
    {
        public string con_emailRecuperarSenha { get; set; }
        public string con_emailPedidoRecebido { get; set; }
        public string con_emailPedidoStatus { get; set; }
    }
}