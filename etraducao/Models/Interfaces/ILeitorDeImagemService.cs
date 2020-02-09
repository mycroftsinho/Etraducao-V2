using System.IO;

namespace etraducao.Models.Interfaces
{
    public interface ILeitorDeImagemService
    {
        int Executar(MemoryStream arquivo);
    }
}
