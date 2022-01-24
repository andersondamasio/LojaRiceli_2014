using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Clientex
{
    public class ClienteConsulta
    {
        public ClienteLogin SelecionarClienteLogin(string cli_email, string cli_senha)
        {
            return new ClienteDao().SelecionarClienteLogin(cli_email, cli_senha);
        }

        public ClienteLogin SelecionarClienteLogin(int cli_id)
        {
            return new ClienteDao().SelecionarClienteLogin(cli_id);
        }

        public ClienteRecuperarSenha SelecionarClienteRecuperarSenha(string cli_email)
        {
            return new ClienteDao().SelecionarClienteRecuperarSenha(cli_email);
        }


        public Cliente SelecionarCliente(int cli_id)
        {
            return new ClienteDao().SelecionarCliente(cli_id);
        }

        public Boolean SelecionarClienteExiste(int cli_id)
        {
            return new ClienteDao().SelecionarClienteExiste(cli_id);
        }

        public Boolean SelecionarClienteExiste(string cli_email)
        {
            return new ClienteDao().SelecionarClienteExiste(cli_email);
        }

        public int SelecionarClienteCount(string cli_email, int cli_id)
        {

            return new ClienteDao().SelecionarClienteCount(cli_email, cli_id);
        }

        public ClienteBean SelecionarCliente(string cli_email)
        {
            return new ClienteDao().SelecionarCliente(cli_email);
        }

        public ClienteEnderecoAdicional SelecionarClienteEnderecoAdicional(int cliEnd_id)
        {
            return new ClienteDao().SelecionarClienteEnderecoAdicional(cliEnd_id);
        }

        public void AtualizarSenha(string cli_email, string senha) {

            new ClienteDao().AtualizarSenha(cli_email, senha);
        }

    }
}