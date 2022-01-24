using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Painel.ClienteX
{
    internal class ClienteDao : Repositorio<Cliente>
    {

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
                                      cli_nome = cli.cli_nome,
                                      cli_senha = cli.cli_senha

                                  }).FirstOrDefault();
            return cliente;
        }

    }
}
