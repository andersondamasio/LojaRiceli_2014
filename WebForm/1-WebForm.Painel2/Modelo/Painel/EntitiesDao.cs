using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Utils;
using System.Reflection;
using System.Data.Objects.SqlClient;
using System.Data.Objects;
using System.Data;
using _2_Library.Modelo;


namespace Loja.Modelo
{
    public class EntitiesDao
    {
        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

        #region Lista os produtos de um grupo
        public IQueryable SelecionaProdutoGrupo(string gru_nomeAmigavel, int startRowIndex, Int32 maximumRows, string orderBy)
        {
            orderBy = orderBy == string.Empty ? "pro_id" : orderBy;

            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from pro in lojaEntities.Produto
                            where
                            pro.ProdutoSku.Where(s => s.pro_id == pro.pro_id && s.proSku_bloquear != true && s.proSku_disponivel == true && (s.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0 &&
                            pro.Produto_Grupo.Any(s => s.Grupo.gru_nomeAmigavel == gru_nomeAmigavel) && pro.loj_id == loj_id
                            select new
                            {
                                pro_id = pro.pro_id,
                                pro_nome = pro.pro_nome,
                                gru_nome = pro.Produto_Grupo.FirstOrDefault().Grupo.gru_nome,
                                pro_nomeAmigavel = pro.pro_nomeAmigavel,
                                ProdutoSkuCores = pro.ProdutoSku.Where(s => s.proSku_bloquear != true && s.proSku_disponivel == true && (s.proSku_quantidadeDisponivel ?? 1) != 0).Select(s =>
                                    new
                                    {
                                        s.ProdutoSkuCor.proSkuCor_nome,
                                        s.ProdutoSkuCor.proSkuCor_imagem,
                                    }).Take(4),
                                ProdutoSkuPreco = pro.ProdutoSku.Where(s => s.proSku_bloquear != true && s.proSku_disponivel == true && (s.proSku_quantidadeDisponivel ?? 1) != 0).Select(s =>
                                new
                                {
                                    s.proSku_precoAnterior,
                                    s.proSku_precoVenda
                                }).OrderBy(s => s.proSku_precoVenda).FirstOrDefault()
                            }).OrderBy(orderBy).Skip(startRowIndex).Take(maximumRows);


             
            return produtos;
        }

        public Int32 SelecionaProdutoGrupoCont(string gru_nomeAmigavel, int startRowIndex, Int32 maximumRows, string orderBy)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var produtos = (from pro in lojaEntities.Produto
                            where
                            pro.ProdutoSku.Where(s => s.pro_id == pro.pro_id && s.proSku_bloquear != true && s.proSku_disponivel == true && (s.proSku_quantidadeDisponivel ?? 1) != 0).Count() > 0 &&
                            pro.Produto_Grupo.Any(s => s.Grupo.gru_nomeAmigavel == gru_nomeAmigavel)
                            && pro.loj_id == loj_id
                            select pro);

            return produtos.Count();
        }
        #endregion

        public List<MensagemSistema> SelecionaMensagemSistema()
        {
            LojaEntities lojaEntities = new LojaEntities();
            List<MensagemSistema> result = (from menSis in lojaEntities.MensagemSistema
                                            select menSis).ToList();

            return result;
        }

        public Retorno SelecionarConfiguracao(Int32 loja_id)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Configuracao result = (from conf in lojaEntities.Configuracao
                                       where conf.loj_id == loja_id
                                       select conf).SingleOrDefault();

