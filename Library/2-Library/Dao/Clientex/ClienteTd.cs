using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.Clientex
{

    public class ClienteTd
    {
        /// <summary>
        /// Seleciona o Cliente para o processo de login
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="cli_email"></param>
        /// <param name="cli_senha"></param>
        /// <returns>Apenas cli_nome, cli_email e cli_senha</returns>
        public ClienteDto SelectClienteLogin(string loj_dominio, string cli_email, string cli_senha)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            cli_senha = Recursos.Hash(cli_senha);

            ClienteDto clienteDto = new ClienteDao().SelectClienteLogin(loj_id, cli_email, cli_senha);

            return clienteDto;
        }

        /// <summary>
      /// Seleciona um Cliente por email
      /// </summary>
      /// <param name="loj_dominio"></param>
      /// <param name="cli_email"></param>
      /// <returns>ClienteDto</returns>
        public ClienteDto SelectCliente(string loj_dominio, string cli_email)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            ClienteDto clienteDto = new ClienteDao().SelectCliente(loj_id, cli_email);

            return clienteDto;
        }

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


        /// <summary>
        /// Seleciona um cliente
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="clienteDto"></param>
        /// <returns>Se true, Cliente já existe</returns>
        public ClienteDto InsertCliente(string loj_dominio, ClienteDto clienteDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            using (ClienteDao clienteDao = new ClienteDao())
            {
                if (clienteDao.Select().Where(s => s.cli_email == clienteDto.cli_email && s.loj_id == loj_id && !s.cli_excluido.HasValue).Count() == 0)
                {
                    Cliente cliente = ToCliente(loj_id, new Cliente(), clienteDto);
                    clienteDao.Add(cliente);
                    clienteDto.cli_id = cliente.cli_id;
                    clienteDto.cli_email = cliente.cli_email;
                    return clienteDto;
                }
                else return null;
            }
        }



        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="clienteDto"></param>
        public void UpdateCliente(string loj_dominio, ClienteDto clienteDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            Cliente cliente = new Cliente();

            using (IRepositorio<Cliente> repCli = new ClienteDao())
            {
              cliente = repCli.Select().Where(s => s.cli_email == clienteDto.cli_email && s.loj_id == loj_id && !s.cli_excluido.HasValue).FirstOrDefault();

              cliente = ToCliente(loj_id, cliente, clienteDto);

              repCli.Update(cliente);

            }
           
        }

        /// <summary>
        /// Altera a senha de um cliente
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="cli_email"></param>
        /// <param name="cli_senha">Nova senha</param>
        public void UpdateClienteSenha(string loj_dominio, string cli_email, string cli_senha)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            Cliente cliente = null;
            using (IRepositorio<Cliente> repCli = new ClienteDao())
            {
                cliente = repCli.Select().Where(s => s.cli_email == cli_email && s.loj_id == loj_id && !s.cli_excluido.HasValue).FirstOrDefault();

                if (!string.IsNullOrEmpty(cli_senha))
                    cliente.cli_senha = Recursos.Hash(cli_senha);

                repCli.Update(cliente);
            }     
        }

        /// <summary>
        /// Verifica a existencia de um Cliente
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="cli_email"></param>
        /// <returns></returns>
        public bool SelectClienteExiste(string loj_dominio, string cli_email)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            bool clienteDto = new ClienteDao().SelectClienteExiste(loj_id, cli_email);

            return clienteDto;
        }

        private Cliente ToCliente(int loj_id, Cliente cliente, ClienteDto clienteDto)
        {

            Type type = typeof(ClienteDto);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                object value = pi.GetValue(clienteDto, null);
                if (value != null && value.GetType().Name == "String")
                {
                    string novoValue = value.ToString().Trim();
                    novoValue = (novoValue == string.Empty) ? null : novoValue;
                    pi.SetValue(clienteDto, novoValue);
                }
            }


            if (cliente.loj_id != loj_id)
                cliente.loj_id = loj_id;

            if (cliente.cli_email != clienteDto.cli_email)
                cliente.cli_email = clienteDto.cli_email;

            if (!string.IsNullOrEmpty(clienteDto.cli_senha))
                cliente.cli_senha = Recursos.Hash(clienteDto.cli_senha);

            if (cliente.cli_cpfCnpj != clienteDto.cli_cpfCnpj)
                cliente.cli_cpfCnpj = clienteDto.cli_cpfCnpj;

            if (cliente.cli_nome != clienteDto.cli_nome)
                cliente.cli_nome = clienteDto.cli_nome;

            if (cliente.cli_sobrenome != clienteDto.cli_sobrenome)
                cliente.cli_sobrenome = clienteDto.cli_sobrenome;

            if (cliente.cli_dataNascimento != clienteDto.cli_dataNascimento)
                cliente.cli_dataNascimento = clienteDto.cli_dataNascimento;

            if (cliente.cli_sexo != clienteDto.cli_sexo)
                cliente.cli_sexo = clienteDto.cli_sexo;

            if (cliente.cli_cep != clienteDto.cli_cep)
                cliente.cli_cep = clienteDto.cli_cep;

            if (cliente.cli_endereco != clienteDto.cli_endereco)
                cliente.cli_endereco = clienteDto.cli_endereco;

            if (cliente.cli_numero != clienteDto.cli_numero)
                cliente.cli_numero = clienteDto.cli_numero;

            if (cliente.cli_complemento != clienteDto.cli_complemento)
                cliente.cli_complemento = clienteDto.cli_complemento;

            if (cliente.cli_bairro != clienteDto.cli_bairro)
                cliente.cli_bairro = clienteDto.cli_bairro;

            if (cliente.cli_cidade != clienteDto.cli_cidade)
                cliente.cli_cidade = clienteDto.cli_cidade;

            if (cliente.cli_estado != clienteDto.cli_estado)
                cliente.cli_estado = clienteDto.cli_estado;

            if (cliente.cli_referencia != clienteDto.cli_referencia)
                cliente.cli_referencia = clienteDto.cli_referencia;

            if (cliente.cli_ddd1 != clienteDto.cli_ddd1)
                cliente.cli_ddd1 = clienteDto.cli_ddd1;

            if (cliente.cli_fone1 != clienteDto.cli_fone1)
                cliente.cli_fone1 = clienteDto.cli_fone1;

            if (cliente.cli_ddd2 != clienteDto.cli_ddd2)
                cliente.cli_ddd2 = clienteDto.cli_ddd2;

            if (cliente.cli_fone2 != clienteDto.cli_fone2)
                cliente.cli_fone2 = clienteDto.cli_fone2;

            if (cliente.cli_ddd3 != clienteDto.cli_ddd3)
                cliente.cli_ddd3 = clienteDto.cli_ddd3;

            if (cliente.cli_fone3 != clienteDto.cli_fone3)
                cliente.cli_fone3 = clienteDto.cli_fone3;

            if (cliente.cli_recebeInformativo != clienteDto.cli_recebeInformativo)
                cliente.cli_recebeInformativo = clienteDto.cli_recebeInformativo;

            if (cliente.cli_bloquear != clienteDto.cli_bloquear)
                cliente.cli_bloquear = clienteDto.cli_bloquear;

            //para juridica
            if (cliente.cli_razaoSocial != clienteDto.cli_razaoSocial)
                cliente.cli_razaoSocial = clienteDto.cli_razaoSocial;

            if (cliente.cli_inscricaoEstadual != clienteDto.cli_inscricaoEstadual)
                cliente.cli_inscricaoEstadual = clienteDto.cli_inscricaoEstadual;

            if (cliente.cli_inscricaoEstadualIsento != clienteDto.cli_inscricaoEstadualIsento)
                cliente.cli_inscricaoEstadualIsento = clienteDto.cli_inscricaoEstadualIsento;

            return cliente;
        }
    
    }
}
