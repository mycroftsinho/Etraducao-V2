using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using etraducao.Models.Modelo;
using System.Collections.Generic;
using etraducao.Models.Interfaces;
using etraducao.Models.Configuration;
using System.Linq;

namespace etraducao.Models.Services
{
    public class EnvioDeEmail : IServicoDeEmail
    {

        public async Task EnviarEmail(Finalizar modelo, ModeloDeFormularioParaSerSalvo formulario, List<FileStream> anexos)
        {
            try
            {
                var msg = new MailMessage
                {
                    From = new MailAddress("contato@etraducaojuramentada.com", "E-Tradução Juramentada")
                };
                msg.To.Add(new MailAddress("lliandry@hotmail.com", "Raaby Liandry"));
                msg.To.Add(new MailAddress(modelo.Email, modelo.Nome));
                msg.Subject = "Solicitação de Tradução";
                msg.BodyEncoding = Encoding.UTF8;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = new CorpoDoEmail(modelo.Observacao, modelo.Nome, modelo.Telefone, formulario.QuantidadeDeCaracteres, formulario.Origem, formulario.Destino).MontandoCorpoDoEmail();

                int cont = 0;
                foreach (var anexo in anexos)
                {
                    ContentType type = new ContentType(formulario.ContentType);
                    Attachment data = new Attachment(anexo, formulario.CaminhoDoArquivo.ElementAt(cont).FileName, type.MediaType);
                    msg.Attachments.Add(data);
                    cont++;
                }

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("apoiodiegosilva@gmail.com", "f99showmam"),
                    EnableSsl = true,
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                smtpClient.Send(msg);
                await Task.CompletedTask;

            }
            catch (System.Exception)
            {
                await Task.CompletedTask;
            }
        }
    }
}
