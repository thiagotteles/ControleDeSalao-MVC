using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;


namespace ControleDeSalao.Infra.Cross.Security
{
    public class Email
    {
        public static void EnviarEmail(string para, string assunto, string mensagem)
        {
            //Define os dados do e-mail
            const string nomeRemetente = "Contato";
            const string emailRemetente = "contato@controledesalao.com.br";
            const string senha = "getT@gw0rk$";

            //Host da porta SMTP
            const string SMTP = "mail.controledesalao.com.br";

            string emailDestinatario = para;
            string assuntoMensagem = assunto;
            string conteudoMensagem = mensagem;

            var objEmail = new MailMessage();

            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");
            objEmail.To.Add(emailDestinatario);
            objEmail.Priority = MailPriority.High;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = assuntoMensagem;
            objEmail.Body = conteudoMensagem;

            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            var objSmtp = new SmtpClient();

            objSmtp.Credentials = new System.Net.NetworkCredential(emailRemetente, senha);
            objSmtp.Host = SMTP;
            objSmtp.Port = 25;
            
            try
            {
                objSmtp.Send(objEmail);
            }
            finally
            {
                objEmail.Dispose();
            }

        }
    }
}
