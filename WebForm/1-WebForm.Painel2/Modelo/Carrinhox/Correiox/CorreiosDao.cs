using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1_WebForm.Painel2.correios.ServiceReferenceCorreios;
using Loja.Modelo.Carrinhox;
using Loja.Modelo.Correiox;
using Loja.Modelo.Entregax;
using _2_Library.Modelo;

namespace Loja.Correiox
{
    public class CorreioDao
    {
/*
        public CorreioBean SelecionarEndereco(string cepDestino)
        {
            CorreioBean correioBean = new CorreioBean();
            CorreiosEntities correiosEntities = new CorreiosEntities();

            correioBean = (from log in correiosEntities.LOG_LOGRADOURO
                           where log.CEP == cepDestino
                           select new CorreioBean
                           {
                               corr_cidade = log.LOG_LOCALIDADE.LOC_NO,
                               corr_estado = log.UFE_SG,
                               corr_endereco = log.LOG_NOME,
                               corr_complemento = log.LOG_COMPLEMENTO,
                               corr_bairro = log.LOG_BAIRRO.BAI_NO
                           }).FirstOrDefault();

            return correioBean;
        }


        public CarrinhoEntrega CalculaPrazo(int cepDestino)
        {
            CarrinhoEntrega carrinhoEntrega = new CarrinhoEntrega();
            cServico cServico = new cServico();
            cResultado resultado = new cResultado();
            string cepOrigem = new _2_Library.Dao.LojaX.LojaDto().loj_cep;

            Entrega entrega = new EntregaConsulta().SelecionarEntrega(cepDestino);

            if (entrega != null)
            {
                carrinhoEntrega.car_meioEntrega = "Correios";
                carrinhoEntrega.car_localizacao = entrega.ent_cidade + " - " + entrega.ent_estado;
                carrinhoEntrega.car_prazoEntrega = entrega.ent_prazo ?? 0;
                carrinhoEntrega.car_valorEntrega = entrega.ent_valor ?? 0;
            }

            if (!(entrega != null && entrega.ent_valor.HasValue && entrega.ent_prazo.HasValue))
            {
                try
                {
                    CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient();
                    resultado = calcPrecoPrazoWSSoapClient.CalcPrazo("40010", cepOrigem.ToString(), cepDestino.ToString());
                    cServico = resultado.Servicos.FirstOrDefault();

                    if (resultado.Servicos.First().Erro != "0" && resultado.Servicos.First().Erro != "")
                        cServico = new cServico() { Valor = "40,00", PrazoEntrega = "15", Erro = "0", MsgErro = string.Empty };
                    else
                        cServico = resultado.Servicos.First();
                }
                catch (Exception ex)
                {
                    cServico = new cServico() { Valor = "40,00", PrazoEntrega = "15", Erro = "0", MsgErro = string.Empty };
                }

                carrinhoEntrega.car_meioEntrega = "Correios";
                carrinhoEntrega.car_localizacao = string.Empty + cServico.Codigo;
                carrinhoEntrega.car_prazoEntrega = Convert.ToInt32(cServico.PrazoEntrega);
                carrinhoEntrega.car_valorEntrega = Convert.ToDecimal(cServico.Valor);

                if (entrega != null)
                    if (entrega.ent_prazo.HasValue || entrega.ent_valor.HasValue)
                    {
                        if (entrega.ent_prazo.HasValue)
                        {
                            carrinhoEntrega.car_meioEntrega = "Correios";
                            carrinhoEntrega.car_prazoEntrega = entrega.ent_prazo.Value;
                        }

                        if (entrega.ent_valor.HasValue)
                        {
                            carrinhoEntrega.car_meioEntrega = "Correios";
                            carrinhoEntrega.car_valorEntrega = entrega.ent_valor.Value;
                        }
                    }
            }
            HttpContext.Current.Session["carrinhoEntrega"] = carrinhoEntrega;
            return carrinhoEntrega;
        }

        public CarrinhoEntrega CalculaPrecoPrazo(int cepDestino)
        {
            CarrinhoEntrega carrinhoEntrega = new CarrinhoEntrega();
            cServico cServico = new cServico();
            string cepOrigem = new _2_Library.Dao.LojaX.LojaDto().loj_cep;

            Entrega entrega = new EntregaConsulta().SelecionarEntrega(cepDestino);

            if (entrega != null)
            {
                carrinhoEntrega.car_meioEntrega = "Correios";
                carrinhoEntrega.car_localizacao = entrega.ent_cidade + " - " + entrega.ent_estado;
                carrinhoEntrega.car_prazoEntrega = entrega.ent_prazo ?? 0;
                carrinhoEntrega.car_valorEntrega = entrega.ent_valor ?? 0;
            }

            if (!(entrega != null && entrega.ent_valor.HasValue && entrega.ent_prazo.HasValue))
            {

                if (entrega != null && entrega.ent_valor.HasValue)
                {
                    carrinhoEntrega = CalculaPrazo(cepDestino);
                    HttpContext.Current.Session["carrinhoEntrega"] = carrinhoEntrega;
                    return carrinhoEntrega;
                }

                Medidas medidas = new Loja.Modelo.Carrinhox.CarrinhoConsulta().SelecionarSomaMedidasItensCarrinho();// (Medidas)HttpContext.Current.Session["CorreioMedidas"];

                if (medidas.totalLargura < 11)
                    medidas.totalLargura = 11;

                if (medidas.totalAltura < 2)
                    medidas.totalAltura = 2;

                if (medidas.totalComprimento < 16)
                    medidas.totalComprimento = 16;
                try
                {
                    CalcPrecoPrazoWSSoapClient calcPrecoPrazoWSSoapClient = new CalcPrecoPrazoWSSoapClient();
                    cResultado resultado = calcPrecoPrazoWSSoapClient.CalcPrecoPrazo("045684", "45685427", "41106", cepOrigem.ToString(), cepDestino.ToString(), medidas.totalPeso.ToString(), 1, medidas.totalComprimento, medidas.totalAltura, medidas.totalLargura, medidas.totalDiametro, "N", 0, "N");

                    if (resultado.Servicos.First().Erro != "0")
                    {
                        HttpContext.Current.Session["car_totalEntrega"] = "40,00";
                        cServico = new cServico() { Valor = "40,00", PrazoEntrega = "15", Erro = "0", MsgErro = string.Empty };
                    }
                    else
                        cServico = resultado.Servicos.First();
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["car_totalEntrega"] = "40,00";
                    cServico = new cServico() { Valor = "40,00", PrazoEntrega = "15", Erro = "0", MsgErro = string.Empty };
                }

                carrinhoEntrega.car_meioEntrega = "Correios";
                carrinhoEntrega.car_localizacao = string.Empty + cServico.Codigo;
                carrinhoEntrega.car_prazoEntrega = Convert.ToInt32(cServico.PrazoEntrega);
                carrinhoEntrega.car_valorEntrega = Convert.ToDecimal(cServico.Valor);

                if (entrega != null)
                    if (entrega.ent_prazo.HasValue || entrega.ent_valor.HasValue)
                    {
                        if (entrega.ent_prazo.HasValue)
                        {
                            carrinhoEntrega.car_meioEntrega = "Correios";
                            carrinhoEntrega.car_prazoEntrega = entrega.ent_prazo.Value;
                        }

                        if (entrega.ent_valor.HasValue)
                        {
                            carrinhoEntrega.car_meioEntrega = "Correios";
                            carrinhoEntrega.car_valorEntrega = entrega.ent_valor.Value;
                        }
                    }
            }
            HttpContext.Current.Session["carrinhoEntrega"] = carrinhoEntrega;
            return carrinhoEntrega;
        }
    */}


}