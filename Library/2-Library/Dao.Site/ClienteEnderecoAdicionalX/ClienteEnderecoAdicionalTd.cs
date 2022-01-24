using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.ClienteEnderecoAdicionalX
{
    public class ClienteEnderecoAdicionalTd
    {
        public List<ClienteEnderecoAdicionalDto> SelectAllClienteEnderecoAdicional(string loj_dominio, int cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            return new ClienteEnderecoAdicionalDao().SelectAllClienteEnderecoAdicional(loj_id, cli_id);
        }

        public ClienteEnderecoAdicionalDto SelectClienteEnderecoAdicional(string loj_dominio, int cliEnd_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            return new ClienteEnderecoAdicionalDao().SelectClienteEnderecoAdicional(loj_id, cliEnd_id);
                
        }

        public void UpdateClienteEnderecoAdicional(ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto)
        {
            if (clienteEnderecoAdicionalDto.loj_id == 0)
            {
                clienteEnderecoAdicionalDto.loj_id = new LojaTd().SelectLoja(null).loj_id;
            }

            using (IRepositorio<ClienteEnderecoAdicional> rep = new ClienteEnderecoAdicionalDao())
            {
                ClienteEnderecoAdicional cliEndAd = rep.Select().Where(s => s.cliEnd_id == clienteEnderecoAdicionalDto.cliEnd_id && s.loj_id == clienteEnderecoAdicionalDto.loj_id).FirstOrDefault();

                cliEndAd = toClienteEnderecoAdicional(cliEndAd, clienteEnderecoAdicionalDto);

                rep.Update(cliEndAd);
            }
        }

        public void RemoveClienteEnderecoAdicional(ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto)
        {
            if (clienteEnderecoAdicionalDto.loj_id == 0)
            {
                clienteEnderecoAdicionalDto.loj_id = new LojaTd().SelectLoja(null).loj_id;
            }

            using (IRepositorio<ClienteEnderecoAdicional> rep = new ClienteEnderecoAdicionalDao())
            {
                ClienteEnderecoAdicional cliEndAd = rep.Select().Where(s => s.cliEnd_id == clienteEnderecoAdicionalDto.cliEnd_id && s.loj_id == clienteEnderecoAdicionalDto.loj_id).FirstOrDefault();

                rep.Remove(cliEndAd);
            }
        }

        public void InsertClienteEnderecoAdicional(ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto)
        {
            if (clienteEnderecoAdicionalDto.loj_id == 0)
            {
                clienteEnderecoAdicionalDto.loj_id = new LojaTd().SelectLoja(null).loj_id;
            }

            using (IRepositorio<ClienteEnderecoAdicional> rep = new ClienteEnderecoAdicionalDao())
            {
                ClienteEnderecoAdicional cliEndAd = new ClienteEnderecoAdicional();

                cliEndAd = toClienteEnderecoAdicional(cliEndAd, clienteEnderecoAdicionalDto);

                rep.Add(cliEndAd);
            }
        }

        private ClienteEnderecoAdicional toClienteEnderecoAdicional(ClienteEnderecoAdicional cliEndAd, ClienteEnderecoAdicionalDto cliEndAdDto)
        {

            if (cliEndAd.loj_id != cliEndAdDto.loj_id)
                cliEndAd.loj_id = cliEndAdDto.loj_id;

            if (cliEndAd.cli_id != cliEndAdDto.cli_id && cliEndAdDto.cli_id != 0)
                cliEndAd.cli_id = cliEndAdDto.cli_id;

            if (cliEndAd.cliEnd_nome != cliEndAdDto.cliEnd_nome)
                cliEndAd.cliEnd_nome = cliEndAdDto.cliEnd_nome;

            if (cliEndAd.cliEnd_sobrenome != cliEndAdDto.cliEnd_sobrenome)
                cliEndAd.cliEnd_sobrenome = cliEndAdDto.cliEnd_sobrenome;

            if (cliEndAd.cliEnd_cep != cliEndAdDto.cliEnd_cep)
                cliEndAd.cliEnd_cep = cliEndAdDto.cliEnd_cep;

            if (cliEndAd.cliEnd_endereco != cliEndAdDto.cliEnd_endereco)
                cliEndAd.cliEnd_endereco = cliEndAdDto.cliEnd_endereco;

            if (cliEndAd.cliEnd_numero != cliEndAdDto.cliEnd_numero)
                cliEndAd.cliEnd_numero = cliEndAdDto.cliEnd_numero;

            if (cliEndAd.cliEnd_complemento != cliEndAdDto.cliEnd_complemento)
                cliEndAd.cliEnd_complemento = cliEndAdDto.cliEnd_complemento;

            if (cliEndAd.cliEnd_referencia != cliEndAdDto.cliEnd_referencia)
                cliEndAd.cliEnd_referencia = cliEndAdDto.cliEnd_referencia;

            if (cliEndAd.cliEnd_bairro != cliEndAdDto.cliEnd_bairro)
                cliEndAd.cliEnd_bairro = cliEndAdDto.cliEnd_bairro;

            if (cliEndAd.cliEnd_cidade != cliEndAdDto.cliEnd_cidade)
                cliEndAd.cliEnd_cidade = cliEndAdDto.cliEnd_cidade;

            if (cliEndAd.cliEnd_estado != cliEndAdDto.cliEnd_estado)
                cliEndAd.cliEnd_estado = cliEndAdDto.cliEnd_estado;

            if (cliEndAd.cliEnd_ddd1 != cliEndAdDto.cliEnd_ddd1)
                cliEndAd.cliEnd_ddd1 = cliEndAdDto.cliEnd_ddd1;

            if (cliEndAd.cliEnd_fone1 != cliEndAdDto.cliEnd_fone1)
                cliEndAd.cliEnd_fone1 = cliEndAdDto.cliEnd_fone1;

            if (cliEndAd.cliEnd_ddd2 != cliEndAdDto.cliEnd_ddd2)
                cliEndAd.cliEnd_ddd2 = cliEndAdDto.cliEnd_ddd2;

            if (cliEndAd.cliEnd_fone2 != cliEndAdDto.cliEnd_fone2)
                cliEndAd.cliEnd_fone2 = cliEndAdDto.cliEnd_fone2;

            if (cliEndAd.cliEnd_ddd3 != cliEndAdDto.cliEnd_ddd3)
                cliEndAd.cliEnd_ddd3 = cliEndAdDto.cliEnd_ddd3;

            if (cliEndAd.cliEnd_fone3 != cliEndAdDto.cliEnd_fone3)
                cliEndAd.cliEnd_fone3 = cliEndAdDto.cliEnd_fone3;

            if (cliEndAd.cliEnd_sexo != cliEndAdDto.cliEnd_sexo)
                cliEndAd.cliEnd_sexo = cliEndAdDto.cliEnd_sexo;

           
            return cliEndAd;
        }

    }
}
