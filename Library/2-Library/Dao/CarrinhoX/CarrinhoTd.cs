using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using _2_Library.Config;
using _2_Library.Dao.CorreioX;
using _2_Library.Dao.CupomX;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Produto_GrupoX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.CarrinhoX
{
    public class CarrinhoTd
    {
        public void InsertCarrinho(string loj_dominio, CarrinhoDto carrinhoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;

            Carrinho carrinho = new Carrinho();
            carrinho.car_sessionId = HttpContext.Current.Session.SessionID;
            carrinho.proSku_id = carrinhoDto.proSku_id;
            carrinho.car_quantidade = carrinhoDto.car_quantidade;
            carrinho.loj_id = loj_id;

            if (carrinhoDto.cli_id.HasValue)
                carrinho.cli_id = carrinhoDto.cli_id;

            HttpCookie carCookieLoja = Recursos.RecuperaCookie("carCookieLoja");

            if (carCookieLoja == null || HttpContext.Current.Server.HtmlEncode(carCookieLoja.Value) != HttpContext.Current.Session.SessionID)
            {
                Recursos.AdicionaCookie("carCookieLoja", HttpContext.Current.Session.SessionID, 2);
            }

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                rep.InsertCarrinho(carrinho);
            }
        }

        public List<CarrinhoDto> SelectCarrinho(string loj_dominio, string car_sessionId)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            List<CarrinhoDto> carrinhoTd = new List<CarrinhoDto>();

            if (car_sessionId == null)
                car_sessionId = HttpContext.Current.Session.SessionID;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                List<CarrinhoDto> carrinho = rep.SelectCarrinho(loj_id, car_sessionId);

                HttpCookie carCookieLoja = Recursos.RecuperaCookie("carCookieLoja");

                if (carCookieLoja != null)
                {
                    string carCookieValueLoja = HttpContext.Current.Server.HtmlEncode(carCookieLoja.Value);
                    //só recupera o carrinho do cookie se o carrinho estiver vazio, assim para não confundir o cliente
                    if (carrinho.Count == 0 && car_sessionId != carCookieValueLoja)
                    {
                        List<Carrinho> carrinhoCookie = rep.Select(s => s.car_sessionId == carCookieValueLoja).ToList();
                        if (carrinhoCookie.Count > 0)
                        {
                            foreach (Carrinho car in carrinhoCookie)
                            {
                                car.car_sessionId = car_sessionId;
                                rep.Update(car);
                            }

                            carrinho = rep.SelectCarrinho(loj_id, car_sessionId);
                            Recursos.AtualizaCookie("carCookieLoja", HttpContext.Current.Session.SessionID, 2);
                        }
                        else
                        {
                            //se tiver algum produto no carrinho remove o cookie
                            Recursos.RemoveCookie("carCookieLoja");
                        }
                    }
                }

                carrinhoTd = (from car in carrinho
                              select new CarrinhoDto
                              {
                                  car_sessionId = car.car_sessionId,
                                  car_quantidade = car.car_quantidade,
                                  cli_id = car.cli_id,
                                  pro_id = car.pro_id,
                                  pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(car.proSku_nome + "-" + car.proSkuCor_nome + "-" + car.proSkuTam_nome) + "-" + car.proSku_id,
                                  proSku_id = car.proSku_id,
                                  proSku_nome = car.proSku_nome,
                                  proSku_altura = car.proSku_altura,
                                  proSku_largura = car.proSku_largura,
                                  proSku_comprimento = car.proSku_comprimento,
                                  proSku_peso = car.proSku_peso,
                                  proSku_prazoEntregaAdicional = car.proSku_prazoEntregaAdicional,
                                  proSku_precoCusto = car.proSku_precoCusto,
                                  proSku_precoAnterior = car.proSku_precoAnterior,
                                  proSku_precoVenda = car.proSku_precoVenda,
                                  proSkuCor_nome = car.proSkuCor_nome,
                                  proSkuTam_nome = car.proSkuTam_nome,
                                  car_itemSubTotal = car.proSku_precoVenda * car.car_quantidade,
                                  proSku_percDesconto = (car.proSku_precoAnterior - car.proSku_precoVenda) / car.proSku_precoAnterior,
                                  proSku_quantidadeDisponivel = car.proSku_quantidadeDisponivel ?? 2,
                                  proSku_disponivel = car.proSku_disponivel && (car.proSku_quantidadeDisponivel ?? 1) != 0,
                                  produtoSkuFotoDto = car.produtoSkuFotoDto,
                                  entregaDto = Produto_GrupoUtils.VerificaEntrega(car.entregaDto, 0),
                                  parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(car.parcelamentoDto, car.proSku_precoVenda),
                                  loj_id = loj_id
                              }).ToList();
            }
            return carrinhoTd;
        }

        /// <summary>
        /// Seleciona os totais do carrinho
        /// </summary>
        /// <param name="carrinho">se null, utiliza o carrinho corrente</param>
        /// <returns></returns>
        public CarrinhoTotaisDto SelectCarrinhoTotais(List<CarrinhoDto> carrinho, string sCepDestino, string cup_chave, int? cli_id)
        {
            if(carrinho == null)
               carrinho = SelectCarrinho(null, null);

            if (carrinho.Count != 0)
            {
                CorreioDto correioDto = null;

                decimal? entregaValorTotal = null;
                int? entregaPrazoTotal = null;
                decimal cupomValor = 0;
                decimal subTotal = carrinho.Sum(s => s.car_itemSubTotal);
                decimal geralTotal = 0;

                List<CarrinhoDto> carrinhoDtoEnt_bloquear = carrinho.Where(s => s.entregaDto.ent_bloquear == false).ToList();
                int? prazoEntregaFixoTotal = null;
                decimal? entregaValorFixoTotal = null;

                //total do prazo da entrega fixado
                if (carrinhoDtoEnt_bloquear.Where(s => s.entregaDto.ent_prazo.HasValue).Count() > 0)
                    prazoEntregaFixoTotal = carrinhoDtoEnt_bloquear.Where(s => s.entregaDto.ent_prazo.HasValue).Sum(s => s.entregaDto.ent_prazo);

                //total do valor da entrega fixado
                if (carrinhoDtoEnt_bloquear.Where(s => s.entregaDto.ent_valor.HasValue).Count() > 0)
                    entregaValorFixoTotal = carrinhoDtoEnt_bloquear.Where(s => s.entregaDto.ent_valor.HasValue).Sum(s => s.entregaDto.ent_valor);


                CupomDto cupomDto = null;
                if (!string.IsNullOrEmpty(cup_chave))
                {
                    cupomDto = new CupomTd().SelectCupom(null, cup_chave, cli_id);

                    if (cupomDto.cup_msgErro == "0")
                        cupomValor = cupomDto.cup_valor;
                }

                if (!string.IsNullOrEmpty(sCepDestino))
                {
                    //se o carrinho tem apenas 1 item
                    if (carrinho.Count == 1)
                    {
                        CarrinhoDto carrinhoDto = carrinho.First();

                        //se a entrega fixa foi abilitada
                        if (carrinhoDto.entregaDto.ent_bloquear == false)
                        {
                            //calcula o prazo e valor externamente
                            if (carrinhoDto.entregaDto.ent_calculaPrazoExterno && carrinhoDto.entregaDto.ent_calculaValorExterno)
                            {
                                //calcula as medidas dos produtos dos quais nao tem frete fixo abilitado
                                CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                                correioDto = new CorreioTd().SelectCorreioCalcPrecoPrazo(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                                entregaValorTotal = Convert.ToDecimal(correioDto.co_valor);
                                entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega);
                            }
                            else
                                //calcula apenas o prazo externamente
                                if (carrinhoDto.entregaDto.ent_calculaPrazoExterno && !carrinhoDto.entregaDto.ent_calculaValorExterno)
                                {
                                    correioDto = new CorreioTd().SelectCorreioCalcPrazo(null, null, null, sCepDestino);
                                    entregaValorTotal = carrinhoDto.entregaDto.ent_valor;
                                    entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega);
                                }
                                else
                                    //calcula apenas o valor externamente
                                    if (!carrinhoDto.entregaDto.ent_calculaPrazoExterno && carrinhoDto.entregaDto.ent_calculaValorExterno)
                                    {
                                        CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                                        correioDto = new CorreioTd().SelectCorreioCalcPreco(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                                        entregaValorTotal = Convert.ToDecimal(correioDto.co_valor);
                                        entregaPrazoTotal = carrinhoDto.entregaDto.ent_prazo;
                                    }
                                    else
                                        //pega o prazo e valor calculado internamente na entrega fixa
                                        if (!carrinhoDto.entregaDto.ent_calculaPrazoExterno && !carrinhoDto.entregaDto.ent_calculaValorExterno)
                                        {
                                            entregaValorTotal = carrinhoDto.entregaDto.ent_valor;
                                            entregaPrazoTotal = carrinhoDto.entregaDto.ent_prazo;
                                            correioDto = new CorreioTd().SelectCorreioLocalidade(sCepDestino);
                                        }
                        }
                        else
                        {
                            CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                            correioDto = new CorreioTd().SelectCorreioCalcPrecoPrazo(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                            entregaValorTotal = Convert.ToDecimal(correioDto.co_valor);
                            entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega);
                        }
                    }
                    else
                    {
                        //se o carrinho tem mais 1 item
                        //ent_bloquear == false
                        //existem valor fixados em todos os itens na entrega          
                        if (carrinho.Where(s => (s.entregaDto.ent_bloquear == false) && s.entregaDto.ent_calculaPrazoExterno && s.entregaDto.ent_calculaValorExterno).Count() == carrinho.Count)
                        {
                            CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                            correioDto = new CorreioTd().SelectCorreioCalcPrecoPrazo(null, carrinho, carrinhoMedidas, null, null, sCepDestino);
                        }
                        else
                            //calcula apenas o prazo externamente
                            if (carrinho.Where(s => (s.entregaDto.ent_bloquear == false) && s.entregaDto.ent_calculaPrazoExterno && !s.entregaDto.ent_calculaValorExterno).Count() == carrinho.Count)
                            {
                                correioDto = new CorreioTd().SelectCorreioCalcPrazo(null, null, null, sCepDestino);
                            }
                            else
                                //calcula apenas o valor externamente
                                if (carrinho.Where(s => (s.entregaDto.ent_bloquear == false) && !s.entregaDto.ent_calculaPrazoExterno && s.entregaDto.ent_calculaValorExterno).Count() == carrinho.Count)
                                {
                                    CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                                    correioDto = new CorreioTd().SelectCorreioCalcPreco(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                                }
                                else
                                    //pega o prazo e valor calculado internamente na entrega fixa
                                    if (carrinho.Where(s => (s.entregaDto.ent_bloquear == false) && !s.entregaDto.ent_calculaPrazoExterno && !s.entregaDto.ent_calculaValorExterno).Count() == carrinho.Count)
                                    {
                                        CarrinhoDto carrinhoDto = carrinho.First();

                                        entregaValorTotal = carrinhoDto.entregaDto.ent_valor;
                                        entregaPrazoTotal = carrinhoDto.entregaDto.ent_prazo;
                                        correioDto = new CorreioTd().SelectCorreioLocalidade(sCepDestino);
                                    }
                                    else
                                    {
                                        if (carrinho.Where(s => s.entregaDto.ent_bloquear == true).Count() > 0)
                                        {
                                            CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                                            correioDto = new CorreioTd().SelectCorreioCalcPrecoPrazo(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                                            entregaValorTotal = Convert.ToDecimal(correioDto.co_valor) + (entregaValorFixoTotal ?? 0);
                                            entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega) > (prazoEntregaFixoTotal ?? 0) ? Convert.ToInt32(correioDto.co_prazoEntrega) : prazoEntregaFixoTotal;

                                        }
                                        else
                                            if (carrinho.Where(s => s.entregaDto.ent_calculaValorExterno).Count() > 0)
                                            {
                                                CarrinhoMedidas carrinhoMedidas = SelectCarrinhoSomaMedidas(carrinho);
                                                correioDto = new CorreioTd().SelectCorreioCalcPrecoPrazo(null, carrinho, carrinhoMedidas, null, null, sCepDestino);

                                                entregaValorTotal = Convert.ToDecimal(correioDto.co_valor) + (entregaValorFixoTotal ?? 0);
                                                entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega) > (prazoEntregaFixoTotal ?? 0) ? Convert.ToInt32(correioDto.co_prazoEntrega) : prazoEntregaFixoTotal;
                                            }
                                            else
                                                if (carrinho.Where(s => s.entregaDto.ent_calculaPrazoExterno).Count() > 0)
                                                {
                                                    correioDto = new CorreioTd().SelectCorreioCalcPrazo(null, null, null, sCepDestino);

                                                    entregaValorTotal = entregaValorFixoTotal.Value;
                                                    entregaPrazoTotal = Convert.ToInt32(correioDto.co_prazoEntrega) > (prazoEntregaFixoTotal ?? 0) ? Convert.ToInt32(correioDto.co_prazoEntrega) : prazoEntregaFixoTotal;

                                                }
                                                else
                                                {

                                                    entregaValorTotal = entregaValorFixoTotal.Value;
                                                    entregaPrazoTotal = prazoEntregaFixoTotal.Value;
                                                    correioDto = new CorreioTd().SelectCorreioLocalidade(sCepDestino);

                                                }
                                    }

                    }

                    geralTotal = (subTotal - cupomValor);

                    geralTotal = (geralTotal < 0 ? 0 : geralTotal)  + entregaValorTotal.Value;

                }
                else
                {

                    if (carrinhoDtoEnt_bloquear.Where(s => s.entregaDto.ent_valor.HasValue && s.entregaDto.ent_valor == 0).Count() == carrinho.Count)
                        entregaValorTotal = 0;
                    else entregaValorTotal = null;

                    geralTotal = (subTotal - cupomValor);
                }

                geralTotal = geralTotal < 0 ? 0 : geralTotal;

                //Escolhe qual item tem o parcelamento mais abrangente para determinar o parcelamento da compra 
                CarrinhoDto itemCarrinhoAbrangente = carrinho.OrderByDescending(s => s.parcelamentoDto.parcelamentoParcelaDto.Count()).FirstOrDefault();
                ParcelamentoDto parcelamentoDto = Produto_GrupoUtils.CalculaParcelamento(itemCarrinhoAbrangente.parcelamentoDto, geralTotal);

                ParcelamentoParcelaDto parcelamentoParcelaDto = parcelamentoDto.parcelamentoParcelaDto.OrderByDescending(s => s.parcPar_quantidade).FirstOrDefault();
                string condicao = parcelamentoParcelaDto.parcPar_quantidade + " x de " + parcelamentoParcelaDto.parcPar_valor.ToString("c");

                CarrinhoTotaisDto carrinhoTotaisDto = new CarrinhoTotaisDto();
                carrinhoTotaisDto.cart_subTotal = subTotal;
                carrinhoTotaisDto.cart_entregaTotal = entregaValorTotal;
                carrinhoTotaisDto.cart_entregaPrazoTotal = entregaPrazoTotal;
                carrinhoTotaisDto.cart_condicao = condicao;
                carrinhoTotaisDto.cart_total = geralTotal;
                //carrinhoTotaisDto.parcelamentoDto = parcelamentoDto;
                carrinhoTotaisDto.correioDto = correioDto;
                carrinhoTotaisDto.carrinhoDto = carrinho;
                carrinhoTotaisDto.cupomDto = cupomDto;

                return carrinhoTotaisDto;
            }else
                return new CarrinhoTotaisDto();

        }

        public CarrinhoMedidas SelectCarrinhoSomaMedidas(List<CarrinhoDto> carrinho)
        {
            CarrinhoMedidas carrinhoMedidas = (from ca in carrinho
                                               where (ca.entregaDto.ent_bloquear == true || !ca.entregaDto.ent_valor.HasValue) && !ca.entregaDto.ent_valor.Equals(0)
                                               group ca by ca.car_sessionId into g
                                               select new CarrinhoMedidas
                                               {
                                                   carm_totalAltura = g.Sum(s => s.proSku_altura * s.car_quantidade),
                                                   carm_totalLargura = g.Sum(s => s.proSku_largura * s.car_quantidade),
                                                   carm_totalComprimento = g.Sum(s => s.proSku_comprimento * s.car_quantidade),
                                                   carm_totalPeso = g.Sum(s => s.proSku_peso * s.car_quantidade)
                                               }).SingleOrDefault();


            return carrinhoMedidas;
        }

        public void UpdateCarrinho(string loj_dominio, CarrinhoDto carrinhoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            string car_sessionId = HttpContext.Current.Session.SessionID;

            if(!string.IsNullOrEmpty(carrinhoDto.car_sessionId))
                car_sessionId = carrinhoDto.car_sessionId;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                Carrinho carrinho = rep.Select().Where(s => s.car_sessionId == car_sessionId && s.proSku_id == carrinhoDto.proSku_id && s.loj_id == loj_id).FirstOrDefault();
                carrinho.car_quantidade = carrinhoDto.car_quantidade;
                if(carrinho.cli_id.HasValue)
                   carrinho.cli_id = carrinhoDto.cli_id;
                rep.Update(carrinho);
            }    
        }

        public void UpdateCarrinhoCli(string loj_dominio, string car_sessionId, int cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            bool cliConValorSessionId = false;
            List<Carrinho> cliConValorCliIdList = new List<Carrinho>();

            if (car_sessionId == null)
                car_sessionId = HttpContext.Current.Session.SessionID;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                List<Carrinho> carrinho = null;
                List<Carrinho> carrinhoSession = rep.Select(s => s.car_sessionId == car_sessionId && s.loj_id == loj_id).ToList();
                IQueryable<Carrinho> carrinhoCli = rep.Select(s => s.car_sessionId != car_sessionId && s.cli_id == cli_id && s.loj_id == loj_id);
               
                if (carrinhoSession.Count() > 0)
                {
                    rep.Remove(carrinhoCli);
                    carrinho = carrinhoSession;
                    cliConValorSessionId = true;
                }
                else
                {
                    cliConValorCliIdList = carrinhoCli.ToList();
                    carrinho = cliConValorCliIdList.GroupBy(s => new { s.proSku_id, s.cli_id }).Select(s=>s.FirstOrDefault()).ToList();
                    rep.Remove(cliConValorCliIdList.Except(carrinho).AsQueryable());
                }

                if (cliConValorSessionId)
                    Recursos.AtualizaCookie("carCookieLoja", HttpContext.Current.Session.SessionID, 2);

                if (cliConValorSessionId || cliConValorCliIdList.Count > 0)
                    foreach (var car in carrinho)
                    {
                        if (cliConValorSessionId)
                            car.cli_id = cli_id;
                        if (cliConValorCliIdList.Count > 0)
                            car.car_sessionId = car_sessionId;
                        rep.Update(car);
                    }


            }
        }

        public void UpdateCarrinho(string loj_dominio, List<CarrinhoDto> carrinhoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            List<Carrinho> carrinhoList = null;
            string car_sessionId = HttpContext.Current.Session.SessionID;

            if (!string.IsNullOrEmpty(carrinhoDto.FirstOrDefault().car_sessionId))
                car_sessionId = carrinhoDto.FirstOrDefault().car_sessionId;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                foreach (CarrinhoDto car in carrinhoDto)
                {
                    Carrinho carrinho = rep.Select().Where(s => s.car_sessionId == car_sessionId && s.proSku_id == car.proSku_id && s.loj_id == loj_id).FirstOrDefault();
                    carrinho.car_quantidade = car.car_quantidade;
                    if (carrinho.cli_id.HasValue)
                        carrinho.cli_id = car.cli_id;
                    carrinhoList.Add(carrinho);
                }

                rep.Update(carrinhoList);
            }
        }

        public void RemoveCarrinho(string loj_dominio, CarrinhoDto carrinhoDto)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            Carrinho carrinho = null;
            string car_sessionId = HttpContext.Current.Session.SessionID;

            if (!string.IsNullOrEmpty(carrinhoDto.car_sessionId))
                car_sessionId = carrinhoDto.car_sessionId;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                carrinho = rep.Select().Where(s => s.car_sessionId == car_sessionId && s.proSku_id == carrinhoDto.proSku_id && s.loj_id == loj_id).FirstOrDefault();
                rep.Remove(carrinho);
            }
        }

        public void RemoveCarrinho(string loj_dominio)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id; 
            string car_sessionId = HttpContext.Current.Session.SessionID;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                IQueryable<Carrinho> carrinho = rep.Select().Where(s => s.car_sessionId == car_sessionId && s.loj_id == loj_id);
                rep.Remove(carrinho);
            }
        }
    }
}
