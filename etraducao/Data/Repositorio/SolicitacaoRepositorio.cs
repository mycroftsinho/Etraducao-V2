using System.Threading.Tasks;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace etraducao.Data.Repositorio
{
    public class SolicitacaoRepositorio : ISolicitacaoRepositorio
    {
        private readonly EtraducaoContexto contexto;

        public SolicitacaoRepositorio(EtraducaoContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task Adicionar(Solicitacao solicitacao)
        {
            await contexto.AddAsync(solicitacao);
            await contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Solicitacao solicitacao)
        {
            contexto.Update(solicitacao);
            await contexto.SaveChangesAsync();
        }

        public async Task<Solicitacao> BuscarSolicitacao(int id)
        {
            return await contexto.Solicitacao
                .Include(x => x.Documentos)
                .Include(x => x.Cliente)
                .Include(x => x.Pagamento)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
