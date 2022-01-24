using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using _2_Library.Modelo;

namespace _2_Library.Utils
{
    public class Tratamento
    {

        public static string GerarNomeAmigavel(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //First to lower case
                value = value.ToLowerInvariant();

                //Remove all accents
                var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
                value = Encoding.ASCII.GetString(bytes);

                //Replace spaces
                value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

                //Remove invalid chars
                value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

                //Trim dashes from end
                value = value.Trim('-', '_');

                //Replace double occurences of - or _
                value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

                string proSku_idValida = value.Substring(value.LastIndexOf('-') + 1);
                Int32 tempnum = 0;
                bool hasNum = Int32.TryParse(proSku_idValida, out tempnum);
                if (hasNum)
                    value = value.Replace(string.Concat("-", proSku_idValida), "-e" + proSku_idValida.ToString());

                if (value.StartsWith("tamanho-"))
                    value.Replace("tamanho-", "tamanhoe-");
                if (value.StartsWith("cor-"))
                    value.Replace("cor-", "core-");

            }
            return value;
        }

        public static string ExtrairGrupo(System.Xml.XmlNode node)
        {
            if (node != null && node.Attributes.Count > 0 && node.Attributes["gru_nome"].Value != string.Empty)
            {
                string gru_nomeAmigavel = node.Attributes["gru_nomeAmigavel"].Value;
                if (!string.IsNullOrEmpty(gru_nomeAmigavel))
                {

                    Static.resultadoNomeAmigavel += gru_nomeAmigavel + "/";
                    ExtrairGrupo(node.ParentNode);
                }
            }
            return string.Join("/", Static.resultadoNomeAmigavel.Split('/').Reverse());
        }

        List<int> idsGrupo = new List<int>();
        public List<int> ExtrairIdsGrupo(Grupo grupo)
        {
            if (grupo.gru_nome != null && grupo.gru_nome != "GRUPOS")
            {
                foreach (var gru in grupo.Grupo1) {
                    idsGrupo.Add(gru.gru_id);
                }
                ExtrairIdsGrupo(grupo.Grupo2);
            }
            return idsGrupo;
        }

    /*    List<GrupoDto> caminhoGrupo = new List<GrupoDto>();
        public List<GrupoDto> ExtrairCaminhoGrupo(Grupo grupo)
        {
            if (grupo.gru_nome != null && grupo.gru_nome != "GRUPOS")
            {
                caminhoGrupo.Add(new GrupoDto { gru_id = grupo.gru_id, gru_nome = grupo.gru_nome, gru_nomeAmigavel = grupo.gru_nomeAmigavel });
                ExtrairCaminhoGrupo(grupo.Grupo2);
            }
            return caminhoGrupo;
        }*/
        
        List<int> idsGrupoFinal = new List<int>();
        public List<int> ExtrairIdsGrupoFinal(Grupo grupo)
        {
            if (grupo != null && grupo.gru_nome != null /*&& grupo.gru_nome != "GRUPOS"*/)
            {
                var gruNivel = grupo.Grupo1.Where(s => s.loj_id == grupo.loj_id && s.gru_bloquear == false && s.gru_subBloquear == false).Select(s => 
                    new { 
                        cont = s.Grupo1.Where(s2 => s.loj_id == grupo.loj_id && s2.gru_bloquear == false && s2.gru_subBloquear == false).Count(),
                        s.gru_id });
                foreach (var gru in gruNivel)
                {
                    if (gru.cont == 0)
                        idsGrupoFinal.Add(gru.gru_id);
                }
                ExtrairIdsGrupoFinal(grupo.Grupo2);
            }
            return idsGrupoFinal;
        }

        public static String[] SplitString(String var)
        {
            String[] listString = new String[] { };

            if (var != null)
                listString = var.Replace("tamanho",string.Empty).TrimStart('-').Split('-');
            else listString = null;

            return listString;
        }

        public static string GetUrlAmigavelAtual() {

            return string.Join("/", HttpContext.Current.Request.RequestContext.RouteData.Values.Where(s => s.Key.StartsWith("pro_nome") || s.Key.StartsWith("gru_nomeAmigavel")).Select((pair) => string.Format("{0}", pair.Value)));//String.Join("/", HttpContext.Current.Request.RequestContext.RouteData.Values.Where(s => s.Key != "proSku_cores" && s.Key != "proSku_tamanhos" && s.Value != null && s.Value != string.Empty).Select(s => s.Value));
        }

        public static string GetUrlRouteCorTamanho()
        {
            var proSku_cores = HttpContext.Current.Request.RequestContext.RouteData.Values["proSku_cores"];
            var proSku_tamanhos = HttpContext.Current.Request.RequestContext.RouteData.Values["proSku_tamanhos"];


            string link = null;
            if (proSku_cores  != null)
            {
                link = "/cor-" + proSku_cores;
            }

            if (proSku_tamanhos != null)
            {
                link += "/tamanho-" + proSku_tamanhos;
            }

            return link;
        }

        public static string FiltroLimparTudo(string proSku_cores, string proSku_tamanhos)
        {
            string link = null;
            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                link += "/tamanho-" + proSku_tamanhos.ToLower();
            }

            if (!string.IsNullOrEmpty(proSku_cores))
            {
                link = "/cor-" + proSku_cores.ToLower();
            }


