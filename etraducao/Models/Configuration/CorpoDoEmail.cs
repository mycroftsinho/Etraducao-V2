using etraducao.Models.Modelo;
using System.Text;

namespace etraducao.Models.Configuration
{
    public class CorpoDoEmail
    {
        public CorpoDoEmail(string mensagemDoEmail, string nome, string telefone, int quantidadeDeCaracteres, string origem, string destino)
        {
            this.mensagemDoEmail = mensagemDoEmail;
            this.nome = nome;
            this.telefone = telefone;
            this.quantidadeDeCaracteres = quantidadeDeCaracteres;
            this.origem = origem;
            this.destino = destino;
        }

        public string mensagemDoEmail { get; private set; }
        public string nome { get; private set; }
        public string telefone { get; private set; }
        public int quantidadeDeCaracteres { get; private set; }
        public string origem { get; private set; }
        public string destino { get; private set; }

        public string MontandoCorpoDoEmail()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<!DOCTYPE html> <html>");
            html.Append("<head>	<meta charset = \"utf-8\" /> </head>");
            html.Append("<body style=\"font-family: 'Roboto Slab', sans-serif;\">");
            html.Append("<div style=\"background-color: #e8e8e8; color: #34495e; padding: 20px 20px; text-align: center\">");
            html.Append("<div style=\"max-width: 550px; background-color:#FFFFFF; color:#34495e;font-size: 20px;margin: 0 auto;padding: 50px 50px;text-align: center;border-radius:20px;\">");
            html.Append("<h1 style=\"text-align: center\">");
            html.Append("<img src=\"https://www.etraducaojuramentada.com.br/web/image/res.company/1/logo?unique=a4487ac\" style=\"width:100px; padding-top:-10px; padding-bottom:20px;\" />");
            html.Append("</h1>");
            html.Append("<hr> <p style =\"margin-top: 50px;\">");
            html.Append("<h1> Olá!</h1>");
            html.Append("<p style=\" padding-top:5px; text-align:center;\">Um serviço de tradução foi solicitado.</p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b>Nome do cliente: </b>" + nome+" </p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b>Quantidade de caracteres: </b>" + quantidadeDeCaracteres+"</p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b> Quantidade de Documentos: </b>"+ quantidadeDeCaracteres+"</p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b> Quantidade de Laudas: </b> "+quantidadeDeCaracteres+"</p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b>Idioma de origem: </b>" + origem+"</p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b>Idioma para tradução: </b>" + destino+"<B/p>");
            html.Append("<p style=\" padding-top:5px; text-align:justify;\"><b>Observação: </b>" + mensagemDoEmail + "</p>");
            html.Append("<br/>");
            html.Append("</p>");
            html.Append("<p style=\"margin-top: 70px; \">");
            html.Append("<small> Para maiores informações: <br/> 0800 042 0159 | (+55) 47 99697-6742<br/> </small>");
            html.Append("<small>contato@etraducaojuramentada.com.br <br/> </small>");
            html.Append("<small> Caso não reconheça este e-mail apenas ignore-o.</small>");
            html.Append("</p>");
            html.Append("<hr>");
            html.Append("<h1 style=\"text-align: center\">");
            html.Append("</h1>");
            html.Append("<small style=\"text-align: center\"> E-Tradução Juramentada <br/>Brasil</small>");
            html.Append("</div>");
            html.Append("</div>");
            html.Append("</body>");
            html.Append("</html>");

            return html.ToString();
        }
    }
}
