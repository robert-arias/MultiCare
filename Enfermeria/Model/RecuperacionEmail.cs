using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Enfermeria.Model {
    public class RecuperacionEmail {

        public SmtpClient MailClient;

        public RecuperacionEmail() {
            string key = File.ReadAllText(@"./scripts/key.dat");
            string decryptedPasskey = Seguridad.Decrypt(ConfigurationManager.AppSettings["passkey"], key);
            MailClient = new SmtpClient("smtp.gmail.com", 587) {
                EnableSsl = true,
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["source"],
                decryptedPasskey)
            };
        }

        /*
         * Obtenido de:
         * https://stackoverflow.com/questions/2031824/what-is-the-best-way-to-check-for-internet-connectivity-using-net
         */
        public bool VerificarConexionInternet() {
            try {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch {
                return false;
            }
        }

        /*
         * Obtenido de:
         * https://stackoverflow.com/questions/17962784/using-mailmessage-to-send-emails-in-c-sharp
         */
        public void EnviarCodigo(string usuario, string nombreCompleto, string correo, string codigo) {
            string key = File.ReadAllText(@"./scripts/key.dat");
            string decryptedPasskey = Seguridad.Decrypt(ConfigurationManager.AppSettings["passkey"], key);

            MailMessage emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(ConfigurationManager.AppSettings["source"], "Sistema Multicare");
            emailMessage.To.Add(new MailAddress(correo));
            emailMessage.Subject = "Código de recuperación";
            string body = File.ReadAllText(@"./scripts/recovery-mail.dat").Replace("#NombreCompleto#", nombreCompleto);
            body = body.Replace("#NombreUsuario#", $"@{usuario}");
            body = body.Replace("#CorreoElectronico#", $"{correo}");
            emailMessage.Body = body.Replace("#Codigo#", codigo);
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;

            MailClient.SendMailAsync(emailMessage);
        }

    }
}
