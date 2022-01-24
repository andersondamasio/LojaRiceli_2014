using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Utils
{
    public class ValidacaoXXX
    {
/*
        // Um número de CPF válido deve ter 11 dígitos numéricos.
        public static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");
            if (valor.Length != 11)

                return false;
            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;



            if (igual || valor == "12345678909")

                return false;



            int[] numeros = new int[11];



            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());



            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];



            int resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {

                if (numeros[9] != 0)

                    return false;

            }

            else if (numeros[9] != 11 - resultado)

                return false;



            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];



            resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {

                if (numeros[10] != 0)

                    return false;

            }

            else

                if (numeros[10] != 11 - resultado)

                    return false;



            return true;

        }

        // Um número de CNPJ válido deve ter 14 dígitos numéricos.
        public static bool ValidaCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");

            CNPJ = CNPJ.Replace("/", "");

            CNPJ = CNPJ.Replace("-", "");



            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;



            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;



            try
            {

                for (nrDig = 0; nrDig < 14; nrDig++)
                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));

                }



                for (nrDig = 0; nrDig < 2; nrDig++)
                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }

            catch
            {

                return false;

            }
        }

        public static string Coalesce(string str, string valor)
        {
            if (string.IsNullOrEmpty(str))
                return valor;
            var trim = str.Trim();
            if (trim.Length == 0)
                return valor;
            return trim;
        }

        public static bool ValidaInteiro(string inteiro)
        {
            try
            {
                int i = int.Parse(inteiro);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Truncate(string source, int length)
        {
            if (source != null)
                if (source.Length > length)
                {
                    source = source.Substring(0, length);
                }
            return source;
        }

        public static string ValidaTruncate(string source, int length, string field)
        {
            if (source != null)
                if (source.Length > length)
                {
                    return "Para a busca por " + field + " insira no máximo " + length + " caracteres";
                }
            return source;
        }


        public static Boolean ValidaData(String data)
        {
            DateTime resultado = DateTime.MinValue;
            if (DateTime.TryParse(data, out resultado))
                return true;
            else
                return false;
        }

        public static Boolean ValidaHora(string hora)
        {
            DateTime resultado;
             if (DateTime.TryParseExact(hora, "HH:mm:ss",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None, out resultado))
                 return true;
            else
                return false;
        }


        public static Boolean ValidaNumero(System.Object Expression)
        {

            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }


        public static Boolean ValidaArquivoImagens(string fileName)
        {
            if (
                !fileName.EndsWith(".jpg") &&
                !fileName.EndsWith(".gif") &&
                !fileName.EndsWith(".swf") &&
                !fileName.EndsWith(".png")
                )
            {
                return false;
            }
            else return true;
        }

        public static int CalculateIdade(DateTime? dataNascimento)
        {
            if (dataNascimento != null)
            {
                int anos = DateTime.Now.Year - dataNascimento.Value.Year;
                if (DateTime.Now.Month < dataNascimento.Value.Month || (DateTime.Now.Month == dataNascimento.Value.Month && DateTime.Now.Day < dataNascimento.Value.Day))
                    anos--;
                return anos;
            }
            else return 0;
        }

        public static string RemoverAcentos(string texto)
        {
            String textoNormalizado = texto.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder textoSemAcento = new System.Text.StringBuilder();
            for (int i = 0; i < textoNormalizado.Length; i++)
            {
                Char c = textoNormalizado[i];
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    textoSemAcento.Append(c);
            }
            return textoSemAcento.ToString();
        }

        public static void Alert(System.Web.UI.Page page, string mensagem)
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(
                                                                  page,
                                                                  page.GetType(),
                                                                  Guid.NewGuid().ToString(),
                                                                  "window.alert(\"" + mensagem.Replace("\"","'") + "\");",
                                                                  true
                                        );

        }
        public static void Redirecionar(System.Web.UI.Page page, string url)
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(
                                                                  page,
                                                                  page.GetType(),
                                                                  Guid.NewGuid().ToString(),
                                                                  "document.location = '" + url + "'",
                                                                  true
                                        );

        }*/
    }
}
