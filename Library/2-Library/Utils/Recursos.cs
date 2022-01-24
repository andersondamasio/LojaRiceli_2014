using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;
using _2_Library.Modelo;

namespace _2_Library.Utils
{
    public class Recursos
    {
        public static string SelecionaUrlDominio(){
        
        string dominio = HttpContext.Current.Request.Url.Host;

        return dominio;
        }

        public static string GetRouteUrl(string routeName, object routeParameters)
        {
            var dict = new RouteValueDictionary(routeParameters);
            var data = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, routeName, dict);
            if (data != null)
            {
                return data.VirtualPath;
            }
            return null;
        }

        public static void DesabilitarDuploClick(System.Web.UI.WebControls.Button botao, string mensagem)
        {
            string theScript = "";
            if (botao.CausesValidation)
            {
                theScript = @"
                    if (typeof(Page_ClientValidate) == 'function') 
                       { 
                        if (Page_ClientValidate() == false )
                            return false; 
                       }";
            }
            theScript += @"
                         this.value = '" + mensagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
            botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

            botao.Attributes["onclick"] = theScript;
        }

        public static void DesabilitarDuploClick(System.Web.UI.WebControls.Button botao, string mensagem, string groupValidade)
        {
            string theScript = "";
            if (botao.CausesValidation)
            {
                theScript = @"
                    if (typeof(Page_ClientValidate) == 'function') 
                       { 
                        if (Page_ClientValidate('" + groupValidade + @"') == false )
                            return false; 
                       }";
            }
            theScript += @"
                         this.value = '" + mensagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
            botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

            botao.Attributes["onclick"] = theScript;
        }

        public static void DesabilitarSemValidarDuploClick(LinkButton botao, string mensagem)
        {
            if (botao != null)
            {
                string theScript = "";
                theScript += @"
                         this.innerHTML = '" + mensagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
                botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

                botao.Attributes["onclick"] = theScript;
            }
        }

        public static void DesabilitarDuploSemValidarClick(System.Web.UI.WebControls.ImageButton botao, string caminhoImagem)
        {
            string theScript = "";
            theScript += @"
                         this.src = '" + caminhoImagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
            botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

            botao.Attributes["onclick"] = theScript;

        }

        public static void DesabilitarDuploSemValidarClick(System.Web.UI.WebControls.Button botao, string mensagem)
        {
            if (botao != null)
            {
                string theScript = "";
                theScript += @"
                         this.value = '" + mensagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
                botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

                botao.Attributes["onclick"] = theScript;
            }
        }

        public static void DesabilitarDuploSemValidarClick(System.Web.UI.WebControls.LinkButton botao, string mensagem)
        {
            string theScript = "";
            theScript += @"
                         this.value = '" + mensagem + @"';
                         this.disabled = true;
                         document.getElementById('" + botao.ClientID + @"').disabled = true;" +
            botao.Page.ClientScript.GetPostBackEventReference(botao, string.Empty) + @";";

            botao.Attributes["onclick"] = theScript;

        }

        public static string HttpPost(string URI, string Parameters)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                //req.Proxy = new System.Net.WebProxy();

