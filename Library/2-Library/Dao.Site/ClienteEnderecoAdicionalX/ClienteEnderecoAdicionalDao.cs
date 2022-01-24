using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ClienteEnderecoAdicionalX
{
    internal class ClienteEnderecoAdicionalDao : Repositorio<ClienteEnderecoAdicional>
    {
        public List<ClienteEnderecoAdicionalDto> SelectAllClienteEnderecoAdicional(int loj_id, int cli_id)
        {
            List<ClienteEnderecoAdicionalDto> clienteEndAdDto = (from cliEnd in Select()
                                                                 where cliEnd.cli_id == cli_id && cliEnd.loj_id == loj_id
                                                                 select new ClienteEnderecoAdicionalDto
                                                                 {
                                                                     cliEnd_id = cliEnd.cliEnd_id,
                                                                     cli_id = cliEnd.cli_id,
                                                                     cliEnd_cpfCnpj = cliEnd.cliEnd_cpfCnpj,
                                                                     cliEnd_nome = cliEnd.cliEnd_nome,
                                                                     cliEnd_sobrenome = cliEnd.cliEnd_sobrenome,
                                                                     cliEnd_cep = cliEnd.cliEnd_cep,
                                                                     cliEnd_endereco = cliEnd.cliEnd_endereco,
                                                                     cliEnd_numero = cliEnd.cliEnd_numero,
                                                                     cliEnd_complemento = cliEnd.cliEnd_complemento,
                                                                     cliEnd_referencia = cliEnd.cliEnd_referencia,
                                                                     cliEnd_bairro = cliEnd.cliEnd_bairro,
                                                                     cliEnd_cidade = cliEnd.cliEnd_cidade,
                                                                     cliEnd_estado = cliEnd.cliEnd_estado,
                                                                     cliEnd_ddd1 = cliEnd.cliEnd_ddd1,
                                                                     cliEnd_fone1 = cliEnd.cliEnd_fone1,
                                                                     cliEnd_ddd2 = cliEnd.cliEnd_ddd2,
                                                                     cliEnd_fone2 = cliEnd.cliEnd_fone2,
                                                                     cliEnd_ddd3 = cliEnd.cliEnd_ddd3,
                                                                     cliEnd_fone3 = cliEnd.cliEnd_fone3,
                                                                     cliEnd_sexo = cliEnd.cliEnd_sexo,

                                                                 }).ToList();
            return clienteEndAdDto;

        }

        public ClienteEnderecoAdicionalDto SelectClienteEnderecoAdicional(int loj_id, int cliEnd_id)
        {
            ClienteEnderecoAdicionalDto clienteEndAdDto = (from cliEnd in Select()
                                                                 where cliEnd.cliEnd_id == cliEnd_id && cliEnd.loj_id == loj_id
                                                                 select new ClienteEnderecoAdicionalDto
                                                                 {
                                                                     cliEnd_cpfCnpj = cliEnd.cliEnd_cpfCnpj,
                                                                     cliEnd_nome = cliEnd.cliEnd_nome,
                                                                     cliEnd_sobrenome = cliEnd.cliEnd_sobrenome,
                                                                     cliEnd_cep = cliEnd.cliEnd_cep,
                                                                     cliEnd_endereco = cliEnd.cliEnd_endereco,
                                                                     cliEnd_numero = cliEnd.cliEnd_numero,
                                                                     cliEnd_complemento = cliEnd.cliEnd_complemento,
                                                                     cliEnd_referencia = cliEnd.cliEnd_referencia,
                                                                     cliEnd_bairro = cliEnd.cliEnd_bairro,
                                                                     cliEnd_cidade = cliEnd.cliEnd_cidade,
                                                                     cliEnd_estado = cliEnd.cliEnd_estado,
                                                                     cliEnd_ddd1 = cliEnd.cliEnd_ddd1,
                                                                     cliEnd_fone1 = cliEnd.cliEnd_fone1,
                                                                     cliEnd_ddd2 = cliEnd.cliEnd_ddd2,
                                                                     cliEnd_fone2 = cliEnd.cliEnd_fone2,
                                                                     cliEnd_ddd3 = cliEnd.cliEnd_ddd3,
                                                                     cliEnd_fone3 = cliEnd.cliEnd_fone3,
                                                                     cliEnd_sexo = cliEnd.cliEnd_sexo,

                                                                 }).FirstOrDefault();
            return clienteEndAdDto;

        }
        

        

    }
}
