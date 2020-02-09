using System.Threading.Tasks;
using etraducao.Models.Entidades;

namespace etraducao.Models.Interfaces
{
    public interface ISolicitacaoRepositorio
    {
        Task Adicionar(Solicitacao solicitacao);

        Task Atualizar(Solicitacao solicitacao);
        
        Task<Solicitacao> BuscarSolicitacao(int id);
    }
}
