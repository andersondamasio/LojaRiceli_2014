using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using Loja.Modelo;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using _2_Library.Modelo;

namespace Loja.Utils
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
                    value = value.Replace(string.Concat("-",proSku_idValida), "-e"+proSku_idValida.ToString());

                if (value.StartsWith("tamanho-"))
                    value.Replace("tamanho-", "tamanhoe-");
                if (value.StartsWith("cor-"))
                    value.Replace("cor-", "core-");

            }
            return value;
        }

        List<string> gruAmigavelCorreto = new List<string>();
        public List<string> ExtrairGrupo(Grupo grupo)
        {
            if (grupo.gru_nome != null && grupo.gru_nome != "GRUPOS")
            {
                gruAmigavelCorreto.Add(grupo.gru_nomeAmigavel);
                ExtrairGrupo(grupo.Grupo2);
            }

            return gruAmigavelCorreto.ToArray().Reverse().ToList();
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

        private Boolean grupoPaiBloqueado = false;
        public Boolean VerificaGrupoBloqueadoPai(Grupo grupo)
        {
            if (grupo != null)
            if (grupo.gru_nome != null && grupo.gru_nome != "GRUPOS")
            {
                if (grupo.gru_bloquear)
                    grupoPaiBloqueado = true;

                VerificaGrupoBloqueadoPai(grupo.Grupo2);
            }
            return grupoPaiBloqueado;
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

        public static Grupo SubBloqueiaGrupoFilho(Grupo grupo, Boolean subBloquear)
        {
            grupo.gru_subBloquear = false;
            foreach (var gru in grupo.Grupo1.Where(s=>s.gru_bloquear != true))
            {
                gru.gru_subBloquear = subBloquear;
                SubBloqueiaGrupoFilhoLista(gru.Grupo1, subBloquear);
            }
            return grupo;
        }

        public static ICollection<Grupo> SubBloqueiaGrupoFilhoLista(ICollection<Grupo> grupo1, Boolean subBloquear)
        {      
            foreach (var gru in grupo1.Where(s=>s.gru_bloquear != true))
            {
                gru.gru_subBloquear = subBloquear;
                SubBloqueiaGrupoFilhoLista(gru.Grupo1, subBloquear);
            }

            return grupo1;
        }

        public Retorno RedimensionaSalvaImagem(Configuracao configuracao, string fotoOriginal, string caminhoDiretorioFoto, string nomeFoto, string extensao)
        {
            Retorno retorno = new Retorno();
            
            Bitmap modificada = (Bitmap)System.Drawing.Image.FromFile(fotoOriginal);
            Bitmap bitmapOriginal = (Bitmap)System.Drawing.Image.FromFile(fotoOriginal);

            using (modificada)
            {
                using (bitmapOriginal)
                {
                    try
                    {

                        // Carrega imagem original

                        double caculoDimensao = ((double)bitmapOriginal.Width / configuracao.con_fotoProporcaoLargura.Value) * configuracao.con_fotoProporcaoAltura.Value;

                        caculoDimensao = Math.Round(caculoDimensao, MidpointRounding.AwayFromZero);

                        if (caculoDimensao == (bitmapOriginal.Height))
                        {

                            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                            info = ImageCodecInfo.GetImageEncoders();
                            EncoderParameters encoderParameters;
                            encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 97L);


                            caculoDimensao = ((double)configuracao.con_fotoMiniaturaLargura.Value / configuracao.con_fotoProporcaoLargura.Value) * configuracao.con_fotoProporcaoAltura.Value;
                            caculoDimensao = Math.Round(caculoDimensao, MidpointRounding.AwayFromZero);
                            if (caculoDimensao == (configuracao.con_fotoMiniaturaAltura.Value))
                            {
                                if ((bitmapOriginal.Width >= configuracao.con_fotoMiniaturaLargura.Value) && (bitmapOriginal.Height >= configuracao.con_fotoMiniaturaAltura.Value))
                                {
                                    GerarImagem("m", configuracao.con_fotoMiniaturaLargura.Value, configuracao.con_fotoMiniaturaAltura.Value, modificada, bitmapOriginal, caminhoDiretorioFoto, nomeFoto, extensao, info, encoderParameters);
                                }
                            }
                            else
                            {
                                retorno.menSis_id = -1;
                                retorno.menSis_mensagem = "Configuração de foto 'Miniatura' inválida: Largura:" + configuracao.con_fotoMiniaturaLargura.Value + " x Altura:" + configuracao.con_fotoMiniaturaAltura.Value + ".Não está nas proporções:" + configuracao.con_fotoProporcaoLargura.Value + "x" + configuracao.con_fotoProporcaoAltura.Value + ".Vá em configuração e ajuste as medidas.";
                                return retorno;
                            }


                            caculoDimensao = ((double)configuracao.con_fotoVitrineLargura.Value / configuracao.con_fotoProporcaoLargura.Value) * configuracao.con_fotoProporcaoAltura.Value;
                            caculoDimensao = Math.Round(caculoDimensao, MidpointRounding.AwayFromZero);
                            if (caculoDimensao == (configuracao.con_fotoVitrineAltura.Value))
                            {
                                if ((bitmapOriginal.Width >= configuracao.con_fotoVitrineLargura.Value) && (bitmapOriginal.Height >= configuracao.con_fotoVitrineAltura.Value))
                                {
                                    GerarImagem("v", configuracao.con_fotoVitrineLargura.Value, configuracao.con_fotoVitrineAltura.Value, modificada, bitmapOriginal, caminhoDiretorioFoto, nomeFoto, extensao, info, encoderParameters);
                                }
                            }
                            else
                            {
                                retorno.menSis_id = -1;
                                retorno.menSis_mensagem = "Configuração de foto 'Vitrine' inválida: Largura:" + configuracao.con_fotoVitrineLargura.Value + " x Altura:" + configuracao.con_fotoVitrineAltura.Value + ".Não está nas proporções:" + configuracao.con_fotoProporcaoLargura.Value + "x" + configuracao.con_fotoProporcaoAltura.Value + ".Vá em configuração e ajuste as medidas.";
                                return retorno;
                            }

                            caculoDimensao = ((double)configuracao.con_fotoDetalheLargura.Value / configuracao.con_fotoProporcaoLargura.Value) * configuracao.con_fotoProporcaoAltura.Value;
                            caculoDimensao = Math.Round(caculoDimensao, MidpointRounding.AwayFromZero);
                            if (caculoDimensao == (configuracao.con_fotoDetalheAltura.Value))
                            {
                                if ((bitmapOriginal.Width >= configuracao.con_fotoDetalheLargura.Value) && (bitmapOriginal.Height >= configuracao.con_fotoDetalheAltura.Value))
                                {
                                    GerarImagem("d", configuracao.con_fotoDetalheLargura.Value, configuracao.con_fotoDetalheAltura.Value, modificada, bitmapOriginal, caminhoDiretorioFoto, nomeFoto, extensao, info, encoderParameters);
                                }
                            }
                            else
                            {
                                retorno.menSis_id = -1;
                                retorno.menSis_mensagem = "Configuração de foto 'Detalhe' inválida: Largura:" + configuracao.con_fotoDetalheLargura.Value + " x Altura:" + configuracao.con_fotoDetalheAltura.Value + ".Não está nas proporções:" + configuracao.con_fotoProporcaoLargura.Value + "x" + configuracao.con_fotoProporcaoAltura.Value + ".Vá em configuração e ajuste as medidas.";
                                return retorno;
                            }

                            caculoDimensao = ((double)configuracao.con_fotoAmpliadaLargura.Value / configuracao.con_fotoProporcaoLargura.Value) * configuracao.con_fotoProporcaoAltura.Value;
                            caculoDimensao = Math.Round(caculoDimensao, MidpointRounding.AwayFromZero);
                            if (caculoDimensao == (configuracao.con_fotoAmpliadaAltura.Value))
                            {
                                if ((bitmapOriginal.Width >= configuracao.con_fotoAmpliadaLargura.Value) && (bitmapOriginal.Height >= configuracao.con_fotoAmpliadaAltura.Value))
                                {
                                    GerarImagem("a", configuracao.con_fotoAmpliadaLargura.Value, configuracao.con_fotoAmpliadaAltura.Value, modificada, bitmapOriginal, caminhoDiretorioFoto, nomeFoto, extensao, info, encoderParameters);
                                }
                            }
                            else
                            {
                                retorno.menSis_id = -1;
                                retorno.menSis_mensagem = "Configuração de foto 'Ampliada' inválida: Largura:" + configuracao.con_fotoAmpliadaLargura.Value + " x Altura:" + configuracao.con_fotoAmpliadaAltura.Value + ".Não está nas proporções:" + configuracao.con_fotoProporcaoLargura.Value + "x" + configuracao.con_fotoProporcaoAltura.Value + ".Vá em configuração e ajuste as medidas.";
                                return retorno;
                            }
                        }
                        else
                        {
                            retorno = Static.MensagemSistema(10);
                        }
                    }

                    catch (Exception ex)
                    {
                        retorno.menSis_id = -1;
                        retorno.menSis_mensagem = ex.Message;
                    }
                }
            }
            return retorno;
        }

        private void GerarImagem(string tipo, int largura, int altura, Bitmap modificada, Bitmap bitmapOriginal, string caminhoDiretorioFoto, string nomeFoto, string extensao, ImageCodecInfo[] info, EncoderParameters encoderParameters)
        {
            modificada = Redimensiona(bitmapOriginal, largura, altura);
            modificada.Save(Path.Combine(caminhoDiretorioFoto, nomeFoto + "-" + tipo + extensao), info[1], encoderParameters);
            modificada.Dispose();
        }

        private Bitmap Redimensiona(Bitmap original, int width, int height)
        {
            Bitmap imagemModifica = new Bitmap(width, height);

            //Redimensiona imagem
            Graphics g = Graphics.FromImage(imagemModifica);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, new Rectangle(0, 0, imagemModifica.Width, imagemModifica.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            g.Dispose();

            return imagemModifica;
        }

        public static String[] SplitString(String var)
        {
            String[] listString = new String[] { };

            if (var != null)
                listString = var.Replace("tamanho",string.Empty).TrimStart('-').Split('-');
            else listString = null;


            return listString;
        }


        private static string GetUrlAmigavelAtual() {

            return String.Join("/", HttpContext.Current.Request.RequestContext.RouteData.Values.Where(s => s.Key != "proSku_cores" && s.Key != "proSku_tamanhos" && s.Value != null && s.Value != string.Empty).Select(s => s.Value));
        }


        public static String FiltroTamanho(string proSku_tamanhos, string proSku_cores, string proSkuTam_nome)
        {
            string link = null;

            if (!string.IsNullOrEmpty(proSku_cores))
            {
                link = "/cor-" + proSku_cores.ToLower();
            }

            if (!string.IsNullOrEmpty(proSku_tamanhos))
            {
                var listTamanhos = proSku_tamanhos.ToLower().Split('-').Except(proSkuTam_nome.ToLower().Split('-')).ToList();
                listTamanhos.Add(proSkuTam_nome.ToLower());
                link += "/tamanho-" + string.Join("-", listTamanhos.OrderBy(s => s));
            }
            else
            {
                link += "/tamanho-" + proSkuTam_nome.ToLower();
            }

            link = GetUrlAmigavelAtual() + link;

            return link;
        }

        public static String FiltroCor(string proSku_cores, string proSku_tamanhos, string proSkuCor_nome)
        {          
            string link = null;
         
            if (!string.IsNullOrEmpty(proSku_cores))
            {
                var listCores = proSku_cores.ToLower().Split('-').Except(proSkuCor_nome.ToLower().Split('-')).ToList();
                listCores.Add(proSkuCor_nome.ToLower());
                link = "/cor-" + string.Join("-", listCores.OrderBy(s => s));
            }
            else {
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
            proSkuTam_nome = proSkuTam_nome.ToLower();
            if (!string.IsNullOrEmpty(proSku_tamanhos) && !string.IsNullOrEmpty(proSkuTam_nome))
            {
                if (proSku_tamanhos.Split('-').Where(s => s.ToString() == proSkuTam_nome).Count() > 0)
                    return true;
            }
            return false;
        }
        
        public static bool FiltroCorConsultada(string proSku_cores, string proSkuCor_nome)
        {
            string link = string.Empty;
            proSkuCor_nome = proSkuCor_nome.ToLower();
            if (!string.IsNullOrEmpty(proSku_cores) && !string.IsNullOrEmpty(proSkuCor_nome))
            {
                if (proSku_cores.Split('-').Where(s => s.ToString() == proSkuCor_nome).Count() > 0)
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




    }
}