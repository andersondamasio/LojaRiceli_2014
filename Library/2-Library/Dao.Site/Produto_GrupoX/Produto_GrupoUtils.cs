using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Site.EntregaX;

namespace _2_Library.Dao.Site.Produto_GrupoX
{
    public class Produto_GrupoUtils
    {

        public static ParcelamentoDto CalculaParcelamento(ParcelamentoDto parcelamentoDto, decimal proSku_precoVenda)
        {
            if (!parcelamentoDto.parc_bloquear && (DateTime.Now >= (parcelamentoDto.parc_periodoDe ?? DateTime.Now.AddDays(-1)) && DateTime.Now <= (parcelamentoDto.parc_periodoAte ?? DateTime.Now.AddDays(1))))
            {
                parcelamentoDto.parc_valorMinimo = (parcelamentoDto.parc_valorMinimo ?? 0);

                foreach (ParcelamentoParcelaDto parcelamentoParcelaDto in parcelamentoDto.parcelamentoParcelaDto)
                {
                    if(parcelamentoParcelaDto.parcPar_quantidade != 1)
                       parcelamentoParcelaDto.parcPar_bloquear = true;


                    if (parcelamentoDto.parc_ativarJuro && (parcelamentoParcelaDto.parcPar_percentualJuro ?? 0) > 0)
                    {
                        //parcelamento normal
                        //decimal valorJuro = (((parcelamentoParcelaDto.parcPar_percentualJuro ?? 0) / 100) * parcelamentoParcelaDto.parcPar_valor);
                        //parcelamentoParcelaDto.parcPar_valor = parcelamentoParcelaDto.parcPar_valor + valorJuro;

                        //parcelamento pagSeguro
                        decimal valorJuro = ((parcelamentoParcelaDto.parcPar_percentualJuro ?? 0) / 100);
                        var fator = (1 - Math.Pow((1 + (double)valorJuro), (parcelamentoParcelaDto.parcPar_quantidade * -1)));

                        parcelamentoParcelaDto.parcPar_valor = Convert.ToDecimal((double)valorJuro / fator) * proSku_precoVenda;
                    }
                    else {
                        parcelamentoParcelaDto.parcPar_valor = (proSku_precoVenda / parcelamentoParcelaDto.parcPar_quantidade);
                        parcelamentoParcelaDto.parcPar_percentualJuro = 0;
                    }

                    if (parcelamentoParcelaDto.parcPar_valor >= parcelamentoDto.parc_valorMinimo)
                    {
                        parcelamentoParcelaDto.parcPar_bloquear = false;
                    }
                    else {
                        foreach(ParcelamentoParcelaDto parcParDto in parcelamentoDto.parcelamentoParcelaDto.Where(s=>s.parcPar_quantidade>parcelamentoParcelaDto.parcPar_quantidade)){

                            if (parcParDto.parcPar_quantidade != parcelamentoParcelaDto.parcPar_quantidade) {
                                   parcParDto.parcPar_bloquear = true;
                            }
                        }
                          break;
                    }     
                }
            }

            parcelamentoDto.parcelamentoParcelaDto = parcelamentoDto.parcelamentoParcelaDto.Where(s => !s.parcPar_bloquear).ToList();

            return parcelamentoDto;
        }

        public static decimal CalculaParcelamento(int quantidadeParcela, decimal percentualJuro, decimal proSku_precoVenda)
        {
            decimal valorParcela = 0;

            if (percentualJuro > 0)
            {
                //parcelamento normal
                //decimal valorJuro = (((parcelamentoParcelaDto.parcPar_percentualJuro ?? 0) / 100) * parcelamentoParcelaDto.parcPar_valor);
                //parcelamentoParcelaDto.parcPar_valor = parcelamentoParcelaDto.parcPar_valor + valorJuro;

                //parcelamento pagSeguro
                decimal valorJuro = (percentualJuro / 100);
                var fator = (1 - Math.Pow((1 + (double)valorJuro), (quantidadeParcela * -1)));

                valorParcela = Convert.ToDecimal((double)valorJuro / fator) * proSku_precoVenda;
            }
            else
            {
                valorParcela = (proSku_precoVenda / quantidadeParcela);
            }

            return valorParcela;
        }

