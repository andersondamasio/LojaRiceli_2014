using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Site.ClienteEnderecoAdicionalX
{
    public class ClienteEnderecoAdicionalDto
    {
        public int cliEnd_id { get; set; }
        public int cli_id { get; set; }
        public string cliEnd_cpfCnpj { get; set; }
        public string cliEnd_nome { get; set; }
        public string cliEnd_sobrenome { get; set; }
        public string cliEnd_email { get; set; }
        public string cliEnd_cep { get; set; }
        public string cliEnd_endereco { get; set; }
        public string cliEnd_numero { get; set; }
        public string cliEnd_complemento { get; set; }
        public string cliEnd_referencia { get; set; }
        public string cliEnd_bairro { get; set; }
        public string cliEnd_cidade { get; set; }
        public string cliEnd_estado { get; set; }
        public string cliEnd_ddd1 { get; set; }
        public string cliEnd_fone1 { get; set; }
        public string cliEnd_ddd2 { get; set; }
        public string cliEnd_fone2 { get; set; }
        public string cliEnd_ddd3 { get; set; }
        public string cliEnd_fone3 { get; set; }
        public string cliEnd_sexo { get; set; }
        public int loj_id { get; set; }
    }
}
