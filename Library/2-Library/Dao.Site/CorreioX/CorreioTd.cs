using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Site.CorreioX
{
    public class CorreioTd
    {
        /// <summary>
        /// Calcula o Prazo de entrega diretamente nos correios
        /// </summary>
        /// <param name="loj_dominio"></param>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem">Opcional, se null seleciona o cep cadastrado na loja</param>
        /// <param name="sCepDestino"></param>
        /// <returns></returns>
        public CorreioDto SelectCorreioCalcPrazo(string loj_dominio, string nCdServico, string sCepOrigem, string sCepDestino)
        {
            if (sCepOrigem == null)
            {
                sCepOrigem = new LojaTd().SelectLoja(loj_dominio).loj_cep;
            }

            return new CorreioDao().SelectCorreioCalcPrazo(nCdServico, sCepOrigem, sCepDestino);
        }

        public CorreioDto SelectCorreioCalcPreco(string loj_dominio, List<CarrinhoDto> carrinho, CarrinhoMedidas carrinhoMedidas, string nCdServico, string sCepOrigem, string sCepDestino)
        {
            if (sCepOrigem == null)
            {
                sCepOrigem = new LojaTd().SelectLoja(loj_dominio).loj_cep;
            }

            carrinhoMedidas = ValidarMedidas(carrinhoMedidas, nCdServico);


            CorreioDto correioDto = new CorreioDao().SelectCorreioCalcPreco(nCdServico, sCepOrigem, sCepDestino, carrinhoMedidas);

            return correioDto;
        }

        public CorreioDto SelectCorreioCalcPrecoPrazo(string loj_dominio, List<CarrinhoDto> carrinho, CarrinhoMedidas carrinhoMedidas, string nCdServico, string sCepOrigem, string sCepDestino)
        {
            if (sCepOrigem == null)
            {
                sCepOrigem = new LojaTd().SelectLoja(loj_dominio).loj_cep;
            }

            carrinhoMedidas = ValidarMedidas(carrinhoMedidas, nCdServico);

            CorreioDto correioDto = new CorreioDao().SelectCorreioCalcPrecoPrazo(nCdServico, sCepOrigem, sCepDestino, carrinhoMedidas);

            return correioDto;
        }

        public CorreioDto SelectCorreioLocalidade(string sCepDestino)
        {
            CorreioDto logra = new CorreioDao().SelectCorreioLocalidade(sCepDestino, null);

            return logra;
        }


        private CarrinhoMedidas ValidarMedidas(CarrinhoMedidas carrinhoMedidas, string nCdServico)
        {
            // se carrinhoMedidas == null, todos os itens tem frete fixo abilitado
            if (carrinhoMedidas != null)
            {
             carrinhoMedidas.carm_msgErros = new List<string>();


                //define os tamanho minimos nescessarios para passar no calculo do correio

                if (carrinhoMedidas.carm_totalLargura < 11)
                {
                    carrinhoMedidas.carm_totalLargura = 11;
                    carrinhoMedidas.carm_msgErros.Add("Largura não pode ser inferior a 11cm");
                }

                if (carrinhoMedidas.carm_totalAltura < 2)
                {
                    carrinhoMedidas.carm_totalAltura = 2;
                    carrinhoMedidas.carm_msgErros.Add("Altura não pode ser inferior a 2cm");
                }

                if (carrinhoMedidas.carm_totalComprimento < 16)
                {
                    carrinhoMedidas.carm_totalComprimento = 16;
                    carrinhoMedidas.carm_msgErros.Add("Comprimento não pode ser inferior a 16cm");
                }

                decimal somaMedidas = carrinhoMedidas.carm_totalLargura + carrinhoMedidas.carm_totalAltura + carrinhoMedidas.carm_totalComprimento;
                carrinhoMedidas.carm_totalPesoCubico = (carrinhoMedidas.carm_totalLargura * carrinhoMedidas.carm_totalAltura * carrinhoMedidas.carm_totalComprimento) / 4800;

                if (nCdServico == "41106")
                {
                    // Calculo de acrescimo por dimensao, para o PAC, caso o peso cubico seja maior que o peso
                    if (carrinhoMedidas.carm_totalPesoCubico > carrinhoMedidas.carm_totalPeso)
                        carrinhoMedidas.carm_totalPeso = carrinhoMedidas.carm_totalPesoCubico;
                }

                // limite da soma
                if (somaMedidas > 150)
                {
                    // retornar erro, ultrapassou limite da soma das dimensoes
                    carrinhoMedidas.carm_msgErros.Add("Soma das medidas ultrapassou limite 150cm");
                }
                else
                    // tamanho limite de cada dimensao
                    if (carrinhoMedidas.carm_totalLargura > 60 || carrinhoMedidas.carm_totalAltura > 60 || carrinhoMedidas.carm_totalComprimento > 60)
                    {
                        // retornar erro, maior que 60
                       carrinhoMedidas.carm_msgErros.Add("Nenhuma medida pode ser superior a 60cm");
                    }
            }
            return carrinhoMedidas;
        }
    }
}
        
            
        