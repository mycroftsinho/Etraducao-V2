using System.IO;
using Google.Cloud.Vision.V1;
using etraducao.Models.Interfaces;

namespace etraducao.Models.Services
{
    public class LeitorDeImagens : ILeitorDeImagemService
    {
        public int Executar(MemoryStream arquivo)
        {
            var documento = arquivo.ToArray();
            //Image imagem = await Image.FromFileAsync(arquivo);
            Image imagem = Image.FromBytes(documento);

            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            TextAnnotation text = client.DetectDocumentText(imagem);
            var conteudo = text.Text.Replace("\n", " ");
            var remocaoDosEspacos = conteudo.Split(' ');

            int total = 0;
            foreach (var item in remocaoDosEspacos)
                total += item.Length;

            return total;
        }
    }
}
