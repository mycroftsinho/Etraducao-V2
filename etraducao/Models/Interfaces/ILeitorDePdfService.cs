using System.IO;

namespace etraducao.Models.Interfaces
{
    public interface ILeitorDePdfService
    {
        int Executar(string nomeDoArquivo, MemoryStream arquivo);

        void RemoverArquivos(string bucketName);
    }
}
