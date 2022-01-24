using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Parcelamentox;
using _2_Library.Modelo;
using System.Configuration;

namespace Loja.Modelo
{
    public class Static
    {
        public static string resultadoNomeAmigavel;
        public static List<MensagemSistema> mensagemSistema = new List<MensagemSistema>();
      
        public static Retorno MensagemSistema(Int32 mensSis)
        {
            if (mensagemSistema.Count == 0)
                mensagemSistema = new Consulta().SelecionaMensagemSistema();

            return mensagemSistema.Where(s => s.menSis_id == mensSis).Select(s => new Retorno { menSis_id = s.menSis_id, menSis_mensagem = s.menSis_mensagem }).FirstOrDefault();
        }

        public static IQueryable<ParcelamentoBean> SelecionarParcelamento(Int32 forPag_id, Int32 parc_id, decimal valor) {
           return new ParcelamentoDao().SelecionarParcelamento(forPag_id, parc_id, valor);
        }
        
        
        public static string urlHttp = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host;
        public static string aplicacaoNome = System.Web.HttpRuntime.AppDomainAppVirtualPath.Replace("/", "\\");
        
        
        public static string pastaFoto = HttpContext.Current.Request.MapPath(".").Replace(aplicacaoNome, string.Empty) + @"\imagens\produtos\fotos";
        public static string pastaCss = HttpContext.Current.Request.MapPath(".").Replace(aplicacaoNome, string.Empty) + @"\css";

        public static String caminhoDiretorioFoto = string.IsNullOrEmpty(ConfigurationManager.AppSettings["pasta.imagens.produtos.fotos"]) ? pastaFoto : ConfigurationManager.AppSettings["pasta.imagens.produtos.fotos"];
        public static String caminhoDiretorioCss  = string.IsNullOrEmpty(ConfigurationManager.AppSettings["pasta.css"])                    ? pastaCss  : ConfigurationManager.AppSettings["pasta.css"];
    }
}