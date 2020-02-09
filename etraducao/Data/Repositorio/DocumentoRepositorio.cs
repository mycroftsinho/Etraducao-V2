using System;
using System.Threading.Tasks;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;

namespace etraducao.Data.Repositorio
{
    public class DocumentoRepositorio : IDocumentoRepositorio
    {
        private readonly EtraducaoContexto contexto;

        public DocumentoRepositorio(EtraducaoContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task Adicionar(Documento documento)
        {
            await contexto.Documento.AddAsync(documento);
            await contexto.SaveChangesAsync();
        }

        public async Task Remover(Documento documento)
        {
            contexto.Documento.Remove(documento);
            await contexto.SaveChangesAsync();
        }

        public async Task<Documento> BuscarDocumento(Guid id)
        {
            return await contexto.Documento.FindAsync(id);
        }
    }
}
