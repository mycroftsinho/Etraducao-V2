using System.Threading.Tasks;
using etraducao.Models.Entidades;

namespace etraducao.Models.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<Cliente> BuscarCliente(string codigo, string email);

        Task<Cliente> BuscarClientePorEmail(string email);

        Task Adicionar(Cliente cliente);

        Task Atualizar(Cliente cliente);
    }
}