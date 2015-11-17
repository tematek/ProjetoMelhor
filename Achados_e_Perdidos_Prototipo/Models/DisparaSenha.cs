using Achados_e_Perdidos_Prototipo.Models; //EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using System.Text;


namespace Achados_e_Perdidos_Prototipo.Models
{
    public class DisparaSenha
    {
        public string RecuperarSenha(String email, RecSenha ModelRecSenha)
        {

			String Qry;

			EmailService e_mail = new EmailService();
			SQLServer SQL = new SQLServer();
			SQL.ConectaDB();

			Qry = "select senhaUsuario from tblUsuario where emailUsuario = '" + email + "'";

			SqlDataReader Rs = SQL.DR(Qry);

			//encontrou o e-mail
			if (Rs.Read() == true)
			{

				ModelRecSenha.Senha = Rs["senhaUsuario"].ToString();




				bool retorno = SendSenha("contato.benfeitores@gmail.com", email, "Sua senha", "AchadosPerdidos\n\nSua senha é: " + ModelRecSenha.Senha);

				// // Gmail Remetente
				//var fromAddress = "contato.benfeitores@gmail.com";

				//// Email destino
				//var toAddress = "contato.benfeitores@gmail.com";

				////Senha do e-mail remetente
				//const string fromPassword = "Projeto032015";

				//// Assunto
				//string subject = Assunto;

				//// Corpo do e-mail
				//string body = "De: " + Nome + "\n";
				//body += "Email: " + Email + "\n";
				//body += "Assunto: " + Assunto + "\n";
				//body += "Mensagem: \n" + Msg + "\n";

				//// smtp settings
				//var smtp = new System.Net.Mail.SmtpClient();
				//{
				//	smtp.Host = "smtp.gmail.com";
				//	smtp.Port = 587;
				//	smtp.EnableSsl = true;
				//	smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				//	smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
				//	smtp.Timeout = 20000;
				//}

				//// Realiza os envios.
				//smtp.Send(fromAddress, toAddress, subject, body);


				//retorno = e_mail.Send()

				if (retorno == false){

					ModelRecSenha.Result = "Não foi possível enviar a senha para o e-mail informado, tente novamente.";

				}
				else{

					ModelRecSenha.Result = "Sua senha foi enviada para o e-mail informado.";
				}

			}

  

            return "Ok";
        }

        public static bool SendSenha(string from, string to, string subject, string mensagem)
        {
            System.Net.Mail.SmtpClient s = null;
            try
            {
                s = new System.Net.Mail.SmtpClient("smtp.live.com", 587);
                s.EnableSsl = true;
                s.UseDefaultCredentials = false;
                s.Credentials = new System.Net.NetworkCredential("contato.benfeitores@gmail.com", "Projeto032015");
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(
                    new System.Net.Mail.MailAddress(from),
                    new System.Net.Mail.MailAddress(to));
                message.Body = mensagem;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                s.Send(message);
                s.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (s != null)
                    s.Dispose();
            }
        }
    }
}
