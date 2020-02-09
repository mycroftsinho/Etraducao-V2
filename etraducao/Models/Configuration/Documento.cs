using System.Text.RegularExpressions;

namespace etraducao.Models.Configuration
{
    public class Documento
    {

        public static bool ValidarImagem(string contentType)
        {
            return Regex.IsMatch(contentType, "gif|jpg|jpeg|tiff|png");
        }

        public static bool ValidarPdf(string contentType)
        {
            return contentType.Equals("application/pdf");
        }
    }
}
