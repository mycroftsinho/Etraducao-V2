using System.Threading.Tasks;
using etraducao.Models.Entidades;

namespace etraducao.Models.Interfaces
{
    public interface IControleDeValorRepositorio
    {
        Task<ControleDeValores> BuscarControleDeValor();
    }
}
