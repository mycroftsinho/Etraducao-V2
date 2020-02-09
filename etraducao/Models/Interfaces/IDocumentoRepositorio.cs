using System;
using System.Threading.Tasks;
using etraducao.Models.Entidades;

namespace etraducao.Models.Interfaces
{
    public interface IDocumentoRepositorio
    {
        Task Remover(Documento documento);

        Task Adicionar(Documento documento);
        
        Task<Documento> BuscarDocumento(Guid id);
    }
}
