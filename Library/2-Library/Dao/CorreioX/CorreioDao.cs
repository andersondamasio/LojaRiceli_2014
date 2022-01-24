using System;
using System.Linq;
using System.ServiceModel;
using _2_Library.Config;
using _2_Library.Dao.CarrinhoX;
using _2_Library.Modelo;
using _2_Library.ServiceReferenceCalcPrecoPrazo;

namespace _2_Library.Dao.CorreioX
{
    internal class CorreioDao : RepositorioCorr<LOG_LOGRADOURO>
    {

        public CorreioDao() { }
        public CorreioDao(CorreiosEntities correiosEntities) : base(correiosEntities) { }

        /// <summary>
        /// Calcula o Prazo de entrega diretamente nos correios
        /// </summary>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <returns></returns>
        public CorreioDto SelectCorreioCalcPrazo(string nCdServico, string sCepOrigem, string sCepDestino)
        {
            cServico cServico = new cServico();
            cResultado resultado = new cResultado();
            CorreioDto correio = new CorreioDto();
            try
            {
                if (_2_Library.Utils.Static.baseLocal)
                {
                    correio = SelectCorreioLocalCalcPreco("41106", sCepOrigem, sCepDestino, 5);
                }
                else
                {
                    try
                    {
                        BasicHttpBinding binding = new BasicHttpBinding();
                        binding.Name = "binding1";

                        EndpointAddress endPointAddress = new EndpointAddress("http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx");
                        using (CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient(binding, endPointAddress))
                        {
                            resultado = calcPrecoPrazoWSSoapClient.CalcPrazo("41106", sCepOrigem, sCepDestino);
                        }


                        correio = (from cr in resultado.Servicos
                                   select new CorreioDto
                                   {
                                       co_codigo = cr.Codigo,
                                       co_valor = cr.Valor,
                                       co_prazoEntrega = cr.PrazoEntrega,
                                       co_valorMaoPropria = cr.ValorMaoPropria,
                                       co_valorAvisoRecebimento = cr.ValorAvisoRecebimento,
                                       co_valorValorDeclarado = cr.ValorValorDeclarado,
                                       co_entregaDomiciliar = cr.ValorValorDeclarado,
                                       co_entregaSabado = cr.EntregaSabado,
                                       co_erro = cr.Erro,
                                       co_msgErro = cr.MsgErro
                                   }).FirstOrDefault();


                    }
                    catch (Exception ex)
                    {
                        correio = (from cr in new FreteTd().SelectFrete("41106", sCepOrigem, sCepDestino, 5)
                                   select new CorreioDto
                                   {
                                       co_codigo = 0,
                                       co_valor = string.Empty,
                                       co_prazoEntrega = cr.fre_prazo.ToString(),
                                       co_valorMaoPropria = string.Empty,
                                       co_valorAvisoRecebimento = string.Empty,
                                       co_valorValorDeclarado = string.Empty,
                                       co_entregaDomiciliar = string.Empty,
                                       co_entregaSabado = string.Empty,
                                       co_erro = "webService fora do ar",
                                       co_msgErro = string.Empty
                                   }).FirstOrDefault();

                    }

                    CorreioDto logra = SelectCorreioLocalidade(sCepDestino, correio);
                }
            }
            catch (Exception ex)
            {

                correio.co_msgErro = ex.Message;
            }
        

            return correio;
        }

