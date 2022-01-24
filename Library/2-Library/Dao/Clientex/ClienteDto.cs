using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.Clientex
{
    public class ClienteDto
    {
        public int cli_id { get; set; }
        public string cli_email { get; set; }
        public string cli_senha { get; set; }
        public string cli_cpfCnpj { get; set; }
        public string cli_nome { get; set; }
        public string cli_sobrenome { get; set; }
        public DateTime? cli_dataNascimento { get; set; }
        public string cli_sexo { get; set; }
        public string cli_cep { get; set; }
        public string cli_endereco { get; set; }
        public string cli_numero { get; set; }
        public string cli_complemento { get; set; }
        public string cli_bairro { get; set; }
        public string cli_cidade { get; set; }
        public string cli_estado { get; set; }
        public string cli_referencia { get; set; }
        public string cli_ddd1 { get; set; }
        public string cli_fone1 { get; set; }
        public string cli_ddd2 { get; set; }
        public string cli_fone2 { get; set; }
        public string cli_ddd3 { get; set; }
        public string cli_fone3 { get; set; }
        public string cli_rg { get; set; }
        public bool cli_recebeInformativo { get; set; }
        public string cli_chave { get; set; }
        public bool cli_bloquear { get; set; }
        public int loj_id { get; set; }

        public string cli_razaoSocial { get; set; }
        public string cli_inscricaoEstadual { get; set; }
        public bool cli_inscricaoEstadualIsento { get; set; }


    }
}
