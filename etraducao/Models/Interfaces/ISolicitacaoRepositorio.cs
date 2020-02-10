using System.Threading.Tasks;
using etraducao.Models.Entidades;
using System.Collections.Generic;

namespace etraducao.Models.Interfaces
{
    public interface ISolicitacaoRepositorio
    {
        Task Adicionar(Solicitacao solicitacao);

        Task Atualizar(Solicitacao solicitacao);
        
        Task<Solicitacao> BuscarSolicitacao(int id);

        Task<int> QuantidadeDeItensDaBusca(int page, string search, string status);

        Task<IEnumerable<Solicitacao>> ListarSolicitacoesPaginadas(int page, string search, string status);
    }
}