        public CorreioDto SelectCorreioCalcPreco(string nCdServico, string sCepOrigem, string sCepDestino, CarrinhoMedidas carrinhoMedidas)
        {
            cServico cServico = new cServico();
            cResultado resultado = new cResultado();
            CorreioDto correio = new CorreioDto();
            try
            {
                try
                {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Name = "binding1";

                EndpointAddress endPointAddress = new EndpointAddress("http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx");
                using (CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient(binding, endPointAddress))
                {
                    resultado = calcPrecoPrazoWSSoapClient.CalcPreco("045684", "45685427", "41106", sCepOrigem, sCepDestino, carrinhoMedidas.carm_totalPeso.ToString(), 1, carrinhoMedidas.carm_totalComprimento, carrinhoMedidas.carm_totalAltura, carrinhoMedidas.carm_totalLargura, carrinhoMedidas.carm_totalDiametro, "N", 0, "N");

                }


                 correio = (from cr in resultado.Servicos
                           select new CorreioDto
                           {
                               co_codigo = cr.Codigo,
                               co_valor = cr.Valor,
                               co_prazoEntrega = cr.PrazoEntrega,
                               co_valorMaoPropria = cr.ValorMaoPropria,
                               co_valorAvisoRecebimento = cr.ValorAvisoRecebimento,
                               co_valorValorDeclarado = cr.ValorValorDeclarado,
                               co_entregaDomiciliar = cr.ValorValorDeclarado,
                               co_entregaSabado = cr.EntregaSabado,
                               co_erro = cr.Erro,
                               co_msgErro = cr.MsgErro

                           }).FirstOrDefault();

                 }
                catch (Exception ex)
                {
                    correio = (from cr in new FreteTd().SelectFrete("41106",sCepOrigem, sCepDestino, carrinhoMedidas.carm_totalPeso)
                           select new CorreioDto
                           {
                               co_codigo = 0,
                               co_valor = cr.fre_valor.ToString(),
                               co_prazoEntrega = cr.fre_prazo.ToString(),
                               co_valorMaoPropria = string.Empty,
                               co_valorAvisoRecebimento = string.Empty,
                               co_valorValorDeclarado = string.Empty,
                               co_entregaDomiciliar = string.Empty,
                               co_entregaSabado = string.Empty,
                               co_erro = "webService fora do ar",
                               co_msgErro = string.Empty
                           }).FirstOrDefault();
                }

                SelectCorreioLocalidade(sCepDestino, correio);
            }
            catch (Exception ex)
            {

                correio.co_msgErro = ex.Message;
            }

            return correio;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <param name="carrinhoMedidas">Se </param>
        /// <returns>Se correio.co_erro = "-5" Localidade não encontrada</returns>
        public CorreioDto SelectCorreioCalcPrecoPrazo(string nCdServico, string sCepOrigem, string sCepDestino, CarrinhoMedidas carrinhoMedidas)
        {
            cServico cServico = new cServico();
            cResultado resultado = new cResultado();
            CorreioDto correio = new CorreioDto();
            
            try
            {
                if (_2_Library.Utils.Static.baseLocal)
                {
                    correio = SelectCorreioLocalCalcPreco("41106", sCepOrigem, sCepDestino, carrinhoMedidas.carm_totalPeso);
                }
                else
                {

                    try
                    {
                        BasicHttpBinding binding = new BasicHttpBinding();
                        binding.Name = "binding1";

                        EndpointAddress endPointAddress = new EndpointAddress("http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx");
                        using (CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient(binding, endPointAddress))
                        {
                            resultado = calcPrecoPrazoWSSoapClient.CalcPrecoPrazo("045684", "45685427", "41106", sCepOrigem, sCepDestino, carrinhoMedidas.carm_totalPeso.ToString(), 1, carrinhoMedidas.carm_totalComprimento, carrinhoMedidas.carm_totalAltura, carrinhoMedidas.carm_totalLargura, carrinhoMedidas.carm_totalDiametro, "N", 0, "N");
                        }

                        correio = (from cr in resultado.Servicos
                                   select new CorreioDto
                                   {
                                       co_codigo = cr.Codigo,
                                       co_valor = cr.Valor,
                                       co_prazoEntrega = cr.PrazoEntrega,
                                       co_valorMaoPropria = cr.ValorMaoPropria,
                                       co_valorAvisoRecebimento = cr.ValorAvisoRecebimento,
                                       co_valorValorDeclarado = cr.ValorValorDeclarado,
                                       co_entregaDomiciliar = cr.ValorValorDeclarado,
                                       co_entregaSabado = cr.EntregaSabado,
                                       co_erro = cr.Erro,
                                       co_msgErro = cr.MsgErro
                                   }).FirstOrDefault();

                    }
                    catch (Exception ex)
                    {
                        correio = SelectCorreioLocalCalcPreco("41106", sCepOrigem, sCepDestino, carrinhoMedidas.carm_totalPeso);
                    }

                    CorreioDto logra = SelectCorreioLocalidade(sCepDestino,correio);

                    
                }
            }
            catch (Exception ex)
            {

                correio.co_msgErro = ex.Message;
            }

            return correio;
        }

        public CorreioDto SelectCorreioLocalidade(string sCepDestino, CorreioDto correio)
        {

            CorreioDto logra = (from cr in Select()
                                where cr.CEP == sCepDestino
                                select new CorreioDto
                                {
                                    co_cidade = cr.LOG_LOCALIDADE.LOC_NO,
                                    co_estado = cr.UFE_SG,
                                    co_endereco = cr.LOG_NOME,
                                    co_complemento = cr.LOG_COMPLEMENTO,
                                    co_bairro = cr.LOG_BAIRRO.BAI_NO
                                }).FirstOrDefault();

           
                if (logra == null)
                {
                     //localidade não encontrada.
                     logra = new CorreioDto();
                     logra.co_erro = "-5";
                }

                    if (correio != null)
                        logra = new CorreioDto
                         {
                             co_codigo = correio.co_codigo,
                             co_valor = correio.co_valor,
                             co_prazoEntrega = correio.co_prazoEntrega,
                             co_valorMaoPropria = correio.co_valorMaoPropria,
                             co_valorAvisoRecebimento = correio.co_valorAvisoRecebimento,
                             co_valorValorDeclarado = correio.co_valorValorDeclarado,
                             co_entregaDomiciliar = correio.co_entregaDomiciliar,
                             co_entregaSabado = correio.co_entregaSabado,
                             co_erro = string.IsNullOrEmpty(logra.co_erro) ? correio.co_erro : logra.co_erro,
                             co_msgErro = correio.co_msgErro,
                             co_cidade = logra.co_cidade,
                             co_estado = logra.co_estado,
                             co_endereco = logra.co_endereco,
                             co_complemento = logra.co_complemento,
                             co_bairro = logra.co_bairro
                         };


            return logra;
        }


        public CorreioDto SelectCorreioLocalCalcPreco(string nCdServico, string sCepOrigem, string sCepDestino, decimal carm_totalPeso)
        {

            CorreioDto correioDto = (from cr in new FreteTd().SelectFrete("41106",sCepOrigem, sCepDestino, carm_totalPeso)
                           select new CorreioDto
                           {
                               co_codigo = 0,
                               co_valor = cr.fre_valor.ToString(),
                               co_prazoEntrega = cr.fre_prazo.ToString(),
                               co_valorMaoPropria = string.Empty,
                               co_valorAvisoRecebimento = string.Empty,
                               co_valorValorDeclarado = string.Empty,
                               co_entregaDomiciliar = string.Empty,
                               co_entregaSabado = string.Empty,
                               co_erro = null,
                               co_msgErro = string.Empty
                           }).FirstOrDefault();


            correioDto = SelectCorreioLocalidade(sCepDestino, correioDto);

            return correioDto;
        }

    }

}
