using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Clientex
{
    public class ClienteDao
    {

        private Int32 loja_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

        public ClienteLogin SelecionarClienteLogin(string cli_email, string cli_senha)
        {
            ClienteLogin clienteLogin = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                clienteLogin = (from cli in lojaEntities.Cliente
                               where cli.cli_email == cli_email && cli.cli_senha == cli_senha &&
                               cli.loj_id == loja_id
                               select new ClienteLogin
                               {
                                   cli_id = cli.cli_id,
                                   cli_cep = cli.cli_cep
                               }).SingleOrDefault();
            }
            return clienteLogin;
        }

        public ClienteLogin SelecionarClienteLogin(int cli_id)
        {
            ClienteLogin clienteLogin = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                clienteLogin = (from cli in lojaEntities.Cliente
                                where cli.cli_id == cli_id && cli.loj_id == loja_id
                                select new ClienteLogin
                                {
                                    cli_id = cli.cli_id,
                                    cli_cep = cli.cli_cep
                                }).SingleOrDefault();
            }
            return clienteLogin;
        }

        public ClienteRecuperarSenha SelecionarClienteRecuperarSenha(string cli_email)
        {
            ClienteRecuperarSenha clienteRecuperarSenha = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                clienteRecuperarSenha = (from cli in lojaEntities.Cliente
                                         where cli.cli_email == cli_email && cli.loj_id == loja_id
                               select new ClienteRecuperarSenha
                                {
                                   cli_nome = cli.cli_nome,
                                   cli_email = cli_email
                                }).SingleOrDefault();
            }
            return clienteRecuperarSenha;
        }

        public Cliente SelecionarCliente(int cli_id)
        {
            Cliente cliente = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                cliente = (from cli in lojaEntities.Cliente
                           where cli.cli_id == cli_id && cli.loj_id == loja_id
                           select cli).SingleOrDefault();
            }
            return cliente;
        }

        public ClienteBean SelecionarCliente(string cli_email)
        {
            ClienteBean clienteBean = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                clienteBean = (from cli in lojaEntities.Cliente
                               where cli.cli_email == cli_email && cli.loj_id == loja_id
                           select new ClienteBean { 
                           cli_id = cli.cli_id
                           }).SingleOrDefault();
            }
            return clienteBean;
        }

        public int SelecionarClienteCount(string cli_email, int excetoCli_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            int count = (from cli in lojaEntities.Cliente
                              where cli.cli_email == cli_email && cli.loj_id == loja_id
                              && cli.cli_id != excetoCli_id
                              select cli).Count();
            return count;
        }

        public Boolean SelecionarClienteExiste(int cli_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Boolean existe = (from cli in lojaEntities.Cliente
                              where cli.cli_id == cli_id && cli.loj_id == loja_id
                              select cli).Count() > 0;

            return existe;

        }

        public Boolean SelecionarClienteExiste(string cli_email)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Boolean existe = (from cli in lojaEntities.Cliente
                              where cli.cli_email == cli_email && cli.loj_id == loja_id
                              select cli).Count() > 0;

            return existe;

        }

        public ClienteEnderecoAdicional SelecionarClienteEnderecoAdicional(int cliEnd_id)
        {
            ClienteEnderecoAdicional clienteEnderecoAdicional = null;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                clienteEnderecoAdicional = (from cliEndAd in lojaEntities.ClienteEnderecoAdicional
                                            where cliEndAd.cliEnd_id == cliEnd_id
                                            select cliEndAd).SingleOrDefault();
            }
            return clienteEnderecoAdicional;
        }

        public void AtualizarSenha(string cli_email, string senha) {

            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Cliente cliente = (from cli in lojaEntities.Cliente
                                   where cli.cli_email == cli_email && cli.loj_id == loja_id
                                   select cli).SingleOrDefault();
                cliente.cli_senha = senha;

                lojaEntities.SaveChanges();
            }

        }
    }
}