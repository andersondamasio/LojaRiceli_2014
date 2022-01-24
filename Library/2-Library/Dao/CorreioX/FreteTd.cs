using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Modelo;
using _2_Library.ServiceReferenceCalcPrecoPrazo;

namespace _2_Library.Dao.CorreioX
{
    public class FreteTd
    {
        public List<FreteDto> SelectFrete(string fre_servico, string fre_cepOrigem, string fre_cepDestino, decimal fre_peso)
        {
         int fre_cepDestinoAux = Convert.ToInt32(fre_cepDestino);

         return new FreteDao().SelectFrete(fre_servico, fre_cepOrigem, fre_cepDestinoAux, fre_peso);
        }

        public void UpdateFrete(FreteDto freteDto)
        {
            using (FreteDao fre = new FreteDao()){

                FRETE frete = fre.Select(s => s.FRE_ID == freteDto.fre_id).FirstOrDefault();
                frete.FRE_PRAZO = frete.FRE_PRAZO;
                frete.FRE_VALOR = frete.FRE_VALOR;
                fre.Update(frete);
            }
        }

        public void UpdateFrete(List<FreteDto> freteDto)
        {
            foreach (FreteDto fr in freteDto)
            {
                using (FreteDao fre = new FreteDao())
                {
                    FRETE frete = fre.Select(s => s.FRE_ID == fr.fre_id).FirstOrDefault();
                    frete.FRE_PRAZO = frete.FRE_PRAZO;
                    frete.FRE_VALOR = frete.FRE_VALOR;
                    fre.Update(frete);
                }
            }
        }


       /* # Código dos Serviços dos Correios  #
#    FRETE PAC = 41106       #
#    FRETE SEDEX = 40010       #
#    FRETE SEDEX 10 = 40215       #
#    FRETE SEDEX HOJE = 40290    #
#    FRETE E-SEDEX = 81019       #
#    FRETE MALOTE = 44105       #
#    FRETE NORMAL = 41017       #
#   SEDEX A COBRAR = 40045       #*/

        public void UpdateAllFrete(string fre_servico)
        {
            cResultado resultado = new cResultado();
            CorreioDto correio = new CorreioDto();
            List<FRETE> fretes = new List<FRETE>();

            int carm_totalLargura = 0;
            int carm_totalAltura = 0;
            int carm_totalComprimento = 0;
            int carm_totalDiametro = 0;

            //define os tamanho minimos nescessarios para passar no calculo do correio
            if (carm_totalLargura < 11)
                carm_totalLargura = 11;

            if (carm_totalAltura < 2)
                carm_totalAltura = 2;

            if (carm_totalComprimento < 16)
                carm_totalComprimento = 16;


            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "binding1";

            EndpointAddress endPointAddress = new EndpointAddress("http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx");

            using (FreteDao freDao = new FreteDao())
            {

                if (fre_servico == "41106")
                {
                    fretes = freDao.Select(s => s.FRE_SERVICO == fre_servico).Where(s => s.FRE_NOME == "PAC").ToList();// .Where(s=>s.FRE_DATAHORAATUALIZACAO.HasValue).ToList();
                }
                else
                {
                    if (fre_servico == "40010")
                    {
                       fretes = freDao.Select(s => s.FRE_SERVICO == fre_servico).Where(s => s.FRE_NOME == "Sedex").ToList();
                    }
                }


                foreach (FRETE frete in fretes)
                {
                    using (CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient(binding, endPointAddress))
                    {
                        resultado = calcPrecoPrazoWSSoapClient.CalcPrecoPrazo(string.Empty, string.Empty, fre_servico, frete.FRE_CEPORIGEM, frete.FRE_CEP_DET_REF.ToString(), frete.FRE_PESO.ToString(), 1, carm_totalComprimento, carm_totalAltura, carm_totalLargura, carm_totalDiametro, "N", 0, "N");
                    }


                    foreach (cServico cS in resultado.Servicos)
                    {
                        if (cS.PrazoEntrega != "0" && cS.Valor != "0" &&  string.IsNullOrEmpty(cS.MsgErro))
                        {
                            frete.FRE_PRAZO = Convert.ToInt32(cS.PrazoEntrega);
                            frete.FRE_VALOR = Convert.ToDecimal(cS.Valor);
                            frete.FRE_CEPINVALIDO = false;
                            frete.FRE_MSGERRO = null;
                            frete.FRE_DATAHORAATUALIZACAO = DateTime.Now;
                            freDao.Update(frete);
                        }
                        else {
                            frete.FRE_CEPINVALIDO = true;
                            if(!string.IsNullOrEmpty(cS.MsgErro))
                                frete.FRE_MSGERRO = cS.MsgErro.Length > 400 ? cS.MsgErro.Substring(0,400) : cS.MsgErro;
                            freDao.Update(frete);
                        
                        }
                    }

                    //System.Threading.Thread.Sleep(5000); 
                }
            }

        }

    }
}