                if (result == null)
                {
                    retorno = Static.MensagemSistema(14);
                    return retorno;
                }
                else
                {
                    retorno.id_registro = result.con_id;
                    retorno.objeto = result;
                }
            }
            return retorno; 
        }

        #region Acoes Grupo

        public IQueryable<Grupo> SelecionarGrupo()
        {
            LojaEntities lojaEntities = new LojaEntities();

            IQueryable<Grupo> grupo = (from gru in lojaEntities.Grupo
                                       where gru.loj_id == loj_id
                           select gru);
            return grupo;
        }

        public Grupo SelecionarGrupo(string gru_nomeAmigavel)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Grupo grupo = (from gru in lojaEntities.Grupo
                                       where gru.gru_nomeAmigavel == gru_nomeAmigavel && (gru.loj_id == loj_id)
                                       select gru).LinqToCacheAdd("Grupo",gru_nomeAmigavel).FirstOrDefault();
            return grupo;
        }

        public Retorno AtualizarGrupo(Grupo grupo)
        {
            Retorno retorno = new Retorno();

            if (grupo.gru_id == 0) {
                retorno = Static.MensagemSistema(4);
                return retorno;
            }

            LojaEntities lojaEntities = new LojaEntities();
            Grupo gr = (from gru in lojaEntities.Grupo
                        where gru.gru_id == grupo.gru_id && grupo.loj_id == loj_id
                        select gru).FirstOrDefault();


            if (gr.Grupo2.Grupo1.Where(s => (s.gru_id != gr.gru_id) && s.gru_nome == grupo.gru_nome && s.loj_id == loj_id).Count() > 0)
            {
                retorno = Static.MensagemSistema(5);
                return retorno;
            }


            gr.gru_nomeAmigavel = Tratamento.GerarNomeAmigavel(grupo.gru_nome);

            if (gr.Grupo2.gru_pai.HasValue)
                gr.gru_nomeAmigavel = gr.Grupo2.gru_nomeAmigavel + "/" + gr.gru_nomeAmigavel;


          /*  if (lojaEntities.Grupo.Where(s => (s.gru_id != grupo.gru_id && s.gru_nomeAmigavel == gr.gru_nomeAmigavel) && s.loj_id == loj_id).Count() > 0)
                {
                    gr.gru_nomeAmigavel += "-e" + grupo.gru_id;
                    if (lojaEntities.Grupo.Where(s => (s.gru_id != grupo.gru_id && s.gru_nomeAmigavel == gr.gru_nomeAmigavel) && s.loj_id == loj_id).Count() > 0)
                    {
                        retorno.menSis_id = -1;
                        retorno.menSis_mensagem = "Nome do Grupo já existe ou é inválido, por favor tente um nome diferente.";
                        return retorno;
                    }
                }*/
                

            if (gr.gru_bloquear != grupo.gru_bloquear)
            {
                gr = Tratamento.SubBloqueiaGrupoFilho(gr, grupo.gru_bloquear);
            }

                gr.gru_nome = grupo.gru_nome;
                gr.gru_descricao = grupo.gru_descricao;
                gr.gru_posicao = grupo.gru_posicao;
                gr.gru_bloquear = grupo.gru_bloquear;
                gr.gru_dataHoraAtualizacao = DateTime.Now;
                
                lojaEntities.SaveChanges();

            return retorno;
        }

        public Retorno InserirGrupo(Grupo grupo)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Grupo gru = SelecionarGrupo().Where(s => s.gru_id == grupo.gru_pai && s.loj_id == loj_id).SingleOrDefault();

                if (gru != null)
                {
                    if (gru.Produto_Grupo.Where(s => s.gru_id == gru.gru_id && s.loj_id == loj_id).Count() > 0)
                    {
                        retorno = Static.MensagemSistema(3);
                        return retorno;
                    }

                    if (gru.Grupo1.Where(s => s.gru_nome == grupo.gru_nome && s.loj_id == loj_id).Count() > 0)
                    {
                        retorno = Static.MensagemSistema(5);
                        return retorno;
                    }
                }

                //grupo.gru_nomeAmigavel = "NOVO";

                    if (grupo.gru_pai == 0)
                    {
                        int encremento = lojaEntities.Grupo.Max(s => s.gru_id);
                        grupo.gru_id = encremento + 1;
                    }
                    grupo.loj_id = loj_id;
                    grupo.gru_dataHora = DateTime.Now;

                    grupo.gru_nomeAmigavel = Tratamento.GerarNomeAmigavel(grupo.gru_nome);

                    if (gru.gru_pai.HasValue)
                        grupo.gru_nomeAmigavel = gru.gru_nomeAmigavel + "/" + grupo.gru_nomeAmigavel;
                
                
                  lojaEntities.Grupo.Add(grupo);
                   
                  lojaEntities.SaveChanges();

                    

                  /*  if (lojaEntities.Grupo.Where(s => s.gru_nomeAmigavel == grupo.gru_nomeAmigavel && s.loj_id == loj_id).Count() > 0)
                    {
                        grupo.gru_nomeAmigavel +="-e"+ grupo.gru_id;
                        if (lojaEntities.Grupo.Where(s => s.gru_nomeAmigavel == grupo.gru_nomeAmigavel && s.loj_id == loj_id).Count() > 0)
                        {
                            retorno.menSis_id = -1;
                            retorno.menSis_mensagem = "Nome do Grupo já existe ou é inválido, por favor tente um nome diferente.";
                            return retorno;
                        }
                    }*/
                    
                    lojaEntities.SaveChanges();
                }
            
            retorno.id_registro = grupo.gru_id;

            return retorno;
        }

        public Retorno ExcluirGrupo(int gru_id)
        {
            Retorno retorno = new Retorno();
             LojaEntities lojaEntities = new LojaEntities();

            var grupos = (from gru in lojaEntities.Grupo
                          where gru.gru_id == gru_id && gru.loj_id == loj_id
                          select gru);


            if (grupos.Select(s => new { count = s.Grupo1.Count() }).SingleOrDefault().count > 0)
            {
                retorno = Static.MensagemSistema(1);
                return retorno;
            }
            else
                if (grupos.Select(s => new { count = s.Produto_Grupo.Count() }).SingleOrDefault().count > 0)
                {
                    retorno = Static.MensagemSistema(2);
                    return retorno;
                }


            Grupo grupo = grupos.SingleOrDefault();

            /*

            grupo.gru_excluido = grupo.gru_id;
            grupo.gru_dataHoraAtualizacao = DateTime.Now;
     */
            lojaEntities.Grupo.Remove(grupo);

            lojaEntities.SaveChanges();

            return retorno;

        }

        #endregion

        public Retorno InserirProduto(Produto produto)
        {
            Retorno retorno = new Retorno();
            try
            {
                using (LojaEntities lojaEntities = new LojaEntities())
                {
                    produto.loj_id = loj_id;
                    produto.pro_dataHora = DateTime.Now;
                    produto.pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(produto.pro_nome);
                    lojaEntities.Produto.Add(produto);
                    lojaEntities.SaveChanges();
                }
                retorno.id_registro = produto.pro_id;
            }
            catch (Exception ex)
            {
                retorno.menSis_id = -1;
                retorno.menSis_mensagem = ex.Message;
            }
            return retorno;
        }

        public IQueryable<Produto> SelecionarProduto(Int32 pro_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable<Produto> produto = (from pro in lojaEntities.Produto
                                           where pro.pro_id == pro_id && pro.loj_id == loj_id
                               select pro);
            return produto;
        }

        public Retorno ExcluirProduto(Int32 pro_id)
        {
            Retorno retorno = new Retorno();

            LojaEntities lojaEntities = new LojaEntities();
            Produto produto = (from pro in lojaEntities.Produto
                                           where pro.pro_id == pro_id && pro.loj_id == loj_id
                                           select pro).SingleOrDefault();
            //produto.pro_dataHoraAtualizacao = DateTime.Now;
            //produto.pro_excluido = produto.pro_id;

            lojaEntities.Produto.Remove(produto);

            lojaEntities.SaveChanges();

            return retorno;
        }

        public Retorno AtualizarProduto(Produto produto)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();

            Produto prod = (from pro in lojaEntities.Produto
                            where pro.pro_id == produto.pro_id && 
                            pro.loj_id == loj_id
                            select pro).FirstOrDefault();


            if (prod.Produto_Grupo.Count == 0)
            {
                retorno = Static.MensagemSistema(8);
                return retorno;
            }

            prod.pro_nome = produto.pro_nome;
            prod.mar_id = produto.mar_id;
            prod.pro_descricao = produto.pro_descricao;
            prod.pro_posicao = produto.pro_posicao;
            prod.pro_paginaInicialDe = produto.pro_paginaInicialDe;
            prod.pro_paginaInicialAte = produto.pro_paginaInicialAte;
            prod.pro_bloquear = produto.pro_bloquear;
            prod.pro_nomeAmigavel = Tratamento.GerarNomeAmigavel(produto.pro_nome);
            prod.pro_dataHoraAtualizacao = DateTime.Now;
            lojaEntities.SaveChanges();

            retorno.objeto = prod;

            return retorno;
        }

        public Retorno InserirProdutoGrupo(Produto_Grupo produtoGrupo)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                if (lojaEntities.Produto_Grupo.Where(s => s.gru_id == produtoGrupo.gru_id && s.pro_id == produtoGrupo.pro_id && produtoGrupo.loj_id == loj_id).Count() == 0)
                {
                    produtoGrupo.loj_id = loj_id;
                    produtoGrupo.proGru_dataHora = DateTime.Now;
                    lojaEntities.Produto_Grupo.Add(produtoGrupo);
                    lojaEntities.SaveChanges();
                }
                else {
                    retorno = Static.MensagemSistema(9);
                    return retorno;
                }
            }
            return retorno;
        }

        public Retorno ExcluirProdutoGrupo(Int32 pro_id, Int32 gru_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();
            var produtoGrupos = (from proGru in lojaEntities.Produto_Grupo
                                 where proGru.pro_id == pro_id &&
                                 proGru.loj_id == loj_id
                                         select proGru);
                                     
            if(produtoGrupos.Count() == 1){
                retorno = Static.MensagemSistema(8);
                return retorno;
            }

            Produto_Grupo produtoGrupo = produtoGrupos.Where(s => s.gru_id == gru_id && s.loj_id == loj_id).FirstOrDefault();

            lojaEntities.Produto_Grupo.Remove(produtoGrupo);
            lojaEntities.SaveChanges();

            return retorno;
        }

        public IQueryable<Produto_Grupo> SelecionarProdutoGrupo(Int32 pro_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable<Produto_Grupo> produtoGrupo = (from proGru in lojaEntities.Produto_Grupo
                                                     where proGru.pro_id == pro_id && proGru.loj_id == loj_id
                                         select proGru);
            return produtoGrupo;
        }

        public Retorno InserirProdutoSku(ProdutoSku produtoSku)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                produtoSku.loj_id = loj_id;
                produtoSku.proSku_dataHora = DateTime.Now;
                lojaEntities.ProdutoSku.Add(produtoSku);
                lojaEntities.SaveChanges();
            }

            retorno.id_registro = produtoSku.proSku_id;
            return retorno;
        }

        public ProdutoSku SelecionarProdutoSku(Int32 proSku_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoSku produtoSku = (from proSku in lojaEntities.ProdutoSku
                                     where proSku.proSku_id == proSku_id && 
                                     proSku.loj_id == loj_id
                                     select proSku).FirstOrDefault();
            return produtoSku;
        }

        public void AtualizarProdutoSku(ProdutoSku produtoSku)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoSku prodSku = (from proSku in lojaEntities.ProdutoSku
                                  where proSku.proSku_id == produtoSku.proSku_id &&
                                  proSku.loj_id == loj_id
                                  select proSku).FirstOrDefault();

            prodSku.loj_id = loj_id;
            prodSku.proSku_nome = produtoSku.proSku_nome;
            prodSku.proSku_precoAnterior = produtoSku.proSku_precoAnterior;
            prodSku.proSku_precoVenda = produtoSku.proSku_precoVenda;
            prodSku.proSku_precoCusto = produtoSku.proSku_precoCusto;
            prodSku.proSku_idReferencia = produtoSku.proSku_idReferencia;
            prodSku.proSku_peso = produtoSku.proSku_peso;
            prodSku.proSku_altura = produtoSku.proSku_altura;
            prodSku.proSku_largura = produtoSku.proSku_largura;
            prodSku.proSku_comprimento = produtoSku.proSku_comprimento;
            prodSku.proSku_prazoEntregaAdicional = produtoSku.proSku_prazoEntregaAdicional;
            prodSku.proSku_quantidadeMaxima = produtoSku.proSku_quantidadeMaxima;
            prodSku.proSku_quantidadeDisponivel = produtoSku.proSku_quantidadeDisponivel;
            prodSku.ent_id = produtoSku.ent_id;
            prodSku.parc_id = produtoSku.parc_id;
            prodSku.proSkuTam_id = produtoSku.proSkuTam_id;
            prodSku.proSkuCor_id = produtoSku.proSkuCor_id;
            prodSku.proSku_disponivel = produtoSku.proSku_disponivel;
            prodSku.proSku_bloquear = produtoSku.proSku_bloquear;
            prodSku.proSku_destaque = produtoSku.proSku_destaque;
            prodSku.proSku_posicao = produtoSku.proSku_posicao;
            prodSku.proSku_dataHoraAtualizacao = DateTime.Now;

            lojaEntities.SaveChanges();
        }

        public Retorno ExcluirProdutoSku(Int32 proSku_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoSku produtoSku = (from proSku in lojaEntities.ProdutoSku
                                      where proSku.proSku_id == proSku_id &&
                                      proSku.loj_id == loj_id
                                      select proSku).FirstOrDefault();

           // produtoSku.proSku_excluido = produtoSku.proSku_id;
            //produtoSku.proSku_dataHoraAtualizacao = DateTime.Now;

            lojaEntities.ProdutoSku.Remove(produtoSku);

            lojaEntities.SaveChanges();

            return retorno;
        }

        public IQueryable<ProdutoSkuCor> SelecionarProdutoSkuCor()
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable<ProdutoSkuCor> result = (from produtoSkuCor in lojaEntities.ProdutoSkuCor
                                          where produtoSkuCor.proSkuCor_bloquear != true &&
                                          produtoSkuCor.loj_id == loj_id
                                          select produtoSkuCor);

            return result;
        }

        public IQueryable<ProdutoSkuTamanho> SelecionarProdutoSkuTamanho()
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable<ProdutoSkuTamanho> result = (from produtoSkuTam in lojaEntities.ProdutoSkuTamanho
                                                    where produtoSkuTam.proSkuTam_bloquear != true &&
                                                    produtoSkuTam.loj_id == loj_id
                                                    select produtoSkuTam);

            return result;
        }

        public IQueryable<ProdutoSkuFoto> SelecionarProdutoSkuFoto()
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable<ProdutoSkuFoto> produtoSkuFoto = 
                                                   (from proSkuFot in lojaEntities.ProdutoSkuFoto
                                                    where proSkuFot.loj_id == loj_id
                                                    select proSkuFot);
            return produtoSkuFoto;
        }

        public Retorno InserirProdutoSkuFoto(ProdutoSkuFoto produtoSkuFoto)
        {
            Retorno retorno = new Retorno();
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                produtoSkuFoto.loj_id = loj_id;
                produtoSkuFoto.proSkuFot_dataHora = DateTime.Now;
                lojaEntities.ProdutoSkuFoto.Add(produtoSkuFoto);
                lojaEntities.SaveChanges();
            }

            retorno.id_registro = produtoSkuFoto.proSkuFot_id;

            return retorno;
        }

        public void AtualizarProdutoSkuFoto(ProdutoSkuFoto produtoSkuFoto)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                ProdutoSkuFoto prodSkuFoto = (from proSkuFoto in lojaEntities.ProdutoSkuFoto
                                              where proSkuFoto.proSkuFot_id == produtoSkuFoto.proSkuFot_id &&
                                              proSkuFoto.loj_id == loj_id
                                              select proSkuFoto).FirstOrDefault();

                if (produtoSkuFoto.proSkuFot_nome != null)
                {
                    prodSkuFoto.proSkuFot_nome = produtoSkuFoto.proSkuFot_nome;
                    prodSkuFoto.proSkuFot_extensao = produtoSkuFoto.proSkuFot_extensao;
                    prodSkuFoto.proSkuFot_titulo = produtoSkuFoto.proSkuFot_titulo;
                    prodSkuFoto.proSkuFot_posicao = produtoSkuFoto.proSkuFot_posicao;
                }
                else prodSkuFoto.proSkuFot_posicao = produtoSkuFoto.proSkuFot_posicao;

                prodSkuFoto.proSkuFot_dataHoraAtualizacao = DateTime.Now;

                lojaEntities.SaveChanges();
            }
        }

        public Retorno ExcluirProdutoSkuFoto(Int32 proSkuFot_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoSkuFoto produtoSkuFoto = (from proSkuFoto in lojaEntities.ProdutoSkuFoto
                                         where proSkuFoto.proSkuFot_id == proSkuFot_id &&
                                         proSkuFoto.loj_id == loj_id
                                         select proSkuFoto).FirstOrDefault();

            lojaEntities.ProdutoSkuFoto.Remove(produtoSkuFoto);
            lojaEntities.SaveChanges();

            retorno.objeto = produtoSkuFoto;

            return retorno;
        }

        public void InserirProdutoInfo(ProdutoInfo produtoInfo)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                produtoInfo.loj_id = loj_id;
                produtoInfo.proInfo_dataHora = DateTime.Now;
                lojaEntities.ProdutoInfo.Add(produtoInfo);
                lojaEntities.SaveChanges();
            }
        }

        public ProdutoInfo SelecionarProdutoInfo(Int32 proInfo_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfo produtoInfo = (from proInfo in lojaEntities.ProdutoInfo
                                      where proInfo.proInfo_id == proInfo_id &&
                                      proInfo.loj_id == loj_id
                                      select proInfo).FirstOrDefault();
            return produtoInfo;
        }

        public void AtualizarProdutoInfo(ProdutoInfo produtoInfo)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfo prodInfo = (from proInfo in lojaEntities.ProdutoInfo
                                    where proInfo.proInfo_id == produtoInfo.proInfo_id &&
                                     proInfo.loj_id == loj_id
                                    select proInfo).FirstOrDefault();

            prodInfo.proInfo_nome = produtoInfo.proInfo_nome;
            prodInfo.proInfo_bloquear = produtoInfo.proInfo_bloquear;
            prodInfo.proInfo_dataHoraAtualizacao = DateTime.Now;

            lojaEntities.SaveChanges();
        }

        public Retorno ExcluirProdutoInfo(Int32 proInfo_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfo produtoInfo = (from proInfo in lojaEntities.ProdutoInfo
                                       where proInfo.proInfo_id == proInfo_id &&
                                       proInfo.loj_id == loj_id
                                       select proInfo).FirstOrDefault();


            /*produtoInfo.proInfo_excluido = produtoInfo.proInfo_id;
            produtoInfo.proInfo_dataHoraAtualizacao = DateTime.Now;
            */
            lojaEntities.ProdutoInfo.Remove(produtoInfo);

            lojaEntities.SaveChanges();

            return retorno;
        }

        public void InserirProdutoInfoItem(ProdutoInfoItem produtoInfoItem)
        {            
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                produtoInfoItem.loj_id = loj_id;
                produtoInfoItem.proInfoItem_dataHora = DateTime.Now;
                lojaEntities.ProdutoInfoItem.Add(produtoInfoItem);
                lojaEntities.SaveChanges();
            }
        }

        public void AtualizarProdutoInfoItem(ProdutoInfoItem produtoInfoItem)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfoItem prodInfoItem = (from proInfoItem in lojaEntities.ProdutoInfoItem
                                        where proInfoItem.proInfoItem_id == produtoInfoItem.proInfoItem_id
                                        && proInfoItem.loj_id == loj_id
                                        select proInfoItem).FirstOrDefault();

            prodInfoItem.proInfoItem_descricao = produtoInfoItem.proInfoItem_descricao;
            prodInfoItem.proInfoItem_valor = produtoInfoItem.proInfoItem_valor;
            prodInfoItem.proInfoItem_bloquear = produtoInfoItem.proInfoItem_bloquear;
            prodInfoItem.proInfoItem_dataHoraAtualizacao = DateTime.Now;

            lojaEntities.SaveChanges();
        }

        public ProdutoInfoItem SelecionarProdutoInfoItem(Int32 proInfoItem_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfoItem produtoInfoItem = (from proInfoItem in lojaEntities.ProdutoInfoItem
                                               where proInfoItem.proInfoItem_id == proInfoItem_id &&
                                               proInfoItem.loj_id == loj_id
                                               select proInfoItem).FirstOrDefault();
            return produtoInfoItem;
        }

        public Retorno ExcluirProdutoInfoItem(Int32 proInfoItem_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();
            ProdutoInfoItem produtoInfoItem = (from proInfoItem in lojaEntities.ProdutoInfoItem
                                               where proInfoItem.proInfoItem_id == proInfoItem_id
                                               && proInfoItem.loj_id == loj_id
                                               select proInfoItem).FirstOrDefault();

            /*produtoInfoItem.proInfoItem_excluido = produtoInfoItem.proInfoItem_id;
            produtoInfoItem.proInfoItem_dataHoraAtualizacao = DateTime.Now;
           */
            lojaEntities.ProdutoInfoItem.Remove(produtoInfoItem);
            
            lojaEntities.SaveChanges();

            return retorno;
        }

        public void InserirMarca(Marca marca)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                marca.loj_id = loj_id;
                marca.mar_dataHora = DateTime.Now;
                lojaEntities.Marca.Add(marca);
                lojaEntities.SaveChanges();
            }
        }

        public IQueryable SelecionaMarca()
        {
            LojaEntities lojaEntities = new LojaEntities();
            IQueryable result = (from mar in lojaEntities.Marca
                                 where mar.loj_id == loj_id
                                 select mar);

            return result;
        }

        public Marca SelecionaMarca(Int32 mar_id)
        {
            LojaEntities lojaEntities = new LojaEntities();
            Marca marca = (from mar in lojaEntities.Marca
                                       where mar.mar_id == mar_id &&
                                        mar.loj_id == loj_id 
                                       select mar).FirstOrDefault();
            return marca;
        }

        public void AtualizarMarca(Marca marca)
        {
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                Marca ma = (from mar in lojaEntities.Marca
                               where mar.mar_id == marca.mar_id &&
                                mar.loj_id == loj_id
                               select mar).FirstOrDefault();

                ma.mar_nome = marca.mar_nome;
                ma.mar_posicao = marca.mar_posicao;
                ma.mar_bloquear = marca.mar_bloquear;
                ma.mar_descricao = marca.mar_descricao;
                ma.mar_dataHoraAtualizacao = DateTime.Now;
                lojaEntities.SaveChanges();
            }


          /*  marca.mar_dataHoraAtualizacao = DateTime.Now;
            object originalItem;
            using (LojaEntities lojaEntities = new LojaEntities())
            {
                EntityKey key = lojaEntities.CreateEntityKey("Marca", marca);

                if (lojaEntities.TryGetObjectByKey(key, out originalItem))
                {
                    lojaEntities.ApplyCurrentValues(key.EntitySetName, marca);
                }
                lojaEntities.SaveChanges();
            }*/
        }

    }
}