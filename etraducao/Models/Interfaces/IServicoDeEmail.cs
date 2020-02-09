using System.IO;
using System.Threading.Tasks;
using etraducao.Models.Modelo;
using System.Collections.Generic;

namespace etraducao.Models.Interfaces
{
    public interface IServicoDeEmail
    {
        Task EnviarEmail(Finalizar modelo, ModeloDeFormularioParaSerSalvo formulario, List<FileStream> anexos);
    }
}
