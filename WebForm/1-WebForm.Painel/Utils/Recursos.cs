using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;
using System.Data.Objects;
using System.Web.UI;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.ProdutoSkux;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using _2_Library.Modelo;

namespace Loja.Utils
{
    public static class Recursos
    {
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

        public static void DesabilitarDuploSemValidarImageClick(System.Web.UI.WebControls.ImageButton botao, string caminhoImagem)
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

        public static void RedirectAndPOST(System.Web.UI.Page page, string destinationUrl, System.Collections.Specialized.NameValueCollection data, Boolean novaPagina)
        {
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

        public static string SelecionarCampoOrdenacao()
        {
            string campo = string.Empty;
            if (HttpContext.Current.Request.QueryString["ordenar"] != null)
                if (HttpContext.Current.Request.QueryString["ordenar"] == "menorpreco")
                {
                    campo = "proSku_precoVenda ASC";
                }
                else if (HttpContext.Current.Request.QueryString["ordenar"] == "maiorpreco")
                {
                    campo = "proSku_precoVenda DESC";
                }
            return campo;
        }

        //Calcula parcelamento (cada sku)
        public static ParcelamentoBean CalculaParcelamento(ParcelamentoBean parcelamentoBean, decimal proSku_precoVenda)
        {
            if (!parcelamentoBean.parc_bloquear && (DateTime.Now >= (parcelamentoBean.parc_periodoDe ?? DateTime.Now.AddDays(-1)) && DateTime.Now <= (parcelamentoBean.parc_periodoAte ?? DateTime.Now.AddDays(1))))
            {
                var ParcelamentoParcelaBean = parcelamentoBean.Parcelamento_ParcelaBean;
                parcelamentoBean.parc_quantidade = ParcelamentoParcelaBean.Count();
                parcelamentoBean.parc_valor = (proSku_precoVenda / parcelamentoBean.parc_quantidade);
                parcelamentoBean.parc_bloquear = true;
                parcelamentoBean.parc_valorMinimo = (parcelamentoBean.parc_valorMinimo ?? 0);

                if (parcelamentoBean.parc_valor >= parcelamentoBean.parc_valorMinimo)
                    parcelamentoBean.parc_bloquear = false;
                else
                    for (int i = parcelamentoBean.parc_quantidade; i > 1; i--)
                    {
                        parcelamentoBean.parc_valor = (proSku_precoVenda / i);

                        if (parcelamentoBean.parc_valor >= parcelamentoBean.parc_valorMinimo)
                        {
                            parcelamentoBean.parc_quantidade = i;
                            parcelamentoBean.parc_bloquear = false;
                            break;
                        }
                    }

                if (parcelamentoBean.parc_ativarJuro)
                {
                    decimal? percentualJuros = ParcelamentoParcelaBean.Where(s => s.parcPar_quantidade == parcelamentoBean.parc_quantidade).SingleOrDefault().parcPar_percentualJuro;
                    decimal valorJuro = (((percentualJuros ?? 0) / 100) * parcelamentoBean.parc_valor);
                    parcelamentoBean.parc_valor = parcelamentoBean.parc_valor + valorJuro;
                }
            }
            else
            { 
                parcelamentoBean.parc_quantidade = 1;
                parcelamentoBean.parc_bloquear = true;
                parcelamentoBean.parc_valor = proSku_precoVenda;
            }
            return parcelamentoBean;
        }

        //Calcula parcelamento (carrinho/finalizacaoPedido)
        public static ParcelamentoBean CalculaParcelamento2(Parcelamento parcelamento, decimal proSku_precoVenda)
        {
            List<ParcelamentoParcelaBean> parcelas = new List<ParcelamentoParcelaBean>();
            ParcelamentoBean parcelamentoBean = (parcelamento != null ? (parcelamentoBean = new ParcelamentoBean
                                                      {
                                                          Parcelamento_ParcelaBean = parcelamento.ParcelamentoParcela.Select(s2 => new ParcelamentoParcelaBean {parcPar_quantidade =  s2.parcPar_quantidade, parcPar_percentualJuro = s2.parcPar_percentualJuro }),
                                                          parc_id = parcelamento.parc_id,
                                                          parc_valorMinimo = parcelamento.parc_valorMinimo,
                                                          parc_ativarJuro = parcelamento.parc_ativarJuro,
                                                          parc_bloquear = parcelamento.parc_bloquear,
                                                          parc_periodoDe = parcelamento.parc_periodoDe,
                                                          parc_periodoAte = parcelamento.parc_periodoAte
                                                      }) : new ParcelamentoBean { parc_bloquear = true });

            if (!parcelamentoBean.parc_bloquear && (DateTime.Now >= (parcelamentoBean.parc_periodoDe ?? DateTime.Now.AddDays(-1)) && DateTime.Now <= (parcelamentoBean.parc_periodoAte ?? DateTime.Now.AddDays(1))))
            {
                var parcelamentoParcelaBean = parcelamentoBean.Parcelamento_ParcelaBean;
                var parc_quantidade = parcelamentoParcelaBean.Count();
                parcelamentoBean.parc_quantidade = parc_quantidade;

                decimal par_valor = (proSku_precoVenda / parc_quantidade);
                parcelamentoBean.parc_bloquear = true;
                parcelamentoBean.parc_valorMinimo = (parcelamentoBean.parc_valorMinimo ?? 0);

                if (par_valor >= parcelamentoBean.parc_valorMinimo)
                    parcelamentoBean.parc_bloquear = false;
                else
                {
                    for (int i = parc_quantidade; i > 1; i--)
                    {
                        par_valor = (proSku_precoVenda / i);

                        if (par_valor >= parcelamentoBean.parc_valorMinimo)
                        {
                            parcelamentoBean.parc_quantidade = i;
                            parcelamentoBean.parc_bloquear = false;
                            break;
                        }
                    }

                }

                if (parcelamentoBean.parc_valorMinimo >= par_valor)
                {
                    parcelamentoBean.parc_bloquear = false;
                    parcelamentoBean.parc_quantidade = 1;
                }


                if (!parcelamentoBean.parc_bloquear)
                    foreach (ParcelamentoParcelaBean parcelamentoParcela in parcelamentoBean.Parcelamento_ParcelaBean.Where(s => s.parcPar_quantidade <= parcelamentoBean.parc_quantidade))
                    {
                        par_valor = proSku_precoVenda / parcelamentoParcela.parcPar_quantidade;
                        parcelamentoParcela.parcPar_valor = par_valor;
                        if (parcelamentoBean.parc_ativarJuro && parcelamentoParcela.parcPar_percentualJuro.HasValue)
                        {
                            decimal valorJuro = (((parcelamentoParcela.parcPar_percentualJuro ?? 0) / 100) * parcelamentoParcela.parcPar_valor);
                            parcelamentoParcela.parcPar_valor = parcelamentoParcela.parcPar_valor + valorJuro;
                            parcelamentoParcela.parcPar_condicao = parcelamentoParcela.parcPar_quantidade + " x de " + Recursos.ValorComFormatacao(parcelamentoParcela.parcPar_valor, 2);
                        }
                        else
                        {
                            parcelamentoParcela.parcPar_condicao = parcelamentoParcela.parcPar_quantidade + " x de " + Recursos.ValorComFormatacao(parcelamentoParcela.parcPar_valor, 2) + " sem juros";
                        }

                        parcelas.Add(parcelamentoParcela);
                    }
                else
                {
                    parcelamentoBean.parc_quantidade = 1;
                    parcelamentoBean.parc_bloquear = true;
                    parcelas.Add(new ParcelamentoParcelaBean() { parcPar_quantidade = 1, parcPar_valor = proSku_precoVenda, parcPar_condicao = 1 + " x de " + Recursos.ValorComFormatacao(proSku_precoVenda, 2), parcPar_percentualJuro = 0 });
                }
            }
            else
            {
                parcelamentoBean.parc_quantidade = 1;
                parcelamentoBean.parc_bloquear = true;
                parcelas.Add(new ParcelamentoParcelaBean() { parcPar_quantidade = 1, parcPar_valor = proSku_precoVenda, parcPar_condicao = 1 + " x de " + Recursos.ValorComFormatacao(proSku_precoVenda, 2), parcPar_percentualJuro = 0 });
            }

            parcelamentoBean.Parcelamento_ParcelaBean = parcelas;

            return parcelamentoBean;
        }

        public static string SelecionarIp()
        {
            string ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return ipaddress;
        }

        public static DateTime StringToDate(string dia, string mes, string ano)
        {
            string dataString = dia + "/" + mes + "/" + ano;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            DateTime data = Convert.ToDateTime(dataString);

            return data;
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

        public static void Redireciona301(string url) {

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

        public static int? GetMaxLength(string entityTypeName, string columnName)
        {
            int? result = null;
            using (LojaEntities context = new LojaEntities())
            {
                Type entType = Type.GetType(entityTypeName);


                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var test = objectContext.MetadataWorkspace.GetItems(DataSpace.CSpace);

                var q = from meta in objectContext.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                  .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                        from p in (meta as EntityType).Properties
                        .Where(p => p.Name == columnName
                                    && p.TypeUsage.EdmType.Name == "String")
                        select p;

                var queryResult = q.Where(p =>
                {
                    bool match = p.DeclaringType.Name == entityTypeName;
                    if (!match && entType != null)
                    {
                        match = entType.Name == p.DeclaringType.Name;
                    }

                    return match;

                }).Select(sel => sel.TypeUsage.Facets["MaxLength"].Value);
                if (queryResult.Any())
                {
                    result = Convert.ToInt32(queryResult.First());
                }

                return result;
            }
            //var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        }

        public static int? GetMaxLength<T>(Expression<Func<T, string>> column)
        {
            int? result = null;
            using (var context = new LojaEntities())
            {
                var entType = typeof(T);
                var columnName = ((MemberExpression)column.Body).Member.Name;

                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var test = objectContext.MetadataWorkspace.GetItems(DataSpace.CSpace);
               // var test = from meta in context.MetadataWorkspace.GetItems(DataSpace.CSpace) select meta;
                if (test == null)
                    return null;

                var q = test
                    .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                    .SelectMany(meta => ((EntityType)meta).Properties
                    .Where(p => p.Name == columnName && p.TypeUsage.EdmType.Name == "String"));

                var queryResult = q.Where(p =>
                {
                    var match = p.DeclaringType.Name == entType.Name;
                    if (!match)
                        match = entType.Name == p.DeclaringType.Name;

                    return match;

                })
                    .Select(sel => sel.TypeUsage.Facets["MaxLength"].Value)
                    .ToList();

                if (queryResult.Any())
                    result = Convert.ToInt32(queryResult.First());

                return result;
            }
        }

        public static string GetChaveCache(object[] objetos, string parametros)
        {
            string chaveCache = string.Empty;
            chaveCache = parametros;
            if (objetos != null && objetos.Count() > 0)
            { 
                foreach(var ob in objetos){
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