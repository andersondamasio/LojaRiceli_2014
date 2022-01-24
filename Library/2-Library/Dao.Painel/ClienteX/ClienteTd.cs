using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Painel.ClienteX
{
    public class ClienteTd
    {
        /// <summary>
        /// Seleciona o cliente por ID
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="cli_id"></param>
        /// <returns></returns>
        public ClienteDto SelectCliente(string loj_dominio, int cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            ClienteDto clienteDto = new ClienteDao().SelectCliente(loj_id, cli_id);

            return clienteDto;
        }
    }
}