            link = GetUrlAmigavelAtual() + link;

            return link;
        
        }

        public static String FiltroTamanho(string proSku_tamanhos, string proSku_cores, string proSkuTam_nome)
        {
            string link = null;

            if (!string.IsNullOrEmpty(proSku_cores))
            {
                link = "/cor-" + proSku_cores.ToLower();
            }
            if(proSkuTam_nome != null)
               proSkuTam_nome = proSkuTam_nome.ToLower();

            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                var listTamanhos = proSku_tamanhos.ToLower().Split('-').Except(proSkuTam_nome.Split('-')).ToList();
                listTamanhos.Add(proSkuTam_nome.ToLower());
                link += "/tamanho-" + string.Join("-", listTamanhos.OrderBy(s => s));
            }
            else
            {
                if (!string.IsNullOrEmpty(proSkuTam_nome))
                    link += "/tamanho-" + proSkuTam_nome.ToLower();
            }

            link = GetUrlAmigavelAtual() + link;

            return link;
        }

        public static String FiltroCor(string proSku_cores, string proSku_tamanhos, string proSkuCor_nome)
        {          
            string link = null;

            if (proSkuCor_nome != null)
                proSkuCor_nome = proSkuCor_nome.ToLower();

            if (!string.IsNullOrEmpty(proSku_cores))
            {
                var listCores = proSku_cores.ToLower().Split('-').Except(proSkuCor_nome.Split('-')).ToList();
                listCores.Add(proSkuCor_nome.ToLower());
                link = "/cor-" + string.Join("-", listCores.OrderBy(s => s));
            }
            else {
                if (!string.IsNullOrEmpty(proSkuCor_nome))
                  link = "/cor-" + proSkuCor_nome.ToLower();
            }

            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                link += "/tamanho-" + proSku_tamanhos.ToLower();
            }

            link = GetUrlAmigavelAtual() + link;

            return link;
        }

        public static bool FiltroTamanhoConsultada(string proSku_tamanhos, string proSkuTam_nome)
        {
            string link = string.Empty;

            if (!string.IsNullOrEmpty(proSku_tamanhos) && !string.IsNullOrEmpty(proSkuTam_nome))
            {
                if (proSku_tamanhos.Split('-').Where(s => s.ToString() == proSkuTam_nome.ToLower()).Count() > 0)
                    return true;
            }
            return false;
        }
        
        public static bool FiltroCorConsultada(string proSku_cores, string proSkuCor_nome)
        {    
            if (!string.IsNullOrEmpty(proSku_cores) && !string.IsNullOrEmpty(proSkuCor_nome))
            {
                if (proSku_cores.Split('-').Where(s => s.ToString() == proSkuCor_nome.ToLower()).Count() > 0)
                    return true;
            }
            return false;
        }

        public static String[] FiltroCorLimparList(string proSku_cores)
        {
            if (!string.IsNullOrEmpty(proSku_cores))
            {
              return proSku_cores.Split('-');
            }
            return new String[]{};
        }

        public static String FiltroCorLimparRemover(string proSku_cores, string proSku_tamanhos, string proSkuCor_nome)
        {
           String[] listCores = new String[]{};

           string link = null;
           if (!string.IsNullOrEmpty(proSku_cores))
           {
               listCores = proSku_cores.ToLower().Split('-').Except(proSkuCor_nome.ToLower().Split('-')).ToArray();
               if (listCores.Count() > 0)
                   link = "/cor-" + string.Join("-", listCores.OrderBy(s => s));
           }

            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                link += "/tamanho-" + proSku_tamanhos.ToLower();
            }

            link = GetUrlAmigavelAtual() + link;

            return link;
        }

        public static String[] FiltroTamanhoLimparList(string proSku_tamanhos)
        {
            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                return proSku_tamanhos.Split('-');
            }
            return new String[] { };
        }

        public static String FiltroTamanhoLimparRemover(string proSku_tamanhos, string proSku_cores, string proSkuTam_nome)
        {
            String[] listTamanhos = new String[] { };

            string link = null;

            if (!string.IsNullOrEmpty(proSku_cores))
            {
                link = "/cor-" + proSku_cores.ToLower();
            }

            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                listTamanhos = proSku_tamanhos.ToLower().Split('-').Except(proSkuTam_nome.ToLower().Split('-')).ToArray();
                if (listTamanhos.Count() > 0)
                    link += "/tamanho-" + string.Join("-", listTamanhos.OrderBy(s => s));
            }

            link = GetUrlAmigavelAtual() + link;

            return link;
        }


       /* public static ProdutoSkuFotoDto TrataProdutoSkuFoto(ProdutoSkuFotoDto produtoSkuFotoDto, int pro_id, int loj_id)
        {
            if (produtoSkuFotoDto == null)
            {
                produtoSkuFotoDto = new ProdutoSkuFotoDto();
                produtoSkuFotoDto.proSkuFot_nome = "sem-foto";
                produtoSkuFotoDto.proSkuFot_extensao = ".jpg";
                produtoSkuFotoDto.pro_id = pro_id;
                produtoSkuFotoDto.loj_id = loj_id;
            }
            return produtoSkuFotoDto;
        }*/

    }
}