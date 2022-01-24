using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.Clientex
{

    internal class ClienteDao : Repositorio<Cliente>
    {
        /// <summary>
        /// Seleciona o Cliente para o processo de login
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="cli_email"></param>
        /// <param name="cli_senha"></param>
        /// <returns>Apenas cli_nome, cli_email e cli_senha</returns>
        public ClienteDto SelectClienteLogin(int loj_id, string cli_email, string cli_senha)
        {
            ClienteDto cliente = (from cli in Select()
                                  where
                                  cli.cli_email == cli_email &&
                                  cli.cli_senha == cli_senha &&
                                  cli.loj_id == loj_id &&
                                  !cli.cli_excluido.HasValue
                                  select new ClienteDto
                                  {
                                      cli_id = cli.cli_id,
                                      cli_nome = cli.cli_nome,
                                      cli_email = cli.cli_email,
                                      sp_id = cli.SocialPerfil.FirstOrDefault().sp_id,
                                      cli_senha = cli.cli_senha
                                  }).FirstOrDefault();

            return cliente;
        }

        /// <summary>
        /// Seleciona o cliente por email
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="cli_email"></param>
        /// <returns>Se true, Cliente já existe</returns>
        public ClienteDto SelectCliente(int loj_id, string cli_email)
        {
            ClienteDto cliente = (from cli in Select()
                                  where
                                  cli.cli_email == cli_email &&
                                  cli.loj_id == loj_id &&
                                  !cli.cli_excluido.HasValue
                                  select new ClienteDto
                                  {
                                      cli_id = cli.cli_id,
                                      cli_email = cli.cli_email,
                                      cli_cpfCnpj = cli.cli_cpfCnpj,
                                      cli_nome = cli.cli_nome,
                                      cli_sobrenome = cli.cli_sobrenome,
                                      cli_dataNascimento = cli.cli_dataNascimento,
                                      cli_sexo = cli.cli_sexo,
                                      cli_cep = cli.cli_cep,
                                      cli_endereco = cli.cli_endereco,
                                      cli_numero = cli.cli_numero,
                                      cli_complemento = cli.cli_complemento,
                                      cli_bairro = cli.cli_bairro,
                                      cli_cidade = cli.cli_cidade,
                                      cli_estado = cli.cli_estado,
                                      cli_referencia = cli.cli_referencia,
                                      cli_ddd1 = cli.cli_ddd1,
                                      cli_fone1 = cli.cli_fone1,
                                      cli_ddd2 = cli.cli_ddd2,
                                      cli_fone2 = cli.cli_fone2,
                                      cli_ddd3 = cli.cli_ddd3,
                                      cli_fone3 = cli.cli_fone3,
                                      cli_recebeInformativo = cli.cli_recebeInformativo,
                                      cli_bloquear = cli.cli_bloquear,
                                      cli_razaoSocial = cli.cli_razaoSocial,
                                      cli_inscricaoEstadual = cli.cli_inscricaoEstadual,
                                      cli_inscricaoEstadualIsento = cli.cli_inscricaoEstadualIsento,

                                  }).FirstOrDefault();
            return cliente;
        }

        /// <summary>
        /// Seleciona o cliente por ID
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="cli_id"></param>
        /// <returns></returns>
        public ClienteDto SelectCliente(int loj_id, int cli_id)
        {
            ClienteDto cliente = (from cli in Select()
                                  where
                                  cli.cli_id == cli_id &&
                                  cli.loj_id == loj_id &&
                                  !cli.cli_excluido.HasValue
                                  select new ClienteDto
                                  {
                                      cli_id = cli.cli_id,
                                      cli_email = cli.cli_email,
                                      cli_cpfCnpj = cli.cli_cpfCnpj,
                                      cli_nome = cli.cli_nome,
                                      cli_sobrenome = cli.cli_sobrenome,
                                      cli_dataNascimento = cli.cli_dataNascimento,
                                      cli_sexo = cli.cli_sexo,
                                      cli_cep = cli.cli_cep,
                                      cli_endereco = cli.cli_endereco,
                                      cli_numero = cli.cli_numero,
                                      cli_complemento = cli.cli_complemento,
                                      cli_bairro = cli.cli_bairro,
                                      cli_cidade = cli.cli_cidade,
                                      cli_estado = cli.cli_estado,
                                      cli_referencia = cli.cli_referencia,
                                      cli_ddd1 = cli.cli_ddd1,
                                      cli_fone1 = cli.cli_fone1,
                                      cli_ddd2 = cli.cli_ddd2,
                                      cli_fone2 = cli.cli_fone2,
                                      cli_ddd3 = cli.cli_ddd3,
                                      cli_fone3 = cli.cli_fone3,
                                      cli_recebeInformativo = cli.cli_recebeInformativo,
                                      cli_bloquear = cli.cli_bloquear,
                                      cli_razaoSocial = cli.cli_razaoSocial,
                                      cli_inscricaoEstadual = cli.cli_inscricaoEstadual,
                                      cli_inscricaoEstadualIsento = cli.cli_inscricaoEstadualIsento,
                                      sp_id = cli.SocialPerfil.FirstOrDefault().sp_id,
                                      loj_id = cli.loj_id

                                  }).FirstOrDefault();
            return cliente;
        }

        /// <summary>
        /// Verifica a existencia de um Cliente
        /// </summary>
        /// <param name="loj_id"></param>
        /// <param name="cli_email"></param>
        /// <returns></returns>
        public bool SelectClienteExiste(int loj_id, string cli_email)
        {
            bool cliente = (from cli in Select()
                            where
                            cli.cli_email == cli_email &&
                            cli.loj_id == loj_id &&
                            !cli.cli_excluido.HasValue
                            select cli).Count() > 0;

            return cliente;
        }

    }
}