                //Add these, as we're doing a POST
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                req.ContentLength = bytes.Length;
                System.IO.Stream os = req.GetRequestStream();
                os.Write(bytes, 0, bytes.Length); //Push it out there
                os.Close();
                System.Net.WebResponse resp = req.GetResponse();
                if (resp == null) return null;
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream(), encoding);
                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {

                //Log.Log.GerarLogErro(string.Empty, ex.Message, "HttpPost > url=" + URI + " parametros " + Parameters);
                return string.Empty;
            }
        }

        public static void RedirectAndPOST(string destinationUrl, System.Collections.Specialized.NameValueCollection data, Boolean novaPagina)
        {
            System.Web.UI.Page page = new System.Web.UI.Page();
            var http = System.Web.HttpContext.Current;
            if ((http != null))
            {
                page = http.CurrentHandler as System.Web.UI.Page;
            }
            string strForm = PreparePOSTForm(destinationUrl, data, novaPagina);
            page.Controls.Add(new System.Web.UI.LiteralControl(strForm));
        }

        private static String PreparePOSTForm(string url, System.Collections.Specialized.NameValueCollection data, Boolean novaPagina)
        {
            string formID = "PostForm";

            System.Text.StringBuilder strForm = new System.Text.StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\"  target=\"" + (novaPagina ? "_blank" : string.Empty) + "\">");
            strForm.Append("<script>document.getElementById(\"carregando\").style.display = '';</script>");
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\"/>");
            }
            strForm.Append("</form>");

            System.Text.StringBuilder strScript = new System.Text.StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            return strForm.ToString() + strScript.ToString();
        }

        public static DateTime StringToDate(string dia, string mes, string ano)
        {
            string dataString = dia + "/" + mes + "/" + ano;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            DateTime data = Convert.ToDateTime(dataString);

            return data;
        }

        public static void DownloadClientArquivo(string caminhoArquivo)
        {
            FileInfo fileInfo = new FileInfo(caminhoArquivo);
            System.IO.FileInfo file = new System.IO.FileInfo(caminhoArquivo);
            if (file.Exists)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(file.FullName);
                HttpContext.Current.Response.End();
            }
        }

        public static void EscreverConsulta(string consulta)
        {
            try
            {
                string path = @"..\..\Queries\" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nQuery Log Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString());
                    w.WriteLine("Query:" + consulta);
                    w.WriteLine("__________________________");
                    w.Flush();
                }
            }
            catch (Exception ex) { }
        }

        public static void AtualizaArquivoCache()
        {

            TextWriter tw = new StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + @"\Cache\Cache.txt");
            tw.WriteLine(DateTime.Now);
            tw.Close();
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.FilePath);
        }

        public static string GetCaminhoArquivoCache()
        {
            string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + @"\Cache";
            string arquivo = diretorio + @"\Cache.txt";

            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
            if (!File.Exists(arquivo))
                File.Create(arquivo);
            return "Cache/Cache.txt";
        }

        public static void SetPaginaCache(DateTime tempoCache)
        {

            string fileDependencyPath = HttpContext.Current.Server.MapPath(GetCaminhoArquivoCache());
            HttpContext.Current.Response.AddFileDependency(fileDependencyPath);

            HttpContext.Current.Response.Cache.SetExpires(tempoCache);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
            HttpContext.Current.Response.Cache.SetValidUntilExpires(true);
        }

        public static List<int> SelecionaDiasMes(int ano, int mes)
        {
            DateTime primeiroDiaDoMes = new DateTime(ano, mes, 01);
            DateTime primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
            DateTime ultimoDiaDoMes = primeiroDiaDoProximoMes.AddDays(-1);

            List<int> listaDias = new List<int>();

            for (int i = 1; i <= ultimoDiaDoMes.Day; i++)
            {
                listaDias.Add(i);
            }
            return listaDias;
        }

        public static string SelecionarCampoOrdenacao(LinkButton LinkButtonOrderMenorPreco, LinkButton LinkButtonOrderMaiorPreco, LinkButton LinkButtonOrderMaiorDesconto)
        {
            string campo = string.Empty;
            if (HttpContext.Current.Request.QueryString["ordenar"] != null)
                if (HttpContext.Current.Request.QueryString["ordenar"] == "menorpreco")
                {
                    campo = "proSku_precoVendaMenor ASC";
                    LinkButtonOrderMenorPreco.Attributes.Add("style", "font-weight:bold");
                }
                else if (HttpContext.Current.Request.QueryString["ordenar"] == "maiorpreco")
                {
                    campo = "proSku_precoVendaMaior DESC";
                    LinkButtonOrderMaiorPreco.Attributes.Add("style", "font-weight:bold");
                }
                else if (HttpContext.Current.Request.QueryString["ordenar"] == "maiordesconto")
                {
                    campo = "proSku_percDesconto DESC";
                    LinkButtonOrderMaiorDesconto.Attributes.Add("style", "font-weight:bold");
                }
            return campo;
        }

        public static string SelecionarIp()
        {
            string ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }

        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public static string ValorComFormatacao(decimal vlr, short decimais)
        {
            try
            {
                // Fator para ajustar casas decimais
                int fator = Convert.ToInt32("1" + (new string('0', decimais)));
                // Converte para Double
                //Double vlr = Convert.ToDouble(valor);
                // Despreza excesso de decimais
                vlr = Math.Truncate(vlr * fator) / fator;
                // Obtendo o formato corrente de n£mero para a cultura Atual
                System.Globalization.NumberFormatInfo culturaAtual = (System.Globalization.NumberFormatInfo)System.Globalization.NumberFormatInfo.CurrentInfo.Clone();
                // Ajustando a quantidade de casas decimais para o desejado...
                culturaAtual.NumberDecimalDigits = decimais;
                // Retorna o n£mero formatado conforme a cultura atual
                return (vlr.ToString("N", culturaAtual));
            }
            catch
            {
                // Sem formata‡Æo
                return vlr.ToString();
            }
        }

        /// <param name="value">String to count chars.</param>
        /// <returns>Number of chars in string.</returns>
        public static int CountChars(string value)
        {
            int result = 0;
            bool lastWasSpace = false;

            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    // A.
                    // Only count sequential spaces one time.
                    if (lastWasSpace == false)
                    {
                        result++;
                    }
                    lastWasSpace = true;
                }
                else
                {
                    // B.
                    // Count other characters every time.
                    result++;
                    lastWasSpace = false;
                }
            }
            return result;
        }

        public static void CopiarPasta(string origem, string destino)
        {
            if (Directory.Exists(origem))
            {
                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(origem);
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo tempfile in files)
                {
                    tempfile.CopyTo(Path.Combine(destino, tempfile.Name));
                }
                DirectoryInfo[] dirctororys = dirInfo.GetDirectories();
                foreach (DirectoryInfo tempdir in dirctororys)
                {
                    CopiarPasta(Path.Combine(origem, tempdir.Name), Path.Combine(destino, tempdir.Name));
                }
            }
        }

        public static void ExcluirPasta(string destino)
        {
            if (Directory.Exists(destino))
            {
                foreach (var file in Directory.GetFiles(destino))
                    File.Delete(file);

                Directory.Delete(destino);
            }
        }

        public static void Redireciona301(string url)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", url);
            HttpContext.Current.Response.End();
        }

        public static void Redireciona404()
        {
            HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath, false);
        }

        public static void Redireciona404(string url)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
            HttpContext.Current.Response.StatusDescription = "Página não encontrada";
            HttpContext.Current.Response.AddHeader("Location", url);
            HttpContext.Current.Response.End();

        }

        public static string GetChaveCache(object[] objetos, string parametros)
        {
            string chaveCache = string.Empty;
            chaveCache = parametros;
            if (objetos != null && objetos.Count() > 0)
            {
                foreach (var ob in objetos)
                {
                    if (ob is string[])
                    {
                        chaveCache += string.Join("_", (string[])ob);
                    }
                    else
                    {
                        Type t = ob.GetType();
                        var campoValor = new object();
                        t = ob.GetType();

                        FieldInfo[] fieldInfo = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                        foreach (FieldInfo campos in fieldInfo)
                        {
                            chaveCache += t.InvokeMember(campos.Name, BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic, null, ob, null) + "-";
                        }
                    }
                }
            }
            return chaveCache;
        }

        public static object SelecionarCampoOrdenacao()
        {
            throw new NotImplementedException();
        }

        public static string Hash(string chave)
        {
            Byte[] cleBytes = UnicodeEncoding.Unicode.GetBytes(chave);
            Byte[] hasBytes = (System.Security.Cryptography.CryptoConfig.CreateFromName("MD5") as System.Security.Cryptography.HashAlgorithm).ComputeHash(cleBytes);
            return BitConverter.ToString(hasBytes).Replace("-", String.Empty);
        }

        public static String MD5Hash(String Text)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static void AdicionaCookie(string nome,string valor, int numeroDiasExpira)
        {
            HttpCookie carCookieLoja = HttpContext.Current.Response.Cookies[nome];
            carCookieLoja.Value = valor;
            carCookieLoja.Expires = DateTime.Now.AddDays(numeroDiasExpira);
        }

        public static HttpCookie RecuperaCookie(string nome)
        {
            HttpCookie carCookieLoja = HttpContext.Current.Request.Cookies[nome];

            return carCookieLoja != null && carCookieLoja.Value != string.Empty ? carCookieLoja : null;
        }

        public static void RemoveCookie(string nome)
        {
            HttpCookie carCookieLoja = HttpContext.Current.Request.Cookies[nome];
            if (carCookieLoja != null)
            {
                carCookieLoja.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(carCookieLoja);
            }
        }

        public static void AtualizaCookie(string nome, string valor, int numeroDiasExpira)
        {
            HttpCookie carCookieLoja = HttpContext.Current.Request.Cookies[nome];
            if (carCookieLoja != null)
            {
                carCookieLoja.Value = valor;
                carCookieLoja.Expires = DateTime.Now.AddDays(numeroDiasExpira);
                HttpContext.Current.Response.SetCookie(carCookieLoja);
            }
        }

        public static void EscreveLog(string caminho,string mensagem)
        {
            using (StreamWriter w = File.AppendText(caminho))
            {
                w.WriteLineAsync(mensagem);
            }
          
        }

     
      public static string SetCache(object[] objetos,  string parametros)
        {
            string chaveCache = string.Empty;
            chaveCache = parametros;
            if (objetos != null && objetos.Count() > 0)
            {
                foreach (var ob in objetos)
                {
                    if (ob is string[])
                    {
                        chaveCache += string.Join("_", (string[])ob);
                    }
                    else
                    {
                        Type t = ob.GetType();
                        var campoValor = new object();
                        t = ob.GetType();

                        FieldInfo[] fieldInfo = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                        foreach (FieldInfo campos in fieldInfo)
                        {
                            chaveCache += t.InvokeMember(campos.Name, BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic, null, ob, null) + "-";
                        }
                    }
                }
            }
            return chaveCache;
        }
      
    }
}
