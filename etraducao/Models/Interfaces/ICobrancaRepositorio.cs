using System.Threading.Tasks;
using etraducao.Models.Entidades;

namespace etraducao.Models.Interfaces
{
    public interface ICobrancaRepositorio
    {
        Task CriarCliente(Cliente cliente);

        Task CriarCobranca(Solicitacao solicitacao, string formaDePagmento);
    }
}