        public static ParcelamentoDto CalculaParcelamentoParcela(ParcelamentoDto parcelamentoDto)
        {
            parcelamentoDto = new ParcelamentoDto
            {
                parc_quantidade = parcelamentoDto.parc_quantidade,
                parc_valorMinimo = parcelamentoDto.parc_valorMinimo,
                parc_periodoDe = parcelamentoDto.parc_periodoDe,
                parc_periodoAte = parcelamentoDto.parc_periodoAte,
                parc_ativarJuro = parcelamentoDto.parc_ativarJuro,
                parc_bloquear = parcelamentoDto.parc_bloquear,
                parcelamentoParcelaDto = parcelamentoDto.parcelamentoParcelaDto.Where(s => !s.parcPar_bloquear).ToList() /*(from parcPar in parcelamentoDto.parcelamentoParcelaDto
                                         select new ParcelamentoParcelaDto
                                         {
                                             parcPar_valor = parcPar.parcPar_valor,
                                             parcPar_quantidade = parcPar.parcPar_quantidade,
                                             parcPar_percentualJuro = parcPar.parcPar_percentualJuro,
                                             parcPar_bloquear = parcPar.parcPar_bloquear
                                         }).Where(s=>!s.parcPar_bloquear).OrderByDescending(s=>s.parcPar_quantidade).Take(1)*/
            };

            return parcelamentoDto;
        }

        public static decimal CalculaPercentualDesconto(decimal proSku_precoAnterior, decimal proSku_precoVenda)
        {
            decimal percentual = 0;
            if (proSku_precoAnterior > proSku_precoVenda)
            {
                percentual = (proSku_precoAnterior - proSku_precoVenda);
                percentual = percentual/proSku_precoAnterior;
            }

            return percentual;
        }

        public static EntregaDto VerificaEntrega(EntregaDto entrega, int ent_cep)
        {
            //não esquecer de configurar linha abaixo antes da colocar em producao.
            EntregaDto entregaDto = new EntregaTd().SelectEntregaGlobal(null).FirstOrDefault();

            if (entregaDto != null)
                entregaDto = ValidaEntrega(entregaDto, ent_cep);

            if (entregaDto == null || entregaDto.ent_bloquear == true)
            {
                entrega = ValidaEntrega(entrega, ent_cep);
                return entrega;
            }

            return entregaDto;
        }

        private static EntregaDto ValidaEntrega(EntregaDto entrega, int ent_cep)
        {
            if (entrega != null && (entrega.ent_cepInicial.HasValue && entrega.ent_cepFinal.HasValue))
            {

                if (entrega.ent_dataHoraInicial.HasValue || entrega.ent_dataHoraFinal.HasValue)
                    if (!((entrega.ent_dataHoraInicial.HasValue ? entrega.ent_dataHoraInicial.Value : DateTime.Now) <= DateTime.Now && (entrega.ent_dataHoraFinal.HasValue ? entrega.ent_dataHoraFinal.Value : DateTime.Now) >= DateTime.Now))
                    {
                        entrega.ent_bloquear = true;
                        return entrega;
                    }

                if (ent_cep >= entrega.ent_cepInicial && ent_cep <= entrega.ent_cepFinal)
                {
                    entrega.ent_bloquear = false;
                }
                else
                    entrega.ent_bloquear = true;

                if (entrega.ent_valor.HasValue && entrega.ent_valor == 0)
                {
                    entrega.ent_gratis = true;
                    entrega.ent_calculaValorExterno = false;
                }
                else
                {
                    entrega.ent_gratis = false;

                    if (!entrega.ent_valor.HasValue)
                        entrega.ent_calculaValorExterno = true;
                }

                if (entrega.ent_prazo.HasValue)
                    entrega.ent_calculaPrazoExterno = false;
                else entrega.ent_calculaPrazoExterno = true;
            }
            else
            {
                entrega = entrega ?? new EntregaDto();
                entrega.ent_bloquear = true;
            }
            return entrega;
        }

    }
}
