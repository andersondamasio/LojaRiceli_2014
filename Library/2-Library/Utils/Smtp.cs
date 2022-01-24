using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Net.Mime;

namespace _2_Library.Utils
{
    public class Smtp
    {
        public Smtp()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //================================================================================
        //================================================================================
        //================================================================================
        public Boolean Enviar(
          String DeEmail,
          String DeNome,
          String ParEmail,
          String ParNome,
          String ComCopia,
          String ComCopOculta,
          String Assunto,
          String Anexo,
          String CorTexto,
          String CorHtml
        )
        {
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(DeEmail, DeNome, Encoding.GetEncoding("iso-8859-1"));
            mensagem.To.Add(new MailAddress(ParEmail, ParNome, Encoding.GetEncoding("iso-8859-1")));
            if (ComCopia != "")
                mensagem.CC.Add(ComCopia);
            if (ComCopOculta != "")
                mensagem.Bcc.Add(ComCopOculta);
            mensagem.Subject = Assunto;
            if (Anexo != "")
                mensagem.Attachments.Add(new Attachment(Anexo));

            AlternateView texto = AlternateView.CreateAlternateViewFromString(
              CorTexto,
              Encoding.GetEncoding("iso-8859-1"),
              "text/plain"
            );
            mensagem.AlternateViews.Add(texto);
            AlternateView html = AlternateView.CreateAlternateViewFromString(
              CorHtml,
              Encoding.GetEncoding("iso-8859-1"),
              "text/html"
            );
            mensagem.AlternateViews.Add(html);

            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(mensagem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string EnviarMail(string mailCliente, string emailLoja, string emailLojaDisplay, string[] emailCopiasOculta, string[] emailCopias, string emailAssunto, string emailCorpo)
        {
            System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();

            try
            {
                if (String.IsNullOrEmpty(emailLojaDisplay))
                    objEmail.From = new System.Net.Mail.MailAddress(emailLoja);
                else objEmail.From = new System.Net.Mail.MailAddress(emailLoja, emailLojaDisplay);


                objEmail.To.Add(mailCliente);

                if(emailCopiasOculta != null)
                if (emailCopiasOculta.Count() > 0)
                {
                    foreach (string copiaOculta in emailCopiasOculta)
                        objEmail.Bcc.Add(copiaOculta);
                }

                if (emailCopias != null)
                if (emailCopias.Count() > 0)
                {
                    foreach (string copiaNaoOculta in emailCopias)
                        objEmail.CC.Add(copiaNaoOculta);
                }

                objEmail.IsBodyHtml = true;
                objEmail.Priority = System.Net.Mail.MailPriority.Normal;

                objEmail.Subject = emailAssunto;
                objEmail.Body = emailCorpo;

               //string anexo =  Recursos.CriarPdf(emailCorpo);
               //objEmail.Attachments.Add(new Attachment(anexo, MediaTypeNames.Application.Pdf));

   
                //Attachment att = new Attachment(stream, new ContentType("image/jpeg"));
                //mail.Attachments.Add(att);

                objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

               /* System.Net.Mail.SmtpClient objSmtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential("anderson@plusnet.com.br", "5964.171"),
                    EnableSsl = true
                };*/
                //client.Send("bernice.zerafa11@gmail.com", "bernice.zerafa11@gmail.com", "Welcome to Writely", "Test content");


                System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

                objSmtp.Send(objEmail);
                objSmtp.Dispose();
                objEmail.Dispose();
            }
            catch (Exception ex)
            {
                //Log.Log.GerarLogErro(string.Empty, ex.Message, "EnviarMail");
                return ex.Message;
            }
            return string.Empty;
        }

        public bool Envia(string mailCliente, string emailLoja, string emailLojaDisplay, string[] emailCopiasOculta, string[] emailCopias, string emailAssunto, string emailCorpo)
        {
            try
            {
                EnviarMail(mailCliente, emailLoja, emailLojaDisplay, emailCopiasOculta, emailCopias, emailAssunto, emailCorpo);
            }
            catch (Exception ex)
            {
                //Log.Log.GerarLogErro(string.Empty, ex.Message, "Envia");
                return false;
            }
            return true;
        }

        /// <summary>
        /// /////////////////////
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="toName"></param>
        /// <returns></returns>

        public delegate bool SendEmailDelegate(string mailCliente, string emailLoja, string emailLojaDisplay, string[] emailCopiasOculta, string[] emailCopias, string emailAssunto, string emailCorpo);

        public void GetResultsOnCallback(IAsyncResult ar)
        {
            SendEmailDelegate del = (SendEmailDelegate)((AsyncResult)ar).AsyncDelegate;
            try
            {
                bool result = del.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                bool result = false;
            }
        }

        public bool SendEmailAsync(string mailCliente, string emailLoja, string emailLojaDisplay, string[] emailCopiasOculta, string[] emailCopias, string emailAssunto, string emailCorpo)
        {
            try
            {
                SendEmailDelegate dc = new SendEmailDelegate(this.Envia);
                AsyncCallback cb = new AsyncCallback(this.GetResultsOnCallback);
                IAsyncResult ar = dc.BeginInvoke(mailCliente, emailLoja, emailLojaDisplay, emailCopiasOculta, emailCopias, emailAssunto, emailCorpo, cb, null);
            }
            catch (Exception ex)
            {
               // Log.Log.GerarLogErro(string.Empty, ex.Message, "SendEmailAsync");
                return false;
            }
            return true;
        }
    }
}